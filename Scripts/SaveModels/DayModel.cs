using System.Collections.Generic;

namespace CommissionTracker.SaveModels
{
	public class DayModel
	{
		public string Date { get; set; }
		public decimal Commission { get; set; }
		public List<JobItemModel> Jobs { get; set; }
	}
}
