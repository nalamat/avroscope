/*************************************************************************
 *                                                                       *
 * This file is a part of the AVROscope project:                         *
 * A Low-Cost Low-Frequency USB Oscilloscope                             *
 * Copyright (C) 2013-2014, Nima Alamatsaz, All rights reserved          *
 * Email: nnalamat@gmail.com                                             *
 * Web:   http://github.com/nalamat/avroscope                            *
 *                                                                       *
 * AVROscope is free software: you can redistribute the software         *
 * and/or modify it under the terms of the GNU General Public License    *
 * version 3 as published by the Free Software Foundation.               *
 *                                                                       *
 * AVROscope is distributed in the hope that it will be useful,          *
 * but WITHOUT ANY WARRANTY; without even the implied warranty of        *
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the          *
 * GNU General Public License for more details.                          *
 *                                                                       *
 * You should have received a copy of the GNU General Public License     *
 * along with AVROscope. If not, see <http://www.gnu.org/licenses/>.     *
 *                                                                       *
 *************************************************************************/

// avrdude -c usbasp -p atmega32 -U flash:w:avroscope.hex

#define SETBIT(data, bit) ( data |= (1<<bit) )
#define CLEARBIT(data, bit) ( data &= ~(1<<bit) )
#define CHECKBIT(data, bit) ( (bool)(data & (1<<bit)) )

#define USB_LED_OFF 0
#define USB_LED_ON  1
#define USB_READ_STRING 2
#define USB_READ_INTEGER 3
#define USB_READ_SWITCHES 4
#define USB_WRITE_PORTC 5
#define USB_READ_ADC 6
#define USB_READ_ADC_BUFFER 7

#define ADC_BUFFER_SIZE 127
#define ADC_BUFFER_COUNT 4

#include <math.h>
#include <avr/io.h>
#include <avr/interrupt.h>
#include <avr/wdt.h>
#include <util/delay.h>
#include "usbdrv/usbdrv.h"
#include "adc.h"
#include "queue.h"

uchar read_string[] = "Hoorrrraaay! :D";
int read_integer = 0;
uchar read_switches[4];
int read_adc = 0;
int adc_buffer[ADC_BUFFER_SIZE];
int adc_buffer_len = 1;
int adc_buffer_index = 0;

// Called when a custom control message is received
USB_PUBLIC uchar usbFunctionSetup(uchar data[8])
{
	// Cast data to correct type
    usbRequest_t *rq = (usbRequest_t *)(void *)data;
	
	// Custom command is in the bRequest field
	switch(rq->bRequest)
	{
		case USB_LED_ON:
			// Turn LED on
			PORTC |= 1;
			return 0;
		case USB_LED_OFF:
			// Turn LED off
			PORTC &= ~1;
			return 0;
		case USB_READ_STRING:
			usbMsgPtr = (int)read_string;
			return sizeof(read_string);
		case USB_READ_INTEGER:
			usbMsgPtr = (int)&read_integer;
			return sizeof(read_integer);
		case USB_READ_SWITCHES:
			read_switches[0] = CHECKBIT(PINA, 7);
			read_switches[1] = CHECKBIT(PINA, 6);
			read_switches[2] = CHECKBIT(PINA, 5);
			read_switches[3] = CHECKBIT(PINA, 4);
			usbMsgPtr = (int)read_switches;
			return sizeof(read_switches);
		case USB_WRITE_PORTC:
			PORTC = rq->wValue.bytes[0];
			return 0;
		case USB_READ_ADC:
			read_adc = adc_read(0);
			usbMsgPtr = (int)&read_adc;
			return sizeof(read_adc);
		case USB_READ_ADC_BUFFER:
			usbMsgPtr = (int)adc_buffer;
			int tmp = adc_buffer_len;
			adc_buffer_len = 1;
			return sizeof(int)*tmp;
	}

	// Should not get here
    return 0;
}

void timer_init()
{	
	// Timer1: 0.0625*256*31250 = 500,000us
	// OCR1A CTC mode, clk/256 prescaler
	TCNT1 = 0;
	OCR1A = 31250-1;
	TCCR1A = 0;
	TCCR1B = (1<<WGM12) | 0b100;
	
	// Timer0: 16MHz / 64 = 250KHz
	// Normal mode counter, no OC0, clk/64 prescaler
	TCCR0 =	0b00000011;
	
	// Timer0: 16MHz / 1024 = 15.625KHz
	// Normal mode, overflow ISR on 61Hz, no OC0, clk/1024 prescaler
	//TCCR2 = 0b00000111;

	TIMSK = 1<<OCIE1A | 1<<TOIE2;
}

inline uchar get_time_elapsed()
{
	uchar time = TCNT0;
	TCNT0 = 0;
	return time;
}

inline void adc_push()
{
	if ( adc_buffer_len >= ADC_BUFFER_SIZE )
	{
		SETBIT(PORTC, 1);
		adc_buffer_len = 1;
	}
	if ( adc_buffer[0]==0 )
		adc_buffer[0] = get_time_elapsed();
	else
		adc_buffer[0] = ((unsigned int)adc_buffer[0] + (unsigned int)get_time_elapsed()) >> 1;
	adc_buffer[adc_buffer_len++] = adc_read(0);
}

int main()
{
	DDRA  = 0b00000110;
	PORTA = 0b11110000;
	DDRC  = 0b11111111;
	PORTC = 0b00000000;
	DDRD  = 0b10000000;
	PORTD = 0b00000000;
	timer_init();
	adc_init();
	adc_turn_on();
	
	// Enable 1s watchdog timer
	wdt_enable(WDTO_1S);
	usbInit();
	
	// Enforce USB re-enumeration
	usbDeviceDisconnect();
	for (uchar i = 0; i<250; i++)    // Wait 500ms
	{
		// Keep the watchdog happy
		wdt_reset();
		_delay_ms(2);
	}
	usbDeviceConnect();
	
	// Enable interrupts after USB re-enumeration
	sei();
	PORTC = 0b00000001;
	
	while (1)
	{
		// Keep the watchdog happy
		wdt_reset();
		usbPoll();
		adc_push();
		
		PORTA = (PORTA & 0b11111001) | (((PINA>>7)&1)<<1) | (((PINA>>6)&1)<<2);
	}
        
	return 0;
}

ISR (TIMER1_COMPA_vect)
{
	read_integer++;
	PORTC ^= 0b10000000;
	PORTD ^= 0b10000000;
}

ISR (ADC_vect)
{
	if ( adc_buffer_len >= ADC_BUFFER_SIZE )
	{
		SETBIT(PORTC, 1);
		adc_buffer_len = 1;
	}	
	adc_buffer[0] = get_time_elapsed();
	adc_buffer[adc_buffer_len++] = ADC;
	adc_read_start(0);
}

/*
int main(void)
{
	DDRA = 0x00;
	PORTA = 0xFF;
	
	DDRC = 0xFF;
	PORTC = 0x00;
	
	timer_init();
	usart_init();
	sei();
	
    while (1)
    {
		PORTC ^= 0b01000000;
		counter++;
		if ( counter%2 == 0 )
			usart_send_str("test!\r");
		_delay_ms(500);
    }
}
*/
