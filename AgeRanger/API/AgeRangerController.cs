using System.Collections.Generic;
using System.Web.Http;
using AgeRanger.Model.DTO;
using AgeRanger.Model.Services;
using Ninject;

namespace AgeRanger.API
{
    /// <summary>
    /// DO NOT add exception handling - see ExceptionFilter class
    /// </summary>
    public class AgeRangerController : ApiController
    {
        private readonly IAgeRangerService ageRangerService;

        [Inject]
        public AgeRangerController(IAgeRangerService ageRangerService)
        {
            this.ageRangerService = ageRangerService;
        }

        /// <summary>
        /// Returns all people
        /// </summary>
        /// <returns>Array of persons</returns>
        [AcceptVerbs("GET")]
        public IEnumerable<DetailedPerson> GetAllPeople()
        {
            return ageRangerService.GetAllPeople();
        }

        /// <summary>
        /// Creates a new person
        /// </summary>
        /// <param name="person">New person</param>
        [AcceptVerbs("POST")]
        public void AddPerson(DetailedPerson person)
        {
            ageRangerService.AddPerson(person);
        }

        /// <summary>
        /// Updates a person
        /// </summary>
        /// <param name="person">Person to be updated</param>
        [AcceptVerbs("PUT")]
        public void UpdatePerson(DetailedPerson person)
        {
            ageRangerService.UpdatePerson(person);
        }

        /// <summary>
        /// Deletes an existing person
        /// </summary>
        /// <param name="id">Person's id</param>
        [AcceptVerbs("DELETE")]
        public void DeletePerson(string id)
        {
            ageRangerService.DeletePerson(long.Parse(id));
        }


        /// <summary>
        /// Returns all age groups
        /// </summary>
        /// <returns>Array of age groups</returns>
        [AcceptVerbs("GET")]
        public IEnumerable<AgeGroup> GetAllAgeGroups()
        {
            return ageRangerService.GetAllAgeGroups();
        }


    }
}
