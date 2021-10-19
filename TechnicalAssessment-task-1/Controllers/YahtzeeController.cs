using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalAssessment_task_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YahtzeeController : ControllerBase
    {
        [HttpGet]
        public string Ping()
        {
            return "Pong";
        }

        [HttpGet]
        [Route("roll/{numberOfDice}")]
        public int[] Roll(int numberOfDice)
        {
            //TODO: Add logic here

            throw new NotImplementedException();
        }

        [HttpPost]
        public int Calculate(int[] diceRoles)
        {
            //TODO: Implement calculation logic
            throw new NotImplementedException();
        }
    }
}
