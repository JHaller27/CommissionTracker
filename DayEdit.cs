using System;
using Godot;

public class DayEdit : Control
{
	public DateTime Date
	{
		get
		{
			string text = this.GetNode<Label>("Panel/MarginContainer/VBoxContainer/TitleVBox/DayTitle").Text;
			return DateTime.Parse(text);
		}
		set
		{
			string text = value.ToString("MMMM dd, yyyy");
			this.GetNode<Label>("Panel/MarginContainer/VBoxContainer/TitleVBox/DayTitle").Text = text;
		}
	}

	public override void _Ready()
	{
		DateTime now = DateTime.Now;
		this.Date = now;
		Console.WriteLine(this.Date);
	}
}
