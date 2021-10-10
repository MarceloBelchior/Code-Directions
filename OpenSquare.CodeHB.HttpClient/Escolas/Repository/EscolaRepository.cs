using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Text;
using OpenSquare.CodeHB.Domain.Helper;
using Microsoft.Extensions.Configuration;
using OpenSquare.CodeHB.Domain;

namespace OpenSquare.CodeHB.HttpClient.Escolas.Repository
{


    public class EscolaRepository : IEscolaRepository
    {
        private readonly IHttpHelper _httpHelper;
        private readonly string httpCreated;
        private readonly string httpUpInsert;
        private readonly string httpConsult;
        private readonly string httpConsultSQL;
        private readonly string resourceId;
        public EscolaRepository(IHttpHelper httpHelper, IConfigurationSection config)
        {
            _httpHelper = httpHelper;
            httpCreated = config["httpCreated"];
            httpUpInsert = config["httpUpInsert"];
            httpConsult = config["httpConsult"];
            httpConsultSQL = config["httpConsultSQL"];
            resourceId = config["resourceId"];


        }

        public async Task<Domain.Model.EscolaModel> GetAllEscolas()
        {
            var result = await _httpHelper.Send(HttpMethod.Get, string.Concat(httpConsult, resourceId), string.Empty);
            if (result.StatusCode == HttpStatusCode.OK)
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Domain.Model.EscolaModel>(await result.Content.ReadAsStringAsync());
            throw new NotImplementedException("Service not avaiable");

        }
        public async Task<Domain.Model.EscolaModel> UpdateInsert(Domain.Model.EscolaModel entity)
        {
            var result = await _httpHelper.Send(HttpMethod.Post, string.Concat(httpUpInsert,resourceId), Newtonsoft.Json.JsonConvert.SerializeObject(entity));
            if (result.StatusCode == HttpStatusCode.OK)
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Domain.Model.EscolaModel>(await result.Content.ReadAsStringAsync());
            throw new NotImplementedException("Service not avaiable");
        }
    }
}
