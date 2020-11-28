using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Data.Entity;
using ViahealthHD.Models;
using System.Web.Security;

namespace ViahealthHD.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        
        private NewActivityDataModel _newcontext;

        public HomeController()
        {                   
            _newcontext = new NewActivityDataModel();
        }

        public ActionResult Index()
        {
            var users = new ApplicationDbContext();
           
            var teamandname = _newcontext.ListofTeams.Include(c=> c.teamnames).Where(c => c.Username == User.Identity.Name).SingleOrDefault();
            ViewBag.name = (teamandname == null) ? "" : teamandname.Username;
            ViewBag.team = (teamandname == null)? "":teamandname.teamnames.name;
            return View();
        }


        public ActionResult Form(int? Id)
        {
            
            var actionid = _newcontext.NewActivitys.SingleOrDefault(c => c.Id == Id);
            var viewmodel = new ActivityViewModel();
            viewmodel.Listofactivitys = _newcontext.ListofActivitys.ToList();
            viewmodel.ActivityModel = actionid;

            return View(viewmodel);
        }


        public ActionResult Overview()
        {

            var overviewdata = _newcontext.NewActivitys.Include(c => c.LisfofActivityModel).Where(b => b.Username == User.Identity.Name).ToList();

            return View(overviewdata);
        }

        public ActionResult Admin()
        {

            AdminViewModel admin = new AdminViewModel();

            admin.teamsdatamodel = _newcontext.ListofTeams.Include(c => c.teamnames).ToList();
            admin.teamnames = _newcontext.teamnames.ToList();

            return View();

        }

  }
}