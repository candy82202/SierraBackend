using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace NiceAdmin.Models.ViewModels.MembersVM
{
	public class MemberCriteria
	{
        public string MemberName { get; set; }
        public DateTime? MinBirth { get; set; }
        public DateTime? MaxBirth { get; set; }
        public bool? IsBan { get; set; }

    }
}