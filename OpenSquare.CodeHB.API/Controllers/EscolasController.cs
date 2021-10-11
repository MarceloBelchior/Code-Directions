using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenSquare.CodeHB.Domain;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OpenSquare.CodeHB.API.Controllers
{
    [Route("api/[controller]")]
    public class EscolasController : Controller
    {
        private readonly CodeHB.Domain.IEscolaService _escolaService;
        public EscolasController(IEscolaService escolaService) => _escolaService = escolaService;


        //O Zip Code de Origem é necessario para poder calcular a distancia da escola mais proxima
        [HttpGet, Route("ListaEscolas")]
        [SwaggerResponse(200, "Listagem de escolas obtidas no site https://dadosabertos.poa.br/ ", typeof(IEnumerable<Domain.Model.Escolas>))]
        [SwaggerResponse(400, "Invalid Request")]
        public async Task<IActionResult> ListaEscolas()
        {

            var result = await _escolaService.GetAllEscolas();
            if (result != null)
                return Ok(result);
            return BadRequest("Erro to connect to webapi");
        }
        [HttpGet, Route("Logradouro/{cep}")]
        [SwaggerResponse(200, "Coordenadas para obter a GEOCOORDENADAS da origem para desenhar o caminho no mapa. Via logradouro demonstrou-se muitas falhas", typeof(Domain.Model.Bing.Point))]
        [SwaggerResponse(400, "Invalid Request")]
        public async Task<IActionResult> Logradouro([FromRoute] string cep)
        {

            var result = await _escolaService.GetLogradouro(cep);
            if (result != null)
                return Ok(result);
            return BadRequest("Erro to connect to webapi");
        }

        [HttpGet, Route("Directions")]
        [SwaggerResponse(200, "Obter as direçoes do ponto A ao Ponto B", typeof(Domain.Model.Bing.Point))]
        public async Task<IActionResult> Directions([FromQuery]double sLatitude, [FromQuery] double sLongitude, [FromQuery] double eLatitude,[FromQuery] double eLongitude)
        {
            var result = await _escolaService.GetBingRoute(sLatitude, sLongitude, eLatitude, eLongitude);
            return Ok(result);
        }
    }
}
