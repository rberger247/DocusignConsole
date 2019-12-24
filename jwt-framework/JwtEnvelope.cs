using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSign.eSign.Model;
using jwt_framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eg_01_csharp_jwt
{
    /// <summary>
    /// Send an envelope with a signer and cc recipient;
    /// </summary>
    internal class JwtEnvelope : AuthToken, IEnvelope
    {
        /// <summary>
        /// This class create and send envelope
        /// </summary>
        /// <param name="apiClient"></param>
        public JwtEnvelope(ApiClient apiClient) : base(apiClient)
        {
        }

        public string SendEnvelope(EnvelopeDefinition envDef)
        {
            EnvelopesApi envelopeApi = new EnvelopesApi(ApiClient.Configuration);
            var results = envelopeApi.CreateEnvelope(AccountID, envDef).ToJson();
            return results;
        }
    }
}
