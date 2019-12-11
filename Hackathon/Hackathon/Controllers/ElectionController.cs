using Hackathon.Models;
using Hackathon.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Hackathon.Controllers
{
    public class ElectionController : Controller
    {
        private ApplicationDbContext _dbContext = null;
        HackathonEntities entities = new HackathonEntities();
        public ElectionController()
        {
            _dbContext = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }
        // GET: Election
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string flatNo, string password)
        {
            var loginPerson = entities.ResidentsOfDreamlands.Where(c => c.FlatNumber == flatNo && c.FlatNumber == password).FirstOrDefault(c => c.FlatNumber == flatNo);
            if (loginPerson != null)
            {
                TempData["FlatNo"] = flatNo;
                return RedirectToAction("Index");
            }
            else
            {
                return View("Login");
            }
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult NomineeRegister()
        {
            var position = _dbContext.RWAPositions.ToList();
            var ViewModel = new Nominee_Post_ViewModel()
            {
                Positions = position,
                Nominees = new NomineeForm()

            };
            return View(ViewModel);
        }
        [HttpPost]
        public ActionResult NomineeRegister(Nominee_Post_ViewModel nomineeForm)
        {
            var FlatNo = TempData["FlatNo"].ToString();
            var NomineeDetails = entities.ResidentsOfDreamlands.Where(c => c.FlatNumber == FlatNo).FirstOrDefault();
            //TempData["CandidateName"] = NomineeDetails.Name;
            if ((DateTime.Now.Year - NomineeDetails.DateOfBirth.Year) >= 21)
            {
                if (nomineeForm.Nominees.Id == 0)
                {
                    nomineeForm.Nominees.FlatNumber = TempData["FlatNo"].ToString();
                    nomineeForm.Nominees.CandidateName = NomineeDetails.Name;
                    _dbContext.NomineeForms.Add(nomineeForm.Nominees);
                    _dbContext.SaveChanges();
                }
                return Content(" You are Successfully Registered for Nominations");
            }
            else
            {
                return Content("Your Age is not Eligible For Nominations.Minimum 21 Years are Required");
            }
        }
        public ActionResult BallotForm()
        {
            var flatNo = TempData["FlatNo"].ToString();
            var voter = _dbContext.VotingDatas.Where(c => c.FlatNo == flatNo).FirstOrDefault();
            if (voter == null)
            {
                var Nominees = _dbContext.NomineeForms.Include(m => m.RWAPosition).ToList();
                return View(Nominees);
            }
            else
                return Content("Your Vote already Polled");
        }
        
        public ActionResult Ballot(int id)
        {
            var d = _dbContext.NomineeForms.Where(c => c.Id == id).FirstOrDefault();
            d.Votes++;
            //var voter = new VotingData()
            //{
            //    PresidentId = d.RWAPosition.Id,
            //    FlatNo=d.FlatNumber
                
            //};
            //_dbContext.VotingDatas.Add(voter);
            _dbContext.SaveChanges();
            return RedirectToAction("Login");
        }
        public ActionResult Result()
        {
            var winner = _dbContext.NomineeForms.ToList();/*.Where(c=>c.RWAPosition.Id==1).OrderByDescending(c => c.Votes)*/;
            int max1 = _dbContext.NomineeForms.Max(p => p.Votes);
            ViewBag.Max = max1;
            return View(winner);
        }
        public ActionResult Result1()
        {
            var winner = _dbContext.NomineeForms.Include(c=>c.RWAPosition).ToList();
            int max1 = _dbContext.NomineeForms.Max(p => p.Votes);
            ViewBag.Max = max1;

            return View(winner);
        }

    }
}
