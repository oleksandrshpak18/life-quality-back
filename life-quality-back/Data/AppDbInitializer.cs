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
                            Gender = "Male",
                            Speciality = "Endocrinologist"
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
                        DiseaseName = "Osteoarthritis",
                        DiseaseDescription = "Osteoarthritis (OA) is a joint disease characterized by the gradual wear and tear of the cartilage tissue that covers the bones in the joint. It is the most common type of arthritis and one of the most common causes of joint pain."
                    },
                    new Disease
                    {
                        DiseaseName = "Diabet",
                        DiseaseDescription = "Diabetes mellitus, often known simply as diabetes, is a group of common endocrine diseases characterized by sustained high blood sugar levels. Diabetes is due to either the pancreas not producing enough insulin, or the cells of the body becoming unresponsive to the hormone's effects. Untreated or poorly treated diabetes accounts for approximately 1.5 million deaths every year."
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
                                            QuestionText = "Taking a bath",
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
                                                    AnswerText = "Controls sitting with hands.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "uses the back of the feet relative to the chair to control sitting.",
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
                                            QuestionText = "Transfer (Place the chair(s) as a guide for transfer. Ask the patient to walk one way to a chair with armrests and the other way to a chair without armrests. You can use two chairs (one with and one without armrests) or a bed and a chair. )",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Able to walk safely with little use of hands.",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Able to walk safely, hands needed.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Able to pass with a whimper and/or under supervision.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "One person is needed to help.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Two people are required to assist or monitor safety.",
                                                    AnswerValue = 0
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Standing without support with eyes closed (Please close your eyes and stand still for 10 seconds)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Can stand for 10 seconds safely.",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Can stand for 10 seconds under control.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Can stand for 3 seconds.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Unable to keep eyes closed for 3 seconds but standing safely.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Needs help not to fall.",
                                                    AnswerValue = 0
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Standing without support with feet together (Put your feet together and stand without support)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Able to put feet together independently and stand for 1 minute safely.",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Able to put feet together independently and stand for 1 minute under control.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Able to put legs together independently but unable to stand for 30 seconds.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Needs assistance to achieve proper posture but can stand for 15 seconds with feet together.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Needs assistance to achieve proper posture and is unable to stand for 15 seconds.",
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
                                    QuestionnaireName = "Problem Areas In Diabetes (PAID)",
                                    QuestionnaireDescription = "The Problem Areas In Diabetes (PAID) scale is a well-validated, psychometrically robust questionnaire with 20 items.",
                                    Questions = new List<Question>
                                    {
                                        new Question
                                        {
                                            QuestionText = "Not having clear and concrete goals for your diabetes care?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Minor problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Feeling discouraged with your diabetes treatment plan?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Minor problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Feeling scared when you think about living with diabetes?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Minor problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Uncomfortable social situations related to your diabetes care (e.g. people telling you what to eat)?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Minor problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 4
                                                }                                           
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Feelings of deprivation regarding food and meals?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Minor problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Feeling depressed when you think about living with diabetes?",
                                           Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Minor problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Not knowing if your mood or feelings are related to your diabetes?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Minor problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Feeling overwhelmed by your diabetes?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Minor problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Worrying about low blood glucose reactions?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Minor problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Feeling angry when you think about living with diabetes?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Minor problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Feeling constantly concerned about food and eating?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Minor problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Worrying about the future and the possibility of serious complications?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Minor problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Feelings of guilt or anxiety when you get off track with your diabetes management?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Minor problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Not accepting your diabetes?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Minor problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Feeling unsatisfied with your diabetes physician?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Minor problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Feeling that diabetes is taking up too much of your mental and physical energy every day?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Minor problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Feeling alone with your diabetes?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Minor problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Feeling that your friends and family are not supportive of your diabetes management efforts?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Minor problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Coping with complications of diabetes?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Minor problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Feeling burned out by the constant effort needed to manage diabetes?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Minor problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 4
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
                                    QuestionnaireName = "Diabetes Distress Scale (DDS-17)",
                                    QuestionnaireDescription = "Living with diabetes can sometimes be tough. There may be many problems and hassles concerning diabetes and they can vary greatly in severity. Problems may range from minor hassles to major life difficulties. Listed below are 17 potential problem areas that people with diabetes may experience.",
                                    Questions = new List<Question>
                                    {
                                        new Question
                                        {
                                            QuestionText = "Feeling that diabetes is taking up too much of my mental and physical energy every day.",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very serious problem",
                                                    AnswerValue = 6
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Feeling that my doctor doesn’t know enough about diabetes and diabetes care.",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very serious problem",
                                                    AnswerValue = 6
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Not feeling confident in my day-to-day ability to manage diabetes.",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very serious problem",
                                                    AnswerValue = 6
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Feeling angry, scared, and/or depressed when I think about living with diabetes.",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very serious problem",
                                                    AnswerValue = 6
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Feeling that my doctor doesn’t give me clear enough directions on how to manage my diabetes.",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very serious problem",
                                                    AnswerValue = 6
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Feeling that I am not testing my blood sugars frequently enough.",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very serious problem",
                                                    AnswerValue = 6
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Feeling that I will end up with serious long-term complications, no matter what I do.",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very serious problem",
                                                    AnswerValue = 6
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Feeling that I am often failing with my diabetes routine.",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very serious problem",
                                                    AnswerValue = 6
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Feeling that friends or family are not supportive enough of self-care efforts (e.g., planning activities that conflict with my schedule, encouraging me to eat the “wrong” foods).",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very serious problem",
                                                    AnswerValue = 6
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Feeling that diabetes controls my life.",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very serious problem",
                                                    AnswerValue = 6
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Feeling that my doctor doesn’t take my concerns seriously enough.",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very serious problem",
                                                    AnswerValue = 6
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Feeling that I am not sticking closely enough to a good meal plan.",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very serious problem",
                                                    AnswerValue = 6
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Feeling that friends or family don’t appreciate how difficult living with diabetes can be.",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very serious problem",
                                                    AnswerValue = 6
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Feeling overwhelmed by the demands of living with diabetes.",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very serious problem",
                                                    AnswerValue = 6
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Feeling that I don’t have a doctor who I can see regularly enough about my diabetes.",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very serious problem",
                                                    AnswerValue = 6
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Not feeling motivated to keep up my diabetes self management.",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very serious problem",
                                                    AnswerValue = 6
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Feeling that friends or family don’t give me the emotional support that I would like.",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not a problem",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight problem",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate problem",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Somewhat serious problem",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Serious problem",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very serious problem",
                                                    AnswerValue = 6
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
                                    QuestionnaireName = "Insulin Treatment Appraisal Scale (ITAS)",
                                    QuestionnaireDescription = "The following questions are about your perception of taking insulin for your diabetes. If you do not use insulin therapy, please answer each question from your current knowledge and thoughts about what insulin therapy would be like.",
                                    Questions = new List<Question>
                                    {
                                        new Question
                                        {
                                            QuestionText = "Taking insulin means I have failed to manage my diabetes with diet and pills",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Strongly disagree",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Disagree",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Neither agree nor disagree",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Agree",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Strongly agree",
                                                    AnswerValue = 5
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Taking insulin means my diabetes has become much worse",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Strongly disagree",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Disagree",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Neither agree nor disagree",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Agree",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Strongly agree",
                                                    AnswerValue = 5
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Taking insulin helps to prevent complications of diabetes",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Strongly disagree",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Disagree",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Neither agree nor disagree",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Agree",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Strongly agree",
                                                    AnswerValue = 5
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Taking insulin means other people see me as a sicker person",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Strongly disagree",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Disagree",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Neither agree nor disagree",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Agree",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Strongly agree",
                                                    AnswerValue = 5
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Taking insulin makes life less flexible",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Strongly disagree",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Disagree",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Neither agree nor disagree",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Agree",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Strongly agree",
                                                    AnswerValue = 5
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "I’m afraid of injecting myself with a needle",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Strongly disagree",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Disagree",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Neither agree nor disagree",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Agree",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Strongly agree",
                                                    AnswerValue = 5
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Taking insulin increases the risk of low blood glucose levels (hypoglycemia) ",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Strongly disagree",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Disagree",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Neither agree nor disagree",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Agree",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Strongly agree",
                                                    AnswerValue = 5
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Taking insulin helps to improve my health",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Strongly disagree",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Disagree",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Neither agree nor disagree",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Agree",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Strongly agree",
                                                    AnswerValue = 5
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Insulin causes weight gain",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Strongly disagree",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Disagree",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Neither agree nor disagree",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Agree",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Strongly agree",
                                                    AnswerValue = 5
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Managing insulin injections takes a lot of time and energy",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Strongly disagree",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Disagree",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Neither agree nor disagree",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Agree",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Strongly agree",
                                                    AnswerValue = 5
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Taking insulin means I have to give up activities I enjoy",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Strongly disagree",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Disagree",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Neither agree nor disagree",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Agree",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Strongly agree",
                                                    AnswerValue = 5
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Taking insulin means my health will deteriorate",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Strongly disagree",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Disagree",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Neither agree nor disagree",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Agree",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Strongly agree",
                                                    AnswerValue = 5
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Injecting insulin is embarrassing",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Strongly disagree",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Disagree",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Neither agree nor disagree",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Agree",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Strongly agree",
                                                    AnswerValue = 5
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Injecting insulin is painful",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Strongly disagree",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Disagree",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Neither agree nor disagree",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Agree",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Strongly agree",
                                                    AnswerValue = 5
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "It is difficult to inject the right amount of insulin correctly at the right time every day",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Strongly disagree",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Disagree",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Neither agree nor disagree",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Agree",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Strongly agree",
                                                    AnswerValue = 5
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Taking insulin makes it more difficult to fulfill my responsibilities (at work, at home)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Strongly disagree",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Disagree",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Neither agree nor disagree",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Agree",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Strongly agree",
                                                    AnswerValue = 5
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Taking insulin helps to maintain good control of blood glucose",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Strongly disagree",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Disagree",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Neither agree nor disagree",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Agree",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Strongly agree",
                                                    AnswerValue = 5
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Being on insulin causes family and friends to be more concerned about me",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Strongly disagree",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Disagree",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Neither agree nor disagree",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Agree",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Strongly agree",
                                                    AnswerValue = 5
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Taking insulin helps to improve my energy level",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Strongly disagree",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Disagree",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Neither agree nor disagree",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Agree",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Strongly agree",
                                                    AnswerValue = 5
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Taking insulin makes me more dependent on my doctor",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Strongly disagree",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Disagree",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Neither agree nor disagree",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Agree",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Strongly agree",
                                                    AnswerValue = 5
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
                        TreatmentStrategyName = "Strategy 3",
                        TreatmentStrategyDescription = "Another treatment strategy description for strategy 2.",
                        QuestionnaireTreatmentStrategy = new List<QuestionnaireTreatmentStrategy>
                        {
                            new QuestionnaireTreatmentStrategy
                            {
                                Questionnaire = new Questionnaire
                                {
                                    QuestionnaireName = "WOMAC (Western Ontario and McMaster Universities Osteoarthritis Index)",
                                    QuestionnaireDescription = "It assesses pain, stiffness, and physical function in individuals with hip or knee osteoarthritis.",
                                    Questions = new List<Question>
                                    {
                                        new Question
                                        {
                                            QuestionText = "Pain while Walking",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extremely",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Pain while Stair Climbing",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extremely",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Pain while Nocturnal",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extremely",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Pain while Rest",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extremely",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Pain while Weight bearing",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extremely",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        
                                        new Question
                                        {
                                            QuestionText = "Stiffness while Morning stiffness",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extremely",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Stiffness while Stiffness occurring later in the day",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extremely",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },

                                        new Question
                                        {
                                            QuestionText = "Physical Function while Descending stairs",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extremely",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Physical Function while Ascending stairs",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extremely",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Physical Function while Rising from sitting",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extremely",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Physical Function while Standing",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extremely",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Physical Function while Bending to floor",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extremely",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Physical Function while Walking on flat surface",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extremely",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Physical Function while Getting in / out of car",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extremely",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Physical Function while Going shopping",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extremely",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Physical Function while Putting on socks",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extremely",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Physical Function while Lying in bed",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extremely",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Physical Function while Taking off socks",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extremely",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Physical Function while Rising from bed",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extremely",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Physical Function while Getting in/out of bath",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extremely",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Physical Function while Sitting",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extremely",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Physical Function while Getting on/off toilet",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extremely",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Physical Function while Heavy domestic duties",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extremely",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Physical Function while Light domestic duties ",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Slight",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Very",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extremely",
                                                    AnswerValue = 4
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
                                    QuestionnaireName = "Knee Injury and Osteoarthritis Outcome Score (KOOS)",
                                    QuestionnaireDescription = "The Knee Injury and Osteoarthritis Outcome Score (KOOS) is a questionnaire designed to assess short and long-term patient-relevant outcomes following knee injury.",
                                    Questions = new List<Question>
                                    {
                                        new Question
                                        {
                                            QuestionText = "How often is your knee painful?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Never",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Monthly",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Weekly",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Daily",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Always",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What degree of pain have you experienced the last week when Twisting/pivoting on your knee?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What degree of pain have you experienced the last week when Straightening knee fully?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What degree of pain have you experienced the last week when Bending knee fully?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What degree of pain have you experienced the last week when Walking on flat surface?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What degree of pain have you experienced the last week when Going up or down stairs?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What degree of pain have you experienced the last week when At night while in bed?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What degree of pain have you experienced the last week when Sitting or lying?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What degree of pain have you experienced the last week when Standing upright?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },

                                        new Question
                                        {
                                            QuestionText = "How severe is your knee stiffness after first wakening in the morning?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "How severe is your knee stiffness after sitting, lying, or resting later in the day?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Do you have swelling in your knee?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Never",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Rarely",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Sometimes",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Often",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Always",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Do you feel grinding, hear clicking or any other type of noise when your knee moves?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Never",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Rarely",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Sometimes",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Often",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Always",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Does your knee catch or hang up when moving?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Never",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Rarely",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Sometimes",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Often",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Always",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Can you straighten your knee fully?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Always",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Often",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Sometimes",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Rarely",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Never",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Can you bend your knee fully?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Always",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Often",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Sometimes",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Rarely",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Never",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },

                                        new Question
                                        {
                                            QuestionText = "What difficulty have you experienced the last week Descending?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What difficulty have you experienced the last week Ascending stairs?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What difficulty have you experienced the last week Rising from sitting?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What difficulty have you experienced the last week Standing?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What difficulty have you experienced the last week Bending to floor/picking up an object?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What difficulty have you experienced the last week Walking on flat surface?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question    
                                        {
                                            QuestionText = "What difficulty have you experienced the last week Getting in/out of car?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What difficulty have you experienced the last week Going shopping?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What difficulty have you experienced the last week Putting on socks/stockings?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What difficulty have you experienced the last week Rising from bed?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What difficulty have you experienced the last week Taking off socks/stockings?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What difficulty have you experienced the last week  Lying in bed (turning over, maintaining knee position)?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What difficulty have you experienced the last week Getting in/out of bath?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What difficulty have you experienced the last week Sitting?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What difficulty have you experienced the last week Getting on/off toilet?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What difficulty have you experienced the last week Heavy domestic duties (shovelling, scrubbing floors, etc)?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What difficulty have you experienced the last week Light domestic duties (cooking, dusting, etc)?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },

                                        new Question
                                        {
                                            QuestionText = "What difficulty have you experienced the last week Squatting?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What difficulty have you experienced the last week Running?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What difficulty have you experienced the last week Jumping?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What difficulty have you experienced the last week Turning/twisting on your injured knee?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What difficulty have you experienced the last week Kneeling?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },

                                        new Question
                                        {
                                            QuestionText = "How often are you aware of your knee problems?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Never",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Monthly",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Weekly",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Daily",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Always",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Have you modified your lifestyle to avoid potentially damaging activities to your knee?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not at all",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mildly",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderately",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severely",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Totally",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "How troubled are you with lack of confidence in your knee?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not at all",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mildly",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderately",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severely",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Totally",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "In general, how much difficulty do you have with your knee?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
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
                                    QuestionnaireName = "HOOS (Hip disability and Osteoarthritis Outcome Score)",
                                    QuestionnaireDescription = "HOOS designed for individuals with hip osteoarthritis, it assesses symptoms, pain, daily living function, sport and recreational function, and hip-related quality of life.",
                                    Questions = new List<Question>
                                    {
                                        new Question
                                        {
                                            QuestionText = "Do you feel grinding, hear clicking, or any other type of noise from your hip?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Never",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Rarely",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Sometimes",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Often",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Always",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Difficulties spreading legs wide apart",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Difficulties to stride out when walking",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "How severe is your hip joint stiffness after first wakening in the morning?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "How severe is your hip stiffness after sitting, lying, or resting later in the day?\r\n",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },

                                        new Question    
                                        {
                                            QuestionText = "How often is your hip painful?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Never",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Monthly",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Weekly",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Daily",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Always",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What amount of hip pain have you experienced the last week during the Straightening your hip fully",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What amount of hip pain have you experienced the last week during the Bending your hip fully",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Never",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Rarely",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Sometimes",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Often",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Always",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What amount of hip pain have you experienced the last week during the Walking on a flat surface",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Never",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Rarely",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Sometimes",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Often",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Always",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What amount of hip pain have you experienced the last week during the Going up or down stairs\r\n",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Never",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Rarely",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Sometimes",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Often",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Always",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What amount of hip pain have you experienced the last week during the At night while in bed",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Always",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Often",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Sometimes",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Rarely",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Never",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What amount of hip pain have you experienced the last week during the Sitting or lying",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Always",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Often",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Sometimes",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Rarely",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Never",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What amount of hip pain have you experienced the last week during the Standing upright",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What amount of hip pain have you experienced the last week during the Walking on a hard surface (asphalt, concrete, etc.)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Never",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Rarely",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Sometimes",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Often",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Always",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What amount of hip pain have you experienced the last week during the Walking on an uneven surface",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Never",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Rarely",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Sometimes",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Often",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Always",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },

                                        new Question
                                        {
                                            QuestionText = "Indicate the degree of difficulty you have experienced in the last week due to your hip, while Descending stairs",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Indicate the degree of difficulty you have experienced in the last week due to your hip, while Ascending stairs",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Indicate the degree of difficulty you have experienced in the last week due to your hip, while Rising from sitting",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question    
                                        {
                                            QuestionText = "Indicate the degree of difficulty you have experienced in the last week due to your hip, while Standing",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Indicate the degree of difficulty you have experienced in the last week due to your hip, while Bending to the floor/pick up an object",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Indicate the degree of difficulty you have experienced in the last week due to your hip, while Walking on a flat surface",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Indicate the degree of difficulty you have experienced in the last week due to your hip, while Getting in/out of car",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Indicate the degree of difficulty you have experienced in the last week due to your hip, while Going shopping",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Indicate the degree of difficulty you have experienced in the last week due to your hip, while Putting on socks/stockings",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Indicate the degree of difficulty you have experienced in the last week due to your hip, while Rising from bed",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Indicate the degree of difficulty you have experienced in the last week due to your hip, while Taking off socks/stockings",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Indicate the degree of difficulty you have experienced in the last week due to your hip, while Lying in bed (turning over, maintaining hip position)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Indicate the degree of difficulty you have experienced in the last week due to your hip, while Getting in/out of bath",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Indicate the degree of difficulty you have experienced in the last week due to your hip, while Sitting",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Indicate the degree of difficulty you have experienced in the last week due to your hip, while Getting on/off toilet",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Indicate the degree of difficulty you have experienced in the last week due to your hip, while Heavy domestic duties (moving heavy boxes, scrubbing floors, etc)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Indicate the degree of difficulty you have experienced in the last week due to your hip, while Light domestic duties (cooking, dusting, etc)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },

                                        new Question
                                        {
                                            QuestionText = "What difficulty have you experienced the last week Squatting?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What difficulty have you experienced the last week Running?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What difficulty have you experienced the last week Twisting/pivoting on loaded leg?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "What difficulty have you experienced the last week Walking on uneven surface?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },

                                        new Question
                                        {
                                            QuestionText = "How often are you aware of your hip problems?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Never",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Monthly",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Weekly",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Daily",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Always",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Have you modified your lifestyle to avoid potentially damaging to your hip?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not at all",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mildly",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderately",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severely",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Totally",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "How troubled are you with lack of confidence in your hip?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Not at all",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mildly",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderately",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severely",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Totally",
                                                    AnswerValue = 4
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "In general, how much difficulty do you have with your hip?",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "None",
                                                    AnswerValue = 0
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Mild",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Moderate",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Severe",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Extreme",
                                                    AnswerValue = 4
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
                        BirthDate = new DateTime(2023, 8, 10),
                        RehabilitationStartDate = DateTime.Now,
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
                                        .Where(x => x.DiseaseName.Equals("Diabet")) // replace later with the name of a real disease from Diseases
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
                                        .Where(x => x.TreatmentStrategyName.Equals("Strategy 2")) // replace later with the name of a real TreamentStrategy from TreatmentStrategies
                                        .Select(x => x.TreatmentStrategyId).First(),
                        DiseaseId = context.Diseases
                                        .Where(x => x.DiseaseName.Equals("Diabet")) // replace later with the name of a real disease from Diseases
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
                                        .Where(x => x.DiseaseName.Equals("Diabet")) // replace later with the name of a real disease from Diseases
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
                    }
                );

                context.SaveChanges();

                // перед додаванням результатів витягнемо собі допоміжну інформацію, щоб менше дублювати
                context.Results.AddRange(
                new Models.Results
                    {
                        Date = new DateTime(2023, 8, 11),
                        isSaved = true,
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
                new Models.Results
                {
                    Date = new DateTime(2023, 8, 18),
                    isSaved = true,
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
                                    AnswerText = "Helpless - the patient must be fed.",
                                    AnswerValue = 0
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Transfer from bed to wheelchair and back",
                                    AnswerText = "Dependent - the patient cannot bring himself to a sitting position.",
                                    AnswerValue = 0
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Personal hygiene",
                                    AnswerText = "The patient needs help.",
                                    AnswerValue = 0
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Using the toilet",
                                    AnswerText = "The patient needs help due to a disturbance in balance or when putting on and taking off clothes, or when using toilet paper.",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking a bath",
                                    AnswerText = "The patient can independently take a bath, shower, or completely wipe the body with a sponge. He should be able to do all the necessary steps, depending on which method is used, without the help of another person.",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Moving on a flat plane",
                                    AnswerText = "If the patient cannot move, but can drive a wheelchair independently. He must be able to go around corners, turn around, maneuver from a chair to a table, bed, toilet, etc. He must be able to drive a cart at least 50 meters.",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Climbing and descending stairs",
                                    AnswerText = "The patient is unable to go up and down stairs. He needs a lift.",
                                    AnswerValue = 0
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Dressing and undressing (patients should not consider wearing a bra or girdle unless specified items of clothing are available)",
                                    AnswerText = "The patient needs help.",
                                    AnswerValue = 0
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Defecation control",
                                    AnswerText = "The patient needs help using suppositories or enemas or has occasional problem cases.",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Urinary control",
                                    AnswerText = "Incontinence.",
                                    AnswerValue = 0
                                }
                            }
                    }
                },
                new Models.Results
                {
                    Date = new DateTime(2023, 8, 20),
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
                                    AnswerText = "Some help is needed (with cutting food, etc., as mentioned above).",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Transfer from bed to wheelchair and back",
                                    AnswerText = "The patient can bring himself to a sitting position without the help of another person, but he must be lifted from the bed or, if he moves, with considerable assistance.",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Personal hygiene",
                                    AnswerText = "The patient can independently wash his face and hands, comb his hair, brush his teeth and shave. He can use any type of razor, but must put down the blade or turn on the razor without assistance, and must retrieve it from a drawer or cabinet. Patients should apply their own make-up, if used, but do not have to braid their hair or style their hair.",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Using the toilet",
                                    AnswerText = "The patient needs help due to a disturbance in balance or when putting on and taking off clothes, or when using toilet paper.",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking a bath",
                                    AnswerText = "The patient can independently take a bath, shower, or completely wipe the body with a sponge. He should be able to do all the necessary steps, depending on which method is used, without the help of another person.",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Moving on a flat plane",
                                    AnswerText = "The patient requires assistance or supervision for all of the walking activities described above, but can walk at least 50 meters with little assistance.",
                                    AnswerValue = 10
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Climbing and descending stairs",
                                    AnswerText = "The patient needs help or supervision when performing any of the above items.",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Dressing and undressing (patients should not consider wearing a bra or girdle unless specified items of clothing are available)",
                                    AnswerText = "The patient needs help in putting on, taking off or fastening any clothes. He should do at least half of the work himself. He must do it in a reasonable amount of time.",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Defecation control",
                                    AnswerText = "The patient needs help using suppositories or enemas or has occasional problem cases.",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Urinary control",
                                    AnswerText = "The patient has intermittent problem cases, or can't wait for the undercarriage, or get to the toilet in time, or needs help with the catheter.",
                                    AnswerValue = 5
                                }
                            }
                    }
                },
                new Models.Results
                {
                    Date = new DateTime(2023, 8, 31),
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
                                    AnswerText = "Independent - the patient can eat independently from a tray or table when someone serves food within his reach. He should set up the utensils if necessary, cut the food, use salt and pepper, spread butter on the bread, etc. He must do it in a reasonable amount of time.",
                                    AnswerValue = 10
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Transfer from bed to wheelchair and back",
                                    AnswerText = "Some minimal assistance is required at some stage of the activity or the patient needs to be reminded or supervised to ensure the safety of one or more stages of the activity.",
                                    AnswerValue = 10
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Personal hygiene",
                                    AnswerText = "The patient can independently wash his face and hands, comb his hair, brush his teeth and shave. He can use any type of razor, but must put down the blade or turn on the razor without assistance, and must retrieve it from a drawer or cabinet. Patients should apply their own make-up, if used, but do not have to braid their hair or style their hair.",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Using the toilet",
                                    AnswerText = "The patient can independently use the toilet, take off and put on clothes, prevent soiling of clothes and use toilet paper without help. He can use handrails on the wall or another stable object for support if necessary. If it is necessary to use a tray instead of a toilet, he must be able to place it on the chair, clean and wash it.",
                                    AnswerValue = 10
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking a bath",
                                    AnswerText = "The patient can independently take a bath, shower, or completely wipe the body with a sponge. He should be able to do all the necessary steps, depending on which method is used, without the help of another person.",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Moving on a flat plane",
                                    AnswerText = "The patient can walk at least 50 meters without assistance or supervision. He can wear orthotics or prostheses and use crutches, sticks, but not walkers. He must be able, if necessary, to lock and unlock the orthopedic device, let's say from a standing position, and to sit down, bring all the necessary mechanisms into proper condition for further use and get rid of them when he is sitting (put on and take off the corset under clothes).",
                                    AnswerValue = 15
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Climbing and descending stairs",
                                    AnswerText = "The patient needs help or supervision when performing any of the above items.",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Dressing and undressing (patients should not consider wearing a bra or girdle unless specified items of clothing are available)",
                                    AnswerText = "The patient is able to put on and take off and fasten all clothing, tie shoelaces (if no device is required for this). This type of daily activity includes donning, removing, and securing a corset or brace when prescribed. If necessary, such special elements of clothing as suspenders, moccasins, dresses that unfasten in the front can be used.",
                                    AnswerValue = 10
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Defecation control",
                                    AnswerText = "The patient is able to control defecation and does not have any problem cases. He may use a suppository or enema when necessary (as for patients with spinal cord injuries undergoing bowel preparation).",
                                    AnswerValue = 10
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Urinary control",
                                    AnswerText = "The patient can control urination during the day and at night. Patients with spinal cord injuries who wear an external catheter and leg bag must independently insert, clean and empty the bag and stay dry day and night.",
                                    AnswerValue = 10
                                }
                            }
                    }
                },

                new Models.Results
                {
                    Date = new DateTime(2023, 8, 25),
                    isSaved = false,
                    PatientId = context.Patients
                            .Where(x => x.Email.Equals("ethan.reynolds@lq.com"))
                            .Select(x => x.PatientId).First(),
                    QuestionnaireId = context.Questionnaires
                            .Where(x => x.QuestionnaireName.Equals("Berg Balance Test (BBS)"))
                            .Select(x => x.QuestionnaireId).First(),
                    ResultsPatientAnswers = new List<ResultsPatientAnswer>
                    {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Stand up from a sitting position (Please stand up. Try not to use your arm for support.)",
                                    AnswerText = "Can stand up with hands after a few tries.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Stand without support (Please stand for two minutes without support.)",
                                    AnswerText = "Able to stand for 30 seconds without support.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Sitting without back support, but with fixed legs on the floor or a chair (Please sit with arms folded for 2 minutes)",
                                    AnswerText = "Able to sit for 10 seconds.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Sitting down from a standing position (Please sit down)",
                                    AnswerText = "Sits independently, but the sitting process is uncontrolled.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Transfer (Place the chair(s) as a guide for transfer. Ask the patient to walk one way to a chair with armrests and the other way to a chair without armrests. You can use two chairs (one with and one without armrests) or a bed and a chair. )",
                                    AnswerText = "Able to walk safely, hands needed.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Standing without support with eyes closed (Please close your eyes and stand still for 10 seconds)",
                                    AnswerText = "Can stand for 3 seconds.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Standing without support with feet together (Put your feet together and stand without support)",
                                    AnswerText = "Needs assistance to achieve proper posture and is unable to stand for 15 seconds.",
                                    AnswerValue = 0
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Standing forward bend with outstretched arm (Raise your arm at a 90-degree angle. Extend your fingers and bend as far as you can. (The examiner places a ruler near the fingertips when the arm is at a 90-degree angle. The fingers should not touch the ruler when bent. Control result is the distance reached by the fingers when the patient is in the maximum tilt position.If possible, ask the patient to use both hands when tilting to avoid rotation of the spine.))",
                                    AnswerText = "Leans forward but needs control.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Picking up an object from the floor while standing (Pick up the shoe/slipper that is in front of your feet)",
                                    AnswerText = "Cannot pick up, but reaches within 2–5 cm (1–2 inches) of the slipper and balances independently.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Looking over the left and right shoulder while standing (Look back to look directly over the left shoulder. Repeat to the right. (The examiner can choose any object behind the patient to look directly at the object to facilitate better rotation.))",
                                    AnswerText = "Only turns sideways, but maintains balance.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "360 Degree Turn (Turn completely around you. Pause. Then turn in the other direction)",
                                    AnswerText = "Careful supervision or verbal support is required.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Standing foot on a step or chair without support (Place each foot alternately on the step/chair. Continue until each foot touches the step/chair four times)",
                                    AnswerText = "Able to take more than 2 steps, needs minimal assistance.",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Standing without support with one foot in front ((Demonstrate to the patient) Place one foot directly in front of the other. If you feel that you cannot place the foot directly in front, try a little further so that the heel of your front foot is in front of the toes of the other foot. (For in order to score 3 points, the length of the step should not exceed the length of the other leg and the width of the posture should be close to the normal width of the patient's step.))",
                                    AnswerText = "Able to take a small step independently and hold the pose for 30 seconds.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Standing on one leg (Stand on one leg as long as you can without support)",
                                    AnswerText = "Tries to raise a leg, unable to hold for 3 seconds, but stands on his own.",
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
                            .Where(x => x.QuestionnaireName.Equals("Berg Balance Test (BBS)"))
                            .Select(x => x.QuestionnaireId).First(),
                    ResultsPatientAnswers = new List<ResultsPatientAnswer>
                    {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Stand up from a sitting position (Please stand up. Try not to use your arm for support.)",
                                    AnswerText = "Can stand up independently with hands.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Stand without support (Please stand for two minutes without support.)",
                                    AnswerText = "Able to stand for 2 minutes under supervision.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Sitting without back support, but with fixed legs on the floor or a chair (Please sit with arms folded for 2 minutes)",
                                    AnswerText = "Able to sit for 2 minutes under supervision.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Sitting down from a standing position (Please sit down)",
                                    AnswerText = "Able to walk safely, hands needed.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Transfer (Place the chair(s) as a guide for transfer. Ask the patient to walk one way to a chair with armrests and the other way to a chair without armrests. You can use two chairs (one with and one without armrests) or a bed and a chair. )",
                                    AnswerText = "Able to walk safely, hands needed.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Standing without support with eyes closed (Please close your eyes and stand still for 10 seconds)",
                                    AnswerText = "Can stand for 10 seconds under control.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Standing without support with feet together (Put your feet together and stand without support)",
                                    AnswerText = "Able to put feet together independently and stand for 1 minute under control.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Standing forward bend with outstretched arm (Raise your arm at a 90-degree angle. Extend your fingers and bend as far as you can. (The examiner places a ruler near the fingertips when the arm is at a 90-degree angle. The fingers should not touch the ruler when bent. Control result is the distance reached by the fingers when the patient is in the maximum tilt position.If possible, ask the patient to use both hands when tilting to avoid rotation of the spine.))",
                                    AnswerText = "Can reach 5 cm (2 in).",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Picking up an object from the floor while standing (Pick up the shoe/slipper that is in front of your feet)",
                                    AnswerText = "Able to lift shoe, but needs supervision.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Looking over the left and right shoulder while standing (Look back to look directly over the left shoulder. Repeat to the right. (The examiner can choose any object behind the patient to look directly at the object to facilitate better rotation.))",
                                    AnswerText = "Only turns sideways, but maintains balance.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "360 Degree Turn (Turn completely around you. Pause. Then turn in the other direction)",
                                    AnswerText = "Able to turn 360 degrees safely one way only in 4 seconds or less.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Standing foot on a step or chair without support (Place each foot alternately on the step/chair. Continue until each foot touches the step/chair four times)",
                                    AnswerText = "Can stand independently and takes 8 steps in more than 20 seconds.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Standing without support with one foot in front ((Demonstrate to the patient) Place one foot directly in front of the other. If you feel that you cannot place the foot directly in front, try a little further so that the heel of your front foot is in front of the toes of the other foot. (For in order to score 3 points, the length of the step should not exceed the length of the other leg and the width of the posture should be close to the normal width of the patient's step.))",
                                    AnswerText = "Able to take a small step independently and hold the pose for 30 seconds.",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Standing on one leg (Stand on one leg as long as you can without support)",
                                    AnswerText = "Able to raise a leg independently and hold for less than 3 seconds.",
                                    AnswerValue = 2
                                }
                            }
                    }
                },
                new Models.Results
                {
                    Date = new DateTime(2023, 9, 15),
                    isSaved = false,
                    PatientId = context.Patients
                            .Where(x => x.Email.Equals("ethan.reynolds@lq.com"))
                            .Select(x => x.PatientId).First(),
                    QuestionnaireId = context.Questionnaires
                            .Where(x => x.QuestionnaireName.Equals("Berg Balance Test (BBS)"))
                            .Select(x => x.QuestionnaireId).First(),
                    ResultsPatientAnswers = new List<ResultsPatientAnswer>
                    {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Stand up from a sitting position (Please stand up. Try not to use your arm for support.)",
                                    AnswerText = "Can stand up without using hands and self-stabilize.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Stand without support (Please stand for two minutes without support.)",
                                    AnswerText = "Able to stand safely for 2 minutes.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Sitting without back support, but with fixed legs on the floor or a chair (Please sit with arms folded for 2 minutes)",
                                    AnswerText = "Able to sit safely and securely for 2 minutes.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Sitting down from a standing position (Please sit down)",
                                    AnswerText = "Controls sitting with hands.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Transfer (Place the chair(s) as a guide for transfer. Ask the patient to walk one way to a chair with armrests and the other way to a chair without armrests. You can use two chairs (one with and one without armrests) or a bed and a chair. )",
                                    AnswerText = "Able to walk safely with little use of hands.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Standing without support with eyes closed (Please close your eyes and stand still for 10 seconds)",
                                    AnswerText = "Can stand for 10 seconds safely.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Standing without support with feet together (Put your feet together and stand without support)",
                                    AnswerText = "Able to put feet together independently and stand for 1 minute under control.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Standing forward bend with outstretched arm (Raise your arm at a 90-degree angle. Extend your fingers and bend as far as you can. (The examiner places a ruler near the fingertips when the arm is at a 90-degree angle. The fingers should not touch the ruler when bent. Control result is the distance reached by the fingers when the patient is in the maximum tilt position.If possible, ask the patient to use both hands when tilting to avoid rotation of the spine.))",
                                    AnswerText = "Can reach 12 cm (5 in).",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Picking up an object from the floor while standing (Pick up the shoe/slipper that is in front of your feet)",
                                    AnswerText = "Able to take a slipper easily and safely.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Looking over the left and right shoulder while standing (Look back to look directly over the left shoulder. Repeat to the right. (The examiner can choose any object behind the patient to look directly at the object to facilitate better rotation.))",
                                    AnswerText = "It looks smaller on one side than on the other; less weight transfer.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "360 Degree Turn (Turn completely around you. Pause. Then turn in the other direction)",
                                    AnswerText = "Can stand independently and safely and takes 8 steps within 20 seconds.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Standing foot on a step or chair without support (Place each foot alternately on the step/chair. Continue until each foot touches the step/chair four times)",
                                    AnswerText = "Can stand independently and takes 8 steps in more than 20 seconds.",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Standing without support with one foot in front ((Demonstrate to the patient) Place one foot directly in front of the other. If you feel that you cannot place the foot directly in front, try a little further so that the heel of your front foot is in front of the toes of the other foot. (For in order to score 3 points, the length of the step should not exceed the length of the other leg and the width of the posture should be close to the normal width of the patient's step.))",
                                    AnswerText = "Can place the feet \"goose\" independently and hold the pose for 30 seconds.",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Standing on one leg (Stand on one leg as long as you can without support)",
                                    AnswerText = "Able to lift leg independently and hold for > 10 seconds.",
                                    AnswerValue = 4
                                }
                            }
                    }
                },


                new Models.Results
                {
                    Date = new DateTime(2023, 8, 11),
                    isSaved = false,
                    PatientId = context.Patients
                            .Where(x => x.Email.Equals("sebastian.carter@lq.com"))
                            .Select(x => x.PatientId).First(),
                    QuestionnaireId = context.Questionnaires
                            .Where(x => x.QuestionnaireName.Equals("Problem Areas In Diabetes (PAID)"))
                            .Select(x => x.QuestionnaireId).First(),
                    ResultsPatientAnswers = new List<ResultsPatientAnswer>
                    {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Not having clear and concrete goals for your diabetes care?",
                                    AnswerText = "Serious problem",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling discouraged with your diabetes treatment plan?",
                                    AnswerText = "Somewhat serious problem",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling scared when you think about living with diabetes?",
                                    AnswerText = "Serious problem",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Uncomfortable social situations related to your diabetes care (e.g. people telling you what to eat)?",
                                    AnswerText = "Somewhat serious problem",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feelings of deprivation regarding food and meals?",
                                    AnswerText = "Serious problem",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling depressed when you think about living with diabetes?",
                                    AnswerText = "Serious problem",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Not knowing if your mood or feelings are related to your diabetes?",
                                    AnswerText = "Moderate problem",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling overwhelmed by your diabetes?",
                                    AnswerText = "Somewhat serious problem",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Worrying about low blood glucose reactions?",
                                    AnswerText = "Serious problem",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling angry when you think about living with diabetes?",
                                    AnswerText = "Somewhat serious problem",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling constantly concerned about food and eating?",
                                    AnswerText = "Moderate problem",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Worrying about the future and the possibility of serious complications?",
                                    AnswerText = "Serious problem",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feelings of guilt or anxiety when you get off track with your diabetes management?",
                                    AnswerText = "Somewhat serious problem",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Not accepting your diabetes?",
                                    AnswerText = "Serious problem",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling unsatisfied with your diabetes physician?",
                                    AnswerText = "Serious problem",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that diabetes is taking up too much of your mental and physical energy every day?",
                                    AnswerText = "Somewhat serious problem",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling alone with your diabetes?",
                                    AnswerText = "Serious problem",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that your friends and family are not supportive of your diabetes management efforts?",
                                    AnswerText = "Moderate problem",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Coping with complications of diabetes?",
                                    AnswerText = "Somewhat serious problem",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling burned out by the constant effort needed to manage diabetes?",
                                    AnswerText = "Somewhat serious problem",
                                    AnswerValue = 3
                                }
                            }
                    }
                },
                new Models.Results
                {
                    Date = new DateTime(2023, 8, 18),
                    isSaved = false,
                    PatientId = context.Patients
                            .Where(x => x.Email.Equals("sebastian.carter@lq.com"))
                            .Select(x => x.PatientId).First(),
                    QuestionnaireId = context.Questionnaires
                            .Where(x => x.QuestionnaireName.Equals("Problem Areas In Diabetes (PAID)"))
                            .Select(x => x.QuestionnaireId).First(),
                    ResultsPatientAnswers = new List<ResultsPatientAnswer>
                    {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Not having clear and concrete goals for your diabetes care?",
                                    AnswerText = "Somewhat serious problem",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling discouraged with your diabetes treatment plan?",
                                    AnswerText = "Moderate problem",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling scared when you think about living with diabetes?",
                                    AnswerText = "Somewhat serious problem",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Uncomfortable social situations related to your diabetes care (e.g. people telling you what to eat)?",
                                    AnswerText = "Moderate problem",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feelings of deprivation regarding food and meals?",
                                    AnswerText = "Somewhat serious problem",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling depressed when you think about living with diabetes?",
                                    AnswerText = "Somewhat serious problem",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Not knowing if your mood or feelings are related to your diabetes?",
                                    AnswerText = "Moderate problem",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling overwhelmed by your diabetes?",
                                    AnswerText = "Minor problem",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Worrying about low blood glucose reactions?",
                                    AnswerText = "Moderate problem",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling angry when you think about living with diabetes?",
                                    AnswerText = "Somewhat serious problem",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling constantly concerned about food and eating?",
                                    AnswerText = "Moderate problem",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Worrying about the future and the possibility of serious complications?",
                                    AnswerText = "Somewhat serious problem",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feelings of guilt or anxiety when you get off track with your diabetes management?",
                                    AnswerText = "Moderate problem",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Not accepting your diabetes?",
                                    AnswerText = "Moderate problem",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling unsatisfied with your diabetes physician?",
                                    AnswerText = "Somewhat serious problem",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that diabetes is taking up too much of your mental and physical energy every day?",
                                    AnswerText = "Somewhat serious problem",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling alone with your diabetes?",
                                    AnswerText = "Somewhat serious problem",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that your friends and family are not supportive of your diabetes management efforts?",
                                    AnswerText = "Minor problem",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Coping with complications of diabetes?",
                                    AnswerText = "Somewhat serious problem",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling burned out by the constant effort needed to manage diabetes?",
                                    AnswerText = "Moderate problem",
                                    AnswerValue = 2
                                }
                            }
                    }
                },
                new Models.Results
                {
                    Date = new DateTime(2023, 8, 30),
                    isSaved = false,
                    PatientId = context.Patients
                            .Where(x => x.Email.Equals("sebastian.carter@lq.com"))
                            .Select(x => x.PatientId).First(),
                    QuestionnaireId = context.Questionnaires
                            .Where(x => x.QuestionnaireName.Equals("Problem Areas In Diabetes (PAID)"))
                            .Select(x => x.QuestionnaireId).First(),
                    ResultsPatientAnswers = new List<ResultsPatientAnswer>
                    {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Not having clear and concrete goals for your diabetes care?",
                                    AnswerText = "Minor problem",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling discouraged with your diabetes treatment plan?",
                                    AnswerText = "Not a problem",
                                    AnswerValue = 0
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling scared when you think about living with diabetes?",
                                    AnswerText = "Somewhat serious problem",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Uncomfortable social situations related to your diabetes care (e.g. people telling you what to eat)?",
                                    AnswerText = "Minor problem",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feelings of deprivation regarding food and meals?",
                                    AnswerText = "Minor problem",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling depressed when you think about living with diabetes?",
                                    AnswerText = "Moderate problem",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Not knowing if your mood or feelings are related to your diabetes?",
                                    AnswerText = "Not a problem",
                                    AnswerValue = 0
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling overwhelmed by your diabetes?",
                                    AnswerText = "Minor problem",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Worrying about low blood glucose reactions?",
                                    AnswerText = "Not a problem",
                                    AnswerValue = 0
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling angry when you think about living with diabetes?",
                                    AnswerText = "Moderate problem",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling constantly concerned about food and eating?",
                                    AnswerText = "Moderate problem",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Worrying about the future and the possibility of serious complications?",
                                    AnswerText = "Not a problem",
                                    AnswerValue = 0
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feelings of guilt or anxiety when you get off track with your diabetes management?",
                                    AnswerText = "Not a problem",
                                    AnswerValue = 0
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Not accepting your diabetes?",
                                    AnswerText = "Minor problem",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling unsatisfied with your diabetes physician?",
                                    AnswerText = "Minor problem",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that diabetes is taking up too much of your mental and physical energy every day?",
                                    AnswerText = "Not a problem",
                                    AnswerValue = 0
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling alone with your diabetes?",
                                    AnswerText = "Minor problem",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that your friends and family are not supportive of your diabetes management efforts?",
                                    AnswerText = "Not a problem",
                                    AnswerValue = 0
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Coping with complications of diabetes?",
                                    AnswerText = "Moderate problem",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling burned out by the constant effort needed to manage diabetes?",
                                    AnswerText = "Minor problem",
                                    AnswerValue = 1
                                }
                            }
                    }
                },

                new Models.Results
                {
                    Date = new DateTime(2023, 8, 12),
                    isSaved = false,
                    PatientId = context.Patients
                            .Where(x => x.Email.Equals("sebastian.carter@lq.com"))
                            .Select(x => x.PatientId).First(),
                    QuestionnaireId = context.Questionnaires
                            .Where(x => x.QuestionnaireName.Equals("Diabetes Distress Scale (DDS-17)"))
                            .Select(x => x.QuestionnaireId).First(),
                    ResultsPatientAnswers = new List<ResultsPatientAnswer>
                    {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that diabetes is taking up too much of my mental and physical energy every day.",
                                    AnswerText = "Very serious problem",
                                    AnswerValue = 6
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that my doctor doesn’t know enough about diabetes and diabetes care.",
                                    AnswerText = "Very serious problem",
                                    AnswerValue = 6
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Not feeling confident in my day-to-day ability to manage diabetes.",
                                    AnswerText = "Serious problem",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling angry, scared, and/or depressed when I think about living with diabetes.",
                                    AnswerText = "Serious problem",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that my doctor doesn’t give me clear enough directions on how to manage my diabetes.",
                                    AnswerText = "Very serious problem",
                                    AnswerValue = 6
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that I am not testing my blood sugars frequently enough.",
                                    AnswerText = "Serious problem",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that I will end up with serious long-term complications, no matter what I do.",
                                    AnswerText = "Very serious problem",
                                    AnswerValue = 6
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that I am often failing with my diabetes routine.",
                                    AnswerText = "Very serious problem",
                                    AnswerValue = 6
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that friends or family are not supportive enough of self-care efforts (e.g., planning activities that conflict with my schedule, encouraging me to eat the “wrong” foods).",
                                    AnswerText = "Serious problem",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that diabetes controls my life.",
                                    AnswerText = "Serious problem",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that my doctor doesn’t take my concerns seriously enough.",
                                    AnswerText = "Very serious problem",
                                    AnswerValue = 6
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that I am not sticking closely enough to a good meal plan.",
                                    AnswerText = "Serious problem",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that friends or family don’t appreciate how difficult living with diabetes can be.",
                                    AnswerText = "Very serious problem",
                                    AnswerValue = 6
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling overwhelmed by the demands of living with diabetes.",
                                    AnswerText = "Very serious problem",
                                    AnswerValue = 6
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that I don’t have a doctor who I can see regularly enough about my diabetes.",
                                    AnswerText = "Serious problem",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Not feeling motivated to keep up my diabetes self management.",
                                    AnswerText = "Serious problem",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that friends or family don’t give me the emotional support that I would like.",
                                    AnswerText = "Very serious problem",
                                    AnswerValue = 6
                                }
                            }
                    }
                },
                new Models.Results
                {
                    Date = new DateTime(2023, 8, 20),
                    isSaved = false,
                    PatientId = context.Patients
                            .Where(x => x.Email.Equals("sebastian.carter@lq.com"))
                            .Select(x => x.PatientId).First(),
                    QuestionnaireId = context.Questionnaires
                            .Where(x => x.QuestionnaireName.Equals("Diabetes Distress Scale (DDS-17)"))
                            .Select(x => x.QuestionnaireId).First(),
                    ResultsPatientAnswers = new List<ResultsPatientAnswer>
                    {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that diabetes is taking up too much of my mental and physical energy every day.",
                                    AnswerText = "Moderate problem",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that my doctor doesn’t know enough about diabetes and diabetes care.",
                                    AnswerText = "Somewhat serious problem",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Not feeling confident in my day-to-day ability to manage diabetes.",
                                    AnswerText = "Somewhat serious problem",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling angry, scared, and/or depressed when I think about living with diabetes.",
                                    AnswerText = "Moderate problem",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that my doctor doesn’t give me clear enough directions on how to manage my diabetes.",
                                    AnswerText = "Moderate problem",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that I am not testing my blood sugars frequently enough.",
                                    AnswerText = "Somewhat serious problem",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that I will end up with serious long-term complications, no matter what I do.",
                                    AnswerText = "Moderate problem",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that I am often failing with my diabetes routine.",
                                    AnswerText = "Somewhat serious problem",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that friends or family are not supportive enough of self-care efforts (e.g., planning activities that conflict with my schedule, encouraging me to eat the “wrong” foods).",
                                    AnswerText = "Somewhat serious problem",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that diabetes controls my life.",
                                    AnswerText = "Moderate problem",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that my doctor doesn’t take my concerns seriously enough.",
                                    AnswerText = "Moderate problem",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that I am not sticking closely enough to a good meal plan.",
                                    AnswerText = "Somewhat serious problem",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that friends or family don’t appreciate how difficult living with diabetes can be.",
                                    AnswerText = "Moderate problem",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling overwhelmed by the demands of living with diabetes.",
                                    AnswerText = "Somewhat serious problem",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that I don’t have a doctor who I can see regularly enough about my diabetes.",
                                    AnswerText = "Moderate problem",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Not feeling motivated to keep up my diabetes self management.",
                                    AnswerText = "Somewhat serious problem",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that friends or family don’t give me the emotional support that I would like.",
                                    AnswerText = "Somewhat serious problem",
                                    AnswerValue = 4
                                }
                            }
                    }
                },
                new Models.Results
                {
                    Date = new DateTime(2023, 8, 31),
                    isSaved = false,
                    PatientId = context.Patients
                            .Where(x => x.Email.Equals("sebastian.carter@lq.com"))
                            .Select(x => x.PatientId).First(),
                    QuestionnaireId = context.Questionnaires
                            .Where(x => x.QuestionnaireName.Equals("Diabetes Distress Scale (DDS-17)"))
                            .Select(x => x.QuestionnaireId).First(),
                    ResultsPatientAnswers = new List<ResultsPatientAnswer>
                    {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that diabetes is taking up too much of my mental and physical energy every day.",
                                    AnswerText = "Not a problem",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that my doctor doesn’t know enough about diabetes and diabetes care.",
                                    AnswerText = "Not a problem",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Not feeling confident in my day-to-day ability to manage diabetes.",
                                    AnswerText = "Slight problem",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling angry, scared, and/or depressed when I think about living with diabetes.",
                                    AnswerText = "Not a problem",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that my doctor doesn’t give me clear enough directions on how to manage my diabetes.",
                                    AnswerText = "Slight problem",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that I am not testing my blood sugars frequently enough.",
                                    AnswerText = "Slight problem",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that I will end up with serious long-term complications, no matter what I do.",
                                    AnswerText = "Not a problem",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that I am often failing with my diabetes routine.",
                                    AnswerText = "Not a problem",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that friends or family are not supportive enough of self-care efforts (e.g., planning activities that conflict with my schedule, encouraging me to eat the “wrong” foods).",
                                    AnswerText = "Slight problem",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that diabetes controls my life.",
                                    AnswerText = "Not a problem",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that my doctor doesn’t take my concerns seriously enough.",
                                    AnswerText = "Slight problem",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that I am not sticking closely enough to a good meal plan.",
                                    AnswerText = "Slight problem",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that friends or family don’t appreciate how difficult living with diabetes can be.",
                                    AnswerText = "Not a problem",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling overwhelmed by the demands of living with diabetes.",
                                    AnswerText = "Not a problem",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that I don’t have a doctor who I can see regularly enough about my diabetes.",
                                    AnswerText = "Slight problem",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Not feeling motivated to keep up my diabetes self management.",
                                    AnswerText = "Not a problem",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Feeling that friends or family don’t give me the emotional support that I would like.",
                                    AnswerText = "Slight problem",
                                    AnswerValue = 2
                                }
                            }
                    }
                },

                new Models.Results
                {
                    Date = new DateTime(2023, 8, 25),
                    isSaved = false,
                    PatientId = context.Patients
                            .Where(x => x.Email.Equals("sebastian.carter@lq.com"))
                            .Select(x => x.PatientId).First(),
                    QuestionnaireId = context.Questionnaires
                            .Where(x => x.QuestionnaireName.Equals("Insulin Treatment Appraisal Scale (ITAS)"))
                            .Select(x => x.QuestionnaireId).First(),
                    ResultsPatientAnswers = new List<ResultsPatientAnswer>
                    {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin means I have failed to manage my diabetes with diet and pills",
                                    AnswerText = "Strongly agree",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin means my diabetes has become much worse",
                                    AnswerText = "Strongly agree",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin helps to prevent complications of diabetes",
                                    AnswerText = "Agree",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin means other people see me as a sicker person",
                                    AnswerText = "Agree",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin makes life less flexible",
                                    AnswerText = "Strongly agree",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "I’m afraid of injecting myself with a needle",
                                    AnswerText = "Agree",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin increases the risk of low blood glucose levels (hypoglycemia) ",
                                    AnswerText = "Strongly agree",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin helps to improve my health",
                                    AnswerText = "Strongly agree",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Insulin causes weight gain",
                                    AnswerText = "Agree",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Managing insulin injections takes a lot of time and energy",
                                    AnswerText = "Agree",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin means I have to give up activities I enjoy",
                                    AnswerText = "Strongly agree",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin means my health will deteriorate",
                                    AnswerText = "Agree",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Injecting insulin is embarrassing",
                                    AnswerText = "Strongly agree",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Injecting insulin is painful",
                                    AnswerText = "Strongly agree",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "It is difficult to inject the right amount of insulin correctly at the right time every day",
                                    AnswerText = "Agree",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin makes it more difficult to fulfill my responsibilities (at work, at home)",
                                    AnswerText = "Agree",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin helps to maintain good control of blood glucose",
                                    AnswerText = "Strongly agree",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Being on insulin causes family and friends to be more concerned about me",
                                    AnswerText = "Agree",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin helps to improve my energy level",
                                    AnswerText = "Strongly agree",
                                    AnswerValue = 5
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin makes me more dependent on my doctor",
                                    AnswerText = "Strongly agree",
                                    AnswerValue = 5
                                }
                            }
                    }
                },
                new Models.Results
                {
                    Date = new DateTime(2023, 9, 5),
                    isSaved = false,
                    PatientId = context.Patients
                            .Where(x => x.Email.Equals("sebastian.carter@lq.com"))
                            .Select(x => x.PatientId).First(),
                    QuestionnaireId = context.Questionnaires
                            .Where(x => x.QuestionnaireName.Equals("Insulin Treatment Appraisal Scale (ITAS)"))
                            .Select(x => x.QuestionnaireId).First(),
                    ResultsPatientAnswers = new List<ResultsPatientAnswer>
                    {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin means I have failed to manage my diabetes with diet and pills",
                                    AnswerText = "Neither agree nor disagree",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin means my diabetes has become much worse",
                                    AnswerText = "Neither agree nor disagree",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin helps to prevent complications of diabetes",
                                    AnswerText = "Agree",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin means other people see me as a sicker person",
                                    AnswerText = "Neither agree nor disagree",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin makes life less flexible",
                                    AnswerText = "Agree",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "I’m afraid of injecting myself with a needle",
                                    AnswerText = "Agree",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin increases the risk of low blood glucose levels (hypoglycemia) ",
                                    AnswerText = "Neither agree nor disagree",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin helps to improve my health",
                                    AnswerText = "Neither agree nor disagree",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Insulin causes weight gain",
                                    AnswerText = "Agree",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Managing insulin injections takes a lot of time and energy",
                                    AnswerText = "Neither agree nor disagree",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin means I have to give up activities I enjoy",
                                    AnswerText = "Agree",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin means my health will deteriorate",
                                    AnswerText = "Agree",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Injecting insulin is embarrassing",
                                    AnswerText = "Neither agree nor disagree",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Injecting insulin is painful",
                                    AnswerText = "Neither agree nor disagree",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "It is difficult to inject the right amount of insulin correctly at the right time every day",
                                    AnswerText = "Agree",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin makes it more difficult to fulfill my responsibilities (at work, at home)",
                                    AnswerText = "Neither agree nor disagree",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin helps to maintain good control of blood glucose",
                                    AnswerText = "Agree",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Being on insulin causes family and friends to be more concerned about me",
                                    AnswerText = "Agree",
                                    AnswerValue = 4
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin helps to improve my energy level",
                                    AnswerText = "Neither agree nor disagree",
                                    AnswerValue = 3
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin makes me more dependent on my doctor",
                                    AnswerText = "Neither agree nor disagree",
                                    AnswerValue = 3
                                }
                            }
                    }
                },
                new Models.Results
                {
                    Date = new DateTime(2023, 9, 15),
                    isSaved = false,
                    PatientId = context.Patients
                            .Where(x => x.Email.Equals("sebastian.carter@lq.com"))
                            .Select(x => x.PatientId).First(),
                    QuestionnaireId = context.Questionnaires
                            .Where(x => x.QuestionnaireName.Equals("Insulin Treatment Appraisal Scale (ITAS)"))
                            .Select(x => x.QuestionnaireId).First(),
                    ResultsPatientAnswers = new List<ResultsPatientAnswer>
                    {
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin means I have failed to manage my diabetes with diet and pills",
                                    AnswerText = "Disagree",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin means my diabetes has become much worse",
                                    AnswerText = "Strongly disagree",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin helps to prevent complications of diabetes",
                                    AnswerText = "Disagree",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin means other people see me as a sicker person",
                                    AnswerText = "Disagree",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin makes life less flexible",
                                    AnswerText = "Strongly disagree",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "I’m afraid of injecting myself with a needle",
                                    AnswerText = "Strongly disagree",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin increases the risk of low blood glucose levels (hypoglycemia) ",
                                    AnswerText = "Disagree",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin helps to improve my health",
                                    AnswerText = "Strongly disagree",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Insulin causes weight gain",
                                    AnswerText = "Disagree",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Managing insulin injections takes a lot of time and energy",
                                    AnswerText = "Disagree",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin means I have to give up activities I enjoy",
                                    AnswerText = "Strongly disagree",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin means my health will deteriorate",
                                    AnswerText = "Strongly disagree",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Injecting insulin is embarrassing",
                                    AnswerText = "Disagree",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Injecting insulin is painful",
                                    AnswerText = "Strongly disagree",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "It is difficult to inject the right amount of insulin correctly at the right time every day",
                                    AnswerText = "Disagree",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin makes it more difficult to fulfill my responsibilities (at work, at home)",
                                    AnswerText = "Disagree",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin helps to maintain good control of blood glucose",
                                    AnswerText = "Strongly disagree",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Being on insulin causes family and friends to be more concerned about me",
                                    AnswerText = "Strongly disagree",
                                    AnswerValue = 1
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin helps to improve my energy level",
                                    AnswerText = "Disagree",
                                    AnswerValue = 2
                                }
                            },
                            new ResultsPatientAnswer
                            {
                                PatientAnswer = new PatientAnswer
                                {
                                    QuestionText = "Taking insulin makes me more dependent on my doctor",
                                    AnswerText = "Strongly disagree",
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
