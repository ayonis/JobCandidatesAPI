using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Test_Task.Models
{
	public class Candidate
	{
		public string Email { get; set; }

		public string First_name { get; set; }
	
		public string Last_name { get; set; }
		public string? Phone_number { get; set; }
		public DateTime? Time_interval_call { get; set; }
		public string? LinkedIn_profile_URL { get; set; }
		public string? GitHub_profile_URL { get; set; }

		public string comment { get; set; }
	}
}
