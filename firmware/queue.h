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

/*
 * do sakhtemane dadeye queue va link list,
 * baraye parhiz az afzayeshe hajme khoruji
 * az STL estefade nashod
*/

/*
 * Queue and Linked List data structure definitions. Didn't use Standard
 * Template Library (STL) in order to keep the program size small.
*/

#ifndef SMS_CONTROLLER_QUEUE_H
#define SMS_CONTROLLER_QUEUE_H

/*
 * note: only for char*
 * link used by queue for dynamic storage
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
 * note: only for char*
 * all the memory management is taken care of
 * after calling the pop() function, never use
 * the data fetched by top() after calling pop()
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


/////////////////////////////////////////

// only for int
class link_int
{
public:
	int *data;
	link_int *sub;

	link_int();
	~link_int();
};

// only for int
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
