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

namespace avroscope
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.btnPlot = new System.Windows.Forms.Button();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.btnTest = new System.Windows.Forms.Button();
			this.lblStatus = new System.Windows.Forms.Label();
			this.chkLED = new System.Windows.Forms.CheckBox();
			this.tmrUSBCheck = new System.Windows.Forms.Timer(this.components);
			this.lblOut = new System.Windows.Forms.Label();
			this.txtDivX = new System.Windows.Forms.TextBox();
			this.txtDivY = new System.Windows.Forms.TextBox();
			this.lblDivX = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cmbTriggerEdge = new System.Windows.Forms.ComboBox();
			this.plotter = new avroscope.Plotter();
			((System.ComponentModel.ISupportInitialize)(this.plotter)).BeginInit();
			this.SuspendLayout();
			// 
			// btnPlot
			// 
			this.btnPlot.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnPlot.Location = new System.Drawing.Point(21, 12);
			this.btnPlot.Name = "btnPlot";
			this.btnPlot.Size = new System.Drawing.Size(58, 23);
			this.btnPlot.TabIndex = 1;
			this.btnPlot.Text = "Plot";
			this.btnPlot.UseVisualStyleBackColor = true;
			this.btnPlot.Click += new System.EventHandler(this.btnPlot_Click);
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 10;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// btnTest
			// 
			this.btnTest.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnTest.Location = new System.Drawing.Point(85, 12);
			this.btnTest.Name = "btnTest";
			this.btnTest.Size = new System.Drawing.Size(58, 23);
			this.btnTest.TabIndex = 2;
			this.btnTest.Text = "Test";
			this.btnTest.UseVisualStyleBackColor = true;
			this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
			// 
			// lblStatus
			// 
			this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblStatus.Location = new System.Drawing.Point(202, 12);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(104, 23);
			this.lblStatus.TabIndex = 5;
			this.lblStatus.Text = "USB Status";
			this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// chkLED
			// 
			this.chkLED.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.chkLED.Location = new System.Drawing.Point(149, 13);
			this.chkLED.Name = "chkLED";
			this.chkLED.Size = new System.Drawing.Size(47, 22);
			this.chkLED.TabIndex = 3;
			this.chkLED.Text = "LED";
			this.chkLED.UseVisualStyleBackColor = true;
			this.chkLED.CheckedChanged += new System.EventHandler(this.chkLED_CheckedChanged);
			// 
			// tmrUSBCheck
			// 
			this.tmrUSBCheck.Enabled = true;
			this.tmrUSBCheck.Interval = 500;
			this.tmrUSBCheck.Tick += new System.EventHandler(this.tmrUSBCheck_Tick);
			// 
			// lblOut
			// 
			this.lblOut.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lblOut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblOut.Location = new System.Drawing.Point(312, 12);
			this.lblOut.Name = "lblOut";
			this.lblOut.Size = new System.Drawing.Size(135, 23);
			this.lblOut.TabIndex = 6;
			this.lblOut.Text = "USB Out";
			this.lblOut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtDivX
			// 
			this.txtDivX.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.txtDivX.Location = new System.Drawing.Point(489, 14);
			this.txtDivX.Name = "txtDivX";
			this.txtDivX.Size = new System.Drawing.Size(53, 20);
			this.txtDivX.TabIndex = 7;
			this.txtDivX.TextChanged += new System.EventHandler(this.txtDivX_TextChanged);
			// 
			// txtDivY
			// 
			this.txtDivY.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.txtDivY.Location = new System.Drawing.Point(584, 14);
			this.txtDivY.Name = "txtDivY";
			this.txtDivY.Size = new System.Drawing.Size(53, 20);
			this.txtDivY.TabIndex = 8;
			this.txtDivY.TextChanged += new System.EventHandler(this.txtDivY_TextChanged);
			// 
			// lblDivX
			// 
			this.lblDivX.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lblDivX.AutoSize = true;
			this.lblDivX.Location = new System.Drawing.Point(452, 17);
			this.lblDivX.Name = "lblDivX";
			this.lblDivX.Size = new System.Drawing.Size(30, 13);
			this.lblDivX.TabIndex = 9;
			this.lblDivX.Text = "DivX";
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(547, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(30, 13);
			this.label1.TabIndex = 10;
			this.label1.Text = "DivY";
			// 
			// cmbTriggerEdge
			// 
			this.cmbTriggerEdge.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.cmbTriggerEdge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTriggerEdge.FormattingEnabled = true;
			this.cmbTriggerEdge.Location = new System.Drawing.Point(651, 13);
			this.cmbTriggerEdge.Name = "cmbTriggerEdge";
			this.cmbTriggerEdge.Size = new System.Drawing.Size(85, 21);
			this.cmbTriggerEdge.TabIndex = 11;
			this.cmbTriggerEdge.SelectedIndexChanged += new System.EventHandler(this.cmbTriggerEdge_SelectedIndexChanged);
			// 
			// plotter
			// 
			this.plotter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.plotter.BackColor = System.Drawing.Color.White;
			this.plotter.DivX = 0.05D;
			this.plotter.DivY = 1D;
			this.plotter.Location = new System.Drawing.Point(12, 38);
			this.plotter.Name = "plotter";
			this.plotter.Plot = false;
			this.plotter.SampleTime = 0D;
			this.plotter.Size = new System.Drawing.Size(734, 398);
			this.plotter.TabIndex = 0;
			this.plotter.TabStop = false;
			this.plotter.TriggerEdge = avroscope.TriggerEdgeType.None;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(757, 448);
			this.Controls.Add(this.cmbTriggerEdge);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lblDivX);
			this.Controls.Add(this.txtDivY);
			this.Controls.Add(this.txtDivX);
			this.Controls.Add(this.lblOut);
			this.Controls.Add(this.chkLED);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.btnTest);
			this.Controls.Add(this.btnPlot);
			this.Controls.Add(this.plotter);
			this.Name = "MainForm";
			this.Text = "AVROscope";
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.plotter)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnPlot;
		private System.Windows.Forms.Timer timer1;
		private Plotter plotter;
		private System.Windows.Forms.Button btnTest;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.CheckBox chkLED;
		private System.Windows.Forms.Timer tmrUSBCheck;
		private System.Windows.Forms.Label lblOut;
		private System.Windows.Forms.TextBox txtDivX;
		private System.Windows.Forms.TextBox txtDivY;
		private System.Windows.Forms.Label lblDivX;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cmbTriggerEdge;
	}
}

