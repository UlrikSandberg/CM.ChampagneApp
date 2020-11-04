using System;
using CM.ChampagneApp.DTO.Builders.Helpers;
namespace CM.ChampagneApp.DTO.Builders
{
    public class FeedbackAndBugSubmission
    {
            
		public bool MayBeContacted { get; set; }
		public string Type { get; }
		public string Content { get; }
		public byte[] Image { get; }
      
		private FeedbackAndBugSubmission(string type, string content, byte[] image, bool mayBeContacted)
        {         
			Type = type;
			Content = content;
			Image = image;
			MayBeContacted = mayBeContacted;
		}

		public class FeedbackAndBugSubmissionBuilder : IBuilder<FeedbackAndBugSubmission>
		{
            private SubmissionType Type = SubmissionType.Bug;
			private bool MayBeContacted = false;
			private string Content = null;
			private byte[] Image = null;
         
			public FeedbackAndBugSubmissionBuilder SetMayBeContacted(bool mayBeContacted)
            {
				MayBeContacted = mayBeContacted;
                return this;
            }         

			public FeedbackAndBugSubmissionBuilder SetSubmissionType(SubmissionType type)
            {
				Type = type;
                return this;
            }

			public FeedbackAndBugSubmissionBuilder SetContent(string content)
            {
				Content = content;
                return this;
            }

			public FeedbackAndBugSubmissionBuilder SetImage(byte[] image)
            {
				Image = image;
                return this;
            }

			public FeedbackAndBugSubmission Build()
			{
				return new FeedbackAndBugSubmission(Type.ToString(), Content, Image, MayBeContacted);
			}

		}

    }
}
