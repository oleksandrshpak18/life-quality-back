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

                if(context.Doctors.Any()) 
                {
                    foreach (var doctor in context.Doctors)
                    {
                        context.Doctors.Remove(doctor);
                    }
                    context.SaveChanges();
                }

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

                if (context.Diseases.Any())
                {
                    foreach (var disease in context.Diseases)
                    {
                        context.Diseases.Remove(disease);
                    }
                    context.SaveChanges();
                }

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

                if (context.TreatmentStrategies.Any())
                {
                    foreach (var treatmentStrategy in context.TreatmentStrategies)
                    {
                        context.TreatmentStrategies.Remove(treatmentStrategy);
                    }
                    context.SaveChanges();
                }

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
            }
        }
    }
}
