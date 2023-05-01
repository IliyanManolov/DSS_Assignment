using DSS_Assignment.Controllers;
using DSS_Assignment.Data;
using DSS_Assignment.Models;
using DSS_Assignment.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace DSS_Assignment_Tests
{
	public  class ArticleControllerUnitTest
	{
		public ArticleControllerUnitTest()
		{
		
		}

		[Fact]
		public void AddArticle()
		{
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDBContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString());
			var mockDbContext = new ApplicationDBContext(optionsBuilder.Options);

			var mockUserRepository = new Mock<IUserRepository>();
			mockUserRepository.Setup(repo => repo.GetUser("test", "testpass"))
				.Returns(new User { Id = 1, Name = "test", Password = "testpass" });
			var mockSession = new Mock<ISession>();

			var mockArticleRepository = new Mock<IArticleRepository>();

			var authController = new AuthController(mockUserRepository.Object);
			var articleController = new ArticleController(mockDbContext, mockArticleRepository.Object);

			authController.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext { Session = mockSession.Object }
			};

			var usr = new User { Id = 1, Name = "test", Password = "testpass" };
			authController.HttpContext.Session.SetInt32("ID", usr.Id);
			var sesid = authController.HttpContext.Session.GetInt32("ID");

			var article = new Article { Id = 1, Body = "testbody", CommentsAmount = 0, Image = "testimage", Title = "testtitle"};
			var result = articleController.WriteArticle(article);
			Assert.NotNull(mockDbContext.Articles);
		}
		[Fact]
		public void AddEmptyArticle()
		{
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var mockDbContext = new ApplicationDBContext(optionsBuilder.Options);

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.GetUser("test", "testpass"))
                .Returns(new User { Id = 1, Name = "test", Password = "testpass" });
            var mockSession = new Mock<ISession>();

            var mockArticleRepository = new Mock<IArticleRepository>();

            var authController = new AuthController(mockUserRepository.Object);
            var articleController = new ArticleController(mockDbContext, mockArticleRepository.Object);

            authController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { Session = mockSession.Object }
            };
        }
    }

}
