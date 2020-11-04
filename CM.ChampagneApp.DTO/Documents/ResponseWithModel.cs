using System;
namespace CM.ChampagneApp.DTO.Documents
{
	public class ResponseWithModel<T> : BaseResponse where T : class
	{
		public T Model { get; private set; }

		public ResponseWithModel(bool isSuccesfull, T model = null, string message = null, Exception exception = null) : base(isSuccesfull, message, exception)
		{
			Model = model;
		}
	}
}
