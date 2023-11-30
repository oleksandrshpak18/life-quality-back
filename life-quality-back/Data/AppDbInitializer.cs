﻿using life_quality_back.Controllers.Authorization;
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
                            Speciality = "Cardiologist"
                        }
                    },
                    new User
                    {
                        Login = "john.doe@lq.com",
                        Password = "6cb75f652a9b52798eb6cf2201057c73",
                        Doctor = new Doctor
                        {
                            FirstName = "John",
                            LastName = "Doe",
                            Email = "john.doe@lq.com",
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
                            },
                            new QuestionnaireTreatmentStrategy
                            {
                                Questionnaire = new Questionnaire
                                {
                                    QuestionnaireName = "Barthel Index (BI)",
                                    QuestionnaireDescription = "This index measures the degree to which someone can function independently and has mobility in their activities of daily living (ADL), i.e. eating, bathing, hygieneprocedures, dressing. The index also indicates the need for care assistance.The Barthel Index (BI) is a widely used indicator of functional impairment. The index was developed for use in the rehabilitation of stroke patientsand other neuromuscular or musculoskeletal diseases, but alsocan also be used for cancer patients.",
                                    Questions = new List<Question>
                                    {
                                        new Question
                                        {
                                            QuestionText = "Meal",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Independent - the patient can eat independently from a tray or table when someone serves food within his reach. He should set up the utensils if necessary, cut the food, use salt and pepper, spread butter on the bread, etc. He must do it in a reasonable amount of time.",
                                                    AnswerValue = 10
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Some help is needed (with cutting food, etc., as mentioned above).",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Helpless - the patient must be fed.",
                                                    AnswerValue = 0
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Transfer from bed to wheelchair and back",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "The patient is independent at all stages of this type of activity. The patient can safely approach the bed in their wheelchair, unlock the brake system, raise the footboard, safely transfer to the bed, lie down, bring themselves to a sitting position on the bed, change the position of the wheelchair to safely transfer to it if necessary and return to wheelchair.",
                                                    AnswerValue = 15
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Some minimal assistance is required at some stage of the activity or the patient needs to be reminded or supervised to ensure the safety of one or more stages of the activity.",
                                                    AnswerValue = 10
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "The patient can bring himself to a sitting position without the help of another person, but he must be lifted from the bed or, if he moves, with considerable assistance.",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Dependent - the patient cannot bring himself to a sitting position.",
                                                    AnswerValue = 0
                                                },
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Personal hygiene",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "The patient can independently wash his face and hands, comb his hair, brush his teeth and shave. He can use any type of razor, but must put down the blade or turn on the razor without assistance, and must retrieve it from a drawer or cabinet. Patients should apply their own make-up, if used, but do not have to braid their hair or style their hair.",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "The patient needs help.",
                                                    AnswerValue = 0
                                                },
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Using the toilet",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "The patient can independently use the toilet, take off and put on clothes, prevent soiling of clothes and use toilet paper without help. He can use handrails on the wall or another stable object for support if necessary. If it is necessary to use a tray instead of a toilet, he must be able to place it on the chair, clean and wash it.",
                                                    AnswerValue = 10
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "The patient needs help due to a disturbance in balance or when putting on and taking off clothes, or when using toilet paper.",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "The patient needs help.",
                                                    AnswerValue = 0
                                                },
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Прийом ванни",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "The patient can independently take a bath, shower, or completely wipe the body with a sponge. He should be able to do all the necessary steps, depending on which method is used, without the help of another person.",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "The patient needs help.",
                                                    AnswerValue = 0
                                                },
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Moving on a flat plane",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "The patient can walk at least 50 meters without assistance or supervision. He can wear orthotics or prostheses and use crutches, sticks, but not walkers. He must be able, if necessary, to lock and unlock the orthopedic device, let's say from a standing position, and to sit down, bring all the necessary mechanisms into proper condition for further use and get rid of them when he is sitting (put on and take off the corset under clothes).",
                                                    AnswerValue = 15
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "The patient requires assistance or supervision for all of the walking activities described above, but can walk at least 50 meters with little assistance.",
                                                    AnswerValue = 10
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "If the patient cannot move, but can drive a wheelchair independently. He must be able to go around corners, turn around, maneuver from a chair to a table, bed, toilet, etc. He must be able to drive a cart at least 50 meters.",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Immobile - the patient needs help moving in a wheelchair.",
                                                    AnswerValue = 0
                                                },
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Climbing and descending stairs",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "The patient can safely walk up and down stairs without assistance or supervision. He can and should use handrails, canes or crutches when necessary. He must be able to carry sticks or crutches when going up or down stairs.",
                                                    AnswerValue = 10
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "The patient needs help or supervision when performing any of the above items.",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "The patient is unable to go up and down stairs. He needs a lift.",
                                                    AnswerValue = 0
                                                },
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Dressing and undressing (patients should not consider wearing a bra or girdle unless specified items of clothing are available)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "The patient is able to put on and take off and fasten all clothing, tie shoelaces (if no device is required for this). This type of daily activity includes donning, removing, and securing a corset or brace when prescribed. If necessary, such special elements of clothing as suspenders, moccasins, dresses that unfasten in the front can be used.",
                                                    AnswerValue = 10
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "The patient needs help in putting on, taking off or fastening any clothes. He should do at least half of the work himself. He must do it in a reasonable amount of time.",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "The patient needs help.",
                                                    AnswerValue = 0
                                                },
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Defecation control",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "The patient is able to control defecation and does not have any problem cases. He may use a suppository or enema when necessary (as for patients with spinal cord injuries undergoing bowel preparation).",
                                                    AnswerValue = 10
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "The patient needs help using suppositories or enemas or has occasional problem cases.",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Incontinence.",
                                                    AnswerValue = 0
                                                },
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Urinary control",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "The patient can control urination during the day and at night. Patients with spinal cord injuries who wear an external catheter and leg bag must independently insert, clean and empty the bag and stay dry day and night.",
                                                    AnswerValue = 10
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "The patient has intermittent problem cases, or can't wait for the undercarriage, or get to the toilet in time, or needs help with the catheter.",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Incontinence.",
                                                    AnswerValue = 0
                                                },
                                            }
                                        },
                                    }
                                },
                            },
                            new QuestionnaireTreatmentStrategy
                            {
                                Questionnaire = new Questionnaire
                                {
                                    QuestionnaireName = "Berg Balance Test (BBS)",
                                    QuestionnaireDescription = "The Berg Balance Test (BBS) was originally developed to quantify moat novaga in the elderly. Among the functional balance assessment tests, the BBS test, as rule is considered the gold standard.",
                                    Questions = new List<Question>
                                    {
                                        new Question
                                        {
                                            QuestionText = "Stand up from a sitting position (Please stand up. Try not to use your arm for support.)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Can stand up without using hands and self-stabilize.",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Can stand up independently with hands.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Can stand up with hands after a few tries.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Requires minimal assistance standing up or stabilizing.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Requires moderate to maximum assistance when standing up.",
                                                    AnswerValue = 0
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Stand without support (Please stand for two minutes without support.)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Able to stand safely for 2 minutes.",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Able to stand for 2 minutes under supervision.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Able to stand for 30 seconds without support.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "It takes a few tries to stand for 30 seconds without support.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Cannot stand for 30 seconds without support.",
                                                    AnswerValue = 0
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Sitting without back support, but with fixed legs on the floor or a chair (Please sit with arms folded for 2 minutes)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Able to sit safely and securely for 2 minutes.",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Able to sit for 2 minutes under supervision.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Able to sit for 30 seconds.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Able to sit for 10 seconds.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Cannot sit unsupported for 10 seconds.",
                                                    AnswerValue = 0
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Sitting down from a standing position (Please sit down)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Sits securely with minimal use of hands.",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Контролює сідання за допомогою рук.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Controls sitting with hands.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Sits independently, but the sitting process is uncontrolled.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Needs help when sitting.",
                                                    AnswerValue = 0
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Переміщення (Поставте стілець(і), як орієнтири при переміщенні. Попросіть пацієнта пройти в один бік до стільця з підлокітниками і в інший бік до стільця без підлокітників. Ви можете використовувати два стільці (один з і один без підлокітників) або ліжко і стілець.)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "У змозі пройти безпечно з незначним використанням рук.",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Здатний пройти безпечно, необхідна допомога рук.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Здатний пройти зі скигленням і/або під наглядом.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Потрібна одна людина, щоб допомогти.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Потрібні дві людини, щоб допомогти або контролювати безпечність.",
                                                    AnswerValue = 0
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Стояння без підтримки із закритими очима (Будь ласка, закрийте очі і стійте на місці протягом 10 секунд)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Може простояти 10 секунд безпечно.",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Може простояти 10 секунд під контролем.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Може простояти 3 секунди.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Не в змозі тримати очі закритими протягом 3 секунд, але стоїть безпечно.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Потребує допомоги, щоб не впасти.",
                                                    AnswerValue = 0
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Стояння без підтримки із ногами разом (Поставте ноги разом і стійте без підтримки)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "У змозі поставити ноги разом самостійно і простояти 1 хвилину безпечно.",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "У змозі поставити ноги разом самостійно і простояти 1 хвилину під контролем.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "У змозі поставити ноги разом самостійно, але не в змозі стояти протягом 30 секунд.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Потребує допомоги, щоб досягти необхідної пози, але може стояти 15 секунд, коли ноги разом.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Потребує допомоги, щоб досягти необхідної пози і не в змозі стояти протягом 15 секунд.",
                                                    AnswerValue = 0
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Standing forward bend with outstretched arm (Raise your arm at a 90-degree angle. Extend your fingers and bend as far as you can. (The examiner places a ruler near the fingertips when the arm is at a 90-degree angle. The fingers should not touch the ruler when bent. Control result is the distance reached by the fingers when the patient is in the maximum tilt position.If possible, ask the patient to use both hands when tilting to avoid rotation of the spine.))",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Can confidently reach 25 cm (10 inches).",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Can reach 12 cm (5 in).",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Can reach 5 cm (2 in).",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Leans forward but needs control.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Loses balance when trying / needs external support.",
                                                    AnswerValue = 0
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Picking up an object from the floor while standing (Pick up the shoe/slipper that is in front of your feet)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Able to take a slipper easily and safely.",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Able to lift shoe, but needs supervision.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Cannot pick up, but reaches within 2–5 cm (1–2 inches) of the slipper and balances independently.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Unable to pick up and needs supervision when trying.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Can't try/needs help to keep from losing balance or falling.",
                                                    AnswerValue = 0
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Looking over the left and right shoulder while standing (Look back to look directly over the left shoulder. Repeat to the right. (The examiner can choose any object behind the patient to look directly at the object to facilitate better rotation.))",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Looks back from both sides and the weight shifts well.",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "It looks smaller on one side than on the other; less weight transfer.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Only turns sideways, but maintains balance.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Needs supervision when turning.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Needs help not to lose balance or avoid falling.",
                                                    AnswerValue = 0
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "360 Degree Turn (Turn completely around you. Pause. Then turn in the other direction)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Able to turn 360 degrees safely in 4 seconds or less.",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Able to turn 360 degrees safely one way only in 4 seconds or less.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Able to turn 360 degrees safely but slowly.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Careful supervision or verbal support is required.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Needs help when turning.",
                                                    AnswerValue = 0
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Standing foot on a step or chair without support (Place each foot alternately on the step/chair. Continue until each foot touches the step/chair four times)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Can stand independently and safely and takes 8 steps within 20 seconds.",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Can stand independently and takes 8 steps in more than 20 seconds.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Able to perform 4 steps unaided under supervision.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Able to take more than 2 steps, needs minimal assistance.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Needs help to keep from falling/can't try.",
                                                    AnswerValue = 0
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Standing without support with one foot in front ((Demonstrate to the patient) Place one foot directly in front of the other. If you feel that you cannot place the foot directly in front, try a little further so that the heel of your front foot is in front of the toes of the other foot. (For in order to score 3 points, the length of the step should not exceed the length of the other leg and the width of the posture should be close to the normal width of the patient's step.))",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Can place the feet \"goose\" independently and hold the pose for 30 seconds.",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Able to place one foot in front independently and hold the pose for 30 seconds.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Able to take a small step independently and hold the pose for 30 seconds.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Needs help to take a step, but can hold a pose for 15 seconds.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Loses balance while walking or standing.",
                                                    AnswerValue = 0
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Standing on one leg (Stand on one leg as long as you can without support)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Able to lift leg independently and hold for > 10 seconds.",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Able to raise the leg independently and hold for 5-10 seconds.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Able to raise a leg independently and hold for less than 3 seconds.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Tries to raise a leg, unable to hold for 3 seconds, but stands on his own.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Can't try, needs help to avoid falling.",
                                                    AnswerValue = 0
                                                }
                                            }
                                        },
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
                                    QuestionnaireDescription = "Description to Questionnaire 3",
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
                        FirstName = "Ethan",
                        LastName = "Reynolds",
                        Anamnesis = "Some anamnesis information",
                        Email = "ethan.reynolds@lq.com",
                        Gender = "Male",
                        BirthDate = new DateTime(1990, 1, 1),
                        RehabilitationStartDate = new DateTime(2023, 8, 10),
                        TreatmentStrategyId = context.TreatmentStrategies
                                        .Where(x => x.TreatmentStrategyName.Equals("Strategy 1")) // replace later with the name of a real TreamentStrategy from TreatmentStrategies
                                        .Select(x => x.TreatmentStrategyId).First(),
                        DiseaseId = context.Diseases
                                        .Where(x => x.DiseaseName.Equals("Stroke")) // replace later with the name of a real disease from Diseases
                                        .Select(x => x.DiseaseId).First(),
                        DoctorId = context.Doctors
                                        .Where(x => x.Email.Equals("mike.dolfino@lq.com")) // replace later with the name of a real doctor from Doctors
                                        .Select(x => x.DoctorId).First(),
                    },
                    new Patient
                    {
                        FirstName = "Oliver",
                        LastName = "Mitchell",
                        Anamnesis = "Some anamnesis information",
                        Email = "oliver.mitchell@lq.com",
                        Gender = "Male",
                        BirthDate = new DateTime(2023, 8, 20),
                        RehabilitationStartDate = DateTime.Now,
                        TreatmentStrategyId = context.TreatmentStrategies
                                        .Where(x => x.TreatmentStrategyName.Equals("Strategy 2")) // replace later with the name of a real TreamentStrategy from TreatmentStrategies
                                        .Select(x => x.TreatmentStrategyId).First(),
                        DiseaseId = context.Diseases
                                        .Where(x => x.DiseaseName.Equals("Stroke")) // replace later with the name of a real disease from Diseases
                                        .Select(x => x.DiseaseId).First(),
                        DoctorId = context.Doctors
                                        .Where(x => x.Email.Equals("mike.dolfino@lq.com")) // replace later with the name of a real doctor from Doctors
                                        .Select(x => x.DoctorId).First(),
                    },
                    new Patient
                    {
                        FirstName = "Sebastian",
                        LastName = "Carter",
                        Anamnesis = "Some anamnesis information",
                        Email = "sebastian.carter@lq.com",
                        Gender = "Male",
                        BirthDate = new DateTime(1990, 1, 1),
                        RehabilitationStartDate = new DateTime(2023, 9, 10),
                        TreatmentStrategyId = context.TreatmentStrategies
                                        .Where(x => x.TreatmentStrategyName.Equals("Strategy 2")) // replace later with the name of a real TreamentStrategy from TreatmentStrategies
                                        .Select(x => x.TreatmentStrategyId).First(),
                        DiseaseId = context.Diseases
                                        .Where(x => x.DiseaseName.Equals("Stroke")) // replace later with the name of a real disease from Diseases
                                        .Select(x => x.DiseaseId).First(),
                        DoctorId = context.Doctors
                                        .Where(x => x.Email.Equals("john.doe@lq.com")) // replace later with the name of a real doctor from Doctors
                                        .Select(x => x.DoctorId).First(),
                    },
                    new Patient
                    {
                        FirstName = "Ava",
                        LastName = "Thompson",
                        Anamnesis = "Some anamnesis information",
                        Email = "ava.thompson@lq.com",
                        Gender = "Female",
                        BirthDate = new DateTime(1990, 1, 1),
                        RehabilitationStartDate = new DateTime(2023, 7, 20),
                        TreatmentStrategyId = context.TreatmentStrategies
                                        .Where(x => x.TreatmentStrategyName.Equals("Strategy 1")) // replace later with the name of a real TreamentStrategy from TreatmentStrategies
                                        .Select(x => x.TreatmentStrategyId).First(),
                        DiseaseId = context.Diseases
                                        .Where(x => x.DiseaseName.Equals("Stroke")) // replace later with the name of a real disease from Diseases
                                        .Select(x => x.DiseaseId).First(),
                        DoctorId = context.Doctors
                                        .Where(x => x.Email.Equals("john.doe@lq.com")) // replace later with the name of a real doctor from Doctors
                                        .Select(x => x.DoctorId).First(),
                    },
                    new Patient
                    {
                        FirstName = "Sophia",
                        LastName = "Rodriguez",
                        Anamnesis = "Some anamnesis information",
                        Email = "sophia.rodriguez@lq.com",
                        Gender = "Female",
                        BirthDate = new DateTime(1990, 1, 1),
                        RehabilitationStartDate = new DateTime(2023, 7, 30),
                        TreatmentStrategyId = context.TreatmentStrategies
                                        .Where(x => x.TreatmentStrategyName.Equals("Strategy 2")) // replace later with the name of a real TreamentStrategy from TreatmentStrategies
                                        .Select(x => x.TreatmentStrategyId).First(),
                        DiseaseId = context.Diseases
                                        .Where(x => x.DiseaseName.Equals("Stroke")) // replace later with the name of a real disease from Diseases
                                        .Select(x => x.DiseaseId).First(),
                        DoctorId = context.Doctors
                                        .Where(x => x.Email.Equals("john.doe@lq.com")) // replace later with the name of a real doctor from Doctors
                                        .Select(x => x.DoctorId).First(),
                    },
                    new Patient
                    {
                        FirstName = "Sebastian",
                        LastName = "Carter",
                        Anamnesis = "Some anamnesis information",
                        Email = "sebastian.carter@lq.com",
                        Gender = "Female",
                        BirthDate = new DateTime(1990, 1, 1),
                        RehabilitationStartDate = new DateTime(2023, 8, 30),
                        TreatmentStrategyId = context.TreatmentStrategies
                                        .Where(x => x.TreatmentStrategyName.Equals("Strategy 2")) // replace later with the name of a real TreamentStrategy from TreatmentStrategies
                                        .Select(x => x.TreatmentStrategyId).First(),
                        DiseaseId = context.Diseases
                                        .Where(x => x.DiseaseName.Equals("Stroke")) // replace later with the name of a real disease from Diseases
                                        .Select(x => x.DiseaseId).First(),
                        DoctorId = context.Doctors
                                        .Where(x => x.Email.Equals("mike.dolfino@lq.com")) // replace later with the name of a real doctor from Doctors
                                        .Select(x => x.DoctorId).First(),
                    }
                );

                context.SaveChanges();

                // перед додаванням результатів витягнемо собі допоміжну інформацію, щоб менше дублювати
                context.Results.AddRange(
                    new Models.Results
                    {
                        Date = new DateTime(2023, 8, 11),
                        isSaved = false,
                        PatientId = context.Patients
                            .Where(x => x.Email.Equals("ethan.reynolds@lq.com"))
                            .Select(x => x.PatientId).First(),
                        QuestionnaireId = context.Questionnaires
                            .Where(x => x.QuestionnaireName.Equals("National Institutes of Health stroke scale (NIHSS)"))
                            .Select(x => x.QuestionnaireId).First(),
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
                                    AnswerText = "Рука одразу падає, немає спроб подолати силу тяжіння.",
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
                new Models.Results
                {
                    Date = new DateTime(2023, 8, 18),
                    isSaved = false,
                    PatientId = context.Patients
                            .Where(x => x.Email.Equals("ethan.reynolds@lq.com"))
                            .Select(x => x.PatientId).First(),
                    QuestionnaireId = context.Questionnaires
                            .Where(x => x.QuestionnaireName.Equals("National Institutes of Health stroke scale (NIHSS)"))
                            .Select(x => x.QuestionnaireId).First(),
                    ResultsPatientAnswers = new List<ResultsPatientAnswer>
                    {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness",
                                    AnswerText = "Drowsy, but can be brought to consciousness by light stimuli, and thencarries out orders, answers, reacts.",
                                    AnswerValue = 1
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
                                    QuestionText = "Eye movements",
                                    AnswerText = "Partial paralysis of gaze or isolated paresis of the nerve.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Fields of vision (Evaluate all fields of vision using simultaneous finger movements)",
                                    AnswerText = "Complete hemianopsia.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Facial muscle weakness",
                                    AnswerText = "Partial paresis (complete or almost complete paralysis of the lower facial muscles).",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the left hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                    AnswerText = "The patient cannot hold the hand in the initial position (90°) for 10 seconds, but there are certain efforts against gravity.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the right hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                    AnswerText = "Рука одразу падає, немає спроб подолати силу тяжіння.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the left leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                    AnswerText = "The leg falls on the bed during the first 5 seconds, some effort against gravity.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the right leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                    AnswerText = "The leg immediately falls on the bed, there is no attempt to overcome the force of gravity.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Language: Naming ordinary things",
                                    AnswerText = "Severe aphasia. Complete aphasia expressive (Broca) or receptive (Wernicke).",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Dysarthria",
                                    AnswerText = "Severe dysarthria; the pronunciation is so distorted that the patient cannot be understood.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Ataxia in the limbs",
                                    AnswerText = "Answer 1 | Question 1 | Questionnaire 1",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Question 1 | Questionnaire 1",
                                    AnswerText = "Ataxia is present in two limbs.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Sensitivity: injections with a disposable needle. In case of impaired consciousness, give points only if there is a grimace or asymmetric manifestations.",
                                    AnswerText = "Norm; sedation or amputation.",
                                    AnswerValue = 0
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Neglect (lack of attention)",
                                    AnswerText = "Lack of attention to visual, tactile, auditory stimuli on one side.",
                                    AnswerValue = 1
                                }
                            }
                    }
                },
                new Models.Results
                {
                    Date = new DateTime(2023, 8, 30),
                    isSaved = false,
                    PatientId = context.Patients
                            .Where(x => x.Email.Equals("ethan.reynolds@lq.com"))
                            .Select(x => x.PatientId).First(),
                    QuestionnaireId = context.Questionnaires
                            .Where(x => x.QuestionnaireName.Equals("National Institutes of Health stroke scale (NIHSS)"))
                            .Select(x => x.QuestionnaireId).First(),
                    ResultsPatientAnswers = new List<ResultsPatientAnswer>
                    {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness",
                                    AnswerText = "Conscious, reacts quickly.",
                                    AnswerValue = 0
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness: questions (Ask the patient what month it is now and how old he is.)",
                                    AnswerText = "Answered both questions correctly or there is a language barrier.",
                                    AnswerValue = 0
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness: commands (Ask the patient to close his eyes and make a fist)",
                                    AnswerText = "Executed both commands correctly.",
                                    AnswerValue = 0
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Eye movements",
                                    AnswerText = "Full range of eye movements.",
                                    AnswerValue = 0
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Fields of vision (Evaluate all fields of vision using simultaneous finger movements)",
                                    AnswerText = "Asymmetric or partial hemianopsia.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Facial muscle weakness",
                                    AnswerText = "Mild paresis (only smoothing of the nasolabial fold).",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the left hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                    AnswerText = "The arm remains in the initial position (90°), but begins to move downward during the first 10 seconds.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the right hand (The patient holds the hand at an angle of 90° to the body (palm down))",
                                    AnswerText = "The patient cannot hold the hand in the initial position (90°) for 10 seconds, but there are certain efforts against gravity.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the left leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                    AnswerText = "The leg lowers to an intermediate position during the first 5 seconds.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Assessment of the motor function of the right leg (The patient holds the leg at an angle of 30° for 5 seconds)",
                                    AnswerText = "The leg falls on the bed during the first 5 seconds, some effort against gravity.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Language: Naming ordinary things",
                                    AnswerText = "Mild to moderate aphasia, errors in naming objects, or paraphasia. Impaired speech and/or language comprehension.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Dysarthria",
                                    AnswerText = "Norm.",
                                    AnswerValue = 0
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Ataxia in the limbs",
                                    AnswerText = "Ataxia is present in one limb.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Sensitivity: injections with a disposable needle. In case of impaired consciousness, give points only if there is a grimace or asymmetric manifestations.",
                                    AnswerText = "Norm; sedation or amputation.",
                                    AnswerValue = 0
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Neglect (lack of attention)",
                                    AnswerText = "There are no violations.",
                                    AnswerValue = 0
                                }
                            }
                    }
                },
                new Models.Results
                {
                    Date = new DateTime(2023, 8, 12),
                    isSaved = false,
                    PatientId = context.Patients
                            .Where(x => x.Email.Equals("ethan.reynolds@lq.com"))
                            .Select(x => x.PatientId).First(),
                    QuestionnaireId = context.Questionnaires
                            .Where(x => x.QuestionnaireName.Equals("Barthel Index (BI)"))
                            .Select(x => x.QuestionnaireId).First(),
                    ResultsPatientAnswers = new List<ResultsPatientAnswer>
                    {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Meal",
                                    AnswerText = "безпомічний — пацієнта необхідно годувати.",
                                    AnswerValue = 0
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
                            },
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
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Question 2 | Questionnaire 1",
                                    AnswerText = "Answer 1 | Question 1 | Questionnaire 1",
                                    AnswerValue = 1
                                }
                            },
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
                            },
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
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Question 2 | Questionnaire 1",
                                    AnswerText = "Answer 1 | Question 1 | Questionnaire 1",
                                    AnswerValue = 1
                                }
                            },
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
                            },
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
                },
                new Models.Results
                {
                    Date = new DateTime(2023, 9, 5),
                    isSaved = false,
                    PatientId = context.Patients
                            .Where(x => x.Email.Equals("ethan.reynolds@lq.com"))
                            .Select(x => x.PatientId).First(),
                    QuestionnaireId = context.Questionnaires
                            .Where(x => x.QuestionnaireName.Equals("National Institutes of Health stroke scale (NIHSS)"))
                            .Select(x => x.QuestionnaireId).First(),
                    ResultsPatientAnswers = new List<ResultsPatientAnswer>
                    {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness",
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
                            },
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
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Question 2 | Questionnaire 1",
                                    AnswerText = "Answer 1 | Question 1 | Questionnaire 1",
                                    AnswerValue = 1
                                }
                            },
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
                            },
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
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Question 2 | Questionnaire 1",
                                    AnswerText = "Answer 1 | Question 1 | Questionnaire 1",
                                    AnswerValue = 1
                                }
                            },
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
                            },
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
                },
                new Models.Results
                {
                    Date = new DateTime(2023, 9, 5),
                    isSaved = false,
                    PatientId = context.Patients
                            .Where(x => x.Email.Equals("ethan.reynolds@lq.com"))
                            .Select(x => x.PatientId).First(),
                    QuestionnaireId = context.Questionnaires
                            .Where(x => x.QuestionnaireName.Equals("National Institutes of Health stroke scale (NIHSS)"))
                            .Select(x => x.QuestionnaireId).First(),
                    ResultsPatientAnswers = new List<ResultsPatientAnswer>
                    {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Level of consciousness",
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
                            },
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
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Question 2 | Questionnaire 1",
                                    AnswerText = "Answer 1 | Question 1 | Questionnaire 1",
                                    AnswerValue = 1
                                }
                            },
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
                            },
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
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Question 2 | Questionnaire 1",
                                    AnswerText = "Answer 1 | Question 1 | Questionnaire 1",
                                    AnswerValue = 1
                                }
                            },
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
                            },
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
