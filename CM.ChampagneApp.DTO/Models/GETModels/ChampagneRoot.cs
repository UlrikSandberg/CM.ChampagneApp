using System;
using System.Collections.Generic;

namespace CM.ChampagneApp.DTO.Models
{
    public class ChampagneRoot
    {
		public Guid Id { get; set; }
        public string FolderName { get; set; }
        public string AuthorName { get; set; }
		public Guid AuthorId { get; set; }
		public Guid DisplayImageId { get; set; }
		public List<Guid> ChampagneIds { get; set; }
        public string FolderContentType { get; set; }
        public string FolderType { get; set; }
        public double AverageRating { get; set; }
        public double SumOfRating { get; set; }
        public double NumberOfTasting { get; set; }      
    }
}
