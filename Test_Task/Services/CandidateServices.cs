using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Diagnostics.Eventing.Reader;
using Test_Task.Interfaces;
using Test_Task.Models;

namespace Test_Task.Services
{
	public class CandidateServices : ICandidateServices
	{
		IDbContext _context; 

		public CandidateServices(IDbContext context) 
		{
			_context = context;
		}	
		public void Add(Candidate candidate)
		{
			_context.Candidates.Add(candidate);
			_context.SaveChanges();
		}

		public short Update(Candidate candidate)
		{
			var existingCandidate = _context.Candidates.FirstOrDefault(c => c.Email == candidate.Email);
			if (existingCandidate == null)
			{
				return 0; // Candidate not found, return 0 indicating failure
			}
			else
			{
				// Update the existing candidate with the new values
				existingCandidate.First_name = candidate.First_name;
				existingCandidate.Last_name = candidate.Last_name;
				existingCandidate.Phone_number = candidate.Phone_number;
				existingCandidate.Time_interval_call = candidate.Time_interval_call;
				existingCandidate.LinkedIn_profile_URL = candidate.LinkedIn_profile_URL;
				existingCandidate.GitHub_profile_URL = candidate.GitHub_profile_URL;
				existingCandidate.comment = candidate.comment;

				_context.SaveChanges();
				return 1; // Candidate updated successfully
			}
		}

		public short Delete(string email)
		{
			var candidate = _context.Candidates.FirstOrDefault(c => c.Email == email);
			if (candidate is null)
			{
				return 0;
			}
			else { 
				_context.Candidates.Remove(candidate);
				_context.SaveChanges();
				return 1;
			}

		}

		public List<Candidate> GetAll()
		{
			return _context.Candidates.ToList();
		}

		public Candidate GetById(string email)
		{
			return _context.Candidates.FirstOrDefault(c => c.Email == email);
		}
	}
}
