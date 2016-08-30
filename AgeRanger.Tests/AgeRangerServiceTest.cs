using System;
using System.Collections.Generic;
using System.Linq;
using AgeRanger.Data;
using AgeRanger.Data.Repositories;
using AgeRanger.Model.DTO;
using AgeRanger.Model.Services;
using Moq;
using NUnit.Framework;
using AgeGroup = AgeRanger.Data.AgeGroup;

namespace AgeRanger.Tests
{
    /// <summary>
    /// Unit tests for AgeRangerService
    /// </summary>
    [TestFixture]
    public class AgeRangerServiceTest
    {

        #region Setup testing data
        
        private Mock<IAgeGroupRepository> mockIAgeGroupRepository;
        [SetUp]
        public void PrepareUnitTests()
        {
            
            mockIAgeGroupRepository = new Mock<IAgeGroupRepository>();

            


            mockIAgeGroupRepository.Setup(m => m.GetAll()).Returns(new List<AgeGroup>
            {
                new AgeGroup
                {
                    Id = 0,
                    MinAge = 0,
                    MaxAge = 2,
                    Description = "Toddler"
                },
                new AgeGroup
                {
                    Id = 1,
                    MinAge = 2,
                    MaxAge = 14,
                    Description = "Child"
                },
                new AgeGroup
                {
                    Id = 3,
                    MinAge = 14,
                    MaxAge = 20,
                    Description = "Teenager"
                }
            });
        }
        #endregion
        
        #region GetAllPeople tests
        [TestCase()]
        public void GetAllPeople_Test_Result()
        {
            // Arrange
            var mockIPersonRepository = new Mock<IPersonRepository>();
            mockIPersonRepository.Setup(m => m.GetAll()).Returns(new List<Person>()
            {
                new Person
                {
                    Id = 0,
                    FirstName = "Isaak",
                    LastName = "Newton",
                    Age = 10
                },
                new Person
                {
                    Id = 1,
                    FirstName = "Nicolaus ",
                    LastName = "Copernicus",
                    Age = 60
                },
                new Person
                {
                    Id = 2,
                    FirstName = "Konstantin",
                    LastName = "Tsiolkovsky",
                    Age = 34
                },
                new Person
                {
                    Id = 3,
                    FirstName = "Galileo",
                    LastName = "Galilei",
                    Age = 14
                }

            });

            var service = new AgeRangerService(mockIPersonRepository.Object, mockIAgeGroupRepository.Object);

            // Act
            var allPeople =  service.GetAllPeople().ToList();

            // Assert
            Assert.AreEqual(allPeople.Count(), 4);
        }
       

        [TestCase()]
        public void GetAllPeople_Test_AgeGroupNA()
        {
            // Arrange
            var mockIPersonRepository = new Mock<IPersonRepository>();
            mockIPersonRepository.Setup(m => m.GetAll()).Returns(new List<Person>()
            {
                new Person
                {
                    Id = 0,
                    FirstName = "Nicolaus ",
                    LastName = "Copernicus",
                    Age = 60
                }

            });

            var service = new AgeRangerService(mockIPersonRepository.Object, mockIAgeGroupRepository.Object);

            // Act
            var allPeople = service.GetAllPeople().ToList();

            // Assert
            var person = allPeople[0];

            Assert.IsNull(person.AgeGroup);
        }

        [TestCase(14,"Teenager")]
        [TestCase(1, "Toddler")]
        [TestCase(0, "Toddler")]
        [TestCase(3, "Child")]
        public void GetAllPeople_Test_BoundaryValues(long age, string ageGroupToExpect)
        {
            // Arrange
            var mockPersonRepository = new Mock<IPersonRepository>();

            mockPersonRepository.Setup(m => m.GetAll()).Returns(new List<Person>()
            {
                new Person
                {
                    Id = 0,
                    FirstName = "Isaak",
                    LastName = "Newton",
                    Age = age
                }

            });
            var service = new AgeRangerService(mockPersonRepository.Object, mockIAgeGroupRepository.Object);

            // Act
            var allPeople = service.GetAllPeople().ToList();
            
            // Assert
            var person = allPeople[0];
            Assert.AreEqual(person.AgeGroup, ageGroupToExpect);
        }
        #endregion

        #region AddPerson tests
        [TestCase]
        public void AddPerson_Test_InsertToRepository()
        {
            // Arrange
            var person = new Person
            {
                Id = 345,
                FirstName = "Johannes",
                LastName = "Kepler",
                Age = 45
            };

            var detailedPerson = new DetailedPerson
            {
                FirstName = "Johannes",
                LastName = "Kepler",
                Age = 45,
                AgeGroup = "Old man"
            };

            var mockIPersonRepository = new Mock<IPersonRepository>();
            mockIPersonRepository.Setup(m => m.Insert(It.IsAny<Person>())).Returns(person);

            var service = new AgeRangerService(mockIPersonRepository.Object, mockIAgeGroupRepository.Object);

            // Act
            service.AddPerson(detailedPerson);

            // Asert
            mockIPersonRepository.Verify(m => m.Insert(It.IsAny<Person>()));
        }

