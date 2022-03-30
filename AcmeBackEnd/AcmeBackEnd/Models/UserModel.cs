using System.ComponentModel.DataAnnotations;

namespace AcmeBackEnd.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public virtual ICollection<QuizModel>? Quizzes { get; set; }
    }
}
