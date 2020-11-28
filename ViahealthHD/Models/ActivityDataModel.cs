using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


namespace ViahealthHD.Models
{
    //newdatamodel for adding activitys

    public class ActivityDataModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public ListofActivitysModel LisfofActivityModel { get; set; }
        public int LisfofActivityModelId { get; set; }
        public int Length { get; set; }
        public DateTime Date { get; set; }
    }

    public class ListofActivitysModel
    {
        public int Id { get; set; }
        public string ActivityName { get; set; }
        public int Ratio { get; set; }

    }

    public class TeamsDataModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public Teamnames teamnames { get; set; }
        public int TeamnamesId { get; set; }
    }

    public class Teamnames
    {
        public int Id { get; set; }
        public string name { get; set; }

    }


    public partial class NewActivityDataModel : DbContext
    {
        public NewActivityDataModel()
            : base("name=DefaultConnection")
        {
        }




        public DbSet<ActivityDataModel> NewActivitys { get; set; }
        public DbSet<ListofActivitysModel> ListofActivitys { get; set; }
        public DbSet<TeamsDataModel> ListofTeams { get; set; }
        public DbSet<Teamnames> teamnames { get; set; }

    }

    
}