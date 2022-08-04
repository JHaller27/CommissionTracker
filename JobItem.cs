using System;
using System.Collections.Generic;
using CommissionTracker;
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
	private LineEdit NoteNode => this.GetNode<LineEdit>("NoteLineEdit");
	private OptionButton TypeNode => this.GetNode<OptionButton>("HBoxContainer/TypeOptionButton");

	[Signal]
	public delegate void DeleteMe();

	public override void _Ready()
	{

	}

	private void _on_DeleteButton_pressed()
	{
		EmitSignal(nameof(DeleteMe));
	}
}
