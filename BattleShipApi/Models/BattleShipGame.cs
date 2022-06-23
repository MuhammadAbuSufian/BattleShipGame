using BattleShipApi.Models;

namespace BattleShipApi
{
    public class BattleShipGame
    {
        public BattleShipGame()
        {
            Player = new Player(1, false);
            Computer = new Player(2, false);
        }
        public Player Player { get; set; }
        public Player Computer { get; set; }
    }
}
