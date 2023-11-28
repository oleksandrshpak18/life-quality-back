using life_quality_back.Data.Models;

namespace life_quality_back.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                // remove all the previous data from the tables on startup
                context.Doctors.RemoveRange(context.Doctors);
                context.Diseases.RemoveRange(context.Diseases);
                context.TreatmentStrategies.RemoveRange(context.TreatmentStrategies);
                context.Patients.RemoveRange(context.Patients);

                // add a template data to the tables
                context.Doctors.AddRange(
                    new Doctor
                    {
                        FirstName = "Mike",
                        LastName = "Dolfino",
                        Email = "mike.dolfino@gmail.com",
                        Education = "Lviv National Medical University",
                        Gender = "Male",
                        Speciality = "Dentist"
                    },
                    new Doctor
                    {
                        FirstName = "Mary",
                        LastName = "Alice",
                        Email = "mary.alice@gmail.com",
                        Education = "University of Fairview",
                        Gender = "Female",
                        Speciality = "Therapist"
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
                        TreatmentStrategyDescription = "A robust description for strategy 1."
                    },
                    new TreatmentStrategy
                    {
                        TreatmentStrategyName = "Strategy 2",
                        TreatmentStrategyDescription = "A robust description for strategy 2. And some additional info here."
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
