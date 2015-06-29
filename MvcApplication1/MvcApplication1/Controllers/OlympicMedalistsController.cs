using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MvcApplication1.Controllers
{
    public class OlympicMedalistsController : ApiController
    {
        // GET: /api/OlympicMedalists/
        public List<OlympicMedalist> Get()
        {

            var ent = new OffLineAppTestEntities();
            var dbResultSet = ent.OlympicMedalists;

            var resultList = new List<OlympicMedalist>();

            foreach (var result in dbResultSet)
            {
                var olympicMedalist = new OlympicMedalist()
                {
                    city = result.city ?? "",
                    year = result.year ?? 0,
                    sport = result.sport ?? "",
                    discipline = result.discipline ?? "",
                    country = result.country ?? "",
                    gender = result.gender ?? "",
                    @event = result.@event ?? "",
                    eventGender = result.eventGender ?? "",
                    color = result.color ?? "",
                    lastName = result.lastName ?? "",
                    firstName = result.firstName ?? ""
                };
                resultList.Add(olympicMedalist);
            }

            return resultList;
        }

    }

    public class OlympicMedalist
    {
        public string city { get; set; }
        public Nullable<int> year { get; set; }
        public string sport { get; set; }
        public string discipline { get; set; }
        public string country { get; set; }
        public string gender { get; set; }
        public string @event { get; set; }
        public string eventGender { get; set; }
        public string color { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
    }

}
