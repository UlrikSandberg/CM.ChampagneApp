using System;
using CM.ChampagneApp.Business.Configuration;
using Microsoft.AppCenter.Crashes;
using System.ComponentModel;

namespace CM.ChampagneApp.UI.UIFacade.Models.UIFollowStructure
{
	public abstract class AbstractFollowModel : BaseUIModel
    {
      
	    public Guid Id { get; set; }
        
	    public Guid ProfileId { get; set; }
	    
	    public Guid ProfileImgId { get; set; }
	    
	    public string ProfileName { get; set; }
	    
	    public abstract string ProfileImgUrl { get; }
	    
	    public bool IsRequesterFollowing { get; set; }

		public bool IsEnabled { get; set; } = true;
    
    }
}