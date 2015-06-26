using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class DataController : Controller
    {
        // GET: /Data/OlympicMedalists
        public ActionResult OlympicMedalists()
        {
            List<OlympicMedalist> resultList = new List<OlympicMedalist>() {
                new OlympicMedalist() { city = "Anders", year = "1896", sport = "Aquatics", discipline = "Swimming", country = "HUN", gender = "Men", @event = "100m freestyle", eventGender = "M", color = "Gold", lastName = "Hajos", firstName = "Alfred" }, 
            };
            return this.Json(resultList, JsonRequestBehavior.AllowGet);
        }

        // GET: /Data/OlympicMedalists
        public ActionResult ColumnDomains()
        {
            object result = new {
                cities = new List<string>() {"Anders"},
                years = new List<string>() {"1896"},
                countries = new List<string>() {"HUN"},
                disciplines = new List<string>() {"Swimming"},
                events = new List<string>() {"100m freestyle"},
                sports = new List<string>() {"Aquatics"},
                genders = new List<string>() {"Men"},
                eventGenders = new List<string>() {"M"},
                colors = new List<string>() {"Gold"},
            };
            
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }



    }

    public class OlympicMedalist
    {
        public string city { get; set; }
        public string year { get; set; }
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
