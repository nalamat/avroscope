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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;

namespace avroscope
{
	public class Plotter : PictureBox
	{
		private Bitmap _imgBack;
		private Bitmap _imgGrid;
		private Bitmap _imgData;
		private int _gridSizeX;
		private int _gridSizeY;
		private double _divX;
		private double _divY;
		private double _sampleTime;
		private TriggerEdgeType _triggerEdge;
		private LinkedList<double> _data;
		private bool _plot;

		private Thread _threadData;
		private Mutex _mutexData;
		private ManualResetEvent _resetData;

		public Plotter()
		{
			_imgBack = null;
			_imgGrid = null;
			_imgData = null;
			_gridSizeX = 20;
			_gridSizeY = 10;
			_divX = 1e-2;
			_divY = .5;
			_sampleTime = 1e-3;
			_triggerEdge = TriggerEdgeType.None;
			_data = new LinkedList<double>();
			_plot = false;

			_threadData = new Thread(new ThreadStart(threadDataMain));
			_mutexData = new Mutex();
			_resetData = new ManualResetEvent(_plot);
			_threadData.Start();

			this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.Selectable, false);
			this.Refresh();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
				_resetData.Set();
			base.Dispose(disposing);
		}

		void threadDataMain()
		{
			while (true)
			{
				_resetData.WaitOne();
				if (this.Disposing || this.IsDisposed) return;
				this.RefreshData();
				Thread.Sleep(50);
			}
		}

		public override void Refresh()
		{
			_mutexData.WaitOne();
			if (_plot) this.DrawData();
			this.DrawGrid();
			this.DrawBack();
			PaintEventArgs e = new PaintEventArgs(this.CreateGraphics(), ClientRectangle);
			this.OnPaint(e);
			_mutexData.ReleaseMutex();
		}

		public void RefreshData()
		{
			_mutexData.WaitOne();
			this.DrawData();
			this.DrawBack();
			if (!this.Disposing && !this.IsDisposed)
			{
				PaintEventArgs e = new PaintEventArgs(this.CreateGraphics(), ClientRectangle);
				this.OnPaint(e);
			}
			_mutexData.ReleaseMutex();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			_mutexData.WaitOne();
			if (_imgBack != null) e.Graphics.DrawImage(_imgBack, 0, 0);
			_mutexData.ReleaseMutex();
		}

		protected override void OnResize(EventArgs e)
		{
			this.Refresh();
		}

		protected override void OnBackColorChanged(EventArgs e)
		{
			this.Refresh();
		}

		private LinkedListNode<double> DetectEdge(LinkedListNode<double> firstNode, double mean)
		{
			LinkedListNode<double> node = firstNode;
			LinkedListNode<double> startNode = null;
			if (node == null) return null;
			int start = firstNode.List.Count - (int)(_divX * _gridSizeX / _sampleTime) - 1;
			int i = 0;

			while (node.Next != null)
			{
				if (startNode == null && i++ >= start)
					startNode = node;
				if (_triggerEdge == TriggerEdgeType.None && startNode != null)
					return startNode;
				else if (_triggerEdge == TriggerEdgeType.Positive && node.Value - mean < -_divY / 5 && _divY / 5 < node.Next.Value - mean)
					return node;
				else if (_triggerEdge == TriggerEdgeType.Negative && node.Value - mean > _divY / 5 && -_divY / 5 < node.Value - mean)
					return node;
				else
					node = node.Next;
			}

			return startNode;
		}

