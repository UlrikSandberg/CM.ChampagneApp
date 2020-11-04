using System;
namespace CM.ChampagneApp.DTO.Models.GETModels.FollowModels
{
	public class FollowersModel
	{

		public Guid Id { get; set; }

		public Guid FollowById { get; set; }
		public string FollowByName { get; set; }
		public Guid FollowByProfileImgId { get; set; }

		public bool IsRequesterFollowing { get; set; }

	}
}
