using CurriculumWebAPI.Domain.Models;

namespace CurriculumWebAPI.App.InputModels
{
    public class UserInputModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}