using System;
using CommissionTracker;
using Godot;

public class SelectDay : Control
{
	private static PackedScene _daySelectionScene = ResourceLoader.Load<PackedScene>("res://Scenes/DaySelectionItem.tscn");

	private Node ButtonList => this.GetNode("%ButtonList");
	private bool DeleteEnabled => this.DeleteLockButton.Pressed;

	private TextureButton DeleteLockButton => this.GetNode<TextureButton>("%DeleteLockButton");

	public override void _Ready()
	{
		foreach (DateTime date in SaveUtils.ListDays())
		{
			DaySelectionItem item = _daySelectionScene.Instance<DaySelectionItem>();
			item.Day = date;
			item.SetLockDelete(this.DeleteEnabled);
			item.Connect("DeleteMe", this, nameof(DeleteDay));

			this.ButtonList.AddChild(item);
		}
	}

	private void DeleteDay(DaySelectionItem source)
	{
		SaveUtils.RemoveDay(source.Day);
		this.ButtonList.RemoveChild(source);
	}

	private void ToggleDeleteLock(bool buttonPressed)
	{
		foreach (DaySelectionItem item in this.ButtonList.GetChildren())
		{
			item.SetLockDelete(this.DeleteEnabled);
		}
	}
}
