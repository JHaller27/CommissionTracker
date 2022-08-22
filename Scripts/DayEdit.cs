using System;
using System.Linq;
using CommissionTracker;
using CommissionTracker.SaveModels;
using Godot;

public class DayEdit : Control
{
	// Public properties
	public DateTime Date
	{
		get
		{
			string text = this.DateTitleNode.Text;
			return Utils.DateFromDisplayString(text);
		}
		set
		{
			string text = value.GetDateDisplayString();
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

	public decimal CommissionMultiplier => this.CommissionPercentage / 100;

	public decimal CommissionTotal
	{
		get => decimal.Parse(this.CommissionTotalNode.Text);
		set => this.CommissionTotalNode.Text = value.ToString("0.00");
	}

	public decimal TipsTotal
	{
		get => decimal.Parse(this.TipsTotalNode.Text);
		set => this.TipsTotalNode.Text = value.ToString("0.00");
	}

	public decimal GrandTotal
	{
		get => decimal.Parse(this.GrandTotalNode.Text);
		set => this.GrandTotalNode.Text = value.ToString("0.00");
	}

	// Node proxies
	private Label DateTitleNode => this.GetNode<Label>("Panel/MarginContainer/VBoxContainer/TitleVBox/DayTitle");
	private SpinBox CommissionPercentageNode => this.GetNode<SpinBox>("Panel/MarginContainer/VBoxContainer/TitleVBox/CommissionHBox/ValueHBox/CommissionSpinBox");
	private Label CommissionTotalNode => this.GetNode<Label>("Panel/MarginContainer/VBoxContainer/TotalGrid/CommissionValue/Value");
	private Label TipsTotalNode => this.GetNode<Label>("Panel/MarginContainer/VBoxContainer/TotalGrid/TipsValue/Value");
	private Label GrandTotalNode => this.GetNode<Label>("Panel/MarginContainer/VBoxContainer/TotalGrid/GrandTotalValue/Value");
	private JobsContainer JobsContainer => this.GetNode<JobsContainer>("%JobsContainer");

	// Private properties
	private bool DoneLoading { get; set; } = false;

	// Methods
	public override void _Ready()
	{
		this.Date = DateTime.Today;
		this.CommissionPercentage = GlobalContext.Model.CommissionPercentage;

		if (GlobalContext.LoadDate != null)
		{
			this.Date = (DateTime)GlobalContext.LoadDate;
		}

		if (SaveUtils.ListDays().Contains(this.Date))
		{
			DayModel model = SaveUtils.LoadDay(this.Date);
			this.Import(model);
			this.RecalculateTotals();
		}

		this.RecalculateTotals();
		this.DoneLoading = true;
	}

	public override void _Process(float delta)
	{
		this.RecalculateTotals();
	}

	private void RecalculateTotals()
	{
		this.CommissionTotal = this.JobsContainer.GetJobValuesOfType(JobType.Commission)
			.Aggregate(decimal.Zero, (acc, curr) => acc + curr * this.CommissionMultiplier);
		this.TipsTotal = this.JobsContainer.GetJobValuesOfType(JobType.Tip).Sum();

		this.GrandTotal = this.CommissionTotal + this.TipsTotal;
	}

	public DayModel Export()
	{
		return new()
		{
			Commission = this.CommissionPercentage,
			Date = this.Date.GetDateSaveString(),
			Jobs = this.JobsContainer.Export(),
		};
	}

	public void Import(DayModel model)
	{
		this.CommissionPercentage = model.Commission;
		this.Date = Utils.DateFromSaveString(model.Date);
		this.JobsContainer.Import(model.Jobs);
	}

	private void UpdateDetected()
	{
		if (!this.DoneLoading) return;

		SaveUtils.SaveDay(this);
		this.GlobalReleaseFocus();
	}

	private void CommissionUpdated(float value)
	{
		this.UpdateDetected();
		GlobalContext.Model.CommissionPercentage = this.CommissionPercentage;
		SaveUtils.SaveGlobalData();
	}

	private void JobsListChanged() => this.UpdateDetected();
}