		private void DrawData()
		{
			if (_imgData != null)
			{
				_imgData.Dispose();
				_imgData = null;
			}
			if (Width == 0 || Height == 0) return;
			_imgData = new Bitmap(Width, Height);
			Graphics g = Graphics.FromImage(_imgData);
			g.SmoothingMode = SmoothingMode.AntiAlias;

			Pen penData = new Pen(Brushes.Blue, 1);

			while (_data.Count > 2 * _gridSizeX * _divX / _sampleTime)
				_data.RemoveFirst();

			double mean = 2.5;
			LinkedListNode<double> node = DetectEdge(_data.First, mean);
			if (node == null) return;
			int i = 0;

			while (node.Next != null && i < _divX * _gridSizeX / _sampleTime)
			{
				g.DrawLine(penData, (int)(i * Width * _sampleTime / _gridSizeX / _divX), (int)(Height / 2 - (node.Value - mean) * Height / _gridSizeY / _divY), (int)((i + 1) * Width * _sampleTime / _gridSizeX / _divX), (int)(Height / 2 - (node.Next.Value - mean) * Height / _gridSizeY / _divY));
				node = node.Next;
				++i;
			}

			g.Dispose();
			g = null;
		}

		private void DrawGrid()
		{
			if (_imgGrid != null)
			{
				_imgGrid.Dispose();
				_imgGrid = null;
			}

			if (Width == 0 || Height == 0) return;
			_imgGrid = new Bitmap(Width, Height);
			Graphics g = Graphics.FromImage(_imgGrid);
			g.SmoothingMode = SmoothingMode.AntiAlias;

			Pen penGrid1 = new Pen(Brushes.Gray, 2);
			penGrid1.DashStyle = DashStyle.Dot;
			for (int i = 0; i < _gridSizeX; ++i)
				g.DrawLine(penGrid1, Width * i / _gridSizeX, 0, Width * i / _gridSizeX, Height);
			for (int i = 0; i < _gridSizeY; ++i)
				g.DrawLine(penGrid1, 0, Height * i / _gridSizeY, Width, Height * i / _gridSizeY);

			Pen penGrid2 = new Pen(Brushes.LightGray, 1);
			penGrid2.DashStyle = DashStyle.Dot;
			for (int i = 0; i < _gridSizeX * 5; ++i)
				g.DrawLine(penGrid2, Width * i / _gridSizeX / 5, 0, Width * i / _gridSizeX / 5, Height);
			for (int i = 0; i < _gridSizeY * 5; ++i)
				g.DrawLine(penGrid2, 0, Height * i / _gridSizeY / 5, Width, Height * i / _gridSizeY / 5);

			Pen penAxis = new Pen(Brushes.Black, 2);
			g.DrawLine(penAxis, Width / 2, 0, Width / 2, Height);
			g.DrawLine(penAxis, 0, Height / 2, Width, Height / 2);

			Pen penBorder = new Pen(Brushes.Black, 3);
			g.DrawRectangle(penBorder, 1, 1, Width - 3, Height - 3);

			g.Dispose();
			g = null;
		}

		private void DrawBack()
		{
			if (_imgBack != null)
			{
				_imgBack.Dispose();
				_imgBack = null;
			}
			if (Width == 0 || Height == 0) return;
			_imgBack = new Bitmap(Width, Height);
			Graphics g = Graphics.FromImage(_imgBack);

			g.Clear(BackColor);
			if (_imgGrid != null) g.DrawImage(_imgGrid, 0, 0);
			if (_imgData != null && _plot) g.DrawImage(_imgData, 0, 0);

			g.Dispose();
			g = null;
		}

		public void AddData(double d)
		{
			try
			{
				//_mutexData.WaitOne();
				_data.AddLast(d);
				//_mutexData.ReleaseMutex();
			}
			catch
			{
			}
		}

		public double DivX
		{
			get { return _divX; }
			set { _divX = value; }
		}

		public double DivY
		{
			get { return _divY; }
			set { _divY = value; }
		}

		public double SampleTime
		{
			get { return _sampleTime; }
			set { _sampleTime = value; }
		}

		public TriggerEdgeType TriggerEdge
		{
			get { return _triggerEdge; }
			set { _triggerEdge = value; }
		}

		public bool Plot
		{
			get
			{
				return _plot;
			}
			set
			{
				if (_plot = value)
					_resetData.Set();
				else
					_resetData.Reset();
				this.Refresh();
			}
		}
	}
}
