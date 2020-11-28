using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data.SqlClient;
using System.Data;
using System.Web.Http;
using ViahealthHD.Models;
using ViahealthHD;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Data.Entity;
using Dapper;



namespace ViahealthHD.Controllers.API
{
      [Authorize]
    public class ActivityAPIController : ApiController
    {
      
        private NewActivityDataModel _newcontext;

        public ActivityAPIController()
        {
         
            _newcontext = new NewActivityDataModel();
        }

        [HttpDelete]
        public void deleteactivity(int id)
        {
            var rowtodelete = _newcontext.NewActivitys.SingleOrDefault(c => c.Id == id);
            _newcontext.NewActivitys.Remove(rowtodelete);
            _newcontext.SaveChanges();

            MyHub1.newdatatoupdate();
        }


        [HttpPost]
        public IHttpActionResult createactivity(ActivityDataModel activity)
        {

            //var date = Convert.ToDateTime(activity.DateA);
            //string fixdatekey = date.ToString("yyyyMMdd");



            if (activity.Id == 0)
            {
                //activity.CurrentTime = DateTime.Now;
                //activity.Mnd = DateTime.Now.Month;
                //activity.Yr = DateTime.Now.Year;
                activity.Username = User.Identity.Name;
                _newcontext.NewActivitys.Add(activity);
            }
            else
            {
                var activityindb = _newcontext.NewActivitys.Single(c => c.Id == activity.Id);
                activityindb.Username = User.Identity.Name;
                activityindb.Date = activity.Date;
                activityindb.Length = activity.Length;
                activityindb.LisfofActivityModelId = activity.LisfofActivityModelId;
            }

            _newcontext.SaveChanges();

            MyHub1.newdatatoupdate();

            return Created(new Uri(Request.RequestUri + "/" + activity.Id), activity);


        }
        [HttpGet]
        [Route("api/teamscores/")]
        public string teamscore()
        {
        
            var data = _newcontext.NewActivitys.Include(c => c.LisfofActivityModel)
                .Join(_newcontext.ListofTeams,
                 activity => activity.Username,
                 Teams => Teams.Username,
                 (Activity, Teams) => new { Activity, Teams }
            )
                .GroupBy(d => d.Teams.teamnames.name)
                .Select(d => new { Team = d.Key, Minutes = Math.Round(d.Sum(v => v.Activity.Length * v.Activity.LisfofActivityModel.Ratio * 0.761 / 1000), 2) }).ToList();

            var sumpackagejson = JsonConvert.SerializeObject(data);

            return sumpackagejson;




        }
        [HttpGet]
        [Route("api/myscore/")]
        public string myscore()
        {
            

            var data = _newcontext.NewActivitys.Include(c => c.LisfofActivityModel).Where(c => c.Username == User.Identity.Name)
                .GroupBy(d => d.LisfofActivityModel.ActivityName)
                .Select(d => new { ActivityName = d.Key, Minutes = Math.Round(d.Sum(v => v.Length * v.LisfofActivityModel.Ratio * 0.761 / 1000), 2) }).ToList();

            var sumpackagejson = JsonConvert.SerializeObject(data);

            return sumpackagejson;

        }

        [HttpGet]
        [Route("api/myteamscore/")]
        public string myteamscore()
        {

            var team = _newcontext.ListofTeams.Where(c => c.Username == User.Identity.Name).Select(b => b.teamnames.name).SingleOrDefault();

            var data = _newcontext.NewActivitys.Include(c => c.LisfofActivityModel)
               .Join(_newcontext.ListofTeams,
                activity => activity.Username,
                Teams => Teams.Username,
                (Activity, Teams) => new { Activity, Teams }
           )
           .Where(c=>c.Teams.teamnames.name==team)
               .GroupBy(d => d.Teams.teamnames.name)
               
               .Select(d => new { ActivityName = d.Key, Minutes = Math.Round(d.Sum(v => v.Activity.Length * v.Activity.LisfofActivityModel.Ratio * 0.761 / 1000), 2) }).ToList();
            
            var sumpackagejson = JsonConvert.SerializeObject(data);

            return sumpackagejson;

        }

        [HttpGet]
        [Route("api/dashstats/")]
        public string dashstats()
        {

            var km = _newcontext.NewActivitys.Include(c => c.LisfofActivityModel)
                .Where(c=>c.Username == User.Identity.Name) 
                .GroupBy(d => d.Username)

                 .Select(d => new { User = d.Key, Km = Math.Round(d.Sum(v => v.Length * v.LisfofActivityModel.Ratio * 0.761 / 1000), 2)
                 ,
                                    steps = Math.Round(d.Sum(v => v.Length * v.LisfofActivityModel.Ratio * 0.761 ), 2),
                                    minutes = d.Sum(v => v.Length * v.LisfofActivityModel.Ratio ),
                                    count = d.Count()
                          
                 
                 }).ToList();

            var sumpackagejson = JsonConvert.SerializeObject(km);

            return sumpackagejson;

        }
        [HttpGet]
        [Route("api/unassigned/")]
        public string unassigned()
        {
            
            var sql = "SELECT[UserName] FROM[AspNetUsers] where UserName not in (select username from [TeamsDataModels])";
            var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            con.Open();
            var condata = con.Query<TeamsDataModel>(sql).ToList();
            var sumpackagejson  = JsonConvert.SerializeObject(condata);
            return sumpackagejson;

        }

        [HttpGet]
        [Route("api/listofteams/")]
        public string listofteams()
        {

            var teams = _newcontext.teamnames.ToList();
            var sumpackagejson = JsonConvert.SerializeObject(teams);

            return sumpackagejson;

        }


        [HttpPost]
        [Route("api/createmap/")]
        public void createam(TeamsDataModel teammap)
        {
            if (_newcontext.ListofTeams.Any(c => c.Username == teammap.Username))
            {
                return;

            }
            else
            {
                _newcontext.ListofTeams.Add(teammap);
                _newcontext.SaveChanges();
            }
        }
        [HttpPost]
        [Route("api/teamname/")]
        public void teamname(Teamnames teamname)
        {
            if (_newcontext.teamnames.Any(c => c.name == teamname.name))
            {
                return;
            }
            else
            {
                _newcontext.teamnames.Add(teamname);
                _newcontext.SaveChanges();
            }
        }

        [HttpPost]
        [Route("api/deleteteam/")]
        public string deleteteam(Teamnames teamName)
        {
            var deleteteam = _newcontext.teamnames.Where(c => c.Id == teamName.Id).SingleOrDefault();
            if (_newcontext.ListofTeams.Any(c => c.TeamnamesId == teamName.Id))
            {
                return "Cannot delete assigned team";
            }
            else
            {
                _newcontext.teamnames.Remove(deleteteam);
                _newcontext.SaveChanges();

                return "Ok";


            }
            

        }



        [HttpGet]
        [Route("api/weather/")]
        public string weather()
        {
            var weather = new WeatherData();
            var weathertoday =  weather.Weather.Include(c=>c.weather).SingleOrDefault();
            var sumpackagejson = JsonConvert.SerializeObject(weathertoday);

            return sumpackagejson;

        }



    }
}
