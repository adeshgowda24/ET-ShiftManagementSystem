namespace ET_ShiftManagementSystem.Models
{
    public class Comment
    {
        public int CommentID { get; set; }

        public string CommentText { get; set; }

        public int ShiftID { get; set; }

        public int UserID { get; set; }

        public bool Shared { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
