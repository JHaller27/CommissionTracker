using System;
using System.Globalization;

namespace CommissionTracker
{
	public static class Utils
	{
		private const string DateSaveFormat = "yyMMdd";
		private const string DateDisplayFormat = "d, MMMM yyyy";

		public static string GetDateSaveString(this DateTime dateTime)
		{
			return dateTime.ToString(DateSaveFormat);
		}

		public static DateTime DateFromSaveString(string saveString)
		{
			return DateTime.ParseExact(saveString, DateSaveFormat, CultureInfo.InvariantCulture);
		}

		public static string GetDateDisplayString(this DateTime dateTime)
		{
			return dateTime.ToString(DateDisplayFormat);
		}
	}
}
