using System;
using System.Collections.Generic;
using CM.ChampagneApp.Business.Configuration;
using System.ComponentModel;

namespace CM.ChampagneApp.UI.UIFacade.Models
{
	public abstract class AbstractProfileCard : BaseUIModel
    {      
		public Guid Id { get; set; }

		public Guid AuthorId { get; set; }

        public string Title { get; set; }

		public Guid CardImgId { get; set; }

        public string Url { get; set; }

        public List<string> Labels { get; set; }

		public abstract string CardImgUrl { get; }
      
    }
}
