using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Test_Task.Controllers;
using Test_Task.Interfaces;
using Test_Task.Models;
using Test_Task.Services;
using Xunit;
using Assert = Xunit.Assert;

namespace Test_Task.Test
{
	public class CandidateServicesTests
	{
		private readonly Mock<ICandidateServices> CandidateServices;
		private readonly CandidateController controller;

		public CandidateServicesTests()
		{
			CandidateServices = new Mock<ICandidateServices>();
			controller = new CandidateController(CandidateServices.Object);
		}
		public void GetAll_ReturnsListOfCandidates()
		{
			// Arrange
			var mockDbContext = new Mock<IDbContext>();
			var mockCandidates = new Mock<DbSet<Candidate>>();
			mockDbContext.Setup(db => db.Candidates).Returns(mockCandidates.Object);
			mockCandidates.Setup(m => m.ToList()).Returns(new List<Candidate> { new Candidate { Email = "Ayounis@gmail.com", First_name = "younis", Last_name = "Hamza", Phone_number = "+201149104905", Time_interval_call = null, LinkedIn_profile_URL = "https://www.linkedin.com/in/abd-elrhman-sayed-younis-550b39b3", GitHub_profile_URL = "https://github.com/ayonis", comment = "No coment" } });
			var candidateServices = new CandidateServices(mockDbContext.Object);

			// Act
			var result = candidateServices.GetAll();

			// Assert
			Assert.Single(result);
		}

		
		public void GetById_ReturnsCandidate_WhenExists()
		{
			// Arrange
			var mockDbContext = new Mock<IDbContext>();
			var mockCandidates = new Mock<DbSet<Candidate>>();
			mockDbContext.Setup(db => db.Candidates).Returns(mockCandidates.Object);
			mockCandidates.Setup(m => m.FirstOrDefault(It.IsAny<Func<Candidate, bool>>()))
				.Returns(new Candidate { Email = "Ayounis@gmail.com", First_name = "younis", Last_name = "Hamza", Phone_number = "+201149104905", Time_interval_call = null, LinkedIn_profile_URL = "https://www.linkedin.com/in/abd-elrhman-sayed-younis-550b39b3", GitHub_profile_URL = "https://github.com/ayonis", comment = "No coment" });
			var candidateServices = new CandidateServices(mockDbContext.Object);

			// Act
			var result = candidateServices.GetById("Ayounis@gmail.com");

			// Assert
			Assert.NotNull(result);
			Assert.Equal("Ayounis@gmail.com", result.Email);
		}

		[Fact]
		public void GetById_ReturnsNull_WhenDoesNotExist()
		{
			// Arrange
			string nonExistentEmail = "nonexistent@example.com";
			CandidateServices.Setup(service => service.GetById(nonExistentEmail))
							 .Returns((Candidate)null); 

			// Act
			var result = controller.Get(nonExistentEmail);

			// Assert
			var notFoundResult = Assert.IsType<NotFoundResult>(result);
		}


		[Fact]
		public void Add_AddsCandidateToDbContext()
		{
			// Arrange
			var mockDbContext = new Mock<IDbContext>();
			var mockCandidates = new Mock<DbSet<Candidate>>();
			mockDbContext.Setup(db => db.Candidates).Returns(mockCandidates.Object);
			var candidateServices = new CandidateServices(mockDbContext.Object);
			var candidate = new Candidate { Email = "Ayounis@gmail.com", First_name = "younis", Last_name = "Hamza", Phone_number = "+201149104905", Time_interval_call = null, LinkedIn_profile_URL = "https://www.linkedin.com/in/abd-elrhman-sayed-younis-550b39b3", GitHub_profile_URL = "https://github.com/ayonis", comment = "No coment" };

			// Act
			candidateServices.Add(candidate);

			// Assert
			mockDbContext.Verify(db => db.Candidates.Add(candidate), Times.Once);
			mockDbContext.Verify(db => db.SaveChanges(), Times.Once);
		}

		[Fact]
		public void Update_ReturnsZero_WhenCandidateDoesNotExist()
		{
			// Arrange
			var mockDbContext = new Mock<IDbContext>();
			var existingCandidates = new List<Candidate>(); // No candidates in the list
			var mockCandidates = new Mock<DbSet<Candidate>>();
			mockCandidates.As<IQueryable<Candidate>>().Setup(m => m.Provider).Returns(existingCandidates.AsQueryable().Provider);
			mockCandidates.As<IQueryable<Candidate>>().Setup(m => m.Expression).Returns(existingCandidates.AsQueryable().Expression);
			mockCandidates.As<IQueryable<Candidate>>().Setup(m => m.ElementType).Returns(existingCandidates.AsQueryable().ElementType);
			mockCandidates.As<IQueryable<Candidate>>().Setup(m => m.GetEnumerator()).Returns(existingCandidates.GetEnumerator());

			mockDbContext.Setup(db => db.Candidates).Returns(mockCandidates.Object);

			var candidateServices = new CandidateServices(mockDbContext.Object);
			var candidate = new Candidate { Email = "NonExistentEmail@gmail.com", First_name = "Non", Last_name = "Existent", Phone_number = "+000000000000", Time_interval_call = null, LinkedIn_profile_URL = "https://www.linkedin.com/in/non-existent", GitHub_profile_URL = "https://github.com/non-existent", comment = "No comment" };

			// Act
			var result = candidateServices.Update(candidate);

			// Assert
			Assert.Equal(0, result);
			mockDbContext.Verify(db => db.SaveChanges(), Times.Never);
		}


