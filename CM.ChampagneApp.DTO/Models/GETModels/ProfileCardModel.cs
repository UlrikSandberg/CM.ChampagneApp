using System;
using System.Collections.Generic;

namespace CM.ChampagneApp.DTO.Models
{
	public class ProfileCardModel
	{
		      
		public Guid Id { get; set; }
		public string Title { get; set; }
		public Guid CardImgId { get; set; }
		public string Url { get; set; }
		public List<string> Labels { get; set; }
              
    }
}
