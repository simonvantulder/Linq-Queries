using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsORM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;


//heloooo
namespace SportsORM.Controllers
{
    public class HomeController : Controller
    {

        private static Context _context;

        public HomeController(Context DBContext)
        {
            _context = DBContext;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.BaseballLeagues = _context.Leagues
                .Where(l => l.Sport.Contains("Baseball"))
                .ToList();
            return View();
        }

        [HttpGet("level_1")]
        public IActionResult Level1()
        {
            ViewBag.BaseballLeagues = _context.Leagues
                .Where(l => l.Sport.Contains("Baseball"))
                .ToList();
            ViewBag.WomensLeagues = _context.Leagues
                .Where(l => l.Name.Contains("Womens'"))
                .ToList();
            ViewBag.HockeyLeagues = _context.Leagues
                .Where(l => l.Sport.Contains("Hockey"))
                .ToList();
            ViewBag.NotFootball = _context.Leagues
                .Where(l => l.Sport !="Football")
                .ToList();
            ViewBag.Conference = _context.Leagues
                .Where(l => l.Name.Contains("Conference"))
                .ToList();
            ViewBag.Atlantic = _context.Leagues
                .Where(l => l.Name.Contains("Atlantic"))
                .ToList();
            ViewBag.Dallas = _context.Teams
                .Where(l => l.Location.Contains("Dallas"))
                .ToList();
            ViewBag.Raptors = _context.Teams
                .Where(l => l.TeamName.Contains("Raptors"))
                .ToList();
            ViewBag.City = _context.Teams
                .Where(l => l.Location.Contains("City"))
                .ToList();
            ViewBag.T = _context.Teams
                .Where(l => l.TeamName.StartsWith("T") )
                .ToList();
            ViewBag.Alpha = _context.Teams
                .OrderBy(Teams => Teams.Location)
                .ToList();
            ViewBag.Zulu = _context.Teams
                .OrderByDescending(Teams => Teams.Location)
                .ToList();
            ViewBag.Cooper = _context.Players
                .Where(l => l.LastName == ("Cooper"))
                .ToList();
            ViewBag.Cooper = _context.Players
                .Where(l => l.FirstName == ("Joshua"))
                .ToList();
            ViewBag.NotJosh = _context.Players
                .Where(l => l.LastName == "Cooper")
                .Where(l => l.FirstName != "Joshua")
                .ToList();
            ViewBag.WyattOrAlex = _context.Players
                .Where(l => l.FirstName == "Alexander" || l.FirstName == "Wyatt")
                .ToList();
            
            

            return View();
        }

        [HttpGet("level_2")]
        public IActionResult Level2()
        {
            // League ASC = _context.Leagues.FirstOrDefault(l => l.Name.Contains("Atlantic Soccer Conference"));
            //     int ASCId = ASC.LeagueId;
            // List<Team> ASCTeams = _context.Teams.Where(p => p.LeagueId == ASCId).ToList(); alternate method if they arent linked (dumb long route way)


            ViewBag.AtlanticSoccer = _context.Teams.Where(p => p.CurrLeague.Name == "Atlantic Soccer Conference").ToList();

            ViewBag.PenguinsPlayers = _context.Teams
            .Where(p => p.Location == "Boston")
            .Where(p => p.TeamName == "Penguins");
            
            Team BostonPenguins = _context.Teams
            .Where(l => l.Location.Contains("Boston"))
            .FirstOrDefault(l => l.TeamName.Contains("Penguins"));
            ViewBag.BostonPenguins = BostonPenguins.CurrentPlayers; // loop through list of players in html

            //Step 1 grab the teams in the league
            List<Team> ICBC = _context.Teams
            .Where(p => p.CurrLeague.Name == "International Collegiate Baseball Conference")
            .ToList();

            //step 2 grab all the team Ids for each team in ICBC conference
            //....could also stop here and loop through Team.CurrentPlayers in html
            List<int> ICBCTeamId = new List<int>();
            foreach(Team team in ICBC){
                ICBCTeamId.Add(team.TeamId);
            }

            //step 3 grab the list of players that have one of those team Ids and put it in Viewbag
            foreach(int teamId in ICBCTeamId){
                ViewBag.ICBCPlayers += _context.Players.Where(p => p.TeamId == teamId);
            }

            //step 1 Grab all football teams from the league
            List<Team> ACAF = _context.Teams
                .Where(p => p.CurrLeague.Name == "American Conference of Amateur Football")
                .ToList();

            //step 2 grab all the team Ids for each team in ICBC conference
            //....could also stop here and loop through Team.CurrentPlayers in html
            List<int> ACAFTeamId = new List<int>();
            foreach(Team team in ICBC){
                ICBCTeamId.Add(team.TeamId);
            }

            //step 3 grab the list of players that have one of those team Ids and lastname and put it in Viewbag
            foreach(int teamId in ACAFTeamId){
                ViewBag.ACAFPlayers += _context.Players.Where(p => p.TeamId == teamId).Where(p => p.LastName == "Lopez");
            }

            //step 1 grab all football teams and display on html with loop through team.currentplayers
            List<Team> Football = _context.Teams
                .Where(p => p.CurrLeague.Name.Contains("Football"))
                .ToList();

            //grab player teams with player names sophia...loop through sophiasTeams.TeamOfPlayer in html & grab team.Team.Name
            List<PlayerTeam> SophiasTeams = new List<PlayerTeam>();
            SophiasTeams = _context.PlayerTeams
            .Include(p => p.TeamOfPlayer)
            .Where(p => p.PlayerOnTeam.FirstName == "Sophia" || p.PlayerOnTeam.LastName == "Sophia")
            .ToList();

            ViewBag.SophiasTeams = SophiasTeams;
            
            List<int> SophiasTeamID = new List<int>();
            //grab teams sophia is a part of. Just like above in line 148 ( or just steal it&make list of IDs)
            foreach(PlayerTeam team in SophiasTeams)
            {
                SophiasTeamID.Add(team.PlayerTeamId); //grab id or do query and check/add team to league.Team????
            }







            return View();
        }

        [HttpGet("level_3")]
        public IActionResult Level3()
        {
            return View();
        }

    }
}