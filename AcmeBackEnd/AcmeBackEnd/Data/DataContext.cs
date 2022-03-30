using AcmeBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace AcmeBackEnd.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<QuizModel> QuizModel { get; set; }
        public DbSet<FieldModel> FieldModel { get; set; }
        public DbSet<UserModel> UserModel { get; set; }
        public DbSet<AnswerModel> AnswerModel { get; set; }
    }
}
