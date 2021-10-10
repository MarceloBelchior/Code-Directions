using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenSquare.CodeHB.Domain
{
    public interface IEscolaService
    {
        Task<IEnumerable<Domain.Model.Escolas>> GetAllEscolas();
        Task<Domain.Model.Bing.Point> GetLogradouro(string logradouro);
    }
}
