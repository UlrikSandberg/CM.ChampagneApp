using System;
namespace CM.ChampagneApp.UI.CustomEventArgs
{
    public class PostCommentEventArgs
    {
        public string Content { get; private set; }

		public PostCommentEventArgs(string content)
        {
            Content = content;
		}
    }
}
