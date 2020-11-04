using System;
using System.Collections.Generic;

namespace CM.ChampagneApp.DTO.Models.POSTModels.DeviceInstallation
{
    public class DeviceInstallation
    {
		public string InstallationId { get; set; }
        public string Platform { get; set; }
        public string PushChannel { get; set; }
        public List<string> Tags { get; set; }
		public long UTCOffset { get; set; }
    }
}
