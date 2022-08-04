using System;
using Godot;

public class DayEdit : Control
{
	// Public properties
	public DateTime Date
	{
		get
		{
			string text = this.DateTitleNode.Text;
			return DateTime.Parse(text);
		}
		set
		{
			string text = value.ToString("MMMM dd, yyyy");
			this.DateTitleNode.Text = text;
		}
	}
	public decimal CommissionPercentage
	{
		get
		{
			decimal value = (decimal)this.CommissionPercentageNode.Value;
			return value;
		}
		set => this.CommissionPercentageNode.Value = (double)value;
	}

	public decimal CommissionTotal
	{
		get => decimal.Parse(this.CommissionTotalNode.Text);
		set => this.CommissionTotalNode.Text = value.ToString();
	}

	public decimal TipsTotal
	{
		get => decimal.Parse(this.TipsTotalNode.Text);
		set => this.TipsTotalNode.Text = value.ToString();
	}

	public decimal GrandTotal
	{
		get => decimal.Parse(this.GrandTotalNode.Text);
		set => this.GrandTotalNode.Text = value.ToString();
	}

	// Node proxies
	private Label DateTitleNode => this.GetNode<Label>("Panel/MarginContainer/VBoxContainer/TitleVBox/DayTitle");
	private SpinBox CommissionPercentageNode => this.GetNode<SpinBox>("Panel/MarginContainer/VBoxContainer/TitleVBox/CommissionHBox/ValueHBox/CommissionSpinBox");
	private Label CommissionTotalNode => this.GetNode<Label>("Panel/MarginContainer/VBoxContainer/TotalGrid/CommissionValue/Value");
	private Label TipsTotalNode => this.GetNode<Label>("Panel/MarginContainer/VBoxContainer/TotalGrid/TipsValue/Value");
	private Label GrandTotalNode => this.GetNode<Label>("Panel/MarginContainer/VBoxContainer/TotalGrid/GrandTotalValue/Value");

	// Methods
	public override void _Ready()
	{
		this.Date = DateTime.Now;
		this.CommissionPercentage = 40;

		this.CommissionTotal = (decimal)100.34;
		this.TipsTotal = 20;
		this.RecalculateTotals();
	}

	private void RecalculateTotals()
	{
		this.GrandTotal = this.CommissionTotal + this.TipsTotal;
	}
}
