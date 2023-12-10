using life_quality_back.Data.Models;
using life_quality_back.Data;
using life_quality_back.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace life_quality_back.Test
{
    public class DiseaseRepositoryTests
    {
        private AppDbContext GetDataBaseContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new AppDbContext(options);
            databaseContext.Database.EnsureCreated();

            databaseContext.Diseases.Add
            (
                new Disease
                {
                    DiseaseName = "1 Disease",
                    DiseaseDescription = "1 disease description"
                }
            );
            databaseContext.Diseases.Add
            (
                new Disease
                {
                    DiseaseName = "2 Disease",
                    DiseaseDescription = "2 disease description"
                }
            );
            databaseContext.Diseases.Add
            (
                new Disease
                {
                    DiseaseName = "3 Disease",
                    DiseaseDescription = "3 disease description"
                }
            );
            databaseContext.Diseases.Add
            (
                new Disease
                {
                    DiseaseName = "4 Disease",
                    DiseaseDescription = "4 disease description"
                }
            );
            databaseContext.Diseases.Add
            (
                new Disease
                {
                    DiseaseName = "5 Disease",
                    DiseaseDescription = "5 disease description"
                }
            );
                

            databaseContext.SaveChanges();
            return databaseContext;
        }
        private AppDbContext GetDataBaseContextNull()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new AppDbContext(options);
            databaseContext.Database.EnsureCreated();

            databaseContext.SaveChanges();
            return databaseContext;
        }

        [Theory]
        [InlineData("1 Disease", "1 disease description")]
        [InlineData("2 Disease", "2 disease description")]
        [InlineData("3 Disease", "3 disease description")]
        [InlineData("4 Disease", "4 disease description")]
        [InlineData("5 Disease", "5 disease description")]
        public void GetAll_ReturnAllDiseases(string expectedDiseaseName, string expectedDiseaseDescription)
        {
            //ARRANGE
            AppDbContext context = GetDataBaseContext();
            DiseaseRepository repository = new DiseaseRepository(context);

            //ACT
            var diseases = repository.GetAll();

            //diseases.Contains(new Disease { DiseaseName = expectedDiseaseName, DiseaseDescription = expectedDiseaseDescription });

            //ASSERT
            Assert.Contains(diseases, 
                d => d.DiseaseName == expectedDiseaseName && 
                d.DiseaseDescription == expectedDiseaseDescription);
        }

        [Fact]
        public void GetAll_ReturnNull()
        {
            //ARRANGE
            AppDbContext context = GetDataBaseContextNull();
            DiseaseRepository repository = new DiseaseRepository(context);

            //ACT
            var diseases = repository.GetAll();

            //ASSERT
            Assert.Empty(diseases);
        }
    }
}
