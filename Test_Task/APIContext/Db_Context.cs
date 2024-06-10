using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Xml;
using Test_Task.EntityTypeConfiguration;
using Test_Task.Interfaces;
using Test_Task.Models;

namespace Test_Task.API_Context
{
	public class Db_Context : DbContext,IDbContext
	{
		IConfiguration _configuration;
		public Db_Context(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				var connectionString = _configuration.GetConnectionString("DefaultConnection");
				optionsBuilder.UseSqlServer(connectionString);
			}

			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			new CandidateEntityTypeConfiguration().Configure(modelBuilder.Entity<Candidate>());
		}
		public override int SaveChanges()
		{
			return base.SaveChanges();
		}
		public DbSet<Candidate> Candidates { get; set; }
	}
}
