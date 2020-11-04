using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FreshMvvm;
using System.Threading.Tasks;
using CM.ChampagneApp.UI.PageModelInitData;
using CM.ChampagneApp.UI.Pages.BrandPages.BrandCellarPage;
using CM.ChampagneApp.UI.Pages.InfoPages.BrandInfoPage;
using CM.ChampagneApp.UI.Pages.BrandPages.BrandProfilePage;
using CM.ChampagneApp.UI.Pages.ChampagnePages.ChampagneProfilePage;
using CM.ChampagneApp.UI.Pages.TastingPages.InspectTastingPage;
using CM.ChampagneApp.UI.Pages.ChampagnePages.Helpers;
using CM.ChampagneApp.UI.Pages.TastingPages.Helpers;
using CM.ChampagneApp.UI.Pages.Helpers;
using CM.ChampagneApp.UI.Pages.UserPages.CellarPage;
using CM.ChampagneApp.UI.Pages.UserPages.CellarPage.Helpers;
using CM.ChampagneApp.UI.Pages.UserPages.ProfilePage;
using CM.ChampagneApp.UI.Pages.UserPages.ProfilePage.Helper;

namespace CM.ChampagneApp.UI.Helpers.Routing
{
    public class PageRouter : IPageRouter
    {
		public Dictionary<urlBrandSpecification, string> urlBrandSpecificationsList = new Dictionary<urlBrandSpecification, string>();
		public Dictionary<urlUserSpecification, string> urlUserSpecificationsList = new Dictionary<urlUserSpecification, string>();
		public Dictionary<urlPublicUserSpecification, string> urlPublicUserSpecificationList = new Dictionary<urlPublicUserSpecification, string>();
		public Dictionary<urlChampagneSpecification, string> urlChampagneSpecificationList = new Dictionary<urlChampagneSpecification, string>();

        public PageRouter()
        {
            //Brand url constants based on respective enum
			urlBrandSpecificationsList.Add(urlBrandSpecification.cellar, urlBrandSpecification.cellar.ToString());
			urlBrandSpecificationsList.Add(urlBrandSpecification.page, urlBrandSpecification.page.ToString());
			urlBrandSpecificationsList.Add(urlBrandSpecification.profile, urlBrandSpecification.profile.ToString());

            //User url constants based on respective enum
			urlUserSpecificationsList.Add(urlUserSpecification.cellar, urlUserSpecification.cellar.ToString());

			//Public user url constants based on respective enum
			urlPublicUserSpecificationList.Add(urlPublicUserSpecification.cellar, urlPublicUserSpecification.cellar.ToString());

            //Champagne url constants based on respective enum

        }

        public async Task RouteToPath(string url, IPageModelCoreMethods coreMethods)
        {
			if(string.IsNullOrEmpty(url))
			{
				return;
			}
			// https://regexr.com/
			//var pattern = @"^([a-z]*)\/(.*)\/{0,1}";
			var pattern = @"^([A-z]*)\/([0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]*)\/?([A-z]*)\/([0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]*)?";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            var match = regex.Match(url);

			int urlDepth = SolveUrlDepth(match);

			var type = match.Groups[1].Value;
         
			if(type.Equals(urlRoot.brand.ToString()))
			{
				//The type id
				var brandId = match.Groups[2].Value;

                //The function under type = brand
				switch(match.Groups[3].Value)
				{
					case "profile":
						await coreMethods.PushPageModel<BrandProfilePageModel>(new BrandProfileInitData(Guid.Parse(brandId)));
                        return;
					case "cellar":                  
						await coreMethods.PushPageModel<BrandCellarPageModel>(new BrandCellarInitData(Guid.Parse(brandId)));
                        return;
					case "page":
                        await coreMethods.PushPageModel<BrandInfoPageModel>(new BrandInfoPageModelInitData(Guid.Parse(brandId), Guid.Parse(match.Groups[4].Value)));
						return;
				}
			}
			else if(type.Equals(urlRoot.user.ToString()))
			{
				//The type id
				var userId = match.Groups[2].Value;

                //The functions under type = user
				switch(match.Groups[3].Value)
				{
					case "cellar":
						await coreMethods.PushPageModel<CellarPageModel>(new CellarPageModelInitData(Guid.Parse(userId)));
						return;                  
				}
				return;
			}
			else if (type.Equals(urlRoot.publicuser.ToString()))
			{
				//The type id
				var publicUserId = match.Groups[2].Value;

				if (urlDepth == 1)//Route to posibilities regarding the first niveau urls
				{
					await coreMethods.PushPageModel<ProfilePageModel>(new ProfilePageInitData(Guid.Parse(publicUserId),
						false));
				}
				else
				{               
					//The functions under type = publicUser
					switch (match.Groups[3].Value)
					{
						case "cellar":
                            await coreMethods.PushPageModel<CellarPageModel>(new CellarPageModelInitData(Guid.Parse(publicUserId)));
							return;
					}
				}
				return;
			}
			else if(type.Equals(urlRoot.champagne.ToString()))
			{
				//The type id
				var champagneId = match.Groups[2].Value;

				if(urlDepth == 1)
				{
					await coreMethods.PushPageModel<ChampagneProfilePageModel>(new ChampagneProfileInitData(Guid.Parse(champagneId)));
				}
				else
				{
                    //Functions under type = champagne
					switch(match.Groups[3].Value)
					{
						case "inspecttasting":
							var tastingId = match.Groups[4].Value;
							await coreMethods.PushPageModel<InspectTastingPageModel>(new InspectTastingInitData(Guid.Parse(tastingId), Guid.Parse(champagneId)));
							break;
					}
				}
				return;
			}

			//Url was defect!         
			System.Diagnostics.Debug.WriteLine("PageRouter.RouteToPath() -> Exception() -> The provided url: " + url + ". Did not meet the specified conventions");

        }

		private int SolveUrlDepth(Match match)
		{
			var groups = match.Groups;

			int depth = 0;

			if(!groups[2].Value.Equals(""))
			{
				depth = 1;
			}

			if(!groups[4].Value.Equals(""))
			{
				depth = 2;
			}

			return depth;
		}
    }
}