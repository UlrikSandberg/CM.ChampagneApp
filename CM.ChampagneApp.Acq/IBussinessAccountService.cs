using System;
using IdentityModel.Client;

namespace CM.ChampagneApp.Acq
{
    public interface IBusinessAccountService
    {
		string GetAccesToken();
		string GetRefreshToken();
		string GetUsername();
		string GetPassword();
		Guid GetId();
		void SuspendUser();
		void UpdateTokens(TokenResponse tokens);
		string GetInstallationId();
    }
}
