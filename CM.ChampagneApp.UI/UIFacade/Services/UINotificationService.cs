using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CM.ChampagneApp.UI.UIFacade.Models.UINotifications;
using CM.ChampagneApp.Business.Services;
using CM.ChampagneApp.DTO.Models.GETModels.Notifications;
using CM.ChampagneApp.DTO.Documents;
using System.Linq;
using AutoMapper;
using CM.ChampagneApp.UI.UIFacade.Mapper;

namespace CM.ChampagneApp.UI.UIFacade.Services
{
    public interface IUINotificationService
	{
		Task<IEnumerable<UINotification>> GetNotificationsPagedAsync(int page, int pageSize);
		Task<BaseResponse> MarkNotificationAsRead(Guid notificationId);
		Task<UILatestNotificationUpdateModel> GetLatestNotificationUpdates(bool includeUpdates, Guid fromId);
		Task<BaseResponse> MarkLatestNotificationSeen(Guid notificationId);
	}

	public class UINotificationService : IUINotificationService
    {
		private readonly INotificationRegistrationService notificationRegistrationService;

		public UINotificationService(INotificationRegistrationService notificationRegistrationService)
        {
            this.notificationRegistrationService = notificationRegistrationService;
		}      

		public async Task<BaseResponse> MarkLatestNotificationSeen(Guid notificationId)
		{
			return await notificationRegistrationService.MarkLatestNotificationSeen(notificationId);
		}

		public async Task<BaseResponse> MarkNotificationAsRead(Guid notificationId)
		{
			return await notificationRegistrationService.MarkNotificationAsRead(notificationId);
		}

		public async Task<IEnumerable<UINotification>> GetNotificationsPagedAsync(int page, int pageSize)
		{
			var result = await notificationRegistrationService.GetNotificationsPagedAsync(page, pageSize);

			if(result == null)
			{
				return null;
			}

			var list = new List<UINotification>();

            foreach(var notification in result)
			{
				list.Add(GenericMapper<Notification, UINotification>.Map(notification));
			}
         
			return list;             
		}

		public async Task<UILatestNotificationUpdateModel> GetLatestNotificationUpdates(bool includeUpdates, Guid fromId)
		{         
			var result = await notificationRegistrationService.GetLatestNotificationUpdates(includeUpdates, fromId);

			if(result == null)
			{
				return null;
			}

			var uiModel = new UILatestNotificationUpdateModel();
			uiModel.NumberOfUnreadNotifications = result.NumberOfUnreadNotifications;

			if(result.NewNotifications != null)
			{
				var list = new List<UINotification>();
                foreach(var notification in result.NewNotifications)
				{
					list.Add(GenericMapper<Notification, UINotification>.Map(new MapperConfiguration(cfg =>
					{
						cfg.CreateMap<Notification, UINotification>()
						.ForMember(x => x.DateInFormat, opt => opt.Ignore());
					}), notification));
				}
				uiModel.NewNotifications = new List<UINotification>(list);
			}

			return uiModel;
		}
	}
}
