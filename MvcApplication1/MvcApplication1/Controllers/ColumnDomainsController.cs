using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MvcApplication1.Controllers
{
    public class ColumnDomainsController : ApiController
    {
        // GET: /api/ColumnDomains/
        public IEnumerable<KeyValuePair<string, List<string>>> Get()
        {
            var ent = new OffLineAppTestEntities();
            var dbResultSet = ent.ColumnDomains.OrderBy(t => t.ID);

            var cityList = new List<string>();
            var yearList = new List<string>();
            var sportList = new List<string>();
            var disciplineList = new List<string>();
            var countryList = new List<string>();
            var genderList = new List<string>();
            var eventList = new List<string>();
            var eventGenderList = new List<string>();
            var colorList = new List<string>();

            foreach (var result in dbResultSet)
            {
                var value = result.value ?? "";
                switch (result.group)
                {
                    case "cities":
                        cityList.Add(value);
                        break;
                    case "years":
                        yearList.Add(value);
                        break;
                    case "countries":
                        countryList.Add(value);
                        break;
                    case "disciplines":
                        disciplineList.Add(value);
                        break;
                    case "events":
                        eventList.Add(value);
                        break;
                    case "sports":
                        sportList.Add(value);
                        break;
                    case "genders":
                        genderList.Add(value);
                        break;
                    case "eventGenders":
                        eventGenderList.Add(value);
                        break;
                    case "colors":
                        colorList.Add(value);
                        break;
                    default:
                        break;
                }
            }

            var resultMap = new Dictionary<string,List<string>>();

            resultMap.Add("cities", cityList);
            resultMap.Add("years", yearList);
            resultMap.Add("countries", countryList);
            resultMap.Add("disciplines", disciplineList);
            resultMap.Add("events", eventList);
            resultMap.Add("sports", sportList);
            resultMap.Add("genders", genderList);
            resultMap.Add("eventGenders", eventGenderList);
            resultMap.Add("colors", colorList);

            return resultMap;
        }

    }
}
