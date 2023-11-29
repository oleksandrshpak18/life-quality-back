﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using life_quality_back.Data;

#nullable disable

namespace life_quality_back.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231129074205_Fix")]
    partial class Fix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("life_quality_back.Data.Models.Answer", b =>
                {
                    b.Property<int>("AnswerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AnswerId"), 1L, 1);

                    b.Property<string>("AnswerText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AnswerValue")
                        .HasColumnType("int");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("AnswerId");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("life_quality_back.Data.Models.Disease", b =>
                {
                    b.Property<int>("DiseaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiseaseId"), 1L, 1);

                    b.Property<string>("DiseaseDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiseaseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DiseaseId");

                    b.ToTable("Diseases");
                });

            modelBuilder.Entity("life_quality_back.Data.Models.Doctor", b =>
                {
                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DoctorId"), 1L, 1);

                    b.Property<string>("Education")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Speciality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeOfDoctor")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DoctorId");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("life_quality_back.Data.Models.Patient", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PatientId"), 1L, 1);

                    b.Property<string>("Anamnesis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DiseaseId")
                        .HasColumnType("int");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RehabilitationStartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TreatmentStrategyId")
                        .HasColumnType("int");

                    b.HasKey("PatientId");

                    b.HasIndex("DiseaseId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("TreatmentStrategyId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("life_quality_back.Data.Models.PatientAnswer", b =>
                {
                    b.Property<int>("PatientAnswerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PatientAnswerId"), 1L, 1);

                    b.Property<string>("AnswerText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AnswerValue")
                        .HasColumnType("int");

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ResultId")
                        .HasColumnType("int");

                    b.Property<int>("ResultsId")
                        .HasColumnType("int");

                    b.HasKey("PatientAnswerId");

                    b.HasIndex("ResultsId");

                    b.ToTable("PatientAnswers");
                });

            modelBuilder.Entity("life_quality_back.Data.Models.Question", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuestionId"), 1L, 1);

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionnaireId")
                        .HasColumnType("int");

                    b.HasKey("QuestionId");

                    b.HasIndex("QuestionnaireId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("life_quality_back.Data.Models.Questionnaire", b =>
                {
                    b.Property<int>("QuestionnaireId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuestionnaireId"), 1L, 1);

                    b.Property<string>("QuestionnaireName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("QuestionnaireId");

                    b.ToTable("Questionnaires");
                });

            modelBuilder.Entity("life_quality_back.Data.Models.QuestionnaireTreatmentStrategy", b =>
                {
                    b.Property<int>("QuestionnaireTreatmentStrategyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuestionnaireTreatmentStrategyId"), 1L, 1);

                    b.Property<int>("QuestionnaireId")
                        .HasColumnType("int");

                    b.Property<int>("TreatmentStrategyId")
                        .HasColumnType("int");

                    b.HasKey("QuestionnaireTreatmentStrategyId");

                    b.HasIndex("QuestionnaireId");

                    b.HasIndex("TreatmentStrategyId");

                    b.ToTable("QuestionnaireTreatmentStrategy");
                });

            modelBuilder.Entity("life_quality_back.Data.Models.Results", b =>
                {
                    b.Property<int>("ResultsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResultsId"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<int>("QuestionnaireId")
                        .HasColumnType("int");

                    b.Property<bool>("isSaved")
                        .HasColumnType("bit");

                    b.HasKey("ResultsId");

                    b.HasIndex("PatientId");

                    b.HasIndex("QuestionnaireId");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("life_quality_back.Data.Models.TreatmentStrategy", b =>
                {
                    b.Property<int>("TreatmentStrategyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TreatmentStrategyId"), 1L, 1);

                    b.Property<string>("TreatmentStrategyDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TreatmentStrategyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TreatmentStrategyId");

                    b.ToTable("TreatmentStrategies");
                });

            modelBuilder.Entity("life_quality_back.Data.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("DoctorId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("life_quality_back.Data.Models.Answer", b =>
                {
                    b.HasOne("life_quality_back.Data.Models.Question", null)
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("life_quality_back.Data.Models.Patient", b =>
                {
                    b.HasOne("life_quality_back.Data.Models.Disease", "Disease")
                        .WithMany()
                        .HasForeignKey("DiseaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("life_quality_back.Data.Models.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("life_quality_back.Data.Models.TreatmentStrategy", "TreatmentStrategy")
                        .WithMany()
                        .HasForeignKey("TreatmentStrategyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Disease");

                    b.Navigation("Doctor");

                    b.Navigation("TreatmentStrategy");
                });

            modelBuilder.Entity("life_quality_back.Data.Models.PatientAnswer", b =>
                {
                    b.HasOne("life_quality_back.Data.Models.Results", "Results")
                        .WithMany("PatientAnswers")
                        .HasForeignKey("ResultsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Results");
                });

            modelBuilder.Entity("life_quality_back.Data.Models.Question", b =>
                {
                    b.HasOne("life_quality_back.Data.Models.Questionnaire", null)
                        .WithMany("Questions")
                        .HasForeignKey("QuestionnaireId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("life_quality_back.Data.Models.QuestionnaireTreatmentStrategy", b =>
                {
                    b.HasOne("life_quality_back.Data.Models.Questionnaire", "Questionnaire")
                        .WithMany()
                        .HasForeignKey("QuestionnaireId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("life_quality_back.Data.Models.TreatmentStrategy", null)
                        .WithMany("QuestionnaireTreatmentStrategy")
                        .HasForeignKey("TreatmentStrategyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Questionnaire");
                });

            modelBuilder.Entity("life_quality_back.Data.Models.Results", b =>
                {
                    b.HasOne("life_quality_back.Data.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("life_quality_back.Data.Models.Questionnaire", "Questionnaire")
                        .WithMany()
                        .HasForeignKey("QuestionnaireId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");

                    b.Navigation("Questionnaire");
                });

            modelBuilder.Entity("life_quality_back.Data.Models.User", b =>
                {
                    b.HasOne("life_quality_back.Data.Models.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("life_quality_back.Data.Models.Question", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("life_quality_back.Data.Models.Questionnaire", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("life_quality_back.Data.Models.Results", b =>
                {
                    b.Navigation("PatientAnswers");
                });

            modelBuilder.Entity("life_quality_back.Data.Models.TreatmentStrategy", b =>
                {
                    b.Navigation("QuestionnaireTreatmentStrategy");
                });
#pragma warning restore 612, 618
        }
    }
}
