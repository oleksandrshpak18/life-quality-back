using life_quality_back.Controllers.Authorization;
using life_quality_back.Data.Models;
using life_quality_back.Data.Repositories;

namespace life_quality_back.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();
                // remove all the previous data from the tables on startup
                context.Doctors.RemoveRange(context.Doctors);
                context.Diseases.RemoveRange(context.Diseases);
                context.TreatmentStrategies.RemoveRange(context.TreatmentStrategies);
                context.Patients.RemoveRange(context.Patients);
                context.Answers.RemoveRange(context.Answers);
                context.Questions.RemoveRange(context.Questions);
                context.Questionnaires.RemoveRange(context.Questionnaires);
                context.QuestionnaireTreatmentStrategy.RemoveRange(context.QuestionnaireTreatmentStrategy);
                context.Users.RemoveRange(context.Users);

                // add a template data to the tables
                
                context.Users.AddRange(
                    new User
                    {
                        Password = "7c6a180b36896a0a8c02787eeafb0e4c", // додаючи користувача згенеруйте MD-5 хеш онайлн та вставте тут 
                        Doctor = new Doctor                            // безпека користувачів понад усе!
                        {
                            FirstName = "Mike",
                            LastName = "Dolfino",
                            Email = "mike.dolfino@gmail.com",
                            Education = "Lviv National Medical University",
                            Gender = "Male",
                            Speciality = "Dentist"
                        }
                    },
                    new User
                    {
                        Password = "6cb75f652a9b52798eb6cf2201057c73",
                        Doctor = new Doctor
                        {
                            FirstName = "Mary",
                            LastName = "Alice",
                            Email = "mary.alice@gmail.com",
                            Education = "University of Fairview, Visteria-Lane",
                            Gender = "Female",
                            Speciality = "Therapist"
                        }
                    }
                );

                // don't forget to save the changes
                context.SaveChanges();

                context.Diseases.AddRange(
                    new Disease
                    {
                        DiseaseName = "Disease 1",
                        DiseaseDescription = "A robust description for disease 1."
                    },
                    new Disease
                    {
                        DiseaseName = "Disease 2",
                        DiseaseDescription = "A robust description for disease 2. And some additional info here."
                    }
                );
                context.SaveChanges();

                context.TreatmentStrategies.AddRange(
                    new TreatmentStrategy
                    {
                        TreatmentStrategyName = "Strategy 1",
                        TreatmentStrategyDescription = "A robust description for strategy 1.",
                        QuestionnaireTreatmentStrategy = new List<QuestionnaireTreatmentStrategy>
                        {
                            new QuestionnaireTreatmentStrategy
                            {
                                Questionnaire = new Questionnaire
                                {
                                    QuestionnaireName = "Questionnaire 1",
                                    Questions = new List<Question>
                                    {
                                        new Question
                                        {
                                            QuestionText = "Question 1 | Questionnaire 1",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Answer 1 | Question 1 | Questionnaire 1",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Answer 2 | Question 1 | Questionnaire 1",
                                                    AnswerValue = 2
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Question 2 | Questionnaire 1",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Answer 1 | Question 2 | Questionnaire 1",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Answer 2 | Question 2 | Questionnaire 1",
                                                    AnswerValue = 2
                                                }
                                            }
                                        }
                                    }
                                }
                            },
                            new QuestionnaireTreatmentStrategy
                            {
                                Questionnaire = new Questionnaire
                                {
                                    QuestionnaireName = "Questionnaire 2",
                                    Questions = new List<Question>
                                    {
                                        new Question
                                        {
                                            QuestionText = "Question 1 | Questionnaire 2",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Answer 1 | Question 1 | Questionnaire 2",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Answer 2 | Question 1 | Questionnaire 2",
                                                    AnswerValue = 2
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Question 2 | Questionnaire 2",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Answer 1 | Question 2 | Questionnaire 2",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Answer 2 | Question 2 | Questionnaire 2",
                                                    AnswerValue = 2
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    },
                    new TreatmentStrategy
                    {
                        TreatmentStrategyName = "Strategy 2",
                        TreatmentStrategyDescription = "Another treatment strategy description for strategy 2.",
                        QuestionnaireTreatmentStrategy = new List<QuestionnaireTreatmentStrategy>
                        {
                            new QuestionnaireTreatmentStrategy
                            {
                                Questionnaire = new Questionnaire
                                {
                                    QuestionnaireName = "Questionnaire 3",
                                    Questions = new List<Question>
                                    {
                                        new Question
                                        {
                                            QuestionText = "Question 1 | Questionnaire 3",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Answer 1 | Question 1 | Questionnaire 3",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Answer 2 | Question 1 | Questionnaire 3",
                                                    AnswerValue = 2
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Question 2 | Questionnaire 3",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Answer 1 | Question 2 | Questionnaire 3",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Answer 2 | Question 2 | Questionnaire 3",
                                                    AnswerValue = 2
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                     }
                );

                context.SaveChanges();

                context.Patients.AddRange(
                    new Patient
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        Anamnesis = "Some anamnesis information",
                        Email = "john.doe@example.com",
                        Gender = "Male",
                        BirthDate = new DateTime(1990, 1, 1),
                        RehabilitationStartDate = DateTime.Now,
                        TreatmentStrategyId = context.TreatmentStrategies
                                        .Where(x => x.TreatmentStrategyName == "Strategy 1") // replace later with the name of a real TreamentStrategy from TreatmentStrategies
                                        .Select(x => x.TreatmentStrategyId).First(),
                        DiseaseId = context.Diseases
                                        .Where(x => x.DiseaseName == "Disease 1") // replace later with the name of a real disease from Diseases
                                        .Select(x => x.DiseaseId).First(),
                        DoctorId = context.Doctors
                                        .Where(x => x.LastName == "Dolfino") // replace later with the name of a real doctor from Doctors
                                        .Select(x => x.DoctorId).First(),
                    },
                    new Patient
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        Anamnesis = "Some anamnesis information",
                        Email = "john.doe@example.com",
                        Gender = "Male",
                        BirthDate = new DateTime(1990, 1, 1),
                        RehabilitationStartDate = DateTime.Now,
                        TreatmentStrategyId = context.TreatmentStrategies
                                        .Where(x => x.TreatmentStrategyName == "Strategy 2") // replace later with the name of a real TreamentStrategy from TreatmentStrategies
                                        .Select(x => x.TreatmentStrategyId).First(),
                        DiseaseId = context.Diseases
                                        .Where(x => x.DiseaseName == "Disease 1") // replace later with the name of a real disease from Diseases
                                        .Select(x => x.DiseaseId).First(),
                        DoctorId = context.Doctors
                                        .Where(x => x.LastName == "Dolfino") // replace later with the name of a real doctor from Doctors
                                        .Select(x => x.DoctorId).First(),
                    },
                    new Patient
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        Anamnesis = "Some anamnesis information",
                        Email = "john.doe@example.com",
                        Gender = "Male",
                        BirthDate = new DateTime(1990, 1, 1),
                        RehabilitationStartDate = DateTime.Now,
                        TreatmentStrategyId = context.TreatmentStrategies
                                        .Where(x => x.TreatmentStrategyName == "Strategy 2") // replace later with the name of a real TreamentStrategy from TreatmentStrategies
                                        .Select(x => x.TreatmentStrategyId).First(),
                        DiseaseId = context.Diseases
                                        .Where(x => x.DiseaseName == "Disease 2") // replace later with the name of a real disease from Diseases
                                        .Select(x => x.DiseaseId).First(),
                        DoctorId = context.Doctors
                                        .Where(x => x.LastName == "Alice") // replace later with the name of a real doctor from Doctors
                                        .Select(x => x.DoctorId).First(),
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