        [TestCase]
        public void AddPerson_Test_NewIdComingBack()
        {
            // Arrange
            var person = new Person
            {
                Id = 345,
                FirstName = "Johannes",
                LastName = "Kepler",
                Age = 45
            };

            var detailedPerson = new DetailedPerson
            {
                FirstName = "Johannes",
                LastName = "Kepler",
                Age = 45,
                AgeGroup = "Old man"
            };

            var mockIPersonRepository = new Mock<IPersonRepository>();
            mockIPersonRepository.Setup(m => m.Insert(It.IsAny<Person>())).Returns(person);

            var service = new AgeRangerService(mockIPersonRepository.Object, mockIAgeGroupRepository.Object);

            // Act
            var detailedPersonResult = service.AddPerson(detailedPerson);

            // Assert
            Assert.AreEqual(detailedPersonResult.Id, 345);
        }

        [TestCase]
        public void AddPerson_Test_FailWithNegativeAge()
        {
            // Arrange
            var detailedPerson = new DetailedPerson
            {
                FirstName = "Johannes",
                LastName = "Kepler",
                Age = -13,
                AgeGroup = "Old man"
            };
            
            var mockIPersonRepository = new Mock<IPersonRepository>();
            var service = new AgeRangerService(mockIPersonRepository.Object, mockIAgeGroupRepository.Object);
            
            // Act Assert
            Assert.Throws<Exception>(() => service.AddPerson(detailedPerson));
        }

        [TestCase]
        public void AddPerson_Test_FailWithBlankName()
        {
            // Arrange
            var detailedPerson = new DetailedPerson
            {
                FirstName = "",
                LastName = "Kepler",
                Age = 14,
                AgeGroup = "Old man"
            };

            var mockIPersonRepository = new Mock<IPersonRepository>();
            var service = new AgeRangerService(mockIPersonRepository.Object, mockIAgeGroupRepository.Object);

            // Act Assert
            Assert.Throws<Exception>(() => service.AddPerson(detailedPerson));
        }
        #endregion

        #region UpdatePerson tests
        [TestCase]
        public void UpdatePerson_Test_UpdateRepository()
        {
            // Arrange
            var detailedPerson = new DetailedPerson
            {
                FirstName = "Johannes",
                LastName = "Kepler",
                Age = 45,
                AgeGroup = "Old man"
            };

            var mockIPersonRepository = new Mock<IPersonRepository>();
            mockIPersonRepository.Setup(m => m.Update(It.IsAny<Person>()));

            var service = new AgeRangerService(mockIPersonRepository.Object, mockIAgeGroupRepository.Object);

            // Act
            service.UpdatePerson(detailedPerson);

            // Assert
            mockIPersonRepository.Verify(m => m.Update(It.IsAny<Person>()));
        }



        [TestCase]
        public void UpdatePerson_Test_FailWithNegativeAge()
        {
            // Arrange
            var detailedPerson = new DetailedPerson
            {
                FirstName = "Johannes",
                LastName = "Kepler",
                Age = -1,
                AgeGroup = "Old man"
            };

            var mockIPersonRepository = new Mock<IPersonRepository>();
            var service = new AgeRangerService(mockIPersonRepository.Object, mockIAgeGroupRepository.Object);

            // Act Assert
            Assert.Throws<Exception>(() => service.UpdatePerson(detailedPerson));
        }

        [TestCase]
        public void UpdatePerson_Test_FailWithBlankName(string nameToPass)
        {
            // Arrange
            var detailedPerson = new DetailedPerson
            {
                FirstName = nameToPass,
                LastName = "Kepler",
                Age = 14,
                AgeGroup = "Old man"
            };

            var mockIPersonRepository = new Mock<IPersonRepository>();
            var service = new AgeRangerService(mockIPersonRepository.Object, mockIAgeGroupRepository.Object);

            // Act Assert
            Assert.Throws<Exception>(() => service.UpdatePerson(detailedPerson));
        }
        #endregion

        #region DeletePerson tests
        [TestCase()]
        public void DeletePerson_Test_DeleteRepository()
        {
            // Arrange
            var mockIPersonRepository = new Mock<IPersonRepository>();
            mockIPersonRepository.Setup(m => m.Delete(It.IsAny<Person>()));
            mockIPersonRepository.Setup(m => m.Get(123)).Returns(new Person
            {
                Id = 123,
                FirstName = "Johannes",
                LastName = "Kepler",
                Age = 56
            });

            var service = new AgeRangerService(mockIPersonRepository.Object, mockIAgeGroupRepository.Object);
            
            // Act
            service.DeletePerson(123);

            // Assert
            mockIPersonRepository.Verify(m => m.Delete(It.IsAny<Person>()));
        }

        [TestCase]
        public void DeletePerson_Test_FailsToDeletePersonIfPersonNotFound()
        {
            // Assert
            var mockIPersonRepository = new Mock<IPersonRepository>();
            mockIPersonRepository.Setup(m => m.Delete(It.IsAny<Person>()));
            mockIPersonRepository.Setup(m => m.Get(123)).Throws(new Exception("Person not found"));

            var service = new AgeRangerService(mockIPersonRepository.Object, mockIAgeGroupRepository.Object);

            // Act Assert
            Assert.Throws<Exception>(() => service.DeletePerson(123)); 

            mockIPersonRepository.Verify(m => m.Delete(It.IsAny<Person>()), Times.Never);

        }

        #endregion

        #region GetAllAgeGroups
        [TestCase(3)]
        public void GetAllAgeGroups_Test_Result(int numberToExpect)
        {
            // Arrange
            var mockIPersonRepository = new Mock<IPersonRepository>();
            var service = new AgeRangerService(mockIPersonRepository.Object, mockIAgeGroupRepository.Object);

            // Act
            var allAgeGroups = service.GetAllAgeGroups().ToList();

            // Assert
            Assert.AreEqual(allAgeGroups.Count(), numberToExpect);
        }

        #endregion
    }
}
