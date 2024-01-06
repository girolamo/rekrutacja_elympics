using Aspnet_for_elympics.Controllers;
using Aspnet_for_elympics.Models;
using AutoMapper;
using Microsoft.Extensions.Options;

namespace Aspnet_for_elympics.Services
{
    public class NumberService : INumberService
    {
        private readonly ASPForElympicsDbContext dbContext;
        private readonly IMapper mapper;
        private readonly int numbersLimit;

        public NumberService(ASPForElympicsDbContext dbContext, IMapper mapper, IOptions<ApplicationSettings> settings)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            numbersLimit = settings.Value.NumbersLimit;
        }

        public IEnumerable<NumberDTO> GetAll()
        {
            var numbers = dbContext.Numbers.ToList();

            var numbersDTO = mapper.Map<List<NumberDTO>>(numbers);

            return numbersDTO;
        }

        public IEnumerable<NumberDTO> GetLatest()
        {
            var numbers = dbContext.Numbers.
                OrderByDescending(n => n.CreationTime).
                Take(numbersLimit).
                ToList();

            var numbersDTO = mapper.Map<List<NumberDTO>>(numbers);

            return numbersDTO;
        }
    }
}
