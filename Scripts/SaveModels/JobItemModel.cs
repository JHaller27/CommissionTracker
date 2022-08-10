namespace CommissionTracker.SaveModels
{
	public class JobItemModel
	{
		public JobType JobType { get; set; }
		public decimal Amount { get; set; }
		public string Note { get; set; }
	}
}
