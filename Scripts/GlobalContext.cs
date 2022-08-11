using System;
using CommissionTracker.SaveModels;

namespace CommissionTracker
{
	public static class GlobalContext
	{
		public static DateTime? LoadDate { get; set; }
		public static GlobalModel Model { get; set; } = new();
	}
}
