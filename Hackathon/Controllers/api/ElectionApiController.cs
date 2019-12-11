using Hackathon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hackathon.Controllers.api
{
    public class ElectionApiController : ApiController
    {
        private ApplicationDbContext _dbContext=null;
        public ElectionApiController()
        {
            _dbContext = new ApplicationDbContext();
        }
        public IEnumerable<BaseData> GetAllPeople()
        {
            return _dbContext.BaseDatas.ToList();
        }
    }
}
