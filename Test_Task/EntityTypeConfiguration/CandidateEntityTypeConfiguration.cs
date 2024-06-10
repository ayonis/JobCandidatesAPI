using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using Test_Task.Models;

namespace Test_Task.EntityTypeConfiguration
{
	public class CandidateEntityTypeConfiguration : IEntityTypeConfiguration<Candidate>
	{
		public void Configure(EntityTypeBuilder<Candidate> builder)
		{
			builder.HasKey(c => c.Email);
			builder.Property(c => c.Email).HasColumnType("varchar(250)");
			builder.Property(c => c.First_name).IsRequired();
			builder.Property(c => c.First_name).HasColumnType("varchar(100)");
			builder.Property(c => c.Last_name).IsRequired();
			builder.Property(c => c.Last_name).HasColumnType("varchar(300)");
			builder.Property(c => c.Phone_number).HasColumnType("varchar(50)");
			builder.Property(c => c.LinkedIn_profile_URL).HasColumnType("varchar(2000)");
			builder.Property(c => c.GitHub_profile_URL).HasColumnType("varchar(2000)");
			builder.Property(c => c.comment).IsRequired();
		    builder.Property(c => c.comment).HasColumnType("varchar(500)");
		}
	}
}
