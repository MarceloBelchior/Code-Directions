using System;
using System.Threading.Tasks;

namespace OpenSquare.CodeHB.Domain
{
    public interface IEscolaRepository
    {
        Task<Domain.Model.EscolaModel> GetAllEscolas();
        Task<Domain.Model.EscolaModel> UpdateInsert(Domain.Model.EscolaModel entity);
    }
}
