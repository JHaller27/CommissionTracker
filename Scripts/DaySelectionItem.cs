using System;
using Godot;

public class DaySelectionItem : HBoxContainer
{
	[Signal]
	public delegate void DeleteMe(DaySelectionItem source);

	public DateTime Day
	{
		get => this.Button.Day;
		set => this.Button.Day = value;
	}

	public void SetLockDelete(bool isEnabled)
	{
		this.DeleteButton.Disabled = !isEnabled;
	}

	private TextureButton DeleteButton => this.GetNode<TextureButton>("DeleteButton");
	private DaySelectionButton Button => this.GetNode<DaySelectionButton>("Button");

	private void Remove() => EmitSignal(nameof(DeleteMe), this);
}
