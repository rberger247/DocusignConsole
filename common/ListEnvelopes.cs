using System;
using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSign.eSign.Model;
using RestSharp;
using static DocuSign.eSign.Api.EnvelopesApi;

namespace eg_01_csharp_jwt
{
    /// <summary>
    /// This class calls the  List status changes API method.
    /// </summary>
    internal class ListEnvelopes: AuthToken
    {
        public ListEnvelopes(ApiClient client) : base(client)
        {
        }
        /// <summary>
        /// This method get status for one or more envelope(s) in the last 30 days
        /// </summary>
        /// <returns>returns envelopes information</returns>
        internal EnvelopesInformation List()
        {
            CheckToken();
            EnvelopesApi envelopeApi = new EnvelopesApi(ApiClient.Configuration);
    
            ListStatusChangesOptions options = new ListStatusChangesOptions();
       //     options.status = "sent";
            DateTime date = DateTime.Now.AddDays(-30);
            options.fromDate = date.ToString("yyyy/MM/dd");
            return envelopeApi.ListStatusChanges(AccountID, options);
        }
    }
}
