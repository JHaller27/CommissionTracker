using System;
using CommissionTracker;
using Godot;

public class SelectDay : Control
{
	private static PackedScene _daySelectionScene = ResourceLoader.Load<PackedScene>("res://Scenes/DaySelectionButton.tscn");

	private Node ButtonList => this.GetNode("%ButtonList");

	public override void _Ready()
	{
		foreach (DateTime date in SaveUtils.ListDays())
		{
			DaySelectionButton button = _daySelectionScene.Instance<DaySelectionButton>();
			button.Day = date;

			this.ButtonList.AddChild(button);
		}
	}
}
