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
 * Universal Synchronous/Asynchronous Receiver/Transmitter peripheral (USART)
 * interface. Initialize the peripheral by calling 'usart_init' once. Data can
 * be sent using 'usart_send_str'. When notified by RXC interrupt, received
 * data are pushed into the 'usart_recv_buf' queue, read them in application's
 * main loop when required.
*/

#ifndef __AVROSCOPE_USART_H__
#define __AVROSCOPE_USART_H__

#include "queue.h"

extern queue_str usart_recv_buf;

void usart_init();
void usart_send_char(const char ch);
/*
 * Sends each character of the given string in order.
*/
void usart_send_str(const char *str);
/*
 * Waits until a character is received.
 * Note: This function is blocking.
*/
char usart_recv_char();
/*
 * Sets a callback for when data arrives data is stored in 'usart_recv_buf'
 * without leading and ending \r\n
 * Note: This function is non-blocking.
*/
void usart_recv_set(void (*call)());

#endif
