using Aspnet_for_elympics.Controllers;
using Aspnet_for_elympics.Entities;
using Aspnet_for_elympics.Models;
using AutoMapper;
using Newtonsoft.Json;

namespace Aspnet_for_elympics.Services
{
    public class RandomNumberService : IRandomNumberService
    {
        private readonly ASPForElympicsDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IRequestManager requestManager;

        public RandomNumberService(ASPForElympicsDbContext dbContext, IMapper mapper, IRequestManager requestManager)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.requestManager = requestManager;
        }

        public void FetchAndStore()
        {
            string jsonContent;
            var evaluatedCorectly = requestManager.TryGetTheNumberRequest(out jsonContent);

            if (!evaluatedCorectly)
                return;

            var numberDto = Deserialize(jsonContent);
            if (numberDto is null)
                return;

            Store(numberDto);
        }

        private static RandomNumberDTO Deserialize(string jsonContent)
        {
            return JsonConvert.DeserializeObject<RandomNumberDTO>(jsonContent);
        }

        private void Store(RandomNumberDTO numberDto)
        {
            var number = mapper.Map<Number>(numberDto);

            dbContext.Numbers.Add(number);
            dbContext.SaveChanges();
        }
    }
}
