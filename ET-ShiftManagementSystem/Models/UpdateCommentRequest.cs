namespace ET_ShiftManagementSystem.Models
{
    public class UpdateCommentRequest
    {
        public int CommentID { get; set; }
        public string CommentText { get; set; }

        public bool Shared { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
