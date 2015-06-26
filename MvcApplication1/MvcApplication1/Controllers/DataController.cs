using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class DataController : Controller
    {
        // GET: /Data/OlympicMedalists
        public ActionResult OlympicMedalists()
        {
            var ent = new OffLineAppTestEntities();
            var dbResultSet = ent.OlympicMedalists.Where(x => x.city == "Tokyo");

            return this.Json(dbResultSet, JsonRequestBehavior.AllowGet);
        }

        // GET: /Data/ColumnDomains
        public ActionResult ColumnDomains()
        
        {
            var ent = new OffLineAppTestEntities();
            var dbResultSet = ent.ColumnDomains;

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
                switch (result.group)
                {
                    case "cities":
                        cityList.Add(result.value);
                        break;
                    case "years":
                        yearList.Add(result.value);
                        break;
                    case "countries":
                        countryList.Add(result.value);
                        break;
                    case "disciplines":
                        disciplineList.Add(result.value);
                        break;
                    case "events":
                        eventList.Add(result.value);
                        break;
                    case "sports":
                        sportList.Add(result.value);
                        break;
                    case "genders":
                        genderList.Add(result.value);
                        break;
                    case "eventGenders":
                        eventGenderList.Add(result.value);
                        break;
                    case "colors":
                        colorList.Add(result.value);
                        break;
                    default:
                        break;
                }
            }

            
            return this.Json(new {
                cities = cityList,
                years = yearList,
                countries = countryList,
                disciplines = disciplineList,
                events = eventList,
                sports = sportList,
                genders = genderList,
                eventGenders = eventGenderList,
                colors = colorList,
            }, JsonRequestBehavior.AllowGet);
        }

    }

}
