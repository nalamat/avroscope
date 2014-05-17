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

/*
 * Analog to digital peripheral (ADC) interface, including functions to
 * initialize, power on, power off and read a desired pin's voltage
*/

#ifndef __AVROSCOPE_ADC_H__
#define __AVROSCOPE_ADC_H__

// Initialize the ADC peripheral without turning it on
void adc_init();

// Power on the ADC peripheral, make sure 'adc_init' is called before
// 'adc_turn_on'. In order to enable asynchronous reading of ADC,
// set 'adie' to true
void adc_turn_on(bool adie=false);

// Power of the ADC peripheral
void adc_turn_off();

// Synchronously read the digitized analog voltage of a desired pin on port A.
// 'pin' can be 0 through 7, the returned value is in range of 0-1023 linearly
// mapped from 0.0v-4.995v
int adc_read(char pin);

void adc_read_start(char pin);

bool adc_read_ended();

#endif
