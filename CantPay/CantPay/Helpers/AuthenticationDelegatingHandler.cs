using CantPay.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CantPay.Helpers
{
    class AuthenticationDelegatingHandler : DelegatingHandler
    {
        protected async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken, string authType)
        {
            //Clone request in case of re-issue
            var clone = await CloneHttpRequestMessageAsync(request);

            //Now perform request
            var response = await base.SendAsync(request, cancellationToken);
        {
        //Request was 404 - We will LoginAsync,
        //which will refresh if appropriate, if not will prompt for credentials
        var user = await ServiceLocator.Instance.Resolve<ICloudService>().LoginAsync(authType); //Remove Authtype?

        //now retry request with cloned request. Remove and replace X-ZUM-AUTH header with new auth token.
        clone.Headers.Remove("X-ZUMO-AUTH");
        clone.Headers.Add("X-ZUMO-AUTH", user.MobileServiceAuthenticationToken);
        response = await base.SendAsync(clone, cancellationToken);
        }
            return response;
        }
               
        //Clone HttpRequestMessage

        public static async Task<HttpRequestMessage> CloneHttpRequestMessageAsync(HttpRequestMessage req)
        {
            HttpRequestMessage clone = new HttpRequestMessage(req.Method, req.RequestUri);

            //Copy request content via stream to cloned object
            var ms = new MemoryStream();
            if (req.Content != null)
            {
                await req.Content.CopyToAsync(ms).ConfigureAwait(false);
                ms.Position = 0;
                clone.Content = new StreamContent(ms);

                //Copy content headers
                if (req.Content.Headers != null)
                    foreach (var h in req.Content.Headers)
                        clone.Content.Headers.Add(h.Key, h.Value);
            }

            clone.Version = req.Version;

            foreach (KeyValuePair<string, object> prop in req.Properties)
                clone.Properties.Add(prop);

            foreach (KeyValuePair<string, IEnumerable<String>> header in req.Headers)
                clone.Headers.TryAddWithoutValidation(header.Key, header.Value);

            return clone;
        }
                              
    }
        
}
