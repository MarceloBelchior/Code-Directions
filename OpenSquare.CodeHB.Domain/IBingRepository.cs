using System;
using System.Threading.Tasks;

namespace OpenSquare.CodeHB.Domain
{
    public interface IBingRepository
    {
        Task<Domain.Model.Bing.GeoCodingBing> GetLatLong(string zipcodeorigem);
    }
}
