using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcmeBackEnd.Models
{
    public class QuizModel
    {
        [Key]
        public int QuizId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string UniqueId { get; set; } = string.Empty;

        [ForeignKey("UserModel")]
        public int UserRefId { get; set; }

        public virtual UserModel? UserModel { get; set; }

        public virtual ICollection<FieldModel>? Fields { get; set; }

        public virtual ICollection<AnswerModel>? Answers { get; set; }
    }
}
