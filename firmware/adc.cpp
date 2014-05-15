/*************************************************************************
 *                                                                       *
 * This file is a part of the AVROscope project:                         *
 * A Low-Cost Low-Frequency USB Oscilloscope                             *
 * Copyright (C) 2013-2014, Nima Alamatsaz, All rights reserved          *
 * Email: nnalamat@gmail.com                                             *
 * Web:   http://github.com/nalamat/avroscope                            *
 *                                                                       *
 * AVROscope is free software: you can redistribute it and/or modify     *
 * it under the terms of the GNU General Public License as published by  *
 * the Free Software Foundation, either version 3 of the License, or     *
 * any later version.                                                    *
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

#include <avr/io.h>
#include "adc.h"

void adc_init()
{
	// Vref=AVCC, right align
	ADMUX = 0b01<<REFS0 | 0<<ADLAR | 0b00000<<MUX0;
	// F_CPU/128 = 125KHz, 
	ADCSRA = 0b111<<ADPS0;
}

void adc_turn_on(bool adie)
{
	ADCSRA |= (1<<ADEN) | (adie<<ADIE);
}

void adc_turn_off()
{
	ADCSRA &= ~( (1<<ADEN) | (1<<ADIE) );
}

int adc_read(char pin)
{
	if ( !(ADCSRA&(1<<ADEN)) ) return 0;

	ADMUX = (ADMUX&0b11100000) | (pin<<MUX0);
	ADCSRA |= 1<<ADSC;

	while ( ADCSRA & (1<<ADSC) );
	return ADC;
}

void adc_read_start(char pin)
{
	if ( !(ADCSRA & (1<<ADEN)) ) return;
	
	ADMUX = (ADMUX&0b11100000) | (pin<<MUX0);
	ADCSRA |= 1<<ADSC;
}

bool adc_read_ended()
{
	return !( ADCSRA & (1<<ADSC) );
}
