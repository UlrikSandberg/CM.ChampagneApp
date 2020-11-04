using System;
namespace CM.ChampagneApp.DTO.Builders
{
	public interface IBuilder<T>
    {
        T Build();
    }
}
