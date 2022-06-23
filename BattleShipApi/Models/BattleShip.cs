namespace BattleShipApi.Models
{
    public class BattleShip
    {
        public int Id { get; set; }
        public IEnumerable<LocationPoint> Locations { get; set; }
        public bool IsSinked { get; set; }
    }
}
