using AgilAds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilAds.BusinessServices.Utility
{
    public interface IPersonServices
    {
        Person GetPersonById(object id);
        Person GetPersonByName(string firstName, string lastName);
        Person GetPersonByName(string fullName);
        IEnumerable<Person> GetAllPeople();
        int CreatePerson(Person person);
        bool UpdatePerson(int id, Person modifiedPerson);
        bool DeletePerson(object id);
    }
}
