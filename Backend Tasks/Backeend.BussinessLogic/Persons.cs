using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using Autofac;
using Backend.Models;

namespace Backend.BussinessLogic
{
    public class Persons : IPersons
    {
        DataAccessLayer.IPersons _dalPersons;

        public Persons(DataAccessLayer.IPersons dalPersons)
        {
            _dalPersons = dalPersons;
        }

        public List<Models.Person> GetBornAfterDateByNationality(DateTime bornDate, string nationality)
        {
            Expression<Func<Person, bool>> filter = p => p.Nationality == nationality && p.DateOfBirth > bornDate;
            List<Models.Person> persons = _dalPersons.GetByFilter(filter);
            return persons;
        }
    }
}
