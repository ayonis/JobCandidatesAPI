using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Test_Task.Interfaces;
using Test_Task.Models;

namespace Test_Task.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CandidateController : ControllerBase
	{
		ICandidateServices _candidateServices;
		public CandidateController(ICandidateServices candidateServices)
		{
			_candidateServices = candidateServices;
		}
		[HttpGet]
		public IActionResult Get()
		{
			var candidates = _candidateServices.GetAll();
			return Ok(candidates);
		}
		[HttpGet("{email}")]
		public IActionResult Get(string email)
		{
			var candidate = _candidateServices.GetById(email);
			if (candidate == null)
			{
				return NotFound(); 
			}
			return Ok(candidate);
		}
	

		[HttpPost]
		public IActionResult Post(Candidate candidate)
		{
			if (ModelState.IsValid) {
				_candidateServices.Add(candidate);
				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}
		[HttpPut]
		public IActionResult Put(Candidate candidate)
		{
			if (ModelState.IsValid)
			{
				short status = _candidateServices.Update(candidate);

				if (status == 0)
					return BadRequest("This Candidate Does not Exist");

				else
					return Ok("Updated");
			}
			else
			{
				return BadRequest("Not Valid Data");
			}
		}
		[HttpDelete("{email}")]
		public IActionResult Delete(string email)
		{
			short status = _candidateServices.Delete(email);

			if(status == 0)
			{
				return BadRequest("This Candidate Does not Exist");
			}
			else
			{
				return Ok("Deleted Successfully");
			}
		}
	}
}
