using life_quality_back.Data.Models;
using life_quality_back.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using life_quality_back.Controllers.Authorization;
using life_quality_back.Data.Repositories;
using life_quality_back.Data.ViewModels;
using life_quality_back.Data.Filtering;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;
using FluentAssertions;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;

namespace life_quality_back.Test
{
    public class ResultsRepositoryTests
    {
        private AppDbContext GetDataBaseContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new AppDbContext(options);
            databaseContext.Database.EnsureDeleted();
            databaseContext.Database.EnsureCreated();

            databaseContext.Users.Add(
                new User
                {
                    Login = "user1@lq.com",
                    // testpassword1
                    Password = "b7e055c6165da55c3e12c49ae5207455",
                    Doctor = new Doctor
                    {
                        FirstName = "TestUser1",
                        LastName = "TestUser1",
                        Email = "user1@lq.com",
                        Education = "TestUniversity",
                        Gender = "Female",
                        Speciality = "TestSpeciality"
                    }
                }
            );
            databaseContext.Users.Add(
                new User
                {
                    Login = "user2@lq.com",
                    // testpassword2
                    Password = "c4d8a57e2ca5dc5d71d2cf3dbbbbaabe",
                    Doctor = new Doctor
                    {
                        FirstName = "TestUser2",
                        LastName = "TestUser2",
                        Email = "user2@lq.com",
                        Education = "TestUniversity",
                        Gender = "Male",
                        Speciality = "TestSpeciality"
                    }
                }
            );
            databaseContext.Users.Add(
                new User
                {
                    Login = "user3@lq.com",
                    // testpassword3
                    Password = "cb310eace3f52787ab5fc2cddf73bd2d",
                    Doctor = new Doctor
                    {
                        FirstName = "TestUser3",
                        LastName = "TestUser3",
                        Email = "user3@lq.com",
                        Education = "TestUniversity",
                        Gender = "Male",
                        Speciality = "TestSpeciality"
                    }
                }
            );
            databaseContext.SaveChanges();


            databaseContext.Diseases.Add(
                    new Disease
                    {
                        DiseaseName = "Stroke",
                        DiseaseDescription = "Stroke (brain attack) is a medical condition in which poor blood flow to the brain causes cell death. There are two main types of stroke: ischemic, due to lack of blood flow, and hemorrhagic, due to bleeding. Both cause parts of the brain to stop functioning properly."
                    }
                );
            databaseContext.SaveChanges();


