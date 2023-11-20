using AccountServer.DB;
using AccountServer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : Controller
    {
        private readonly AppDbContext _context;

        public PlayerController(AppDbContext context)
        {
            _context = context;
        }


        //[HttpPost]
        //[Route("login")]
        //public PlayerLoginRes Login([FromBody] PlayerLoginReq req)
        //{
        //    PlayerDb player = _context.Player
        //            .AsNoTracking()
        //            .Where(p => p.Nickname == req.PlayerName)
        //            .Include(a => a.Items)
        //            .Include(s => s.PlayerStat)
        //            .FirstOrDefault();

        //    PlayerLoginRes res = new PlayerLoginRes();

        //    if (player == null)
        //    {
        //        res.IsLoginSucceed = false;
        //        return res;
        //    }


        //    ICollection<ItemDb> items = player.Items;

        //    res.Items = new List<PlayerItemRes>();
        //    if (items != null)
        //    {
        //        foreach (ItemDb item in items)
        //        {
        //            PlayerItemRes playerItemRes = new PlayerItemRes() { Quantity = item.Quantity, TemplateId = item.TemplateId };
        //            res.Items.Add(playerItemRes);
        //        }
        //    }
        //    PlayerStatDb playerStat = player.PlayerStat;
        //    res.PlayerId = player.PlayerId;

        //    if (playerStat != null)
        //        res.PlayerStatRes = new PlayerStatRes(playerStat);

        //    res.IsLoginSucceed = true;
        //    return res;
        //}


        //[HttpPost]
        //[Route("stat")]
        //public PlayerStatRes InitPlayerStat(InitPlayerStatReq req)
        //{
        //    PlayerDb player = _context.Player
        //            .AsNoTracking()
        //            .Where(p => p.PlayerId == req.PlayerId)
        //            .FirstOrDefault();

        //    PlayerStatRes res = new PlayerStatRes();
        //    if (player == null)
        //        return res;

        //    PlayerStatDb playerStat = new PlayerStatDb(req);
        //    _context.PlayerStat.Add(playerStat);
        //    player.PlayerStat = playerStat;
        //    _context.Update(player);
        //    _context.SaveChanges();


        //    res = new PlayerStatRes(playerStat);
        //    return res;
        //}


        //[HttpGet]
        //[Route("stat")]
        //public PlayerStatRes GetPlayerStat(GetPlayerStatReq req)
        //{
        //    PlayerDb player = _context.Player
        //            .AsNoTracking()
        //            .Where(p => p.PlayerId == req.PlayerId)
        //            .Include(s => s.PlayerStat)
        //            .FirstOrDefault();

        //    PlayerStatRes res = null;
        //    if (player == null)
        //        return res;

        //    res = new PlayerStatRes(player.PlayerStat);
        //    return res;
        //}
    }
}
