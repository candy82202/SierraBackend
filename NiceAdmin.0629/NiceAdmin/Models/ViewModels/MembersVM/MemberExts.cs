using NiceAdmin.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NiceAdmin.Models.ViewModels.MembersVM
{
	public static class MemberExts
	{
		public static MemberIndexVM ToIndexVM(this Member mbr)
		{
			return new MemberIndexVM
			{
				MemberId = mbr.MemberId,
				MemberName = mbr.MemberName,
				Email = mbr.Email,
				Address = mbr.Address,
				Phone = mbr.Phone,
				CreateAt = mbr.CreateAt,
				Birth = mbr.Birth,
				Gender = mbr.Gender,
				ImageName = mbr.ImageName,
				IsBan = mbr.IsBan,
				CancelTimes = mbr.CancelTimes,

			};
		}
	}
}