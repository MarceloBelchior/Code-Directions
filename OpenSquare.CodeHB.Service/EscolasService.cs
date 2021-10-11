using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenSquare.CodeHB.Domain;

namespace OpenSquare.CodeHB.Service
{
    public class EscolasService : IEscolaService
    {

        private readonly IEscolaRepository _escolaRepository;
        private readonly IBingRepository _bingRepository;

        public EscolasService(IEscolaRepository escolaRepository, IBingRepository bingRepository) {
            _escolaRepository = escolaRepository;
            _bingRepository = bingRepository;
        } 

        public async Task<IEnumerable<Domain.Model.Escolas>> GetAllEscolas()
        {


            var result = await _escolaRepository.GetAllEscolas();
            return result.result.records.AsEnumerable();

        
            
        }
        public async Task<Domain.Model.Bing.Point> GetLogradouro(string logradouro)
        {
            var result = await _bingRepository.GetLatLong(logradouro);
            return result.resourceSets.FirstOrDefault().resources.FirstOrDefault().point;
        }

        public async Task<string> GetBingRoute(double sLatitude, double sLongitude, double eLatitude,
                               double eLongitude)
        {
            return await _bingRepository.GetbingRoute(sLatitude, sLongitude, eLatitude, eLongitude);
        }

        private async Task<double> Calculate(double sLatitude, double sLongitude, double eLatitude,
                               double eLongitude)
        {
            var rad = (Math.PI / 180.0);

            var sLatitudeRadians = sLatitude * rad;
            var sLongitudeRadians = sLongitude * rad;
            var eLatitudeRadians = eLatitude * rad;
            var eLongitudeRadians = eLongitude * rad;

            var dLongitude = eLongitudeRadians - sLongitudeRadians;
            var dLatitude = eLatitudeRadians - sLatitudeRadians;

            var result1 = Math.Pow(Math.Sin(dLatitude / 2.0), 2.0) +
                          Math.Cos(sLatitudeRadians) * Math.Cos(eLatitudeRadians) *
                          Math.Pow(Math.Sin(dLongitude / 2.0), 2.0);

 
            var result2 = 3956.0 * 2.0 *
                          Math.Atan2(Math.Sqrt(result1), Math.Sqrt(1.0 - result1));

            return result2;
        }

    }
}
