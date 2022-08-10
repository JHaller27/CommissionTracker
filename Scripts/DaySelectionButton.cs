using System;
using CommissionTracker;

public class DaySelectionButton : ChangeSceneButton
{
	private DateTime _Date { get; set; }
	public DateTime Day
	{
		get => this._Date;
		set
		{
			this._Date = value;
			this.Text = this._Date.GetDateDisplayString();
		}
	}

	protected override void ChangeScene()
	{
		GlobalContext.LoadDate = this.Day;
		base.ChangeScene();
	}
}
