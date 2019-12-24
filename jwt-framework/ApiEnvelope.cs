using DocuSign.eSign.Client;
using DocuSign.eSign.Model;
using System.Collections.Generic;
using RestSharp;
using System.Text;
using Newtonsoft.Json;
using DocuSign.eSign.Api;
using System.Net.Http;

namespace eg_01_csharp_jwt
{
    internal class ApiEnvelope : AuthToken
    {
        public ApiEnvelope(ApiClient apiClient) : base(apiClient)
        {
        }

        internal EnvelopeSummary Send()
        {
            CheckToken();

            //EnvelopeDefinition envelope = this.CreateEvelope();
            EnvelopeDefinition envelope = this.CreateEnvelopeApi();
            EnvelopesApi envelopeApi = new EnvelopesApi(ApiClient.Configuration);
            EnvelopeSummary results = envelopeApi.CreateEnvelope(AccountID, envelope);

            return results;
        }

        public EnvelopeDefinition CreateEnvelopeApi()
        {


            EnvelopeDefinition envDef = new EnvelopeDefinition();
            

            string templateId = "212fcb4c-75b2-41a3-9f40-1056373a5c6e";
            string accountId = "9597916";

            TemplateRole tRole = new TemplateRole();
            tRole.Email = "rberger@weitzlux.com";
            //tRole.Name = "Rafael Berger";
            tRole.RoleName = "signer";
            tRole.Tabs = new Tabs();
            tRole.Tabs.TextTabs = new List<Text>();
            Text textTab = new Text();
            textTab.TabLabel = "FullName";
            textTab.Value = "R Berger";
            tRole.Tabs.TextTabs.Add(textTab);
            List<TemplateRole> rolesList = new List<TemplateRole>() { tRole };


            envDef.EmailSubject = "[DocuSign] - Please sign this doc";
            envDef.TemplateId = templateId;
            envDef.TemplateRoles = rolesList;
            envDef.Status = "sent";

            var envelopeJson = JsonConvert.SerializeObject(envDef);
            var content = new StringContent(envelopeJson, Encoding.UTF8, "application/json");

            var request = new RestRequest(Method.POST);
            request.Resource = $"/restapi/v2/accounts/{accountId}/envelopes/";
            request.RequestFormat = DataFormat.Json;
            var client = new RestClient("https://demo.docusign.net");
            AuthToken authToken = new AuthToken(ApiClient);
            var accessToken = authToken.CheckToken();
            request.AddBody(envDef);
            request.AddParameter("Authorization", string.Format("Bearer " + accessToken), ParameterType.HttpHeader);         
            IRestResponse response = client.Execute(request);
            var result = response.Content;
            return envDef;
        }


    }

  
}

