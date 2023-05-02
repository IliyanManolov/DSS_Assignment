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
    //IMPORTANT
    //For some reason when testing with mocking the HttpContext.Session.GetInt32("ID") == null check in the controller runs into an exception
	//All of the tests work correctly when manually done, only fail with mock tests
    public class ArticleControllerUnitTest
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

			var mockArticleRepository = new Mock<IArticleRepository>();

			var mockSession = new Mock<ISession>();

			var authController = new AuthController(mockUserRepository.Object);
			var articleController = new ArticleController(mockDbContext, mockArticleRepository.Object);

			authController.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext { Session = mockSession.Object }
			};

			var usr = new User { Id = 1, Name = "test", Password = "testpass" };
			authController.HttpContext.Session.SetInt32("ID", usr.Id);

			var article = new Article { Id = 1, Body = "testbody", Image = "testimage", Title = "testtitle", UserId = usr.Id};
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
            var mockArticleRepository = new Mock<IArticleRepository>();

            var mockSession = new Mock<ISession>();

            var authController = new AuthController(mockUserRepository.Object);
            var articleController = new ArticleController(mockDbContext, mockArticleRepository.Object);

            authController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { Session = mockSession.Object }
            };

            var usr = new User { Id = 1, Name = "test", Password = "testpass" };
            var article = new Article { Id = 1, Body = "", Title = "testtitle", UserId = usr.Id};
            authController.HttpContext.Session.SetInt32("ID", usr.Id);


            var result = articleController.WriteArticle(article);
			Assert.Null(mockDbContext.Articles);
        }
		[Fact]
		public void RemoveArticle()
		{
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var mockDbContext = new ApplicationDBContext(optionsBuilder.Options);

            var usr = new User { Id = 1, Name = "test", Password = "testpass" };

            var article = new Article { Id = 1, Body = "testbody", Image = "testimage", Title = "testtitle", UserId = usr.Id };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.GetUser("test", "testpass"))
                .Returns(new User { Id = 1, Name = "test", Password = "testpass" });
            var mockSession = new Mock<ISession>();

            var mockArticleRepository = new Mock<IArticleRepository>();
            mockArticleRepository.Setup(repo => repo.DeleteArticle(article, usr.Id))
                .Returns(true);

            var authController = new AuthController(mockUserRepository.Object);
            var articleController = new ArticleController(mockDbContext, mockArticleRepository.Object);

            authController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { Session = mockSession.Object }
            };
            authController.HttpContext.Session.SetInt32("ID", usr.Id);

            var result = articleController.DeleteArticle(article.Id);
            Assert.Null(mockDbContext.Articles);
        }
    }

}
