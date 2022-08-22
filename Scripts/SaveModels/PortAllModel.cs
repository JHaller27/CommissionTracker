using System.Collections.Generic;

namespace CommissionTracker.SaveModels
{
	public class PortAllModel
	{
		public GlobalModel Global { get; set; }
		public List<DayModel> Days { get; set; }
	}
}
