using System;
using System.Collections.Generic;
using CommissionTracker;
using CommissionTracker.SaveModels;
using Godot;

public class JobItem : VBoxContainer
{
	// Public properties
	public decimal Value
	{
		get => (decimal)this.ValueNode.Value;
		set => this.ValueNode.Value = (double)value;
	}

	public string Note
	{
		get => this.NoteNode.Text;
		set => this.NoteNode.Text = value;
	}

	private static readonly List<JobType> JobTypeMap = new() { JobType.Commission, JobType.Tip };
	public JobType JobType
	{
		get => JobTypeMap[this.TypeNode.Selected];
		set => this.TypeNode.Select(JobTypeMap.IndexOf(value));
	}

	// Node proxies
	private SpinBox ValueNode => this.GetNode<SpinBox>("HBoxContainer/AmountEdit");
	private LineEdit NoteNode => this.GetNode<LineEdit>("%NoteLineEdit");
	private OptionButton TypeNode => this.GetNode<OptionButton>("HBoxContainer/TypeOptionButton");

	[Signal]
	public delegate void DeleteMe();

	[Signal]
	public delegate void Changed();

	public override void _Ready()
	{
		LineEdit le = this.ValueNode.GetLineEdit();
		le.GrabFocus();
		le.Text = string.Empty;

		TextureButton button = this.GetNode<TextureButton>("%DeleteButton");
		Control basedOn = this.GetChild<Control>(0);
		Utils.SetTextureButtonSize(button, basedOn);
	}

	public JobItemModel Export()
	{
		return new()
		{
			Amount = this.Value,
			JobType = this.JobType,
			Note = this.Note,
		};
	}

	public void Import(JobItemModel model)
	{
		this.Value = model.Amount;
		this.JobType = model.JobType;
		this.Note = model.Note;
	}

	private void _on_DeleteButton_pressed() => EmitSignal(nameof(DeleteMe));

	private void AmountChanged(float value) => EmitSignal(nameof(Changed));
	private void NoteChanged(string newText) => EmitSignal(nameof(Changed));
	private void JobTypeChanged(int index) => EmitSignal(nameof(Changed));
}