		[Fact]
		public void Update_ReturnsOne_WhenCandidateExists()
		{
			// Arrange
			var mockDbContext = new Mock<IDbContext>();
			var existingCandidates = new List<Candidate> {
		new Candidate { Email = "Ayounis@gmail.com", First_name = "younis", Last_name = "Hamza", Phone_number = "+201149104905", Time_interval_call = null, LinkedIn_profile_URL = "https://www.linkedin.com/in/abd-elrhman-sayed-younis-550b39b3", GitHub_profile_URL = "https://github.com/ayonis", comment = "No coment" }
	};
			var mockCandidates = new Mock<DbSet<Candidate>>();
			mockCandidates.As<IQueryable<Candidate>>().Setup(m => m.Provider).Returns(existingCandidates.AsQueryable().Provider);
			mockCandidates.As<IQueryable<Candidate>>().Setup(m => m.Expression).Returns(existingCandidates.AsQueryable().Expression);
			mockCandidates.As<IQueryable<Candidate>>().Setup(m => m.ElementType).Returns(existingCandidates.AsQueryable().ElementType);
			mockCandidates.As<IQueryable<Candidate>>().Setup(m => m.GetEnumerator()).Returns(existingCandidates.GetEnumerator());

			mockDbContext.Setup(db => db.Candidates).Returns(mockCandidates.Object);

			var candidateServices = new CandidateServices(mockDbContext.Object);
			var candidate = new Candidate { Email = "Ayounis@gmail.com", First_name = "younis", Last_name = "Hamza", Phone_number = "+201149104905", Time_interval_call = null, LinkedIn_profile_URL = "https://www.linkedin.com/in/abd-elrhman-sayed-younis-550b39b3", GitHub_profile_URL = "https://github.com/ayonis", comment = "No coment" };

			// Act
			var result = candidateServices.Update(candidate);

			// Assert
			Assert.Equal(1, result);
			mockDbContext.Verify(db => db.SaveChanges(), Times.Once);
		}



		[Fact]
		public void Delete_ReturnsZero_WhenCandidateDoesNotExist()
		{
			// Arrange
			var candidatesData = new List<Candidate>
		{
			new Candidate { Email = "Ayounis@gmail.com" }
		}.AsQueryable();

			var mockDbSet = new Mock<DbSet<Candidate>>();
			mockDbSet.As<IQueryable<Candidate>>().Setup(m => m.Provider).Returns(candidatesData.Provider);
			mockDbSet.As<IQueryable<Candidate>>().Setup(m => m.Expression).Returns(candidatesData.Expression);
			mockDbSet.As<IQueryable<Candidate>>().Setup(m => m.ElementType).Returns(candidatesData.ElementType);
			mockDbSet.As<IQueryable<Candidate>>().Setup(m => m.GetEnumerator()).Returns(candidatesData.GetEnumerator());

			var mockContext = new Mock<IDbContext>();
			mockContext.Setup(c => c.Candidates).Returns(mockDbSet.Object);

			var candidateServices = new CandidateServices(mockContext.Object);

			// Act
			var result = candidateServices.Delete("NonExistingEmail@gmail.com");

			// Assert
			Assert.Equal(0, result);
		}

		[Fact]
		public void Delete_ReturnsOne_WhenCandidateExists()
		{
			// Arrange
			var mockContext = new Mock<IDbContext>();
			var mockDbSet = new Mock<DbSet<Candidate>>();

			var candidate = new Candidate { Email = "Ayounis@gmail.com" };
			var data = new List<Candidate> { candidate }.AsQueryable();

			mockDbSet.As<IQueryable<Candidate>>().Setup(m => m.Provider).Returns(data.Provider);
			mockDbSet.As<IQueryable<Candidate>>().Setup(m => m.Expression).Returns(data.Expression);
			mockDbSet.As<IQueryable<Candidate>>().Setup(m => m.ElementType).Returns(data.ElementType);
			mockDbSet.As<IQueryable<Candidate>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

			mockContext.Setup(c => c.Candidates).Returns(mockDbSet.Object);

			var candidateServices = new CandidateServices(mockContext.Object);

			// Act
			var result = candidateServices.Delete("Ayounis@gmail.com");

			// Assert
			Assert.Equal(1, result);
		}
	}
}
