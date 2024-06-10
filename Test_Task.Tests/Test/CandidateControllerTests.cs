using Microsoft.AspNetCore.Mvc;
using Moq;
using Test_Task.Controllers;
using Test_Task.Interfaces;
using Test_Task.Models;
using Xunit;
using Assert =  Xunit.Assert;



namespace Test_Task.Test
{
	public class CandidateControllerTests
	{
		[Fact]
		public void Get_ReturnsOkResult_WithListOfCandidates()
		{
			// Arrange
			var CandidateServices = new Mock<ICandidateServices>();
			CandidateServices.Setup(service => service.GetAll())
				.Returns(new List<Candidate> { new Candidate { Email = "Ayounis@gmail.com", First_name = "younis", Last_name = "Hamza" , Phone_number ="+201149104905" , Time_interval_call = null, LinkedIn_profile_URL = "https://www.linkedin.com/in/abd-elrhman-sayed-younis-550b39b3" , GitHub_profile_URL = "https://github.com/ayonis"  , comment = "No coment"} });
			var controller = new CandidateController(CandidateServices.Object);

			// Act
			var result = controller.Get();

			// Assert
			var okResult = Xunit.Assert.IsType<OkObjectResult>(result);
			var candidates = Xunit.Assert.IsType<List<Candidate>>(okResult.Value);
			Assert.Single(candidates);
		}

		[Fact]
		public void Get_ReturnsOkResult_WithCandidate()
		{
			// Arrange
			var CandidateServices = new Mock<ICandidateServices>();
			CandidateServices.Setup(service => service.GetById("Ayounis@gmail.com"))
				.Returns(new Candidate { Email = "Ayounis@gmail.com", First_name = "younis", Last_name = "Hamza", Phone_number = "+201149104905", Time_interval_call = null, LinkedIn_profile_URL = "https://www.linkedin.com/in/abd-elrhman-sayed-younis-550b39b3", GitHub_profile_URL = "https://github.com/ayonis", comment = "No coment" });
			var controller = new CandidateController(CandidateServices.Object);

			// Act
			var result = controller.Get("Ayounis@gmail.com");

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var candidate = Assert.IsType<Candidate>(okResult.Value);
			Assert.Equal("Ayounis@gmail.com", candidate.Email);
		}

		[Fact]
		public void Post_ReturnsOkResult_WhenModelStateIsValid()
		{
			// Arrange
			var CandidateServices = new Mock<ICandidateServices>();
			var controller = new CandidateController(CandidateServices.Object);
			var candidate = new Candidate { Email = "Ayounis@gmail.com", First_name = "younis", Last_name = "Hamza", Phone_number = "+201149104905", Time_interval_call = null, LinkedIn_profile_URL = "https://www.linkedin.com/in/abd-elrhman-sayed-younis-550b39b3", GitHub_profile_URL = "https://github.com/ayonis", comment = "No coment" };
			controller.ModelState.Clear();

			// Act
			var result = controller.Post(candidate);

			// Assert
			Assert.IsType<OkResult>(result);
		}

		[Fact]
		public void Post_ReturnsBadRequest_WhenModelStateIsInvalid()
		{
			// Arrange
			var CandidateServices = new Mock<ICandidateServices>();
			var controller = new CandidateController(CandidateServices.Object);
			var candidate = new Candidate { Email = "Ayounis@gmail.com", First_name = "younis", Last_name = "Hamza", Phone_number = "+201149104905", Time_interval_call = null, LinkedIn_profile_URL = "https://www.linkedin.com/in/abd-elrhman-sayed-younis-550b39b3", GitHub_profile_URL = "https://github.com/ayonis", comment = "No coment" };
			controller.ModelState.AddModelError("Name", "Required");

			// Act
			var result = controller.Post(candidate);

			// Assert
			Assert.IsType<BadRequestResult>(result);
		}

		[Fact]
		public void Put_ReturnsOkResult_WhenModelStateIsValid()
		{
			// Arrange
			var CandidateServices = new Mock<ICandidateServices>();
			CandidateServices.Setup(repo => repo.Update(It.IsAny<Candidate>())).Returns((short)1);
			var controller = new CandidateController(CandidateServices.Object);
			var candidate = new Candidate { Email = "Ayounis@gmail.com", First_name = "younis", Last_name = "Hamza", Phone_number = "+201149104905", Time_interval_call = null, LinkedIn_profile_URL = "https://www.linkedin.com/in/abd-elrhman-sayed-younis-550b39b3", GitHub_profile_URL = "https://github.com/ayonis", comment = "No coment" };
			controller.ModelState.Clear();

			// Act
			var result = controller.Put(candidate);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("Updated", okResult.Value);
		}

		[Fact]
		public void Put_ReturnsBadRequest_WhenCandidateDoesNotExist()
		{
			// Arrange
			var CandidateServices = new Mock<ICandidateServices>();
			CandidateServices.Setup(service => service.Update(It.IsAny<Candidate>())).Returns((short)0);
			var controller = new CandidateController(CandidateServices.Object);
			var candidate = new Candidate { Email = "Ayounis@gmail.com", First_name = "younis", Last_name = "Hamza", Phone_number = "+201149104905", Time_interval_call = null, LinkedIn_profile_URL = "https://www.linkedin.com/in/abd-elrhman-sayed-younis-550b39b3", GitHub_profile_URL = "https://github.com/ayonis", comment = "No coment" };
			controller.ModelState.Clear();

			// Act
			var result = controller.Put(candidate);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
			Assert.Equal("This Candidate Does not Exist", badRequestResult.Value);
		}

		[Fact]
		public void Put_ReturnsBadRequest_WhenModelStateIsInvalid()
		{
			// Arrange
			var CandidateServices = new Mock<ICandidateServices>();
			var controller = new CandidateController(CandidateServices.Object);
			var candidate = new Candidate { Email = "Ayounis@gmail.com", First_name = "younis", Last_name = "Hamza", Phone_number = "+201149104905", Time_interval_call = null, LinkedIn_profile_URL = "https://www.linkedin.com/in/abd-elrhman-sayed-younis-550b39b3", GitHub_profile_URL = "https://github.com/ayonis", comment = "No coment" };
			controller.ModelState.AddModelError("Name", "Required");

			// Act
			var result = controller.Put(candidate);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
			Assert.Equal("Not Valid Data", badRequestResult.Value);
		}

		[Fact]
		public void Delete_ReturnsOkResult_WithDeletedMessage()
		{
			// Arrange
			var CandidateServices = new Mock<ICandidateServices>();
			CandidateServices.Setup(service => service.Delete("Ayounis@gmail.com")).Returns((short)1);
			var controller = new CandidateController(CandidateServices.Object);

			// Act
			var result = controller.Delete("Ayounis@gmail.com");

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("Deleted Successfully", okResult.Value);
		}

		[Fact]
		public void Delete_ReturnsBadRequest_WhenCandidateDoesNotExist()
		{
			// Arrange
			var CandidateServices = new Mock<ICandidateServices>();
			CandidateServices.Setup(service => service.Delete("Ayounis@gmail.com")).Returns((short)0);
			var controller = new CandidateController(CandidateServices.Object);

			// Act
			var result = controller.Delete("Ayounis@gmail.com");

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
			Assert.Equal("This Candidate Does not Exist", badRequestResult.Value);
		}
	}
}
