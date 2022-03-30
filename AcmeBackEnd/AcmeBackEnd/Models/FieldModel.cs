using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcmeBackEnd.Models
{
    public class FieldModel
    {
        [Key]
        public int FieldId { get; set; }
        
        public string Name { get; set; } = string.Empty;
        
        public string Title { get; set; } = string.Empty;

        public string Required { get; set; } = string.Empty;

        public string TypeField { get; set; } = string.Empty;

        [ForeignKey("QuizModel")]
        public int QuizRefId { get; set; }

        public virtual QuizModel? QuizModel { get; set; }

        public virtual ICollection<AnswerModel>? Answers { get; set; }
    }
}
