using Hackathon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackathon.ViewModel
{
    public class Nominee_Post_ViewModel
    {
        public IEnumerable<RWAPosition> Positions { get; set; }
        public NomineeForm Nominees{ get; set; }
    }
}