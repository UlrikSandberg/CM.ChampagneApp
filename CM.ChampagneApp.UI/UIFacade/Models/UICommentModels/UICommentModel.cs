using System;
using CM.ChampagneApp.Business.Configuration;
using System.ComponentModel;
namespace CM.ChampagneApp.UI.UIFacade.Models.UICommentModels
{
	public class UICommentModel : BaseUIModel
	{
		public Guid Id { get; set; }

		public Guid ContextId { get; set; }
		public string ContextType { get; set; }

		//
		public Guid AuthorId { get; set; }
		public string AuthorName { get; set; }
		public Guid AuthorProfileImgId { get; set; }

		//
		public DateTime Date { get; set; }
		public string Comment { get; set; }

		public int NumberOfLikes { get; set; }
		public bool IsLikedByRequester { get; set; }

		public bool IsRequesterAuthor { get; set; }

		public bool IsEnabled { get; set; } = true;

        public string AuthorProfileImgUrl
		{
            get
			{
				if(AuthorProfileImgId.Equals(Guid.Empty))
				{
					return "PlaceholderProfileImg.png";
				}
				else
				{
                    return new Uri(Configuration.BlobUserStorageUrl, $"{AuthorId}/images/{AuthorProfileImgId}/small.jpg").ToString();
				}
			}
		}

		public string CommentDateStringRepresentation
        {
            get
            {
				var now = DateTime.Now;
				var utc = DateTime.UtcNow;

				var timeZoneBuffer = now.Ticks - utc.Ticks;

				var timeRespectiveToZone = Date.Ticks + timeZoneBuffer;

				var dateRespectiveToZone = new DateTime(timeRespectiveToZone);

				return dateRespectiveToZone.ToString("MMMM dd, yyyy");
                //return Date.ToString("MMMM dd, yyyy");
            }
        }
    }
}