            databaseContext.TreatmentStrategies.Add(
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
                                                    AnswerText = "Drowsy, but can be brought to consciousness by light stimuli, and thencarries out orders, answers, reacts.",
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
                            }
                        }
                    }
                    );
            

            databaseContext.Patients.AddRange(
                    new Patient
                    {
                        FirstName = "TestPatient1",
                        LastName = "TestPatient1",
                        Anamnesis = "TestAnamnesis1",
                        Email = "test.patient1@lq.com",
                        Gender = "Male",
                        BirthDate = new DateTime(1990, 1, 1),
                        RehabilitationStartDate = new DateTime(2023, 8, 10),
                        TreatmentStrategyId = 1,
                        DiseaseId = 1,
                        DoctorId = 1,
                    },
                    new Patient
                    {
                        FirstName = "TestPatient2",
                        LastName = "TestPatient2",
                        Anamnesis = "TestAnamnesis2",
                        Email = "test.patient2@lq.com",
                        Gender = "Female",
                        BirthDate = new DateTime(1990, 1, 1),
                        RehabilitationStartDate = new DateTime(2023, 8, 10),
                        TreatmentStrategyId = 1,
                        DiseaseId = 1,
                        DoctorId = 2,
                    },
                    new Patient
                    {
                        FirstName = "TestPatient3",
                        LastName = "TestPatient3",
                        Anamnesis = "TestAnamnesis3",
                        Email = "test.patient3@lq.com",
                        Gender = "Male",
                        BirthDate = new DateTime(1990, 1, 1),
                        RehabilitationStartDate = new DateTime(2023, 8, 10),
                        TreatmentStrategyId = 1,
                        DiseaseId = 1,
                        DoctorId = 3,
                    }
                );
            databaseContext.SaveChanges();

            databaseContext.Results.AddRange(
                // patient 1 results
                new Results
                {
                    Date = new DateTime(2022, 1, 10),
                    isSaved = true,
                    PatientId = 1,
                    QuestionnaireId = 1,
                    ResultsPatientAnswers = new List<ResultsPatientAnswer>
                        {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness",
                                    AnswerText = "Reacts only with reflex movements or vegetative manifestations or does not react at all.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness: questions (Ask the patient what month it is now and how old he is.)",
                                    AnswerText = "Answered one question correctly.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness: commands (Ask the patient to close his eyes and make a fist)",
                                    AnswerText = "Completed one command correctly.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Eye movements",
                                    AnswerText = "Tonic abduction of the eyes or complete paralysis of the gaze, which persists during the test of the oculocephalic reflex.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Fields of vision (Evaluate all fields of vision using simultaneous finger movements)",
                                    AnswerText = "Bilateral hemianopsia (blindness, including cortical blindness) or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Facial muscle weakness",
                                    AnswerText = "Complete paresis (absence of facial expressions in the upper and lower parts of the face) or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the left hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                    AnswerText = "The hand immediately falls, there is no attempt to overcome the force of gravity.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the right hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                    AnswerText = "No movement whatsoever.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the left leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                    AnswerText = "The leg immediately falls on the bed, there is no attempt to overcome the force of gravity.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the right leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                    AnswerText = "No movement whatsoever.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Language: Naming ordinary things",
                                    AnswerText = "Dumbness, complete aphasia or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Dysarthria",
                                    AnswerText = "Mild or moderate dysarthria; the patient \"smears\" some words, and sometimes it is difficult to understand him.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Ataxia in the limbs",
                                    AnswerText = "Ataxia is present in two limbs.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Sensitivity: injections with a disposable needle. In case of impaired consciousness, give points only if there is a grimace or asymmetric manifestations.",
                                    AnswerText = "Mild or moderate loss of sensitivity; the patient feels the touch as less sharp or dull; but feels when it is touched.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Neglect (lack of attention)",
                                    AnswerText = "Severe lack of attention or exclusion (selective perception) of stimuli of more than one modality.",
                                    AnswerValue = 2
                                }
                            }
                        }
                },
                new Results
                {
                    Date = new DateTime(2022, 2, 10),
                    isSaved = false,
                    PatientId = 1,
                    QuestionnaireId = 1,
                    ResultsPatientAnswers = new List<ResultsPatientAnswer>
                        {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness",
                                    AnswerText = "Reacts only with reflex movements or vegetative manifestations or does not react at all.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness: questions (Ask the patient what month it is now and how old he is.)",
                                    AnswerText = "Answered one question correctly.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness: commands (Ask the patient to close his eyes and make a fist)",
                                    AnswerText = "Completed one command correctly.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Eye movements",
                                    AnswerText = "Tonic abduction of the eyes or complete paralysis of the gaze, which persists during the test of the oculocephalic reflex.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Fields of vision (Evaluate all fields of vision using simultaneous finger movements)",
                                    AnswerText = "Bilateral hemianopsia (blindness, including cortical blindness) or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Facial muscle weakness",
                                    AnswerText = "Complete paresis (absence of facial expressions in the upper and lower parts of the face) or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the left hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                    AnswerText = "The hand immediately falls, there is no attempt to overcome the force of gravity.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the right hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                    AnswerText = "No movement whatsoever.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the left leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                    AnswerText = "The leg immediately falls on the bed, there is no attempt to overcome the force of gravity.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the right leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                    AnswerText = "No movement whatsoever.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Language: Naming ordinary things",
                                    AnswerText = "Dumbness, complete aphasia or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Dysarthria",
                                    AnswerText = "Mild or moderate dysarthria; the patient \"smears\" some words, and sometimes it is difficult to understand him.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Ataxia in the limbs",
                                    AnswerText = "Ataxia is present in two limbs.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Sensitivity: injections with a disposable needle. In case of impaired consciousness, give points only if there is a grimace or asymmetric manifestations.",
                                    AnswerText = "Mild or moderate loss of sensitivity; the patient feels the touch as less sharp or dull; but feels when it is touched.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Neglect (lack of attention)",
                                    AnswerText = "Severe lack of attention or exclusion (selective perception) of stimuli of more than one modality.",
                                    AnswerValue = 2
                                }
                            }
                        }
                },
                new Results
                {
                    Date = new DateTime(2022, 3, 10),
                    isSaved = true,
                    PatientId = 1,
                    QuestionnaireId = 1,
                    ResultsPatientAnswers = new List<ResultsPatientAnswer>
                        {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness",
                                    AnswerText = "Reacts only with reflex movements or vegetative manifestations or does not react at all.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness: questions (Ask the patient what month it is now and how old he is.)",
                                    AnswerText = "Answered one question correctly.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness: commands (Ask the patient to close his eyes and make a fist)",
                                    AnswerText = "Completed one command correctly.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Eye movements",
                                    AnswerText = "Tonic abduction of the eyes or complete paralysis of the gaze, which persists during the test of the oculocephalic reflex.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Fields of vision (Evaluate all fields of vision using simultaneous finger movements)",
                                    AnswerText = "Bilateral hemianopsia (blindness, including cortical blindness) or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Facial muscle weakness",
                                    AnswerText = "Complete paresis (absence of facial expressions in the upper and lower parts of the face) or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the left hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                    AnswerText = "The hand immediately falls, there is no attempt to overcome the force of gravity.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the right hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                    AnswerText = "No movement whatsoever.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the left leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                    AnswerText = "The leg immediately falls on the bed, there is no attempt to overcome the force of gravity.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the right leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                    AnswerText = "No movement whatsoever.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Language: Naming ordinary things",
                                    AnswerText = "Dumbness, complete aphasia or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Dysarthria",
                                    AnswerText = "Mild or moderate dysarthria; the patient \"smears\" some words, and sometimes it is difficult to understand him.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Ataxia in the limbs",
                                    AnswerText = "Ataxia is present in two limbs.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Sensitivity: injections with a disposable needle. In case of impaired consciousness, give points only if there is a grimace or asymmetric manifestations.",
                                    AnswerText = "Mild or moderate loss of sensitivity; the patient feels the touch as less sharp or dull; but feels when it is touched.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Neglect (lack of attention)",
                                    AnswerText = "Severe lack of attention or exclusion (selective perception) of stimuli of more than one modality.",
                                    AnswerValue = 2
                                }
                            }
                        }
                },
                new Results
                {
                    Date = new DateTime(2022, 4, 12),
                    isSaved = false,
                    PatientId = 1,
                    QuestionnaireId = 1,
                    ResultsPatientAnswers = new List<ResultsPatientAnswer>
                        {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness",
                                    AnswerText = "Reacts only with reflex movements or vegetative manifestations or does not react at all.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness: questions (Ask the patient what month it is now and how old he is.)",
                                    AnswerText = "Answered one question correctly.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness: commands (Ask the patient to close his eyes and make a fist)",
                                    AnswerText = "Completed one command correctly.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Eye movements",
                                    AnswerText = "Tonic abduction of the eyes or complete paralysis of the gaze, which persists during the test of the oculocephalic reflex.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Fields of vision (Evaluate all fields of vision using simultaneous finger movements)",
                                    AnswerText = "Bilateral hemianopsia (blindness, including cortical blindness) or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Facial muscle weakness",
                                    AnswerText = "Complete paresis (absence of facial expressions in the upper and lower parts of the face) or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the left hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                    AnswerText = "The hand immediately falls, there is no attempt to overcome the force of gravity.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the right hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                    AnswerText = "No movement whatsoever.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the left leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                    AnswerText = "The leg immediately falls on the bed, there is no attempt to overcome the force of gravity.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the right leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                    AnswerText = "No movement whatsoever.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Language: Naming ordinary things",
                                    AnswerText = "Dumbness, complete aphasia or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Dysarthria",
                                    AnswerText = "Mild or moderate dysarthria; the patient \"smears\" some words, and sometimes it is difficult to understand him.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Ataxia in the limbs",
                                    AnswerText = "Ataxia is present in two limbs.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Sensitivity: injections with a disposable needle. In case of impaired consciousness, give points only if there is a grimace or asymmetric manifestations.",
                                    AnswerText = "Mild or moderate loss of sensitivity; the patient feels the touch as less sharp or dull; but feels when it is touched.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Neglect (lack of attention)",
                                    AnswerText = "Severe lack of attention or exclusion (selective perception) of stimuli of more than one modality.",
                                    AnswerValue = 2
                                }
                            }
                        }
                },
                // patient 2 results
                new Results
                {
                    Date = new DateTime(2022, 1, 11),
                    isSaved = true,
                    PatientId = 2,
                    QuestionnaireId = 1,
                    ResultsPatientAnswers = new List<ResultsPatientAnswer>
                        {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness",
                                    AnswerText = "Reacts only with reflex movements or vegetative manifestations or does not react at all.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness: questions (Ask the patient what month it is now and how old he is.)",
                                    AnswerText = "Answered one question correctly.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness: commands (Ask the patient to close his eyes and make a fist)",
                                    AnswerText = "Completed one command correctly.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Eye movements",
                                    AnswerText = "Tonic abduction of the eyes or complete paralysis of the gaze, which persists during the test of the oculocephalic reflex.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Fields of vision (Evaluate all fields of vision using simultaneous finger movements)",
                                    AnswerText = "Bilateral hemianopsia (blindness, including cortical blindness) or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Facial muscle weakness",
                                    AnswerText = "Complete paresis (absence of facial expressions in the upper and lower parts of the face) or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the left hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                    AnswerText = "The hand immediately falls, there is no attempt to overcome the force of gravity.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the right hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                    AnswerText = "No movement whatsoever.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the left leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                    AnswerText = "The leg immediately falls on the bed, there is no attempt to overcome the force of gravity.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the right leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                    AnswerText = "No movement whatsoever.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Language: Naming ordinary things",
                                    AnswerText = "Dumbness, complete aphasia or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Dysarthria",
                                    AnswerText = "Mild or moderate dysarthria; the patient \"smears\" some words, and sometimes it is difficult to understand him.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Ataxia in the limbs",
                                    AnswerText = "Ataxia is present in two limbs.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Sensitivity: injections with a disposable needle. In case of impaired consciousness, give points only if there is a grimace or asymmetric manifestations.",
                                    AnswerText = "Mild or moderate loss of sensitivity; the patient feels the touch as less sharp or dull; but feels when it is touched.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Neglect (lack of attention)",
                                    AnswerText = "Severe lack of attention or exclusion (selective perception) of stimuli of more than one modality.",
                                    AnswerValue = 2
                                }
                            }
                        }
                },
                new Results
                {
                    Date = new DateTime(2022, 2, 11),
                    isSaved = false,
                    PatientId = 2,
                    QuestionnaireId = 1,
                    ResultsPatientAnswers = new List<ResultsPatientAnswer>
                        {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness",
                                    AnswerText = "Reacts only with reflex movements or vegetative manifestations or does not react at all.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness: questions (Ask the patient what month it is now and how old he is.)",
                                    AnswerText = "Answered one question correctly.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness: commands (Ask the patient to close his eyes and make a fist)",
                                    AnswerText = "Completed one command correctly.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Eye movements",
                                    AnswerText = "Tonic abduction of the eyes or complete paralysis of the gaze, which persists during the test of the oculocephalic reflex.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Fields of vision (Evaluate all fields of vision using simultaneous finger movements)",
                                    AnswerText = "Bilateral hemianopsia (blindness, including cortical blindness) or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Facial muscle weakness",
                                    AnswerText = "Complete paresis (absence of facial expressions in the upper and lower parts of the face) or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the left hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                    AnswerText = "The hand immediately falls, there is no attempt to overcome the force of gravity.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the right hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                    AnswerText = "No movement whatsoever.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the left leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                    AnswerText = "The leg immediately falls on the bed, there is no attempt to overcome the force of gravity.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the right leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                    AnswerText = "No movement whatsoever.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Language: Naming ordinary things",
                                    AnswerText = "Dumbness, complete aphasia or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Dysarthria",
                                    AnswerText = "Mild or moderate dysarthria; the patient \"smears\" some words, and sometimes it is difficult to understand him.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Ataxia in the limbs",
                                    AnswerText = "Ataxia is present in two limbs.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Sensitivity: injections with a disposable needle. In case of impaired consciousness, give points only if there is a grimace or asymmetric manifestations.",
                                    AnswerText = "Mild or moderate loss of sensitivity; the patient feels the touch as less sharp or dull; but feels when it is touched.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Neglect (lack of attention)",
                                    AnswerText = "Severe lack of attention or exclusion (selective perception) of stimuli of more than one modality.",
                                    AnswerValue = 2
                                }
                            }
                        }
                },
                new Results
                {
                    Date = new DateTime(2022, 3, 13),
                    isSaved = true,
                    PatientId = 2,
                    QuestionnaireId = 1,
                    ResultsPatientAnswers = new List<ResultsPatientAnswer>
                        {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness",
                                    AnswerText = "Reacts only with reflex movements or vegetative manifestations or does not react at all.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness: questions (Ask the patient what month it is now and how old he is.)",
                                    AnswerText = "Answered one question correctly.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness: commands (Ask the patient to close his eyes and make a fist)",
                                    AnswerText = "Completed one command correctly.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Eye movements",
                                    AnswerText = "Tonic abduction of the eyes or complete paralysis of the gaze, which persists during the test of the oculocephalic reflex.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Fields of vision (Evaluate all fields of vision using simultaneous finger movements)",
                                    AnswerText = "Bilateral hemianopsia (blindness, including cortical blindness) or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Facial muscle weakness",
                                    AnswerText = "Complete paresis (absence of facial expressions in the upper and lower parts of the face) or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the left hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                    AnswerText = "The hand immediately falls, there is no attempt to overcome the force of gravity.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the right hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                    AnswerText = "No movement whatsoever.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the left leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                    AnswerText = "The leg immediately falls on the bed, there is no attempt to overcome the force of gravity.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the right leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                    AnswerText = "No movement whatsoever.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Language: Naming ordinary things",
                                    AnswerText = "Dumbness, complete aphasia or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Dysarthria",
                                    AnswerText = "Mild or moderate dysarthria; the patient \"smears\" some words, and sometimes it is difficult to understand him.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Ataxia in the limbs",
                                    AnswerText = "Ataxia is present in two limbs.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Sensitivity: injections with a disposable needle. In case of impaired consciousness, give points only if there is a grimace or asymmetric manifestations.",
                                    AnswerText = "Mild or moderate loss of sensitivity; the patient feels the touch as less sharp or dull; but feels when it is touched.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Neglect (lack of attention)",
                                    AnswerText = "Severe lack of attention or exclusion (selective perception) of stimuli of more than one modality.",
                                    AnswerValue = 2
                                }
                            }
                        }
                },
                new Results
                {
                    Date = new DateTime(2022, 4, 12),
                    isSaved = false,
                    PatientId = 2,
                    QuestionnaireId = 1,
                    ResultsPatientAnswers = new List<ResultsPatientAnswer>
                        {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness",
                                    AnswerText = "Reacts only with reflex movements or vegetative manifestations or does not react at all.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness: questions (Ask the patient what month it is now and how old he is.)",
                                    AnswerText = "Answered one question correctly.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness: commands (Ask the patient to close his eyes and make a fist)",
                                    AnswerText = "Completed one command correctly.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Eye movements",
                                    AnswerText = "Tonic abduction of the eyes or complete paralysis of the gaze, which persists during the test of the oculocephalic reflex.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Fields of vision (Evaluate all fields of vision using simultaneous finger movements)",
                                    AnswerText = "Bilateral hemianopsia (blindness, including cortical blindness) or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Facial muscle weakness",
                                    AnswerText = "Complete paresis (absence of facial expressions in the upper and lower parts of the face) or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the left hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                    AnswerText = "The hand immediately falls, there is no attempt to overcome the force of gravity.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the right hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                    AnswerText = "No movement whatsoever.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the left leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                    AnswerText = "The leg immediately falls on the bed, there is no attempt to overcome the force of gravity.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the right leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                    AnswerText = "No movement whatsoever.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Language: Naming ordinary things",
                                    AnswerText = "Dumbness, complete aphasia or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Dysarthria",
                                    AnswerText = "Mild or moderate dysarthria; the patient \"smears\" some words, and sometimes it is difficult to understand him.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Ataxia in the limbs",
                                    AnswerText = "Ataxia is present in two limbs.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Sensitivity: injections with a disposable needle. In case of impaired consciousness, give points only if there is a grimace or asymmetric manifestations.",
                                    AnswerText = "Mild or moderate loss of sensitivity; the patient feels the touch as less sharp or dull; but feels when it is touched.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Neglect (lack of attention)",
                                    AnswerText = "Severe lack of attention or exclusion (selective perception) of stimuli of more than one modality.",
                                    AnswerValue = 2
                                }
                            }
                        }
                },
                // patient 3 results
                new Results
                {
                    Date = new DateTime(2022, 1, 11),
                    isSaved = true,
                    PatientId = 3,
                    QuestionnaireId = 1,
                    ResultsPatientAnswers = new List<ResultsPatientAnswer>
                        {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness",
                                    AnswerText = "Reacts only with reflex movements or vegetative manifestations or does not react at all.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness: questions (Ask the patient what month it is now and how old he is.)",
                                    AnswerText = "Answered one question correctly.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness: commands (Ask the patient to close his eyes and make a fist)",
                                    AnswerText = "Completed one command correctly.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Eye movements",
                                    AnswerText = "Tonic abduction of the eyes or complete paralysis of the gaze, which persists during the test of the oculocephalic reflex.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Fields of vision (Evaluate all fields of vision using simultaneous finger movements)",
                                    AnswerText = "Bilateral hemianopsia (blindness, including cortical blindness) or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Facial muscle weakness",
                                    AnswerText = "Complete paresis (absence of facial expressions in the upper and lower parts of the face) or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the left hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                    AnswerText = "The hand immediately falls, there is no attempt to overcome the force of gravity.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the right hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                    AnswerText = "No movement whatsoever.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the left leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                    AnswerText = "The leg immediately falls on the bed, there is no attempt to overcome the force of gravity.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the right leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                    AnswerText = "No movement whatsoever.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Language: Naming ordinary things",
                                    AnswerText = "Dumbness, complete aphasia or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Dysarthria",
                                    AnswerText = "Mild or moderate dysarthria; the patient \"smears\" some words, and sometimes it is difficult to understand him.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Ataxia in the limbs",
                                    AnswerText = "Ataxia is present in two limbs.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Sensitivity: injections with a disposable needle. In case of impaired consciousness, give points only if there is a grimace or asymmetric manifestations.",
                                    AnswerText = "Mild or moderate loss of sensitivity; the patient feels the touch as less sharp or dull; but feels when it is touched.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Neglect (lack of attention)",
                                    AnswerText = "Severe lack of attention or exclusion (selective perception) of stimuli of more than one modality.",
                                    AnswerValue = 2
                                }
                            }
                        }
                },
                new Results
                {
                    Date = new DateTime(2022, 2, 11),
                    isSaved = false,
                    PatientId = 3,
                    QuestionnaireId = 1,
                    ResultsPatientAnswers = new List<ResultsPatientAnswer>
                        {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness",
                                    AnswerText = "Reacts only with reflex movements or vegetative manifestations or does not react at all.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness: questions (Ask the patient what month it is now and how old he is.)",
                                    AnswerText = "Answered one question correctly.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness: commands (Ask the patient to close his eyes and make a fist)",
                                    AnswerText = "Completed one command correctly.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Eye movements",
                                    AnswerText = "Tonic abduction of the eyes or complete paralysis of the gaze, which persists during the test of the oculocephalic reflex.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Fields of vision (Evaluate all fields of vision using simultaneous finger movements)",
                                    AnswerText = "Bilateral hemianopsia (blindness, including cortical blindness) or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Facial muscle weakness",
                                    AnswerText = "Complete paresis (absence of facial expressions in the upper and lower parts of the face) or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the left hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                    AnswerText = "The hand immediately falls, there is no attempt to overcome the force of gravity.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the right hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                    AnswerText = "No movement whatsoever.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the left leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                    AnswerText = "The leg immediately falls on the bed, there is no attempt to overcome the force of gravity.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the right leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                    AnswerText = "No movement whatsoever.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Language: Naming ordinary things",
                                    AnswerText = "Dumbness, complete aphasia or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Dysarthria",
                                    AnswerText = "Mild or moderate dysarthria; the patient \"smears\" some words, and sometimes it is difficult to understand him.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Ataxia in the limbs",
                                    AnswerText = "Ataxia is present in two limbs.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Sensitivity: injections with a disposable needle. In case of impaired consciousness, give points only if there is a grimace or asymmetric manifestations.",
                                    AnswerText = "Mild or moderate loss of sensitivity; the patient feels the touch as less sharp or dull; but feels when it is touched.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Neglect (lack of attention)",
                                    AnswerText = "Severe lack of attention or exclusion (selective perception) of stimuli of more than one modality.",
                                    AnswerValue = 2
                                }
                            }
                        }
                },
                new Results
                {
                    Date = new DateTime(2022, 3, 13),
                    isSaved = true,
                    PatientId = 3,
                    QuestionnaireId = 1,
                    ResultsPatientAnswers = new List<ResultsPatientAnswer>
                        {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness",
                                    AnswerText = "Reacts only with reflex movements or vegetative manifestations or does not react at all.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness: questions (Ask the patient what month it is now and how old he is.)",
                                    AnswerText = "Answered one question correctly.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness: commands (Ask the patient to close his eyes and make a fist)",
                                    AnswerText = "Completed one command correctly.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Eye movements",
                                    AnswerText = "Tonic abduction of the eyes or complete paralysis of the gaze, which persists during the test of the oculocephalic reflex.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Fields of vision (Evaluate all fields of vision using simultaneous finger movements)",
                                    AnswerText = "Bilateral hemianopsia (blindness, including cortical blindness) or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Facial muscle weakness",
                                    AnswerText = "Complete paresis (absence of facial expressions in the upper and lower parts of the face) or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the left hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                    AnswerText = "The hand immediately falls, there is no attempt to overcome the force of gravity.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the right hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                    AnswerText = "No movement whatsoever.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the left leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                    AnswerText = "The leg immediately falls on the bed, there is no attempt to overcome the force of gravity.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the right leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                    AnswerText = "No movement whatsoever.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Language: Naming ordinary things",
                                    AnswerText = "Dumbness, complete aphasia or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Dysarthria",
                                    AnswerText = "Mild or moderate dysarthria; the patient \"smears\" some words, and sometimes it is difficult to understand him.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Ataxia in the limbs",
                                    AnswerText = "Ataxia is present in two limbs.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Sensitivity: injections with a disposable needle. In case of impaired consciousness, give points only if there is a grimace or asymmetric manifestations.",
                                    AnswerText = "Mild or moderate loss of sensitivity; the patient feels the touch as less sharp or dull; but feels when it is touched.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Neglect (lack of attention)",
                                    AnswerText = "Severe lack of attention or exclusion (selective perception) of stimuli of more than one modality.",
                                    AnswerValue = 2
                                }
                            }
                        }
                },
                new Results
                {
                    Date = new DateTime(2022, 4, 12),
                    isSaved = false,
                    PatientId = 3,
                    QuestionnaireId = 1,
                    ResultsPatientAnswers = new List<ResultsPatientAnswer>
                        {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness",
                                    AnswerText = "Reacts only with reflex movements or vegetative manifestations or does not react at all.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness: questions (Ask the patient what month it is now and how old he is.)",
                                    AnswerText = "Answered one question correctly.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness: commands (Ask the patient to close his eyes and make a fist)",
                                    AnswerText = "Completed one command correctly.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Eye movements",
                                    AnswerText = "Tonic abduction of the eyes or complete paralysis of the gaze, which persists during the test of the oculocephalic reflex.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Fields of vision (Evaluate all fields of vision using simultaneous finger movements)",
                                    AnswerText = "Bilateral hemianopsia (blindness, including cortical blindness) or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Facial muscle weakness",
                                    AnswerText = "Complete paresis (absence of facial expressions in the upper and lower parts of the face) or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the left hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                    AnswerText = "The hand immediately falls, there is no attempt to overcome the force of gravity.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the right hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                    AnswerText = "No movement whatsoever.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the left leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                    AnswerText = "The leg immediately falls on the bed, there is no attempt to overcome the force of gravity.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the right leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                    AnswerText = "No movement whatsoever.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Language: Naming ordinary things",
                                    AnswerText = "Dumbness, complete aphasia or coma.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Dysarthria",
                                    AnswerText = "Mild or moderate dysarthria; the patient \"smears\" some words, and sometimes it is difficult to understand him.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Ataxia in the limbs",
                                    AnswerText = "Ataxia is present in two limbs.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Sensitivity: injections with a disposable needle. In case of impaired consciousness, give points only if there is a grimace or asymmetric manifestations.",
                                    AnswerText = "Mild or moderate loss of sensitivity; the patient feels the touch as less sharp or dull; but feels when it is touched.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Neglect (lack of attention)",
                                    AnswerText = "Severe lack of attention or exclusion (selective perception) of stimuli of more than one modality.",
                                    AnswerValue = 2
                                }
                            }
                        }
                }
            );

            databaseContext.SaveChanges();
            return databaseContext;
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Filter_ReturnsResultsForDoctor_RealId(int doctorId)
        {
            //ARRANGE
            AppDbContext context = GetDataBaseContext();
            ResultsRepository repository = new ResultsRepository(context);
            FilterParameters filterParameters = new FilterParametersBuilder()
                .SetDoctorId(doctorId)
                .Build();

            //ACT
            IEnumerable<ResultsVM> results = repository.GetFiltered(filterParameters);

            //ASSERT
            Assert.NotNull(results);
            results.Should().HaveCount(4);
            results.Should().OnlyContain(p => p.DoctorId == doctorId);
            
        }

        [Theory]
        [InlineData(100)]
        [InlineData(200)]
        [InlineData(300)]
        public void Filter_ReturnsResultsForDoctor_FakeId(int doctorId)
        {
            //ARRANGE
            AppDbContext context = GetDataBaseContext();
            ResultsRepository repository = new ResultsRepository(context);
            FilterParameters filterParameters = new FilterParametersBuilder()
                .SetDoctorId(doctorId)
                .Build();

            //ACT
            IEnumerable<ResultsVM> results = repository.GetFiltered(filterParameters);

            //ASSERT
            results.Should().BeEmpty();
            results.Should().HaveCount(0);
        }

        [Theory]
        [InlineData(1, 20, 40)]
        [InlineData(2, 20, 40)]
        [InlineData(3, 20, 40)]
        public void Filter_ReturnsResultsForDoctor_Age(int doctorId, int minAge, int maxAge)
        {
            //ARRANGE
            AppDbContext context = GetDataBaseContext();
            ResultsRepository repository = new ResultsRepository(context);
            FilterParameters filterParameters = new FilterParametersBuilder()
                .SetDoctorId(doctorId)
                .SetMinAge(minAge)
                .SetMaxAge(maxAge)
                .Build();

            //ACT
            IEnumerable<ResultsVM> results = repository.GetFiltered(filterParameters);

            //ASSERT
            
            results.Should().OnlyContain(p => p.Age >= minAge && p.Age <= maxAge);
            results.Should().OnlyContain(p => p.DoctorId == doctorId);
        }

        [Theory]
        [InlineData(1, "2022-01-01", "2022-01-30")]
        [InlineData(1, "2022-02-01", "2022-02-28")] 
        [InlineData(1, "2022-03-01", "2023-03-30")] 
        [InlineData(1, "2022-04-01", "2023-04-30")]
        [InlineData(2, "2022-01-01", "2022-01-30")]
        [InlineData(2, "2022-02-01", "2022-02-28")]
        [InlineData(2, "2022-03-01", "2023-03-30")]
        [InlineData(2, "2022-04-01", "2023-04-30")]
        [InlineData(3, "2022-01-01", "2022-01-30")]
        [InlineData(3, "2022-02-01", "2022-02-28")]
        [InlineData(3, "2022-03-01", "2023-03-30")]
        [InlineData(3, "2022-04-01", "2023-04-30")]
        public void Filter_ReturnsResultsForDoctor_Date(int doctorId, string minDateString, string maxDateString)
        {
            // ARRANGE
            AppDbContext context = GetDataBaseContext();
            ResultsRepository repository = new ResultsRepository(context);

            // Convert string representations to DateTime
            DateTime minDate = DateTime.Parse(minDateString);
            DateTime maxDate = DateTime.Parse(maxDateString);

            FilterParameters filterParameters = new FilterParametersBuilder()
                .SetDoctorId(doctorId)
                .SetBeginDate(minDate)
                .SetEndDate(maxDate)
                .Build();

            // ACT
            IEnumerable<ResultsVM> results = repository.GetFiltered(filterParameters);

            // ASSERT
            results.Should().OnlyContain(p => p.Date >= minDate && p.Date <= maxDate);
            results.Should().OnlyContain(p => p.DoctorId == doctorId);
        }

        [Theory]
        [InlineData(1, "National Institutes of Health stroke scale (NIHSS)")]
        [InlineData(2, "National Institutes of Health stroke scale (NIHSS)")]
        [InlineData(3, "National Institutes of Health stroke scale (NIHSS)")]
        public void Filter_ReturnsResultsForDoctor_QuestionnaireName(int doctorId, string questionnaireName)
        {
            // ARRANGE
            AppDbContext context = GetDataBaseContext();
            ResultsRepository repository = new ResultsRepository(context);

            FilterParameters filterParameters = new FilterParametersBuilder()
                .SetDoctorId(doctorId)
                .SetQuestionnaireName(questionnaireName)
                .Build();

            // ACT
            IEnumerable<ResultsVM> results = repository.GetFiltered(filterParameters);

            // ASSERT
            //ASSERT
            Assert.NotNull(results);
            results.Should().HaveCount(4);
            results.Should().OnlyContain(p => p.QuestionnaireName == questionnaireName);
            results.Should().OnlyContain(p => p.DoctorId == doctorId);
        }

        [Theory]
        [InlineData(1, "Stroke")]
        [InlineData(2, "Stroke")]
        [InlineData(3, "Stroke")]
        public void Filter_ReturnsResultsForDoctor_DiseaseName(int doctorId, string diseaseName)
        {
            // ARRANGE
            AppDbContext context = GetDataBaseContext();
            ResultsRepository repository = new ResultsRepository(context);

            FilterParameters filterParameters = new FilterParametersBuilder()
                .SetDoctorId(doctorId)
                .SetDiseaseName(diseaseName)
                .Build();

            // ACT
            IEnumerable<ResultsVM> results = repository.GetFiltered(filterParameters);

            // ASSERT
            //ASSERT
            Assert.NotNull(results);
            results.Should().HaveCount(4);
            results.Should().OnlyContain(p => p.DiseaseName == diseaseName);
            results.Should().OnlyContain(p => p.DoctorId == doctorId);
        }

        [Theory]
        [InlineData(1, "Male")]
        [InlineData(2, "Female")]
        [InlineData(3, "Male")]
        public void Filter_ReturnsResultsForDoctor_Gender_True(int doctorId, string gender)
        {
            // ARRANGE
            AppDbContext context = GetDataBaseContext();
            ResultsRepository repository = new ResultsRepository(context);

            FilterParameters filterParameters = new FilterParametersBuilder()
                .SetDoctorId(doctorId)
                .SetGender(gender)
                .Build();

            // ACT
            IEnumerable<ResultsVM> results = repository.GetFiltered(filterParameters);

            // ASSERT
            results.Should().OnlyContain(p => p.Gender == gender);
            results.Should().OnlyContain(p => p.DoctorId == doctorId);
        }

        [Theory]
        [InlineData(1, "Female")]
        [InlineData(2, "Male")]
        [InlineData(3, "Female")]
        public void Filter_ReturnsResultsForDoctor_Gender_False(int doctorId, string gender)
        {
            // ARRANGE
            AppDbContext context = GetDataBaseContext();
            ResultsRepository repository = new ResultsRepository(context);

            FilterParameters filterParameters = new FilterParametersBuilder()
                .SetDoctorId(doctorId)
                .SetGender(gender)
                .Build();

            // ACT
            IEnumerable<ResultsVM> results = repository.GetFiltered(filterParameters);

            // ASSERT
            results.Should().BeNullOrEmpty();
        }

        [Theory]
        [InlineData(1, "2022-01-01", "2022-01-30", "Male", "Stroke", 20, 40, "National Institutes of Health stroke scale (NIHSS)")]
        [InlineData(1, "2022-01-01", "2022-02-28", "Male", "Stroke", 20, 40, "National Institutes of Health stroke scale (NIHSS)")]
        [InlineData(2, "2022-01-01", "2022-01-30", "Female", "Stroke", 20, 40, "National Institutes of Health stroke scale (NIHSS)")]
        [InlineData(2, "2022-02-01", "2022-02-28", "Female", "Stroke", 20, 40, "National Institutes of Health stroke scale (NIHSS)")]
        [InlineData(3, "2022-01-01", "2022-01-30", "Male", "Stroke", 20, 40, "National Institutes of Health stroke scale (NIHSS)")]
        [InlineData(3, "2022-03-01", "2022-03-30", "Male", "Stroke", 20, 40, "National Institutes of Health stroke scale (NIHSS)")]
        
        public void Filter_ReturnsResultsForDoctor_AllFilters(
            int doctorId,
            string beginTime,
            string endTime,
            string gender,
            string diseaseName,
            int minAge,
            int maxAge,
            string questionnaireName
            )
        {
            // ARRANGE
            AppDbContext context = GetDataBaseContext();
            ResultsRepository repository = new ResultsRepository(context);

            // Convert string representations to DateTime
            DateTime minDate = DateTime.Parse(beginTime);
            DateTime maxDate = DateTime.Parse(endTime);

            FilterParameters filterParameters = new FilterParametersBuilder()
                .SetDoctorId(doctorId)
                .SetGender(gender)
                .SetMinAge(minAge)
                .SetMaxAge(maxAge)
                .SetBeginDate(minDate)
                .SetEndDate(maxDate)
                .SetQuestionnaireName(questionnaireName)
                .SetDiseaseName(diseaseName)
                .Build();

            // ACT
            IEnumerable<ResultsVM> results = repository.GetFiltered(filterParameters);

            // ASSERT
            //ASSERT
            results.Should().OnlyContain(p => p.DiseaseName == diseaseName);
            results.Should().OnlyContain(p => p.DoctorId == doctorId);
            results.Should().OnlyContain(p => p.Gender == gender);
            results.Should().OnlyContain(p => p.QuestionnaireName == questionnaireName);
            results.Should().OnlyContain(p => p.Date >= minDate && p.Date <= maxDate);
            results.Should().OnlyContain(p => p.Age>= minAge && p.Age <= maxAge);
        }

        public static TheoryData<int> ResultIds => new TheoryData<int>
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12
        };

        [Theory]
        [MemberData(nameof(ResultIds))]
        public void ToggleSavedResult_Toggls(int resultId)
        {
            // ARRANGE
            AppDbContext context = GetDataBaseContext();
            ResultsRepository repository = new ResultsRepository(context);
            bool prevIsSavedValue = repository.GetById(resultId).isSaved;

            // ACT
            Results res = repository.ToggleSavedResult(resultId);
            var resAfter = repository.GetById(resultId).isSaved;
            // ASSERT
            res.Should().NotBeNull();
            res.isSaved.Should().Be(resAfter);
            res.isSaved.Should().Be(!prevIsSavedValue);
        }

    }


}
