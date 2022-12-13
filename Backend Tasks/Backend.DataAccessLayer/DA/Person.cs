using Backend.DataAccessLayer.Utilities;
using Backend.Models;
using log4net;
using log4net.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Policy;
using System.Text;

namespace Backend.DataAccessLayer
{
    public class Persons : IPersons
    {
        ILog _log;

        public Persons(ILog log)
        {
            _log = log;
        }

        private string _function;
        public List<Models.Person> GetAll()
        {
            List<Models.Person> retValue = new List<Models.Person>();
            try
            {
                List<Models.Person> persons = GetPersons();
                _function = String.Empty;
                return persons.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error trying to get the persons");
            }
        }

        public List<Models.Person> GetByFilter(Expression<Func<Models.Person, bool>> filter)
        {
            try
            {
                List<Models.Person> persons = GetPersons(filter);
                _function = String.Empty;
                return persons;
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error trying to get the persons");
            }
        }

        private List<Models.Person> GetPersons(Expression<Func<Models.Person, bool>> filter = null)
        {
            string jsonSource = Properties.Settings.Default.JsonSource;
            string jsonData = string.Empty;

            try
            {
                //Verify if the source is file or api
                if (jsonSource.ToLower().StartsWith("http://"))
                {
                    jsonData = GetJsonFromUri(jsonSource);
                }
                else
                {
                    jsonData = GetJsonFromFile(jsonSource);
                }
                _function = String.Empty;
                var retValue = JsonConvert.DeserializeObject<List<Models.Person>>(jsonData);
                if (filter != null)
                {
                    retValue = retValue.AsQueryable().Where(filter).ToList();
                }
                return retValue;
            }
            catch (Exception ex)
            {
                _function = String.IsNullOrEmpty(_function) ? "GetPersons" : _function;
                this.WriteLogInfo(_log, _function, ex.Message);
                throw ex;
            }
        }

        private string GetJsonFromFile(string JsonSource)
        {
            string json = string.Empty;
            try
            {
                json = File.ReadAllText(JsonSource);
            }
            catch (Exception ex)
            {
                _function = String.IsNullOrEmpty(_function) ? "GetPersons" : _function;
                this.WriteLogInfo(_log, _function, ex.Message);
                throw ex;
            }

            return json;
        }

        private string GetJsonFromUri(string JsonSource)
        {
            string json = string.Empty;
            try
            {
                WebRequest request = WebRequest.Create(JsonSource);
                using (var wb = new WebClient())
                {
                    string response = wb.DownloadString(JsonSource);
                    if (response != null && response.Trim() != string.Empty)
                    {
                        json = response.Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                _function = "GetJsonFromUri";
                throw ex;
            }
            return json;
        }
    }
}
