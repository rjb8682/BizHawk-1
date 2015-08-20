﻿using System.Collections.Generic;
using System.Linq;

using BizHawk.Emulation.Common;
using BizHawk.Emulation.Common.IEmulatorExtensions;

namespace BizHawk.Client.Common
{
	public class LuaFunctionList : List<NamedLuaFunction>
	{
		public NamedLuaFunction this[string guid]
		{
			get
			{
				return this.FirstOrDefault(x => x.Guid.ToString() == guid);
			}
		}

		public new bool Remove(NamedLuaFunction function)
		{
			if (Global.Emulator.CanPollInput())
			{
				Global.Emulator.AsInputPollable().InputCallbacks.Remove(function.Callback);
			}

			if (Global.Emulator.CanDebug() && Global.Emulator.MemoryCallbacksAvailable())
			{
				Global.Emulator.AsDebuggable().MemoryCallbacks.Remove(function.Callback);
			}

			return base.Remove(function);
		}

		public void ClearAll()
		{
			if (Global.Emulator.CanPollInput())
			{
				Global.Emulator.AsInputPollable().InputCallbacks.RemoveAll(this.Select(x => x.Callback));
			}

			if (Global.Emulator.CanDebug())
			{
				Global.Emulator.AsDebuggable().MemoryCallbacks.RemoveAll(this.Select(x => x.Callback));
			}

			Clear();
		}
	}
}