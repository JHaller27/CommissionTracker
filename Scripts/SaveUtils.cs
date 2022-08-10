using System;
using System.Collections.Generic;
using System.Linq;
using CommissionTracker.SaveModels;
using Godot;
using Newtonsoft.Json;

namespace CommissionTracker
{
	public static class SaveUtils
	{
		private const string ListOfDaysPath = "user://ListOfDays.sav";

		public static void SaveDay(DayEdit dayEditScene)
		{
			DayModel model = dayEditScene.Export();
			string json = JsonConvert.SerializeObject(model);

			File file = new();
			file.Open(GetDaySavePath(dayEditScene.Date), File.ModeFlags.Write);
			file.StoreLine(json);
			file.Close();

			AddDay(dayEditScene.Date);
		}

		public static DayModel LoadDay(DateTime dateTime)
		{
			File file = new();
			file.Open(GetDaySavePath(dateTime), File.ModeFlags.Read);
			string json = file.GetAsText();
			file.Close();

			DayModel model = JsonConvert.DeserializeObject<DayModel>(json);

			return model;
		}

		private static string GetDaySavePath(DateTime dateTime)
		{
			string dateString = dateTime.GetDateSaveString();
			return $"user://{dateString}.sav";
		}

		public static IEnumerable<DateTime> ListDays()
		{
			File file = new();
			file.Open(ListOfDaysPath, File.ModeFlags.Read);
			while (file.GetPosition() < file.GetLen())
			{
				string line = file.GetLine();
				DateTime dateTime = Utils.DateFromSaveString(line);
				yield return dateTime;
			}
			file.Close();
		}

		public static void RemoveDay(DateTime date)
		{
			HashSet<DateTime> dates = ListDays().ToHashSet();
			dates.Remove(date);
			SaveDayList(dates);
		}

		private static void AddDay(DateTime dateTime)
		{
			HashSet<DateTime> dates = ListDays().ToHashSet();
			dates.Add(dateTime);
			SaveDayList(dates);
		}

		private static void SaveDayList(IEnumerable<DateTime> dates)
		{
			File file = new();
			file.Open(ListOfDaysPath, File.ModeFlags.Write);
			foreach (DateTime date in dates)
			{
				file.StoreLine(date.GetDateSaveString());
			}
			file.Close();
		}
	}
}
