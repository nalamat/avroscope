/*************************************************************************
 *                                                                       *
 * This file is a part of the AVROscope project:                         *
 * A Low-Cost Low-Frequency USB Oscilloscope                             *
 * Copyright (C) 2013-2014 Nima Alamatsaz, All rights reserved           *
 * Email: nialamat@gmail.com                                             *
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
 * Common utility functions and defines
*/

#ifndef __AVROSCOPE_COMMON_H__
#define __AVROSCOPE_COMMON_H__

#include <stdlib.h>

#define delay_us _delay_us
#define delay_ms _delay_ms

#define char_to_lower(c) ( 'A'<=c&&c<='Z' ? (c-'A'+'a') : (c) )
#define char_to_upper(c) ( 'a'<=c&&c<='z' ? (c-'a'+'A') : (c) )

void *operator new(size_t objsize);
void operator delete(void* obj);

#endif
