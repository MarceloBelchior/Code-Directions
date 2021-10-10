using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace OpenSquare.CodeHB.Domain.Helper
{
    public interface IHttpHelper
    {
        Task<HttpResponseMessage> Send(HttpMethod verboHttp, string url, string bodyRequest);
        Task<HttpResponseMessage> Send(HttpMethod verboHttp, string url);
        Task<HttpResponseMessage> Send(HttpMethod verboHttp, string url, string bodyRequest, Dictionary<string, string> headers);
        Task<HttpResponseMessage> Send(HttpMethod verboHttp, string url, string bodyRequest, Dictionary<string, string> headers, int timeout);
    }
}
