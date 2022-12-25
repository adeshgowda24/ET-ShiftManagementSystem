using AutoMapper;
using ET_ShiftManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using ShiftManagementServises.Servises;
using ShiftMgtDbContext.Data;
using ShiftMgtDbContext.Entities;

namespace ET_ShiftManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentServices commentServices;
        private readonly IMapper mapper;

        public CommentController(ICommentServices commentServices , IMapper mapper)
        {
            this.commentServices = commentServices;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCommentDetails(int id)
        {
            var commnet = await commentServices.GetComment(id);

            if (commnet == null)
            {
                return NotFound();
            }

            var CommentDTO = mapper.Map<Models.CommentDetailes>(commnet);

            return Ok(CommentDTO);
        }

        [HttpGet]
        [Route("/allComment")]
        public async Task<IActionResult> GetAllComments()
        {
            var comment = await commentServices.GetAllCommentsAsync();

            var CommentDTO = mapper.Map<List<Models.CommentDetailes>>(comment);

            return Ok(CommentDTO);
        }

        [HttpPost]

        public IActionResult AddComment(ShiftMgtDbContext.Entities.Comment comment)
        {
            var com = new ShiftMgtDbContext.Entities.Comment()
            {
                CommentText = comment.CommentText,
                Shared = comment.Shared,
                CreatedDate = DateTime.Now,

            };
            commentServices.AddComment(comment);

            return Ok(com);

            //var AddComment = new ShiftMgtDbContext.Entities.Comment()
            //{
            //    CommentText = comment.CommentText,
            //    Shared = comment.Shared,
            //    ShiftID = comment.ShiftID,
            //    UserID = comment.UserID
            //};

            //comment = await (ET_ShiftManagementSystem.Models.AddComments)commentServices.AddComment(AddComment);

            //var CommentDTO = new Models.Comment()
            //{
            //    CommentText = comment.CommentText,
            //    ShiftID = comment.ShiftID,
            //    CreatedDate = DateTime.Now,
            //    Shared = comment.Shared,
            //    UserID = comment.UserID
            //};

            //return Ok(CommentDTO);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var delete = await commentServices.DeleteCommentAsync(id);

            if (delete == null)
            {
                return NotFound();
            }

            var DeleteDTO = new Models.Comment()
            {

                CommentText = delete.CommentText,
                CreatedDate = DateTime.Now,
                ShiftID = delete.ShiftID,
                CommentID = delete.CommentID,
                Shared = delete.Shared,
                UserID = delete.UserID,


            };

            return Ok(DeleteDTO);
        }

        [HttpPut]

        public async Task<IActionResult> UpdateComment(int id ,Models.UpdateCommentRequest comment)
        {
            var com = new ShiftMgtDbContext.Entities.Comment()
            {
                CommentID= comment.CommentID,
                CommentText = comment.CommentText,
                Shared = comment.Shared,
                CreatedDate= DateTime.Now,
            };

            await commentServices.UpdateCommentAsync(id, com);

            return Ok(com);
        }
    }
}
