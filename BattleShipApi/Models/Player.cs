namespace BattleShipApi.Models
{
    public class Player
    {
        public Player(int id, bool isWinner)
        {
            this.Id = id;
            this.IsWinner = isWinner;
            LocationPoints = new List<LocationPoint>();
            for (int i = 0; i < 100; i++)
                LocationPoints.Add(new LocationPoint { Id = i, IsSinked = false });
        }
        public int Id { get; set; }
        public List<LocationPoint> LocationPoints { get; set; }
        public IEnumerable<BattleShip> BattleShips { get; set; }
        public bool IsWinner { get; set; }
    }
}
