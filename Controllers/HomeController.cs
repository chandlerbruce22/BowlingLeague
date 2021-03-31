using BowlingLeague.Models;
using BowlingLeague.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //Bowling League DBset - context
        private BowlingLeagueContext context { get; set; }

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext con)
        {
            _logger = logger;
            context = con;
        }

        //Index action. Returns all bowlers if noting is passed. Or Team roster depending on the clicked link.
        public IActionResult Index(long? teamid, string teamname, int pageNum = 0)
        {
            int pageSize = 5;

            // View bag for teamname to pass to the index. This will help the title adjust accordingly.
            ViewBag.TeamName = teamname;

            return View(new IndexViewModel
            {
                Bowlers = (context.Bowlers
                .Where(b => b.TeamId == teamid || teamid == null)
                .OrderBy(b => b.Team)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList()),

                PageNumberingInfo = new PageNumberingInfo
                {
                    NumberItemsPerPage = pageSize,
                    CurrentPage = pageNum,

                    TotalNumItems = (teamid == null ? context.Bowlers.Count() :
                        context.Bowlers.Where(x => x.TeamId == teamid).Count())
                },

                Team = teamname
            });

        }
    
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
