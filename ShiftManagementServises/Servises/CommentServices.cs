using ShiftMgtDbContext.Data;
using ShiftMgtDbContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftManagementServises.Servises
{
    public interface ICommentServices
    {
     List<Comment> GetComment(int commentid);
    }

    public  class CommentServices : ICommentServices
    {
        private readonly ShiftManagementDbContext _commnet;

        public CommentServices(ShiftManagementDbContext comment)
        {
            _commnet= comment;
        }
        public List<Comment> GetComment(int commentid)
        {
            return _commnet.Comments.Where(x => x.CommentID == commentid).ToList();
        }
    }
}
