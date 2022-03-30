namespace AcmeBackEnd.DTO
{
    public class FieldDto
    {
        public int FieldId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Required { get; set; } = string.Empty;

        public string TypeField { get; set; } = string.Empty;

        public int QuizRefId { get; set; }
    }
}
