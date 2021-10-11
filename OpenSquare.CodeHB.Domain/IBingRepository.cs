using System;
using System.Threading.Tasks;

namespace OpenSquare.CodeHB.Domain
{
    public interface IBingRepository
    {
        Task<Domain.Model.Bing.GeoCodingBing> GetLatLong(string zipcodeorigem);
        Task<string> GetbingRoute(double sLatitude, double sLongitude, double eLatitude,
                               double eLongitude);
    }
}
