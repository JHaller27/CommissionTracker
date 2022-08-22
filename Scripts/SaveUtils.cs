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
		private const string GlobalModelsPath = "user://Globals.sav";

		public static void SaveDay(DayModel model)
		{
			DateTime date = Utils.DateFromSaveString(model.Date);
			SaveAsJson(model, GetDaySavePath(date));
			AddDay(date);
		}

		public static DayModel LoadDay(DateTime dateTime)
		{
			return LoadFromJson<DayModel>(GetDaySavePath(dateTime));
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

		private static void SaveAsJson<T>(T obj, string path)
		{
			File file = new();
			string json = JsonConvert.SerializeObject(obj);
			file.Open(path, File.ModeFlags.Write);
			file.StoreLine(json);
			file.Close();
		}

		private static T? LoadFromJson<T>(string path)
		{
			File file = new();
			file.Open(path, File.ModeFlags.Read);
			string json = file.GetAsText();
			file.Close();

			return JsonConvert.DeserializeObject<T>(json);
		}

		public static void SaveGlobalData()
		{
			SaveAsJson(GlobalContext.Model, GlobalModelsPath);
		}

		public static GlobalModel LoadGlobalData()
		{
			return LoadFromJson<GlobalModel>(GlobalModelsPath);
		}

		public static string ExportAll()
		{
			PortAllModel allModel = new()
			{
				Global = GlobalContext.Model,
				Days = ListDays().Select(LoadDay).ToList(),
			};

			return JsonConvert.SerializeObject(allModel);
		}

		public static void ImportAll(string json)
		{
			PortAllModel allModel = JsonConvert.DeserializeObject<PortAllModel>(json);
			if (allModel == null) return;

			GlobalContext.Model = allModel.Global;
			allModel.Days.ForEach(SaveDay);
		}
	}
}
