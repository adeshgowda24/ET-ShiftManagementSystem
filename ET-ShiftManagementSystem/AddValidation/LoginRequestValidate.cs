using FluentValidation;

namespace ET_ShiftManagementSystem.AddValidation
{
    public class LoginRequestValidate : AbstractValidator<Models.LoginRequest>
    {
        public LoginRequestValidate()
        {
            RuleFor(x => x.username).NotEmpty();
            RuleFor(x => x.password).NotEmpty();

        }
    }
}
