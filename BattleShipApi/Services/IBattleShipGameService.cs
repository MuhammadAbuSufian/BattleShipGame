using BattleShipApi.Models;

namespace BattleShipApi.Services
{
    public interface IBattleShipGameService
    {
        public BattleShipGame InitializeGame();
        public BattleShipGame StartGame(IEnumerable<BattleShip> ships);
        public BattleShipGame HitBattleShipByEachOther(int locationPointId);
    }
}
