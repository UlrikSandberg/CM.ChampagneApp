using System;
namespace CM.ChampagneApp.DTO.Models
{
    public class ReviewModel
    {

        public string AuthorId
        {
            get;
            set;
       
        }

        public string Username
        {
            get;
            set;
        }

        public int Niveau
        {
            get;
            set;
        }

        public string Date
        {
            get;
            set;
        }

        public int NumberOfLikes
        {
            get;
            set;
        }

        public int NumberOfComments
        {
            get;
            set;
        }

        public double ReviewRating
        {
            get;
            set;
        }

        public string ReviewContent
        {
            get;
            set;
        }

    }
}
