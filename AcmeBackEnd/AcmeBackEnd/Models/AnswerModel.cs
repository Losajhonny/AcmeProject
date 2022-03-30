using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcmeBackEnd.Models
{
    public class AnswerModel
    {
        [Key]
        public int AnswerId { get; set; }

        [ForeignKey("QuizModel")]
        public int? QuizRefId { get; set; }

        [ForeignKey("FieldModel")]
        public int? FieldRefId { get; set; }

        public virtual QuizModel? QuizModel { get; set; }
        public virtual FieldModel? FieldModel { get; set; }

        public string Result { get; set; } = string.Empty;
    }
}
