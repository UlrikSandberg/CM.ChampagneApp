using System;

namespace CM.ChampagneApp.DTO.Builders
{
    public class UserSettingsUpdate
    {
		public string Name { get; }
        public string ProfileName { get; }
        public string Email { get; }
        public string Biography { get; }

        public byte[] ProfileImage { get; }
        public byte[] ProfileCoverImage { get; }
		public byte[] ProfileCellarCardImage { get; }

        private UserSettingsUpdate(string name, string profileName, string email, string biography, byte[] profileImage, byte[] profileCoverImage, byte[] personalCellarCardImage)
        {
            Name = name;
            ProfileName = profileName;
            Email = email;
            Biography = biography;
            ProfileImage = profileImage;
            ProfileCoverImage = profileCoverImage;
            ProfileCellarCardImage = personalCellarCardImage;
        }

        public class UserSettingsUpdateBuilder : IBuilder<UserSettingsUpdate>
        {
            private string Name;
            private string ProfileName;
            private string Email;
            private string Biography;

            private byte[] ProfileImage;
            private byte[] ProfileCoverImage;
            private byte[] PersonalCellarCardImage;

            public UserSettingsUpdateBuilder SetName(string name)
            {
                Name = name;
                return this;
            }

            public UserSettingsUpdateBuilder SetProfileName(string profileName)
            {
                ProfileName = profileName;
                return this;
            }

            public UserSettingsUpdateBuilder SetEmail(string email)
            {
                Email = email;
                return this;
            }

            public UserSettingsUpdateBuilder SetBiography(string biography)
            {
                Biography = biography;
                return this;
            }

            public UserSettingsUpdateBuilder SetProfileImage(byte[] profileImage)
            {
                ProfileImage = profileImage;
                return this;
            }

            public UserSettingsUpdateBuilder SetProfileCover(byte[] profileCoverImage)
            {
                ProfileCoverImage = profileCoverImage;
                return this;
            }

            public UserSettingsUpdateBuilder SetPersonalCellarCardImg(byte[] cellarCardImage)
            {
                PersonalCellarCardImage = cellarCardImage;
                return this;
            }


            public UserSettingsUpdate Build()
            {
                return new UserSettingsUpdate(Name, ProfileName, Email, Biography, ProfileImage, ProfileCoverImage, PersonalCellarCardImage);
            }
        }
    }
}
