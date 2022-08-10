using System;
using CommissionTracker;
using Godot;

public class SelectDay : Control
{
	private static PackedScene _daySelectionScene = ResourceLoader.Load<PackedScene>("res://Scenes/DaySelectionItem.tscn");

	private Node ButtonList => this.GetNode("%ButtonList");

	public override void _Ready()
	{
		foreach (DateTime date in SaveUtils.ListDays())
		{
			DaySelectionItem item = _daySelectionScene.Instance<DaySelectionItem>();
			item.Day = date;
			item.Connect("DeleteMe", this, nameof(DeleteDay));

			this.ButtonList.AddChild(item);
		}
	}

	private void DeleteDay(DaySelectionItem source)
	{
		SaveUtils.RemoveDay(source.Day);
		this.ButtonList.RemoveChild(source);
	}
}
