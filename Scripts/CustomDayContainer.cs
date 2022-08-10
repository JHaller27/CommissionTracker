using Godot;
using System;
using CommissionTracker;

public class CustomDayContainer : VBoxContainer
{
	private DateTime Date
	{
		get => new((int)this.YearEditNode.Value, this.MonthSelectNode.Selected + 1, (int)this.DayEditNode.Value);
		set
		{
			YearEditNode.Value = value.Year;
			MonthSelectNode.Selected = value.Month - 1;

			UpdateDaysInMonth();

			DayEditNode.Value = Math.Min(value.Day, DayEditNode.MaxValue);
		}
	}

	private SpinBox YearEditNode => this.GetNode<SpinBox>("%YearEdit");
	private OptionButton MonthSelectNode => this.GetNode<OptionButton>("%MonthSelect");
	private SpinBox DayEditNode => this.GetNode<SpinBox>("%DayEdit");

	public override void _Ready()
	{
		this.Date = DateTime.Today;
	}

	private void UpdateDaysInMonth()
	{
		int newMax = DateTime.DaysInMonth((int)this.YearEditNode.Value, this.MonthSelectNode.Selected+1);
		if (DayEditNode.Value > newMax)
		{
			DayEditNode.Value = newMax;
		}

		DayEditNode.MaxValue = newMax;
	}

	private void StoreDate()
	{
		GlobalContext.LoadDate = this.Date;
	}

	private void MonthUpdated(int index) => UpdateDaysInMonth();
	private void YearUpdated(float value) => UpdateDaysInMonth();
}
