using DSS_Assignment.Controllers;
using DSS_Assignment.Models;
using DSS_Assignment.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DSS_Assignment_Tests
{
	public class AuthControllerUnitTest
	{
		public AuthControllerUnitTest()
		{

		}
		[Fact]
		public void FailedLogin()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			mockUserRepository.Setup(repo => repo.GetUser("test", "testpass"))
				.Returns(new User { Id = 1, Name = "test", Password = "testpass" });

			var mockSession = new Mock<ISession>();

			var authController = new AuthController(mockUserRepository.Object);
			authController.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext { Session = mockSession.Object }
			};

			var usr = new User { Name = "test", Password = "password" };

			var result = authController.Login(usr);

			Assert.Equal("User not found", authController.ViewBag.Error);
		}
		[Fact]
		public void Login()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			mockUserRepository.Setup(repo => repo.GetUser("test", "testpass"))
				.Returns(new User { Id = 1, Name = "test", Password = "testpass" });

			var mockSession = new Mock<ISession>();

			var authController = new AuthController(mockUserRepository.Object);
			authController.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext { Session = mockSession.Object }
			};

			var usr = new User { Name = "test", Password = "testpass" };

			var result = authController.Login(usr);
			Assert.Equal(null, authController.ViewBag.Error);
		}
		[Fact]
		public void Register()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockSession = new Mock<ISession>();

			var authController = new AuthController(mockUserRepository.Object);
			authController.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext { Session = mockSession.Object }
			};

			var usr = new User { Name = "test", Password = "testpass" };
			var result = authController.Register(usr);
			Assert.Equal(usr, authController.ViewBag.CurrentUser);
		}
		[Fact]
		public void FailedRegisterNoName()
		{
			var mockUserRepository = new Mock<IUserRepository>();
			var mockSession = new Mock<ISession>();

			var authController = new AuthController(mockUserRepository.Object);
			authController.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext { Session = mockSession.Object }
			};

			var usr = new User { Password = "testpass" };
			var result = authController.Register(usr);

			//For some reason mocking BYPASSES the ModelState.IsValid check
			//this test should be passing but the "bug" (?) causes it to fail
			Assert.Equal("No name given", authController.ViewBag.RegisterError);
		}
	}
}