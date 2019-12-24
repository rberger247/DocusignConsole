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

            CheckToken();
            //Envelopes sendEnvelopes = new Envelopes(ApiClient);
            //var envDef = sendEnvelopes.CreateEvelope();
            EnvelopesApi envelopeApi = new EnvelopesApi(ApiClient.Configuration);
            var results = envelopeApi.CreateEnvelope(AccountID, envDef).ToJson();
            return results;

        }

  
        //    EnvelopesApi envelopesApi = new EnvelopesApi(ApiClient.Configuration);
        //    EnvelopesInformation envelopesList = new ListEnvelopes(ApiClient).List();

        //    var url = "https://account-d.docusign.com";
        //    var client = new RestClient(url);

        //    string filePath = String.Empty;
        //    FileStream fs = null;
        //    foreach (var envelope in envelopesList.Envelopes)
        //    {
        //        CheckToken();
        //        FoldersApi foldersApi = new FoldersApi(ApiClient.Configuration);
        //        //FoldersRequest foldersRequest = new FoldersRequest();
        //        //foldersRequest.EnvelopeIds = new List<string>();
        //        //foldersRequest.EnvelopeIds.Add(envelope.EnvelopeId);


        //        //foldersApi.MoveEnvelopes(accountId, "recyclebin", foldersRequest);

        //        EnvelopeDocumentsResult docsList = envelopesApi.ListDocuments(accountId, envelope.EnvelopeId);


        //        foreach (var document in docsList.EnvelopeDocuments)
        //        {

        //            MemoryStream docStream = (MemoryStream)envelopesApi.GetDocument(accountId, envelope.EnvelopeId, document.DocumentId);

        //            filePath = @"C:\wlcollection\WORKAREA RafaelB\Docusign\Envelopes\" + envelope.EnvelopeId + document.DocumentId + ".pdf";
        //            fs = new FileStream(filePath, FileMode.Create);
        //            docStream.Seek(0, SeekOrigin.Begin);
        //            docStream.CopyTo(fs);
        //            fs.Close();
        //        }
        //    }



    }
}
