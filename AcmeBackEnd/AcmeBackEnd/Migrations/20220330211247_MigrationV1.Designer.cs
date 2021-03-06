// <auto-generated />
using System;
using AcmeBackEnd.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AcmeBackEnd.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220330211247_MigrationV1")]
    partial class MigrationV1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AcmeBackEnd.Models.AnswerModel", b =>
                {
                    b.Property<int>("AnswerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AnswerId"), 1L, 1);

                    b.Property<int?>("FieldRefId")
                        .HasColumnType("int");

                    b.Property<int?>("QuizRefId")
                        .HasColumnType("int");

                    b.Property<string>("Result")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AnswerId");

                    b.HasIndex("FieldRefId");

                    b.HasIndex("QuizRefId");

                    b.ToTable("AnswerModel");
                });

            modelBuilder.Entity("AcmeBackEnd.Models.FieldModel", b =>
                {
                    b.Property<int>("FieldId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FieldId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuizRefId")
                        .HasColumnType("int");

                    b.Property<string>("Required")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeField")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FieldId");

                    b.HasIndex("QuizRefId");

                    b.ToTable("FieldModel");
                });

            modelBuilder.Entity("AcmeBackEnd.Models.QuizModel", b =>
                {
                    b.Property<int>("QuizId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuizId"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UniqueId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserRefId")
                        .HasColumnType("int");

                    b.HasKey("QuizId");

                    b.HasIndex("UserRefId");

                    b.ToTable("QuizModel");
                });

            modelBuilder.Entity("AcmeBackEnd.Models.UserModel", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("UserModel");
                });

            modelBuilder.Entity("AcmeBackEnd.Models.AnswerModel", b =>
                {
                    b.HasOne("AcmeBackEnd.Models.FieldModel", "FieldModel")
                        .WithMany("Answers")
                        .HasForeignKey("FieldRefId");

                    b.HasOne("AcmeBackEnd.Models.QuizModel", "QuizModel")
                        .WithMany("Answers")
                        .HasForeignKey("QuizRefId");

                    b.Navigation("FieldModel");

                    b.Navigation("QuizModel");
                });

            modelBuilder.Entity("AcmeBackEnd.Models.FieldModel", b =>
                {
                    b.HasOne("AcmeBackEnd.Models.QuizModel", "QuizModel")
                        .WithMany("Fields")
                        .HasForeignKey("QuizRefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QuizModel");
                });

            modelBuilder.Entity("AcmeBackEnd.Models.QuizModel", b =>
                {
                    b.HasOne("AcmeBackEnd.Models.UserModel", "UserModel")
                        .WithMany("Quizzes")
                        .HasForeignKey("UserRefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserModel");
                });

            modelBuilder.Entity("AcmeBackEnd.Models.FieldModel", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("AcmeBackEnd.Models.QuizModel", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("Fields");
                });

            modelBuilder.Entity("AcmeBackEnd.Models.UserModel", b =>
                {
                    b.Navigation("Quizzes");
                });
#pragma warning restore 612, 618
        }
    }
}
