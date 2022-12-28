using AutoFixture;
using ET_ShiftManagementSystem.Controllers;
using Moq;
using Servises.ProjectServises;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET_ShiftmanagementSystem.Tests.Controller
{
    public  class ProjectControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IProjectServises> _servicemock;
        private readonly ProjectController _controller;

        public ProjectControllerTests()
        {
            _fixture = new Fixture();
            _servicemock= _fixture.Freeze<Mock<IProjectServises>>();
            //_controller = new ProjectController(_servicemock.Object);

        }
        [Fact]
        public void Test1()
        {

        }
    }
}
