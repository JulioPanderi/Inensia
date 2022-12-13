using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Backend.DataAccessLayer
{
    public interface IPersons
    {
        List<Person> GetAll();
        List<Person> GetByFilter(Expression<Func<Person, bool>> filter);
    }
}