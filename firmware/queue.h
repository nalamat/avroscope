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
 * Queue and Link data structure definitions. Didn't use the Standard
 * Template Library (STL) in order to keep the program size small.
*/

#ifndef __AVROSCOPE_QUEUE_H__
#define __AVROSCOPE_QUEUE_H__

/*
 * Used by queue for dynamic storage. (Only for char*)
*/
class link_str
{
public:
	char *data;
	link_str *sub;

	link_str();
	~link_str();
};

/*
 * All memory management is taken care of after calling the 'pop' function,
 * never use data fetched by 'top' after calling 'pop'. (Only for char*)
*/
class queue_str
{
private:
	link_str *begin;
public:
	queue_str();
	~queue_str();
	char *top();
	void pop();
	void push(char *data);
	bool empty();
};


///////////////////////////////////////////////////////////////////

// Note: Only for int
class link_int
{
public:
	int *data;
	link_int *sub;

	link_int();
	~link_int();
};

// Note: Only for int
class queue_int
{
private:
	link_int *begin;
public:
	queue_int();
	~queue_int();
	int *top();
	void pop();
	void push(int *data);
	bool empty();
};

#endif
