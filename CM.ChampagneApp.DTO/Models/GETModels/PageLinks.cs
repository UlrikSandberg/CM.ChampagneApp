using System;
using System.Collections.Generic;

namespace CM.ChampagneApp.DTO.Models
{
    public class pageLink
    {
        public string URLPath
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public List<string> Labels
        {
            get;
            set;
        }

        public string Body
        {
            get;
            set;
        }

        public string ImageId
        {
            get;
            set;
        }
    }
}
