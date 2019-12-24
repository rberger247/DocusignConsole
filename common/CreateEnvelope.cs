using System;
using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSign.eSign.Model;
using System.Collections.Generic;
using System.IO;
using RestSharp;
using jwt_framework;

namespace eg_01_csharp_jwt
{
    /// <summary>
    /// Send an envelope with a signer and cc recipient;
    /// </summary>
    internal class SendEnvelope : AuthToken, ISendEnvelope
    {
        /// <summary>
        /// This class create and send envelope
        /// </summary>
        /// <param name="apiClient"></param>
        public SendEnvelope(ApiClient apiClient) : base(apiClient)
        {
        }
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        internal EnvelopeSummary Send()
        {
            CheckToken();
            EnvelopeDefinition envelope = new EnvelopeDefinition();
            EnvelopesApi envelopeApi = new EnvelopesApi(ApiClient.Configuration);
            EnvelopeSummary results = envelopeApi.CreateEnvelope(AccountID, envelope);
            
            return results;
        }
        ///// <summary>
        ///// This method creates the envelope request body 
        ///// </summary>
        ///// <returns></returns>
        //private EnvelopeDefinition CreateEvelope()
        //{       
        //    string accountId = "9597916";
        //    string templateId = "212fcb4c-75b2-41a3-9f40-1056373a5c6e";
               
        //    EnvelopeDefinition envDef = new EnvelopeDefinition();

        //    envDef.TemplateId = templateId;
        //    TemplateRole tRole = new TemplateRole();
        //    tRole.Email = "rberger@weitzlux.com";
        //    //tRole.Name = "Rafael Berger";
        //    tRole.RoleName = "signer";

        //    tRole.Tabs = new Tabs();
        //    tRole.Tabs.TextTabs = new List<Text>();
        //    Text textTab = new Text();
        //    textTab.TabLabel = "FullName";
        //    textTab.Value = "R Berger";
        //    tRole.Tabs.TextTabs.Add(textTab);
        //    List<TemplateRole> rolesList = new List<TemplateRole>() { tRole };

        //    envDef.EmailSubject = "[DocuSign] - Please sign this doc";
        //    envDef.TemplateRoles = rolesList;
        //    envDef.Status = "sent";
      
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
        //    return envDef;
        //}

        EnvelopeDefinition ISendEnvelope.SendEnvelope()
        {
            CreateEnvelope createEnvelope = new CreateEnvelope(ApiClient);
            EnvelopeDefinition envelope =  createEnvelope.CreateEvelope();
            EnvelopesApi envelopeApi = new EnvelopesApi(ApiClient.Configuration);
            EnvelopeSummary results = envelopeApi.CreateEnvelope(AccountID, envelope);

            throw new NotImplementedException();
        }
    }
}
