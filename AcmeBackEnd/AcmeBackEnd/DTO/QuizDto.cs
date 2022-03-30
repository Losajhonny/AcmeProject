namespace AcmeBackEnd.DTO
{
    public class QuizDto
    {
        public int QuizId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string UniqueId { get; set; } = string.Empty;

        public int UserRefId { get; set; }
    }
}
