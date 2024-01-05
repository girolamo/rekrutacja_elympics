using Aspnet_for_elympics.Models;
using Aspnet_for_elympics.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aspnet_for_elympics.Controllers
{
    [Route("api/numbers")]
    [ApiController]
    public class NumberController : ControllerBase
    {
        private readonly INumberService numberService;
        private readonly IRandomNumberService randomNumberService;

        public NumberController(INumberService numberService, IRandomNumberService randomNumberService)
        {
            this.numberService = numberService;
            this.randomNumberService = randomNumberService;
        }

        [HttpPost]
        public ActionResult<IEnumerable<NumberDTO>> FetchStoreThenReturn()
        {
            randomNumberService.FetchAndStore();

            var numbers = numberService.GetLatest();

            return Ok(numbers);
        }
    }
}
