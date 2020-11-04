using System;

namespace CM.ChampagneApp.DTO.Models.UserModels
{
    public class ImageCustomization
    {
        public Guid profilePictureImgId { get; set; }
        public Guid profileCoverImgId { get; set; }
        public Guid cellarCardImgId { get; set; }
        public Guid cellarHeaderImgId { get; set; }
    }
}