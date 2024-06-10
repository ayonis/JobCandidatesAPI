using Microsoft.EntityFrameworkCore;
using Test_Task.Models;

namespace Test_Task.Interfaces
{
	public interface IDbContext
	{
		DbSet<Candidate> Candidates { get; set; }
		int SaveChanges();
	}
}
