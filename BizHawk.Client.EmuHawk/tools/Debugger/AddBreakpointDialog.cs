﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using BizHawk.Emulation.Common;

namespace BizHawk.Client.EmuHawk
{
	public partial class AddBreakpointDialog : Form, IHasShowDialog
	{
		public AddBreakpointDialog(BreakpointOperation op)
		{
			InitializeComponent();
			Operation = op;
		}

		public AddBreakpointDialog(BreakpointOperation op, uint address, MemoryCallbackType type):this(op)
		{
			Address = address;
			BreakType = type;
		}

		private BreakpointOperation _operation;

		private BreakpointOperation Operation
		{
			get 
			{ 
				return _operation; 
			}
			set
			{
				switch (value)
				{
					case BreakpointOperation.Add:
						Text = "Add Breakpoint";
						break;
					case BreakpointOperation.Duplicate:
						Text = "Duplicate Breakpoint";
						break;
					case BreakpointOperation.Edit:
						Text = "Edit Breakpoint";
						break;
				}
				_operation = value;
			}
		}

		public void DisableExecuteOption()
		{
			if (ExecuteRadio.Checked)
			{
				ReadRadio.Checked = true;
			}

			ExecuteRadio.Enabled = false;
			
		}

		public MemoryCallbackType BreakType	
		{
			get
			{
				if (ReadRadio.Checked)
				{
					return MemoryCallbackType.Read;
				}

				if (WriteRadio.Checked)
				{
					return MemoryCallbackType.Write;
				}

				if (ExecuteRadio.Checked)
				{
					return MemoryCallbackType.Execute;
				}

				return MemoryCallbackType.Read;
			}

			set
			{
				ReadRadio.Checked = WriteRadio.Checked = ExecuteRadio.Checked = false;
				switch (value)
				{
					case MemoryCallbackType.Read:
						ReadRadio.Checked = true;
						break;
					case MemoryCallbackType.Write:
						WriteRadio.Checked = true;
						break;
					case MemoryCallbackType.Execute:
						ExecuteRadio.Checked = true;
						break;
				}
			}
		}

		public uint Address
		{
			get { return (uint)AddressBox.ToRawInt().Value; }
			set { AddressBox.SetFromLong(value); }
		}

		public long MaxAddressSize
		{
			get
			{
				return AddressBox.GetMax();
			}
			set
			{
				AddressBox.SetHexProperties(value);
			}
		}

		private void AddButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void CancelBtn_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void AddBreakpointDialog_Load(object sender, EventArgs e)
		{

		}

		public enum BreakpointOperation
		{
			Add, Edit, Duplicate
		}

	}
}
