using System;

namespace CM.ChampagneApp.DTO.Models.GETModels.FollowModels
{
    public class FollowingModel
    {
        public Guid Id { get; set; }
        
        public Guid FollowToId { get; set; }
        public string FollowToName { get; set; }
        public Guid FollowToProfileImg { get; set; }
        
        public bool IsRequesterFollowing { get; set; }
    }
}