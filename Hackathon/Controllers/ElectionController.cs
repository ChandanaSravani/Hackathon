using Hackathon.Models;
using Hackathon.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Configuration;
using System.Threading.Tasks;
using System.Net.Http;

namespace Hackathon.Controllers
{
   
    public class ElectionController : Controller
    {
        private string WebApiAddress = ConfigurationManager.AppSettings["ApiBaseAddress"];
        private ApplicationDbContext _dbContext = null;

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
        [HttpGet]
        public async Task<ActionResult> ListOfNominees()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(WebApiAddress);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("election");
                if (response.IsSuccessStatusCode)
                {
                    var nominees = await response.Content.ReadAsAsync<IList<NomineeForm>>();
                    return View(nominees);
                }
                else
                    return Content("No records");
            }
            
        }
        [HttpPost]
        public ActionResult Login(string flatNo, string password)
        {
            TempData["FlatNo"] = flatNo;
            var loginPerson = _dbContext.BaseDatas.Where(c => c.FlatNumber == flatNo && c.FlatNumber == password).FirstOrDefault(c => c.FlatNumber == flatNo);
            if (loginPerson != null)
            {
                
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
            var FlatNo = TempData["FlatNo"].ToString();
            TempData["FlatNo"] = FlatNo;
            var NomineeDetails = _dbContext.BaseDatas.Where(c => c.FlatNumber == FlatNo).FirstOrDefault();
            //TempData["CandidateName"] = NomineeDetails.Name;
            if ((DateTime.Now.Year - NomineeDetails.DateOfBirth.Year) >= 21)
            {
                var position = _dbContext.RWAPositions.ToList();
                var ViewModel = new Nominee_Post_ViewModel()
                {
                    Positions = position,
                    Nominees = new NomineeForm()

                };
                return View(ViewModel);
            }
            else
            {
                return View("MinAge");
            }
        }
        [HttpGet]
        public async Task<ActionResult> EditNomineeDetails(int id)
        {
            var position = _dbContext.RWAPositions.ToList();
            var nominee_vm = new Nominee_Post_ViewModel();
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(WebApiAddress);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync($"election/{id}");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var nominees = await response.Content.ReadAsAsync<NomineeForm>();
                    nominee_vm.Nominees = nominees;
                    nominee_vm.Positions = position;

                    return View("NomineeRegister", nominee_vm);
                }
                return Content("Not going to loop");
            }
            //var nominee = _dbContext.NomineeForms.Include(c => c.RWAPosition).Where(c => c.Id == id).SingleOrDefault();
            //var nominee_vm = new Nominee_Post_ViewModel();
            //nominee_vm.Nominees = nominee;
            //var position = _dbContext.RWAPositions.ToList();
            //nominee_vm.Positions = position;
            //return View("NomineeRegister", nominee_vm);
        }
        [HttpGet]
        public ActionResult DeleteNomineeDetails(int id)
        {
            var nominee = _dbContext.NomineeForms.Include(c => c.RWAPosition).Where(c => c.Id == id).SingleOrDefault();
            var nominee_vm = new Nominee_Post_ViewModel();
            nominee_vm.Nominees = nominee;
            var position = _dbContext.RWAPositions.ToList();
            nominee_vm.Positions = position;
            return View(nominee);
        }
        [HttpPost]
        public async Task<ActionResult> DeleteNomineeDetails(NomineeForm nominee)
        {
            using (var client = new HttpClient())
            {
                
                client.BaseAddress = new Uri(WebApiAddress);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.DeleteAsync($"election/delete/{nominee.Id}");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Content("Deleted Succesfully");
                }
            }

            return View("MinAge");
        }

        public ActionResult MinAge()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> NomineeRegister(Nominee_Post_ViewModel nomineeForm)
        {
            
            var FlatNo = TempData["FlatNo"].ToString();
            var NomineeDetails = _dbContext.BaseDatas.Where(c => c.FlatNumber == FlatNo).FirstOrDefault();

            if (nomineeForm.Nominees.Qualification == null)
            {
                return Content(" Fill all the details");
            }
            if (nomineeForm.Nominees.Id == 0)
            {
                using (var client = new HttpClient())
                {
                    nomineeForm.Nominees.FlatNumber = TempData["FlatNo"].ToString();
                    client.BaseAddress = new Uri(WebApiAddress);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await client.PostAsJsonAsync("election/new", nomineeForm.Nominees);
                    if(response.StatusCode==System.Net.HttpStatusCode.Created)
                    {
                        return View("DisplayMessage");
                    }
                }
               
            }
            else if(nomineeForm.Nominees.Id!=0)
            {
                using (var client = new HttpClient())
                {
                    nomineeForm.Nominees.FlatNumber = TempData["FlatNo"].ToString();
                    client.BaseAddress = new Uri(WebApiAddress);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await client.PutAsJsonAsync($"election/update/{nomineeForm.Nominees.Id}", nomineeForm.Nominees);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return Content("Succesfully edited");
                    }
                }
            }
            return HttpNotFound();

        }
        public ActionResult DisplayMessage()
        {
            return View();
        }
        public async Task<ActionResult> BallotForm()
        {
            var flatNo = TempData["FlatNo"].ToString();
            var voter = _dbContext.VotingDatas.Where(c => c.FlatNo == flatNo).FirstOrDefault();
            if (voter == null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(WebApiAddress);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await client.GetAsync("election");
                    if (response.IsSuccessStatusCode)
                    {
                        var nominees = await response.Content.ReadAsAsync<List<NomineeForm>>();
                        return View(nominees);
                    }
                    else
                        return Content("No records");
                }
                //var Nominees = _dbContext.NomineeForms.Include(m => m.RWAPosition).ToList();
                //return View(Nominees);
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
            return View("Message");
        }
        public ActionResult Message()
        {
            return View();
        }
        public ActionResult Result()
        {
            var winner = _dbContext.NomineeForms.Where(c=>c.RWAPosition.Id==1).OrderByDescending(c => c.Votes);
            int max1 = _dbContext.NomineeForms.Max(p => p.Votes);
            ViewBag.Max = max1;
            return View(winner);
        }
        public ActionResult Result1()
        {
            var winner = _dbContext.NomineeForms.Include(c => c.RWAPosition).ToList();
            int max1 = _dbContext.NomineeForms.Max(p => p.Votes);
            ViewBag.Max = max1;

            return View(winner);
        }

    }
}
