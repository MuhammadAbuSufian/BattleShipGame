using BattleShipApi.Models;
using BattleShipApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BattleShipApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {

        private readonly ILogger<GameController> _logger;
        private readonly IBattleShipGameService _battleShipGameService;

        public GameController(ILogger<GameController> logger, IBattleShipGameService battleShipGameService)
        {
            _logger = logger;
            _battleShipGameService = battleShipGameService;
        }

        [HttpGet]
        [Route("Initialize")]
        public IActionResult Initialize()
        {
            BattleShipGame game = _battleShipGameService.InitializeGame();
            return Ok(game);
        }

        [HttpPost]
        [Route("Start")]
        public IActionResult Start(IEnumerable<BattleShip> ships)
        {
            BattleShipGame game = _battleShipGameService.StartGame(ships);
            return Ok(game);
        }

        [HttpGet]
        [Route("Target")]
        public  IActionResult Target(int locationId)
        {
            BattleShipGame game = _battleShipGameService.HitBattleShipByEachOther(locationId);
            return Ok(game);
        }
    }
}