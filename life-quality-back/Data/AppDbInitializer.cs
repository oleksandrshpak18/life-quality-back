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
                        Login = "mike.dolfino@lq.com",
                        Password = "7c6a180b36896a0a8c02787eeafb0e4c", // додаючи користувача згенеруйте MD-5 хеш онайлн та вставте тут 
                        Doctor = new Doctor                            // безпека користувачів понад усе!
                        {
                            FirstName = "Mike",
                            LastName = "Dolfino",
                            Email = "mike.dolfino@lq.com",
                            Education = "Lviv National Medical University",
                            Gender = "Male",
                            Speciality = "Dentist"
                        }
                    },
                    new User
                    {
                        Login = "mary.alice@lq.com",
                        Password = "6cb75f652a9b52798eb6cf2201057c73",
                        Doctor = new Doctor
                        {
                            FirstName = "Mary",
                            LastName = "Alice",
                            Email = "mary.alice@lq.com",
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
                        DiseaseName = "Stroke",
                        DiseaseDescription = "Stroke (brain attack) is a medical condition in which poor blood flow to the brain causes cell death. There are two main types of stroke: ischemic, due to lack of blood flow, and hemorrhagic, due to bleeding. Both cause parts of the brain to stop functioning properly."
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
                                    QuestionnaireName = "National Institutes of Health stroke scale (NIHSS)",
                                    QuestionnaireDescription = "The NIHSS is an 11-item scale designed to assess neurologic outcomes and the degree of recovery for stroke patients. The scale evaluates the level consciousness, eyeball movements, field of vision, facial muscle functions, limb strength, sensory functions, coordination (ataxia), language (aphasia), speech (dysarthria) and unilateral spatial ignoring (neglect).",
                                    Questions = new List<Question>
                                    {
                                        new Question
                                        {
                                            QuestionText = "Level of consciousness",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Conscious, reacts quickly.",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Drowsy, but can be brought to consciousness by light stimuli, and then\r\ncarries out orders, answers, reacts.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Reacts only with reflex movements or vegetative manifestations or does not react at all.",
                                                    AnswerValue = 2
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Level of consciousness: questions (Ask the patient what month it is now and how old he is.)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Answered both questions correctly or there is a language barrier.",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Answered one question correctly.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Did not give any correct answer or cannot answer.",
                                                    AnswerValue = 2
                                                },
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Level of consciousness: commands (Ask the patient to close his eyes and make a fist)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Executed both commands correctly.",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Completed one command correctly.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Did not execute any of the commands.",
                                                    AnswerValue = 2
                                                },
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Eye movements",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Full range of eye movements.",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Partial paralysis of gaze or isolated paresis of the nerve.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Tonic abduction of the eyes or complete paralysis of the gaze, which persists during the test of the oculocephalic reflex.",
                                                    AnswerValue = 2
                                                },
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Fields of vision (Evaluate all fields of vision using simultaneous finger movements)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Fields of vision are preserved or old blindness.",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Asymmetric or partial hemianopsia.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Complete hemianopsia.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Bilateral hemianopsia (blindness, including cortical blindness) or coma.",
                                                    AnswerValue = 3
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Facial muscle weakness",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Normal facial expressions or sedation.",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild paresis (only smoothing of the nasolabial fold).",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Partial paresis (complete or almost complete paralysis of the lower facial muscles).",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Complete paresis (absence of facial expressions in the upper and lower parts of the face) or coma.",
                                                    AnswerValue = 3
                                                },
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Assessment of the motor function of the left hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "The hand is held in the initial position for 10 seconds, joint effusion or amputation.",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "The arm remains in the initial position (90°), but begins to move downward during the first 10 seconds.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "The patient cannot hold the hand in the initial position (90°) for 10 seconds, but there are certain efforts against gravity.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Рука одразу падає, немає спроб подолати силу тяжіння.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "No movement whatsoever.",
                                                    AnswerValue = 4
                                                },
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Assessment of the motor function of the right hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "The hand is held in the initial position for 10 seconds, joint effusion or amputation.",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "The arm remains in the initial position (90°), but begins to move downward during the first 10 seconds.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "The patient cannot hold the hand in the initial position (90°) for 10 seconds, but there are certain efforts against gravity.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Рука одразу падає, немає спроб подолати силу тяжіння.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "No movement whatsoever.",
                                                    AnswerValue = 4
                                                },
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Assessment of the motor function of the left leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "The leg is held at an angle of 30° for 5 seconds, joint effusion or amputation.",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "The leg lowers to an intermediate position during the first 5 seconds.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "The leg falls on the bed during the first 5 seconds, some effort against gravity.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "The leg immediately falls on the bed, there is no attempt to overcome the force of gravity.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "No movement whatsoever.",
                                                    AnswerValue = 4
                                                },
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Assessment of the motor function of the right leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "The leg is held at an angle of 30° for 5 seconds, joint effusion or amputation.",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "The leg lowers to an intermediate position during the first 5 seconds.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "The leg falls on the bed during the first 5 seconds, some effort against gravity.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "The leg immediately falls on the bed, there is no attempt to overcome the force of gravity.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "No movement whatsoever.",
                                                    AnswerValue = 4
                                                },
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Language: Naming ordinary things",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Norm.",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild to moderate aphasia, errors in naming objects, or paraphasia. Impaired speech and/or language comprehension.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe aphasia. Complete aphasia expressive (Broca) or receptive (Wernicke).",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Dumbness, complete aphasia or coma.",
                                                    AnswerValue = 3
                                                },
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Dysarthria",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Norm.",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild or moderate dysarthria; the patient \"smears\" some words, and sometimes it is difficult to understand him.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe dysarthria; the pronunciation is so distorted that the patient cannot be understood.",
                                                    AnswerValue = 2
                                                },
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Ataxia in the limbs",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None (no limb movements), impossible to assess.",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Ataxia is present in one limb.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Ataxia is present in two limbs.",
                                                    AnswerValue = 2
                                                },
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Sensitivity: injections with a disposable needle. In case of impaired consciousness, give points only if there is a grimace or asymmetric manifestations.",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Norm; sedation or amputation.",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild or moderate loss of sensitivity; the patient feels the touch as less sharp or dull; but feels when it is touched.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe or complete loss of sensitivity; the patient does not feel touch.",
                                                    AnswerValue = 2
                                                },
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Neglect (lack of attention)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "There are no violations.",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Lack of attention to visual, tactile, auditory stimuli on one side.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe lack of attention or exclusion (selective perception) of stimuli of more than one modality.",
                                                    AnswerValue = 2
                                                },
                                            }
                                        },
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
                        Email = "john.doe1@example.com",
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
                        Email = "john.doe2@example.com",
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
                        Email = "john.doe3@example.com",
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

                // перед додаванням результатів витягнемо собі допоміжну інформацію, щоб менше дублювати
                context.Results.AddRange(
                    new Models.Results
                    {
                        Date = new DateTime(2023, 11, 18),
                        isSaved = false,
                        PatientId = context.Patients
                            .Where(x => x.Email == "john.doe1@example.com")
                            .Select(x => x.PatientId).First(),
                        QuestionnaireId = context.Questionnaires
                            .Where(x => x.QuestionnaireName == "Questionnaire 1")
                            .Select(x => x.QuestionnaireId).First(),
                        ResultsPatientAnswers = new List<ResultsPatientAnswer>
                        {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Question 1 | Questionnaire 1",
                                    AnswerText = "Answer 2 | Question 1 | Questionnaire 1",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Question 2 | Questionnaire 1",
                                    AnswerText = "Answer 1 | Question 1 | Questionnaire 1",
                                    AnswerValue = 1
                                }
                            }
                        }
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
