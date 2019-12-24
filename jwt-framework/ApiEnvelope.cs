using DocuSign.eSign.Client;
using DocuSign.eSign.Model;
using System.Collections.Generic;
using RestSharp;
using System.Text;
using Newtonsoft.Json;
using DocuSign.eSign.Api;
using System.Net.Http;
using jwt_framework;

namespace eg_01_csharp_jwt
{
    internal class ApiEnvelope : AuthToken, IEnvelope
    {
        public ApiEnvelope(ApiClient apiClient) : base(apiClient)
        {
        }      

        public string SendEnvelope(EnvelopeDefinition envDef)
        {
            string accountId = "9597916";
            var envelopeJson = JsonConvert.SerializeObject(envDef);
            var request = new RestRequest("/resource/", Method.POST);
            request.Resource = $"/restapi/v2/accounts/{accountId}/envelopes/";
            AuthToken authToken = new AuthToken(ApiClient);
            var accessToken = authToken.CheckToken();
            var client = new RestClient("https://demo.docusign.net");

            request.AddParameter("Authorization", string.Format("Bearer " + accessToken), ParameterType.HttpHeader);
            request.AddParameter("application/json; charset=utf-8", envelopeJson, ParameterType.RequestBody);
            IRestResponse responses = client.Execute(request);
            return responses.ToString();

        }
    }

  
}

