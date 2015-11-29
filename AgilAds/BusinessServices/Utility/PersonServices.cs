using AgilAds.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace AgilAds.BusinessServices.Utility
{
    public class PersonServices : IPersonServices
    {
        private readonly IUnitOfWork _uow;
        public PersonServices(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public Models.Person GetPersonById(object id)
        {
            return _uow.PersonRepository.GetByID(id);
        }

        public Models.Person GetPersonByName(string firstName, string lastName)
        {
            return _uow.PersonRepository.GetSingle(
                p => p.Firstname.Equals(firstName) &&
                    p.Lastname.Equals(lastName));
        }

        public Models.Person GetPersonByName(string fullName)
        {
            return _uow.PersonRepository.GetSingle(
                p => p.Fullname.Equals(fullName));
        }

        public IEnumerable<Models.Person> GetAllPeople()
        {
            return _uow.PersonRepository.GetAll();
        }

        public int CreatePerson(Models.Person person)
        {
            using (var scope = new TransactionScope())
            {
                _uow.PersonRepository.Insert(person);
                _uow.Save();
                scope.Complete();
            }
            return person.id;
        }

        public bool UpdatePerson(int id, Models.Person modifiedPerson)
        {
            var success = false;
            using (var scope = new TransactionScope())
            {
                var personExists = _uow.PersonRepository.Exists(id);
                if (personExists)
                {
                    _uow.PersonRepository.Update(modifiedPerson);
                    _uow.Save();
                    scope.Complete();
                    success = true;
                }
                return success;
            }
        }

        public bool DeletePerson(object id)
        {
            var success = false;
            using (var scope = new TransactionScope())
            {
                var personExists = _uow.PersonRepository.Exists(id);
                if (personExists)
                {
                    _uow.PersonRepository.Delete(id);
                    _uow.Save();
                    scope.Complete();
                    success = true;
                }
                return success;
            }
        }
    }
}