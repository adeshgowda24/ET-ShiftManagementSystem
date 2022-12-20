using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Servises.ProjectServises;
using ShiftMgtDbContext.Entities;
using ShiftMgtDbContext.Data;
using ET_ShiftManagementSystem.Models;
using ShiftManagementServises.Servises;

namespace ET_ShiftManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectServises _projectServices;
        private readonly IProjectDatailServises _projectDatailServises;
        private readonly IUserServices _userServices;
        private readonly IShiftServices _shiftServices;
        private readonly ICommentServices _commentServices;

        public ProjectController(IProjectServises projectServices, IProjectDatailServises projectDatailServises , IUserServices userServices, IShiftServices shiftServices, ICommentServices commentServices)
        {
            _projectServices = projectServices;
            _projectDatailServises = projectDatailServises;
            _userServices = userServices;
            _shiftServices = shiftServices;
            _commentServices = commentServices; 
        }
        [Route("{projectId}")]
        [HttpGet]
        public IActionResult GetProjectDetails(int projectId)
        {
            var projectDetailsData = new ProjectDetailsDTO();
            var projectdetail = _projectServices.GetProject(projectId);
            projectDetailsData.ProjectName= projectdetail.ProjectName;
            projectDetailsData.ClientName= projectdetail.ClientName;
            projectDetailsData.Description = projectdetail.Description;
            projectDetailsData.ProjectID = projectId;
            var projectData = _projectDatailServises.GetProjecDetails(projectId);


            var CommentDetail = _commentServices.GetComment(projectId);

            var ShiftDetail = _shiftServices.GetShiftDetails(projectId);
            
            foreach ( var item in projectData)
            {
                var userDetail = _userServices.GetUserDetails(item.UserID);
                var projectUser = new ProjectUser()
                {
                    UserId = item.UserID,
                    UserName = userDetail.UserName,
                    FullName = userDetail.FullName,
                    Email = userDetail.Email,

                };
                if (item.ShiftID.HasValue)
                {
                    var shiftDetails = _shiftServices.GetShiftDetails(item.ShiftID.Value);
                    if (shiftDetails != null) 
                    {
                        projectUser.ShiftID = item.ShiftID.Value;
                        projectUser.ShiftName = shiftDetails.ShiftName;

                    }
                };

                projectDetailsData.ProjectUsers.Add(projectUser);

                
            }
            foreach (var value in CommentDetail)
            {
                var Comment = _commentServices.GetComment(value.CommentID);
                var userComment = new CommentDetailes()
                {
                    CommentText = value.CommentText,
                    CreatedDate= value.CreatedDate,
                    EmployeeName= projectDetailsData.ProjectUsers.FirstOrDefault(a => a.UserId==value.UserID).UserName,
                    ShiftID= value.ShiftID,
                    UserID  = value.UserID,
                    Shift = projectDetailsData.ProjectUsers.FirstOrDefault(a => a.ShiftID == value.ShiftID).ShiftName
                };
                projectDetailsData.CommentDetiles.Add(userComment);
            }
                

            return Ok(projectDetailsData);
        }


        [HttpPost]
        public IActionResult AddProject(ProjectDto projectDto)
        {
            var Proj = new Project()
            {
                ProjectName = projectDto.ProjectName,
                Description = projectDto.Description,
                ClientName = projectDto.ClientName,
                CreatedBy = projectDto.CreatedBy,
                CreatedDate = DateTime.Now,
                ModifieBy = projectDto.ModifieBy,
                ModifieDate = DateTime.Now,
                IsActive = projectDto.IsActive
            };

            _projectServices.AddProject(Proj);    

            return Ok(Proj);
        }
      

        [HttpPut]
        public IActionResult UpdateProject(ProjectDto ProjectDto) 
        {
            var Project = new Project()
            {
                ProjectName = ProjectDto.ProjectName,
                Description = ProjectDto.Description,
                ClientName = ProjectDto.ClientName,
                CreatedBy = ProjectDto.CreatedBy,
                CreatedDate = DateTime.Now,
                ModifieBy = ProjectDto.ModifieBy,
                ModifieDate = DateTime.Now,
                IsActive = ProjectDto.IsActive
            };

            //_projectServices.UpdateProject(Project);

            return Ok(Project);
        }

        
    }
}
