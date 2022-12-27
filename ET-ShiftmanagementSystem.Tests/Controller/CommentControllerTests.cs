//using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using FluentAssertions;
using Xunit;
using ShiftManagementServises.Servises;
using ET_ShiftManagementSystem.Controllers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShiftMgtDbContext.Entities;
using ET_ShiftManagementSystem.Models;

namespace ET_ShiftmanagementSystem.Tests.Controller
{
    public class CommentControllerTests
    {
        private readonly IFixture _fixture;
        private readonly IMapper mapper;
        private readonly Mock<ICommentServices> _service;
        private readonly CommentController _commentController;

        public CommentControllerTests()
        {
            _fixture= new Fixture();
            _service =  _fixture.Freeze<Mock<ICommentServices>>();
            _commentController = new CommentController(_service.Object , mapper ); //creates implementation in memeory
                
        }


        [Fact]
        public async Task GetComments_ShouldReturnOkResponce_WhenData_isFound()
        {
            //Arrange 
            var CommentMock = _fixture.Create<IEnumerable<ShiftMgtDbContext.Entities.Comment>>();
            _service.Setup(p => p.GetAllCommentsAsync()).ReturnsAsync(CommentMock);

            //Act
            var Result = await _commentController.GetAllComments().ConfigureAwait(false);

            //Assert
            //Assert.NotNull(Result);
            Result.Should().NotBeNull();
            Result.Should().BeAssignableTo<ActionResult<IEnumerable<CommentDetailes>>>();
            Result.Should().BeAssignableTo<OkObjectResult>();
            Result.As<OkObjectResult>().Should().NotBeNull().And.BeOfType(CommentMock.GetType());
            _service.Verify(p => p.GetAllCommentsAsync(), Times.Once());


        }

        [Fact]

        public async Task GetComments_ShouldReturnOkResponce_WhenData_isNotFound()
        {
            //arrange
            List<ShiftMgtDbContext.Entities.Comment> responce = null;
            _service.Setup(x => x.GetAllCommentsAsync()).ReturnsAsync(responce);

            //act 
            var Result = await _commentController.GetAllComments().ConfigureAwait(false);

            //assert

            Result.Should().NotBeNull();
            Result.Should().BeAssignableTo<NotFoundResult>();
            _service.Verify(x => x.GetAllCommentsAsync(), Times.Once());
        }
    }
}
