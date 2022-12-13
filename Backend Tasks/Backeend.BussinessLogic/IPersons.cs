using Backend.Models;
using System;
using System.Collections.Generic;

namespace Backend.BussinessLogic
{
    public interface IPersons
    {
        List<Person> GetBornAfterDateByNationality(DateTime bornDate, string nationality);
    }
}