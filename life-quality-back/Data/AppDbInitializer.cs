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
                                                    AnswerText = "Незалежний — пацієнт може самостійно їсти з підносу або столу, коли хтось подає їжу в межах його досяжності. Він повинен поставити допоміжні прибори, якщо це необхідно, розрізати їжу, використовувати сіль і перець, намастити масло на хліб тощо. Він повинен зробити це за прийнятний час.",
                                                    AnswerValue = 10
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Потрібна певна допомога (з розрізанням їжі та ін., що зазначено вище).",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "безпомічний — пацієнта необхідно годувати.",
                                                    AnswerValue = 0
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Переміщення з ліжка на інвалідний візок і назад",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Пацієнт незалежний на всіх етапах цього виду діяльності. Пацієнт може безпечно під’їхати до ліжка в своєму інвалідному візку, розблокувати систему гальм, підняти підніжку, безпечно переміститися в ліжко, лягти, привести себе в сидяче положення на ліжку, змінити положення інвалідного візка, щоб при необхідності безпечно переміститися в нього і повернутися до інвалідного візка.",
                                                    AnswerValue = 15
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Потрібна певна мінімальна допомога на якомусь етапі цього виду діяльності або пацієнту треба нагадати чи контролювати для забезпечення безпеки одного або декількох етапів цього виду діяльності.",
                                                    AnswerValue = 10
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Пацієнт може привести себе в сидяче положення без допомоги іншої людини, але його необхідно підняти з ліжка або, якщо він переміщується, то зі значною допомогою.",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Залежний — пацієнт не може привести себе в сидяче положення.",
                                                    AnswerValue = 0
                                                },
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Персональна гігієна",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Пацієнт може самостійно вимити обличчя і руки, розчесати волосся, почистити зуби і поголитися. Він може використовувати будь-який вид бритви, але повинен покласти лезо або ввімкнути бритву без сторонньої допомоги, а також дістати її з ящика або шафи. Пацієнтки повинні самостійно наносити макіяж, якщо він використовується, але не обов’язково заплітати волосся або робити зачіску.",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Пацієнту потрібна допомога.",
                                                    AnswerValue = 0
                                                },
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Користування туалетом",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Пацієнт може самостійно користуватися туалетом, знімати та вдягати одяг, запобігти забрудненню одягу і використовувати туалетний папір без сторонньої допомоги. Він може використовувати поручні на стіні або інший стійкий об’єкт для підтримки в разі необхідності. Якщо необхідно використовувати підкладне судно замість туалету, він повинен бути в змозі помістити його на стілець, очистити і помити.",
                                                    AnswerValue = 10
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Пацієнт потребує допомоги через порушення рівноваги або при вдяганні та знятті одягу, або при використанні туалетного паперу.",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "пацієнту потрібна допомога.",
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
                                                    AnswerText = "Пацієнт може самостійно приймати ванну, душ або провести повне обтирання тіла губкою. Він повинен бути в змозі зробити всі необхідні етапи в залежності від того, який метод використовується, без допомоги іншої людини.",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Пацієнту потрібна допомога.",
                                                    AnswerValue = 0
                                                },
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Пересування по рівній площині",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Пацієнт може пройти щонайменше 50 метрів без сторонньої допомоги або нагляду. Він може носити ортопедичний апарат або протези і використовувати милиці, палиці, але не ходунки-опори. Він повинен бути в змозі за необхідності заблокувати і розблокувати ортопедичний апарат, припустимо з положення стоячи, і сісти, привести всі необхідні механізми в належний стан для подальшого використання і звільнитися від них, коли він сидить (одягати і знімати корсет під одягом).",
                                                    AnswerValue = 15
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Пацієнт потребує допомоги або нагляду при всіх видах ходьби, що описані вище, але може пройти щонайменше 50 метрів з невеликою допомогою.",
                                                    AnswerValue = 10
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Якщо пацієнт не може пересуватися, але може самостійно керувати інвалідним візком. Він повинен бути в змозі оминати кути, розвернутися, маневрувати від стільця до столу, ліжка, туалету та ін. Він повинен бути в змозі проїхати візком принаймні 50 метрів.",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Нерухомий — пацієнт потребує допомоги при пересуванні в інвалідному візку.",
                                                    AnswerValue = 0
                                                },
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Підняття та спускання сходами",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Пацієнт може безпечно піти вгору і вниз сходами без допомоги або нагляду. Він може і повинен використовувати поручні, палиці або милиці, коли це необхідно. Він повинен бути в змозі нести палиці або милиці, коли піднімається або спускається сходами.",
                                                    AnswerValue = 10
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Пацієнт потребує допомоги або нагляду при виконанні будь-якого з перерахованих вище пунктів.",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Пацієнт не в змозі йти вгору і вниз сходами. Він потребує ліфта.",
                                                    AnswerValue = 0
                                                },
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Одягання та роздягання (пацієнткам не слід враховувати використання бюстгальтера або пояса, якщо немає вказаних елементів одягу)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Пацієнт здатний надіти і зняти та застібнути весь одяг, зав’язати шнурки взуття (якщо не потрібно використовувати пристосування для цього). Цей вид повсякденної діяльності включає надягання, зняття і кріплення корсета або бандажу, коли вони передбачені. За необхідності можуть бути використані такі спеціальні елементи одягу, як підтяжки, мокасини, сукні, які розстібаються спереду.",
                                                    AnswerValue = 10
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Пацієнт потребує допомоги в одяганні, знятті або застібанні будь-якого одягу. Принаймні половину роботи він повинен зробити самостійно. Він повинен зробити це за прийнятний час.",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Пацієнту потрібна допомога.",
                                                    AnswerValue = 0
                                                },
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Контроль дефекації",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Пацієнт здатний контролювати дефекацію і не має жодного проблемного випадку. Він може використовувати свічку або клізму, коли це необхідно (як для пацієнтів з ушкодженнями спинного мозку, що проходили підготовку кишечнику).",
                                                    AnswerValue = 10
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Пацієнт потребує допомоги у використанні супозиторіїв чи клізми або час від часу має проблемні випадки.",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Нетримання.",
                                                    AnswerValue = 0
                                                },
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Контроль сечовипускання",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Пацієнт може контролювати сечовипускання вдень і вночі. Пацієнти з ушкодженнями спинного мозку, які носять зовнішній катетер і сумку на нозі, повинні самостійно їх ставити, чистити й опорожнювати мішок і залишитися сухими вдень і вночі.",
                                                    AnswerValue = 10
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Пацієнт має періодичні проблемні випадки, або не може чекати підкладне судно, або потрапити в туалет вчасно, або потребує допомоги з катетером.",
                                                    AnswerValue = 5
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Нетримання.",
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
                                            QuestionText = "Встати із сидячого положення (Будь ласка, встаньте. Намагайтеся не використовувати Вашу руку для підтримки.)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Може встати без використання рук і стабілізуватись самостійно.",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Може встати самостійно за допомогою рук",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Може встати за допомогою рук після декількох спроб.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Потребує мінімальної допомоги при вставанні або стабілізації.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Потребує помірної або максимальної допомоги при вставанні.",
                                                    AnswerValue = 0
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Стояти без підтримки (Будь ласка, простійте протягом двох хвилин без підтримки.)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = " У змозі безпечно стояти протягом 2 хвилин.",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "У змозі простояти 2 хвилини під наглядом.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "У змозі простояти 30 секунд без підтримки.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Потрібно кілька спроб, щоб простояти 30 секунд без підтримки.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Не може стояти 30 секунд без підтримки.",
                                                    AnswerValue = 0
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Сидіння без підтримки спини, але з фіксованими ногами на підлозі або стільчику (Будь ласка, сидіть, склавши руки, протягом 2 хвилин)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "У змозі сидіти безпечно і надійно протягом 2 хвилин.",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "У змозі сидіти 2 хвилини під наглядом.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "У змозі сидіти протягом 30 секунд.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "У змозі сидіти 10 секунд.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Не може сидіти без підтримки 10 секунд.",
                                                    AnswerValue = 0
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Сідання із положення стоячи (Будь ласка, сідайте)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Сідає безпечно з мінімальним використанням рук.",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Контролює сідання за допомогою рук.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Використовує задню поверхню ніг відносно стільця, щоб контролювати сідання.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Сидить самостійно, але процес сідання неконтрольований.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Потребує допомоги при сидінні.",
                                                    AnswerValue = 0
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Переміщення (Поставте стілець(і), як орієнтири при переміщенні. Попросіть пацієнта пройти в \r\nодин бік до стільця з підлокітниками і в інший бік до стільця без підлокітників. Ви \r\nможете використовувати два стільці (один з і один без підлокітників) або ліжко і \r\nстілець.)",
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
                                            QuestionText = "Нахиляння вперед з витягнотою рукою в положенні стоячи (Підійміть руку під кутом 90 градусів. Простягніть пальці і нахиліться, наскільки можете. (Екзаменатор ставить лінійку біля кінчиків пальців, коли рука знаходиться під \r\nкутом 90 градусів. Пальці не повинні торкатися лінійки при нахилі. Контрольний \r\nрезультат — це відстань, якої досягли пальці, коли пацієнт перебуває в положенні \r\nмаксимального нахилу. Якщо це можливо, попросіть пацієнта використовувати \r\nобидві руки при нахилі, щоб уникнути обертання хребта.))",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Може впевнено досягти 25 см (10 дюймів).",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Може досягти 12 см (5 дюймів).",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Може досягти 5 см (2 дюйми).",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Нахиляється вперед, але потребує контролю.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Втрачає рівновагу при спробі / потребує зовнішньої підтримки.",
                                                    AnswerValue = 0
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Взяття предмета з підлоги в положенні стоячи (Візьміть туфлю/капець, що знаходиться перед Вашими ногами)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "У змозі взяти капець легко і безпечно.",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "У змозі підняти черевичок, але потребує нагляду.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Не може підібрати, але досягає відстані 2–5 см (1–2 дюйми) від капця і самостійно зберігає рівновагу.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Не в змозі підібрати і потребує нагляду при спробі.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Не може спробувати/потребує допомоги, щоб утриматися від втрати рівноваги або падіння.",
                                                    AnswerValue = 0
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Оглядання через ліве і праве плече в положенні стоячи (Озирніться, щоб подивитися прямо через ліве плече. Повторіть вправо. (Екзаме\u0002натор може вибрати будь-який предмет позаду пацієнта, щоб той дивився безпо\u0002середньо на предмет, щоб сприяти кращому повороту.))",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Озирається назад з обох боків, і вага добре зміщується.",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "З одного боку виглядає менше, ніж з іншого; менше перенесення ваги.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Тільки повертається боком, але утримує рівновагу.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "При повороті потребує нагляду.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Потребує допомоги, щоб не втратити рівновагу або уникнути падіння.",
                                                    AnswerValue = 0
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Повертання на 360 градусів (Поверніться повністю навколо себе. Пауза. Потім поверніться в іншому напрямку)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "У змозі повернутися на 360 градусів безпечно за 4 секунди або менше.",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "У змозі повернутися на 360 градусів безпечно тільки в один бік за 4 секунди \r\nабо менше.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Здатний повертатися на 360 градусів безпечно, але повільно.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Потрібен ретельний нагляд або словесний супровід.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Потребує допомоги при повороті.",
                                                    AnswerValue = 0
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Поставити ногу на сходинку або стільчик, стоячи без підтримки (Поставте кожну ногу по черзі на сходинку/стільчик. Продовжуйте, поки кожна нога \r\nне торкнеться сходинки/стільчика чотири рази)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Може стояти самостійно і безпечно і виконує 8 кроків протягом 20 секунд.",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Може стояти самостійно і виконує 8 кроків більше ніж за 20 секунд.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "У змозі виконати 4 кроки без сторонньої допомоги під наглядом.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "У змозі зробити більше 2 кроків, потребує мінімальної допомоги.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Потребує допомоги, щоб не впасти/не може спробувати.",
                                                    AnswerValue = 0
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Стояння без підтримки з однією ногою попереду ((Продемонструйте пацієнту) Поставте одну ногу прямо перед іншою. Якщо Ви \r\nвідчуваєте, що не можете поставити ногу прямо спереду, спробуйте трохи далі, \r\nщоб п’ятка Вашої передньої ноги була попереду пальців іншої ноги. (Для того, \r\nщоб набрати 3 бали довжина кроку не повинна перевищувати довжину іншої ноги \r\nі ширина пози повинна наближатись до нормальної ширини кроку пацієнта.))",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "Може розмістити стопи «гусаком» самостійно і утримувати позу 30 секундy.",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "У змозі помістити ногу попереду самостійно і утримувати позу 30 секунд.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "У змозі зробити невеликий крок самостійно і утримувати позу 30 секунд.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Потребує допомоги, щоб зробити крок, але може утримувати позу 15 секунд.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Втрачає рівновагу під час кроку або стояння\r\n.",
                                                    AnswerValue = 0
                                                }
                                            }
                                        },
                                        new Question
                                        {
                                            QuestionText = "Стояння на одній нозі (Стійте на одній нозі стільки, скільки Ви можете без підтримки)",
                                            Answers = new List<Answer>
                                            {
                                                new Answer
                                                {
                                                    AnswerText = "У змозі підняти ногу самостійно і утримуватись > 10 секунд.",
                                                    AnswerValue = 4
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "У змозі підняти ногу самостійно і утримуватись 5–10 секунд.",
                                                    AnswerValue = 3
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "У змозі підняти ногу самостійно і утримуватись менше 3 секунд.",
                                                    AnswerValue = 2
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Намагається підняти ногу, не в силах утримуватись 3 секунди, але стоїть \r\nсамостійно.",
                                                    AnswerValue = 1
                                                },
                                                new Answer
                                                {
                                                    AnswerText = "Не може спробувати, потребує допомоги, щоб уникнути падіння.",
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
