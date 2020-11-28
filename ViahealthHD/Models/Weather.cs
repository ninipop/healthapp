using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ViahealthHD
{
    public class Weather
    {
        public int id { get; set; }
        public String main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
    }

    public class RootObject
    {
        public int id { get; set; }
        public List<Weather> weather { get; set; }
        public Main main { get; set; }
        public string name { get; set; }

    }

    public class Weathermaker
    {



        public void weatherstarter()
        {


            WebClient client = new WebClient();
            var stream = client.OpenRead("https://api.openweathermap.org/data/2.5/weather?q=Oslo&appid=71ea65d33e22b217347df8153aa09eca&units=metric");
            StreamReader dataflow = new StreamReader(stream);
            var data = dataflow.ReadToEnd();
            var stronglytypeid = JsonConvert.DeserializeObject<RootObject>(data);
            var weathercontext = new WeatherData();
            weathercontext.Database.ExecuteSqlCommand("delete from weathers");
            weathercontext.Database.ExecuteSqlCommand("delete from rootobjects");
            weathercontext.Weather.Add(stronglytypeid);
            weathercontext.SaveChanges();
           
        }


    }
    public partial class WeatherData : DbContext
    {
        public WeatherData()
            : base("name=WeatherConnection")
        {
        }

      


        public DbSet<RootObject> Weather { get; set; }

    }
}