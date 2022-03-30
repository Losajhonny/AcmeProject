namespace AcmeBackEnd.DTO
{
    public class AnswerDto
    {
        public int AnswerId { get; set; }

        public int? QuizRefId { get; set; }

        public int? FieldRefId { get; set; }

        public string Result { get; set; } = string.Empty;
    }
}
