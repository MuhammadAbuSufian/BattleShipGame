using BattleShipApi.Models;

namespace BattleShipApi.Services
{
    public class BattleShipGameService: IBattleShipGameService
    {
        private  BattleShipGame game;
        static Random rnd = new Random();
        private int? lastComputerHitId;

        public BattleShipGame InitializeGame()
        {
            game = new BattleShipGame();
            PlaceBattleShipForComputer();
            game.Computer.BattleShips = PlaceBattleShipForComputer();
            return game;
        }

        public BattleShipGame StartGame(IEnumerable<BattleShip> ships)
        {
            game.Player.BattleShips = ships;
            game.Computer.BattleShips = PlaceBattleShipForComputer();
            return game;
        }
        
        public BattleShipGame HitBattleShipByEachOther(int locationPointId)
        {
            Hit(locationPointId, game.Player);
            var locationPointIdToHitByComp = GetLocationPointIdToHitByComp();
            Hit(locationPointIdToHitByComp, game.Computer);
            SetWinner();
            return game;
        }

        public void Hit(int? locationPointId, Player player)
        {

            player.LocationPoints.FirstOrDefault(l => l.Id == locationPointId).IsSinked = true;
            foreach (var ship in player.BattleShips)
            {
                var shipLocation = ship.Locations.FirstOrDefault(l => l.Id == locationPointId);
                if (shipLocation != null)
                    shipLocation.IsSinked = true;
            }
        }

        public int? GetLocationPointIdToHitByComp()
        {
            if(lastComputerHitId == null)
                lastComputerHitId = rnd.Next(100);
            var battleship = game.Computer.BattleShips.SelectMany(x=>x.Locations).Where(l=>l.Id == lastComputerHitId).FirstOrDefault();
            if (battleship == null)
                return lastComputerHitId;
            if (!battleship.IsSinked)
                return lastComputerHitId;
            var posibleMoves = TakeBattleShipPositions(2).Where(x => x.IsSinked == false).ToList();
            lastComputerHitId = posibleMoves[rnd.Next(posibleMoves.Count)].Id;
            return lastComputerHitId;
        }

        private IEnumerable<BattleShip> PlaceBattleShipForComputer()
        {
            List<BattleShip> battleShips = new List<BattleShip>();
            battleShips.Add(new BattleShip
            {
                Id = 1,
                Locations = TakeBattleShipPositions(5),
                IsSinked = false
            });
            battleShips.Add(new BattleShip
            {
                Id = 2,
                Locations = TakeBattleShipPositions(4),
                IsSinked = false
            });
            battleShips.Add(new BattleShip
            {
                Id = 3,
                Locations = TakeBattleShipPositions(4),
                IsSinked = false
            });
            return battleShips;
        }

        private IEnumerable<LocationPoint> TakeBattleShipPositions(int length)
        {
            List<LocationPoint> battleshipsPositions = new List<LocationPoint>();
            int startPosition = rnd.Next(100);
            int endPosition = 0;
            List<int> startCordinates = startPosition.ToString().ToCharArray().Select(x => (int)Char.GetNumericValue(x)).ToList();
            if (startCordinates.Count == 1)
                startCordinates.Insert(0, 0);
            List<int[]> possibleEndPositions = new List<int[]>();

            if ((startCordinates[0] - length) >= 0)
                possibleEndPositions.Add(new int[] { startCordinates[0] - length, startCordinates[1] });

            if ((startCordinates[0] + length) < 10)
                possibleEndPositions.Add(new int[] { startCordinates[0] + length, startCordinates[1] });

            if ((startCordinates[1] - length) >= 0)
                possibleEndPositions.Add(new int[] { startCordinates[0], startCordinates[1] - length });

            if ((startCordinates[1] + length) < 10)
                possibleEndPositions.Add(new int[] { startCordinates[0], startCordinates[1] + length });

            var endCordinates = possibleEndPositions[rnd.Next(possibleEndPositions.Count)];
            
            if (startCordinates[0] == endCordinates[0])
            {
                int start = (startCordinates[1] > endCordinates[1]) ? endCordinates[1] : startCordinates[1];
                int end = ((startCordinates[1] > endCordinates[1]) ? startCordinates[1] : endCordinates[1]);
                for (int i = start; i < end; i++)
                {
                    int id = Convert.ToInt32((endCordinates[0].ToString() + i.ToString()));
                    battleshipsPositions.Add(new LocationPoint
                    {
                        Id = id,
                        IsSinked = false
                    });
                }
            }
            else
            {
                int start = (startCordinates[0] > endCordinates[0]) ? endCordinates[0] : startCordinates[0];
                int end = ((startCordinates[0] > endCordinates[0]) ? startCordinates[0] : endCordinates[0]);
                for (int i = start; i < end; i++)
                {
                    int id = Convert.ToInt32((i.ToString() + endCordinates[1].ToString()));
                    battleshipsPositions.Add(new LocationPoint
                    {
                        Id = id,
                        IsSinked = false
                    });
                }
            }
            return battleshipsPositions;
        }

        private void SetWinner()
        {
            var unsinkedCompShipLocation = game.Computer.BattleShips.SelectMany(x => x.Locations).Where(l => l.IsSinked == false).Count();
            game.Player.IsWinner = unsinkedCompShipLocation == 0;
            var unsinkedPlayerShipLocation = game.Player.BattleShips.SelectMany(x => x.Locations).Where(l => l.IsSinked == false).Count();
            game.Computer.IsWinner = unsinkedPlayerShipLocation == 0;
        }
    }
}
