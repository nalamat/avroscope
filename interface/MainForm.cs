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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LibUsbDotNet.LibUsb;
using LibUsbDotNet.Main;
using System.Diagnostics;

namespace avroscope
{
	public partial class MainForm : Form
	{
		private LibUsbDevice _usbDevice = null;
		Stopwatch s = new Stopwatch();
		byte[] buffer = new byte[254*2];

		public MainForm()
		{
			InitializeComponent();
			usbDisconnected();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			plotter.Plot = true;
			txtDivX.Text = plotter.DivX.ToString();
			txtDivY.Text = plotter.DivY.ToString();

			cmbTriggerEdge.Items.Clear();
			cmbTriggerEdge.Items.AddRange(Enum.GetNames(typeof(TriggerEdgeType)));
			cmbTriggerEdge.SelectedItem = TriggerEdgeType.None.ToString();
		}

		private void btnPlot_Click(object sender, EventArgs e)
		{
			timer1.Enabled = !timer1.Enabled;
		}

		private int ti = 0;

		private void timer1_Tick(object sender, EventArgs e)
		{
			//double t = (ti++)/1000.0;
			//plotter.AddData(2);

			if (_usbDevice != null)
			{
				s.Restart();
				UsbSetupPacket setup = new UsbSetupPacket((byte)(UsbCtrlFlags.RequestType_Vendor | UsbCtrlFlags.Recipient_Device | UsbCtrlFlags.Direction_In), (byte)OUSBRequest.ReadADCBuffer, 0, 0, 0);
				int length;
				if (!_usbDevice.ControlTransfer(ref setup, buffer, buffer.Length, out length))
					usbDisconnected();
				else if (length >= 2)
				{
					double time = (double)BitConverter.ToInt16(buffer, 0) / 250000.0;
					if (plotter.SampleTime == 0) 
						plotter.SampleTime = time;
					else
						plotter.SampleTime = (plotter.SampleTime*19 + time)/20;

					//int time = BitConverter.ToInt16(buffer, 0);
					//lblOut.Text = time.ToString();
					//plotter.AddData(time);

					for (int i = 2; i<length; i += 2)
					{
						double adc = BitConverter.ToInt16(buffer, i) * 5.0 / 1024.0;
						plotter.AddData(adc);
					}
				}

				s.Stop();
				double elapsed = s.ElapsedTicks * 1000.0 / Stopwatch.Frequency;
				lblOut.Text = plotter.SampleTime.ToString();
			}
		}

