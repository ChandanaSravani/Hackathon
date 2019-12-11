using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hackathon.dtos
{
    public class NomineeDto
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Your Highest Qualification")]
        public string Qualification { get; set; }

        [Required]
        [Display(Name = "Your Current Occupation")]
        public string CurrentOccupation { get; set; }

        [Required]
        [Display(Name = "Are you a previous RWA Member?")]
        public bool PreviousRwaMember { get; set; }

        [Required]
        [Display(Name = "Part Of Government/Police/NGO")]
        public bool PublicSectorEmployee { get; set; }

        [Required]
        [Display(Name = "Criminal Record")]
        public bool CriminalRecord { get; set; }
        [Required]
        public string FlatNumber { get; set; }
        public int Votes { get; set; }
        public string CandidateName { get; set; }
        //reference table and refernce key
        public RWAPositionDto RWAPosition { get; set; }

        [Required]
        public int RWAPositionId { get; set; }
    }
}