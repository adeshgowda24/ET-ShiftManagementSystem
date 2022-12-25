using FluentValidation;

namespace ET_ShiftManagementSystem.AddValidation
{
    public class UpdateCommentValidator : AbstractValidator<Models.UpdateCommentRequest>
    {
        public UpdateCommentValidator()
        {
            RuleFor(D => D.CreatedDate).NotEmpty();
            RuleFor(D => D.CommentID).NotEmpty();
            RuleFor(D => D.CommentText).NotEmpty();
            RuleFor(D => D.Shared).NotEmpty();

        }
    }
}
