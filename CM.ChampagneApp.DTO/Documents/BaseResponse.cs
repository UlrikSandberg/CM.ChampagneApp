using System;
namespace CM.ChampagneApp.DTO.Documents
{
    public class BaseResponse
    {
		public bool IsSuccesfull { get; set; }
		public string Message { get; set; }
		public Exception Exception { get; set; }

		public BaseResponse(bool isSuccesfull, string message = null, Exception exception = null)
        {
            Exception = exception;
			Message = message;
			IsSuccesfull = isSuccesfull;
		}
    }
}
