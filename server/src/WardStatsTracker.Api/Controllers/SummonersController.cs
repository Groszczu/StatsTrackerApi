using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Refit;
using WardStatsTracker.Core.Interfaces;
using WardStatsTracker.Core.Models;
using WardStatsTracker.Core.Parameters;

namespace WardStatsTracker.Api.Controllers
{
    [ResponseCache(Duration = 300)]
    [Route("{platformId}/summoners")]
    [ApiController]
    public class SummonersController : ControllerBase
    {
        private readonly IRiotService _riotService;

        public SummonersController(IRiotService riotService)
        {
            _riotService = riotService;
        }

        [HttpGet("{summonerName}")]
        public async Task<ActionResult<SummonerModel>> GetSummonerByName(string platformId, string summonerName, 
            [FromQuery] bool includeMatches = true, 
            [FromQuery] bool includeLeagues = true)
        {
            var summoner = await _riotService.GetSummoner(platformId, summonerName);
            if (summoner == null)
                return NotFound("Summoner with given name could not be find");
            
            if (includeMatches)
            {
                summoner.Matches = await _riotService.GetMatchesByAccount(platformId, summoner.AccountId!) 
                    ?? new List<MatchOverviewModel>();
            }

            if (includeLeagues)
            {
                summoner.Leagues = await _riotService.GetLeaguesBySummoner(platformId, summoner.Id!);
            }

            return summoner;
        }
    }
}