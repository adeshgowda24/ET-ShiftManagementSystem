using FluentValidation;

namespace ET_ShiftManagementSystem.AddValidation
{
    public class updateShiftRequestValidator : AbstractValidator<Models.UpdateShiftRequest>
    {
        public updateShiftRequestValidator()
        {
                RuleFor( s => s.ShiftId ).NotEmpty();
                RuleFor( s => s.StartTime ).NotEmpty();
                RuleFor( s => s.EndTime ).NotEmpty();
                RuleFor( s => s.ShiftName ).NotEmpty();

        }
    }
}
