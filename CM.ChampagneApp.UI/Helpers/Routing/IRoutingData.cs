namespace CM.ChampagneApp.UI.Helpers.Routing
{
    public interface IRoutingData
    {
        string Id { get; set; }
    }

    public class RoutingData : IRoutingData
    {
        public string Id { get; set; }
    }
}