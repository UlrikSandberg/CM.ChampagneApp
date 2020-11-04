using System;
using CM.ChampagneApp.Business.Configuration;
using System.Threading.Tasks;
using CM.ChampagneApp.DTO.Documents;
using CM.ChampagneApp.Acq;
using Newtonsoft.Json;
using CM.ChampagneApp.DTO.Models.POSTModels.DeviceInstallation;
using CM.ChampagneApp.DTO.Models.Helpers.EnumOptions;
using System.Collections.Generic;
using CM.ChampagneApp.DTO.Models.GETModels.Notifications;
using CM.ChampagneApp.Instrumentation.Http;

namespace CM.ChampagneApp.Business.Services
{
    public interface INotificationRegistrationService
	{
		Task<BaseResponse> RegisterInstallationId();
		Task<BaseResponse> DeregisterInstallationId();
		Task<IEnumerable<Notification>> GetNotificationsPagedAsync(int page, int pageSize);
		Task<BaseResponse> MarkNotificationAsRead(Guid notificationId);
		Task<BaseResponse> MarkLatestNotificationSeen(Guid notificationId);
		Task<LatestNotificationUpdateModel> GetLatestNotificationUpdates(bool includeUpdates, Guid fromId);
	}

	public class NotificationRegistrationService : BaseDataService, INotificationRegistrationService
    {

		private const string forbiddenTokenChars = "<> ";

        public NotificationRegistrationService(IBusinessAccountService authService, IAuthenticationService authenticationService, IAppConfiguration appConfig, IHttpProxy httpProxy) : base(authService, authenticationService, appConfig, httpProxy)
		{
		}

		public async Task<BaseResponse> MarkLatestNotificationSeen(Guid notificationId)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/notifications/currentUser/markLatestNotificationSeen/" + notificationId);

			var response = await PostAsync(baseurl, null, true);

			if (!response.IsSuccessStatusCode)
            {
                return new BaseResponse(false, response.ReasonPhrase);
            }

            return new BaseResponse(true);
		}

		public async Task<BaseResponse> MarkNotificationAsRead(Guid notificationId)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/notifications/currentuser/notifications/markAsRead/" + notificationId.ToString());

			var response = await PostAsync(baseurl, null, true);

			if(!response.IsSuccessStatusCode)
			{
				return new BaseResponse(false, response.ReasonPhrase);
			}

			return new BaseResponse(true);
		}

		public async Task<IEnumerable<Notification>> GetNotificationsPagedAsync(int page, int pageSize)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/notifications/currentuser/notifications?page=" + page + "&pageSize=" + pageSize);

			var response = await GetAsync(baseurl, true);

			return await TryReadAsync<IEnumerable<Notification>>(response);
		}


		public async Task<BaseResponse> DeregisterInstallationId()
		{
			var installId = AuthService.GetInstallationId();//"<60163ae7 1d8e33ac c6e40a34 0a0f5a86 e42b0d64 76659f58 57e5f9c8 ddce60c5>";

			if(installId != null)
			{            
				var preparedId = installId;

				foreach(var character in forbiddenTokenChars)
				{
					preparedId = preparedId.Replace(character.ToString(), "");
				}
            
                var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/notifications/currentUser/deviceInstallations/" +preparedId);

				var response = await DeleteAsync(baseurl, true);

                if(response.IsSuccessStatusCode)
                {
                    System.Diagnostics.Debug.WriteLine("Succesfully deregistered installationId for currentUser in backend");
                    return new BaseResponse(true);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Failed to deregister currentUsers installationId in backend");
                    return new BaseResponse(false, response.ReasonPhrase);
                }             
			}
			else
			{
				System.Diagnostics.Debug.WriteLine("Installation Id is null only happends if the app failed to register installationIds internally through AppDelegate.cs");
				return new BaseResponse(false);
			}
		}

		public async Task<BaseResponse> RegisterInstallationId()
		{
			var installId = AuthService.GetInstallationId();//"<60163ae7 1d8e33ac c6e40a34 0a0f5a86 e42b0d64 76659f58 57e5f9c8 ddce60c5>";

			if(installId != null)
			{
				var preparedId = installId;

				foreach(var character in forbiddenTokenChars)
				{
					preparedId = preparedId.Replace(character.ToString(), "");
				}

				var deviceInstallation = new DeviceInstallation
				{
					InstallationId = null,
					Platform = NotificationPlatform.apns.ToString(),
					PushChannel = preparedId,
					Tags = new List<string>(),
					UTCOffset = GetUTCOffset()
				};

				var json = JsonConvert.SerializeObject(deviceInstallation, Formatting.Indented);

                var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/notifications/currentUser/deviceInstallations");

				var response = await PostAsync(baseurl, json, true);

				if(response.IsSuccessStatusCode)
				{
					System.Diagnostics.Debug.WriteLine("Succesfully registered installationId for currentUser in backend");
					return new BaseResponse(true);
				}
				else
				{
					System.Diagnostics.Debug.WriteLine("Failed to register currentUsers installationId in backend");
					return new BaseResponse(false, response.ReasonPhrase);
				}            
			}
			else
			{
				System.Diagnostics.Debug.WriteLine("Failed to register currentUsers installationId in backend -> SettingsApp might be causing troubles");
                return new BaseResponse(false);
			}         
		}

		public async Task<LatestNotificationUpdateModel> GetLatestNotificationUpdates(bool includeUpdates, Guid fromId)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/notifications/currentUser/notifications/getLatest?includeUpdates=" + includeUpdates + "&fromId=" + fromId);
            
			var response = await GetAsync(baseurl, true);

			return await TryReadAsync<LatestNotificationUpdateModel>(response);
		}

        public static long GetUTCOffset()
		{
			var utc = DateTime.UtcNow;
			var now = DateTime.Now;

			return now.Ticks - utc.Ticks;       
		}
	}
}
