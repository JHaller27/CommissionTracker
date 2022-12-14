using System;
using System.Globalization;
using Godot;

namespace CommissionTracker
{
	public static class Utils
	{
		private const string DateSaveFormat = "yyMMdd";
		private const string DateDisplayFormat = "MMMM d, yyyy";

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

		public static DateTime DateFromDisplayString(string displayString)
		{
			return DateTime.ParseExact(displayString, DateDisplayFormat, CultureInfo.InvariantCulture);
		}

		public static void SetTextureButtonSize(TextureButton button, Control basedOn)
		{
			button.Expand = true;
			int height = (int)basedOn.RectSize.y;
			button.RectMinSize = new(height, height);
		}

		public static void GlobalReleaseFocus(this Control control)
		{
			Control currentFocus = control.GetFocusOwner();
			currentFocus?.ReleaseFocus();
		}
	}
}
