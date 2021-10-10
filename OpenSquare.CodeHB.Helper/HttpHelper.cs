using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenSquare.CodeHB.Domain.Helper;

namespace OpenSquare.CodeHB.Helper
{
    public class HttpHelper : IHttpHelper
    {
   
        public HttpHelper() { }
      

        public async Task<HttpResponseMessage> Send(HttpMethod verboHttp, string url, string bodyRequest)
        {
            return await Send(verboHttp, url, bodyRequest, null);
        }

        public async Task<HttpResponseMessage> Send(HttpMethod verboHttp, string url)
        {
            return await Send(verboHttp, url, string.Empty, null);
        }
        public async Task<HttpResponseMessage> Send(HttpMethod verboHttp, string url, string bodyRequest, Dictionary<string, string> headers)
        {
            int timeout = 5;
            return await this.Send(verboHttp, url, bodyRequest, headers, timeout);
        }

        public async Task<HttpResponseMessage> Send(HttpMethod verboHttp, string url, string bodyRequest, Dictionary<string, string> headers, int timeout)
        {

            var handle = new HttpClientHandler();
                  

                    using (HttpClient client = new HttpClient(handle))
                    {
                        client.Timeout = TimeSpan.FromSeconds(timeout);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


                        using (HttpRequestMessage request = new HttpRequestMessage(verboHttp, url))
                        {
                            if (bodyRequest != null && verboHttp != HttpMethod.Get && bodyRequest != null)
                                request.Content = new JSONContent((string)(object)bodyRequest);

                            if (headers != null && headers.Count > 0)
                            {
                                foreach (var item in headers)
                                {
                                    request.Headers.Add(item.Key, item.Value);
                                }
                            }

                            return await client.SendAsync(request);

                        }
                    }
               
           
        }
    }
    public class JSONContent : StringContent
    {
        public JSONContent(object objeto)
            : base(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json")
        { }

        public JSONContent(string objeto)
            : base(objeto, Encoding.UTF8, "application/json")
        { }
    }
}
