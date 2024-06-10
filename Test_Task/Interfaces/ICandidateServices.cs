using Microsoft.EntityFrameworkCore;
using Test_Task.Models;

namespace Test_Task.Interfaces
{
	public interface ICandidateServices
	{
		 void Add(Candidate candidate);
		 short Update(Candidate candidate);
		 short Delete(string email);
		 List<Candidate> GetAll();
		 Candidate GetById(string email);
	}
}
