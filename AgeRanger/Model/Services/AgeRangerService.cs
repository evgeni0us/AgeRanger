using System;
using System.Collections.Generic;
using System.Linq;
using AgeRanger.Data;
using AgeRanger.Data.Repositories;
using AgeRanger.Model.DTO;
using NLog;
using AgeGroup = AgeRanger.Model.DTO.AgeGroup;

namespace AgeRanger.Model.Services
{
    public class AgeRangerService : IAgeRangerService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IPersonRepository personRepository;
        private readonly IAgeGroupRepository ageGroupRepository;

        public AgeRangerService(IPersonRepository personRepository, IAgeGroupRepository ageGroupRepository)
        {
            this.personRepository = personRepository;
            this.ageGroupRepository = ageGroupRepository;
        }

        public IEnumerable<DetailedPerson> GetAllPeople()
        {
            try
            {
                logger.Debug("Getting all people from database");

                //TODO: Cache AgeGroups
                return (from p in personRepository.GetAll()
                        select new DetailedPerson
                        {
                            Id = p.Id,
                            FirstName = p.FirstName,
                            LastName = p.LastName,
                            Age = p.Age,
                            AgeGroup = (from ag in ageGroupRepository.GetAll()
                                        where (p.Age >= ag.MinAge || ag.MinAge == null) && (p.Age < ag.MaxAge || ag.MaxAge == null)
                                        select ag.Description).FirstOrDefault()
                        });
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred in method GetAllPeople of AgeRangerService");
                throw;
            }
            
        }

        public IEnumerable<DetailedPerson> FindPeople(string searchCondition)
        {
            try
            {
                logger.Debug("Searching people in database");

                //TODO: Cache AgeGroups
                return (from p in personRepository.Search(searchCondition)
                        select new DetailedPerson()
                        {
                            Id = p.Id,
                            FirstName = p.FirstName,
                            LastName = p.LastName,
                            Age = p.Age,
                            AgeGroup = (from ag in ageGroupRepository.GetAll()
                                        where (p.Age >= ag.MinAge || ag.MinAge == null) && (p.Age < ag.MaxAge || ag.MaxAge == null)
                                        select ag.Description).FirstOrDefault()
                        });
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred in method FindPeople of AgeRangerService");
                throw;
            }

        }

        public DetailedPerson AddPerson(DetailedPerson detailedPerson)
        {
            try
            {
                logger.Debug("Adding a new person to database");

                if (detailedPerson.Age < 0)
                    throw new Exception("Age cannot be less than zero");

                if (string.IsNullOrWhiteSpace(detailedPerson.FirstName))
                    throw new Exception("First name cannot be blank");

                if (string.IsNullOrWhiteSpace(detailedPerson.LastName))
                    throw new Exception("Last name cannot be blank");

                //TODO: Replace with automapper
                detailedPerson.Id = personRepository.Insert(new Person
                {
                    FirstName = detailedPerson.FirstName,
                    LastName = detailedPerson.LastName,
                    Age = detailedPerson.Age
                }).Id;

                return detailedPerson;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred in method AddPerson of AgeRangerService");
                throw;
            }
        }

        public void UpdatePerson(DetailedPerson detailedPerson)
        {
            try
            {
                logger.Debug("Updating a person in database");

                if (detailedPerson.Age < 0)
                    throw new Exception("Age cannot be less than zero");

                if (string.IsNullOrWhiteSpace(detailedPerson.FirstName))
                    throw new Exception("Person's first name cannot be blank");

                if (string.IsNullOrWhiteSpace(detailedPerson.LastName))
                    throw new Exception("Person's last name cannot be blank");

                //TODO: Replace with automapper
                personRepository.Update(new Person
                {
                    Id = detailedPerson.Id,
                    FirstName = detailedPerson.FirstName,
                    LastName = detailedPerson.LastName,
                    Age = detailedPerson.Age
                });

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred in method UpdatePerson of AgeRangerService");
                throw;
            }
        }

        public void DeletePerson(long id)
        {
            try
            {
                logger.Debug("Deleting a person from database");

                var person = personRepository.Get(id);

                personRepository.Delete(person);

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred in method DeletePerson of AgeRangerService");
                throw;
            }
        }

        public IEnumerable<AgeGroup> GetAllAgeGroups()
        {
            try
            {
                logger.Debug("Getting all age groups");

                //TODO: replace with automapper
                return ageGroupRepository.GetAll().Select(g => new AgeGroup
                {
                    Id = g.Id,
                    MinAge = g.MinAge,
                    MaxAge = g.MaxAge,
                    Description = g.Description
                });

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred in method GetAllAgeGroups");
                throw;
            }

        }
    }
}