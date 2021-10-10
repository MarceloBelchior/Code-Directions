using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OpenSquare.CodeHB.Domain;
using OpenSquare.CodeHB.Domain.Helper;

namespace OpenSquare.CodeHB.HttpClient.Bing.Repository
{
    public class BingRepository : IBingRepository
    {
        private readonly IHttpHelper _httpHelper;

        private readonly string GeoCoding;
        private readonly string BingKey;
  
        public BingRepository(IHttpHelper httpHelper, IConfigurationSection config)
        {
            _httpHelper = httpHelper;
            GeoCoding = config["GeoCoding"];
            BingKey = config["BingKey"];

        }
        public async Task<Domain.Model.Bing.GeoCodingBing> GetLatLong(string zipcodeorigem)
        {
            var result = await _httpHelper.Send(System.Net.Http.HttpMethod.Get, string.Format(GeoCoding, BingKey, zipcodeorigem), string.Empty);
   
            if (result.StatusCode == HttpStatusCode.OK)
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Domain.Model.Bing.GeoCodingBing>(await result.Content.ReadAsStringAsync());
            throw new NotImplementedException("Service not avaiable");


        }
    }
}
