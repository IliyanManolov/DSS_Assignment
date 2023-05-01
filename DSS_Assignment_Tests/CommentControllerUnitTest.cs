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

namespace DSS_Assignment_Tests
{
    public class CommentControllerUnitTest
    {
        public CommentControllerUnitTest() 
        { 
        
        }

        [Fact]
        public void AddComment()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var mockDbContext = new ApplicationDBContext(optionsBuilder.Options);
            var mockSession = new Mock<ISession>();
            
            
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.GetUser("test", "testpass"))
                .Returns(new User { Id = 1, Name = "test", Password = "testpass" });

            var mockArticleRepository = new Mock<IArticleRepository>();

            var mockCommentRepository = new Mock<ICommentRepository>();

            var authController = new AuthController(mockUserRepository.Object);
            var articleController = new ArticleController(mockDbContext, mockArticleRepository.Object);
            var commentController = new CommentController(mockDbContext, mockCommentRepository.Object, mockArticleRepository.Object);

            authController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { Session = mockSession.Object }
            };

            var usr = new User { Id = 1, Name = "test", Password = "testpass" };
            var article = new Article { Id = 1, Body = "testbody", Image = "testimage", Title = "testtitle", UserId = usr.Id, CommentsAmount = 1 };
            var comment = new Comment { Id = 1, Body = "testcomment", UserId = usr.Id };
            authController.HttpContext.Session.SetInt32("ID", usr.Id);
            var result = commentController.WriteComment(comment, article.Id);

            Assert.NotNull(mockDbContext.Comments);
        }
    }
}
