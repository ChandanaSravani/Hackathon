using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackathon.Models
{
    public class VotingData
    {
        public int Id{ get; set; }
        public string FlatNo { get; set; }
        public int PresidentId { get; set; }
        public int? VicePresidentId { get; set; }
        public int? SecretaryId { get; set; }
        public int ?TreasurerId { get; set; }
        public List<int?> GeneralBodyMembersIds { get; set; }
        //public RWAPosition RWAPosition { get; set; }
        //public int RWAPositionId { get; set; }
        //public NomineeForm NomineeForm { get; set; }
        //public int NomineeFormId { get; set; }

    }
}