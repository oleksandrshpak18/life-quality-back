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

                // don't forget to save tha changes
                context.SaveChanges();
            }
        }
    }
}