		private void btnTest_Click(object sender, EventArgs e)
		{
			// OUSBRequest.ReadString
			if (_usbDevice != null)
			{
				UsbSetupPacket setup = new UsbSetupPacket((byte)(UsbCtrlFlags.RequestType_Vendor | UsbCtrlFlags.Recipient_Device | UsbCtrlFlags.Direction_In), (byte)OUSBRequest.ReadString, 0, 0, 0);
				int length;
				byte[] buffer = new byte[100];
				if (!_usbDevice.ControlTransfer(ref setup, buffer, 100, out length))
					usbDisconnected();
				else
					lblOut.Text = Encoding.ASCII.GetString(buffer);
			}

			// OUSBRequest.ReadInteger
			//if (_usbDevice != null)
			//{
			//    UsbSetupPacket setup = new UsbSetupPacket((byte)(UsbCtrlFlags.RequestType_Vendor | UsbCtrlFlags.Recipient_Device | UsbCtrlFlags.Direction_In), (byte)OUSBRequest.ReadInteger, 0, 0, 0);
			//    int length;
			//    byte[] buffer = new byte[4];
			//    if (!_usbDevice.ControlTransfer(ref setup, buffer, 4, out length))
			//        usbDisconnected();
			//    else
			//        lblOut.Text = BitConverter.ToInt32(buffer, 0).ToString();
			//}

			// OUSBRequest.ReadSwitches
			//if (_usbDevice != null)
			//{
			//    UsbSetupPacket setup = new UsbSetupPacket((byte)(UsbCtrlFlags.RequestType_Vendor | UsbCtrlFlags.Recipient_Device | UsbCtrlFlags.Direction_In), (byte)OUSBRequest.ReadSwitches, 0, 0, 0);
			//    int length;
			//    byte[] buffer = new byte[4];
			//    if (!_usbDevice.ControlTransfer(ref setup, buffer, 4, out length))
			//        usbDisconnected();
			//    else
			//    {
			//        lblOut.Text = "";
			//        for (int i = 0; i < 4; ++i)
			//            lblOut.Text += buffer[i].ToString();
			//    }
			//}

			// OUSBRequest.WritePortC
			//if (_usbDevice != null)
			//{
			//    UsbSetupPacket setup = new UsbSetupPacket((byte)(UsbCtrlFlags.RequestType_Vendor | UsbCtrlFlags.Recipient_Device | UsbCtrlFlags.Direction_In), (byte)OUSBRequest.WritePortC, 0x00FF, 0, 0);
			//    int length;
			//    byte[] buffer = new byte[4];
			//    if (!_usbDevice.ControlTransfer(ref setup, buffer, 4, out length))
			//        usbDisconnected();
			//}

			// OUSBRequest.ReadADC
			//if (_usbDevice != null)
			//{
			//    UsbSetupPacket setup = new UsbSetupPacket((byte)(UsbCtrlFlags.RequestType_Vendor | UsbCtrlFlags.Recipient_Device | UsbCtrlFlags.Direction_In), (byte)OUSBRequest.ReadADC, 0, 0, 0);
			//    int length;
			//    byte[] buffer = new byte[2];
			//    if (!_usbDevice.ControlTransfer(ref setup, buffer, 2, out length))
			//        usbDisconnected();
			//    else
			//        lblOut.Text = BitConverter.ToInt16(buffer, 0).ToString();
			//}
		}

		private void chkLED_CheckedChanged(object sender, EventArgs e)
		{
			if (_usbDevice != null)
			{
				OUSBRequest request = (chkLED.Checked ? OUSBRequest.LEDOn : OUSBRequest.LEDOff);
				UsbSetupPacket setup = new UsbSetupPacket((byte)(UsbCtrlFlags.RequestType_Vendor | UsbCtrlFlags.Recipient_Device | UsbCtrlFlags.Direction_In), (byte)request, 0, 0, 0);
				int length;
				if (!_usbDevice.ControlTransfer(ref setup, null, 0, out length))
					usbDisconnected();
			}
		}

		private void tmrUSBCheck_Tick(object sender, EventArgs e)
		{
			if (_usbDevice == null)
			{
				// (0x16C1, "Nima Alamatsaz", 0x15DC, "AVROscope")
				foreach (LibUsbRegistry reg in LibUsbDevice.AllLibUsbDevices)
					if (reg.Vid == 0x16C1 && reg.Pid == 0x0002 && reg.Device.Info.ManufacturerString == "Nima Alamatsaz" && reg.Device.Info.ProductString == "AVROscope")
					{
						_usbDevice = (LibUsbDevice)reg.Device;
						if (_usbDevice.Open())
							lblStatus.Text = "Connected";
						else
						{
							lblStatus.Text = "Can't Connect";
							_usbDevice = null;
						}
					}
			}
		}

		private void usbDisconnected()
		{
			_usbDevice = null;
			lblStatus.Text = "Not Connected";
		}

		private void txtDivX_TextChanged(object sender, EventArgs e)
		{
			try
			{
				double temp = double.Parse(txtDivX.Text);
				if (temp > 0) plotter.DivX = temp;
			}
			catch
			{
			}
		}

		private void txtDivY_TextChanged(object sender, EventArgs e)
		{
			try
			{
				double temp = double.Parse(txtDivY.Text);
				if (temp > 0) plotter.DivY = temp;
			}
			catch
			{
			}
		}

		private void cmbTriggerEdge_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				plotter.TriggerEdge = (TriggerEdgeType)Enum.Parse(typeof(TriggerEdgeType), cmbTriggerEdge.SelectedItem.ToString());
			}
			catch
			{
			}
		}
	}
}
