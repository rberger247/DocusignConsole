﻿using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSign.eSign.Model;
using System;
using System.Collections.Generic;

namespace eg_01_csharp_jwt
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var apiClient = new ApiClient();

                Console.WriteLine("\nSending an envelope with three documents. This takes about 15 seconds...");
                // EnvelopeSummary result = new SendEnvelope(apiClient).Send();
                var result = new SendEnvelope(apiClient).Send();
              
               
                //  EnvelopeSummary result = new ApiEnvelope(apiClient).Send();


                Console.WriteLine("\nDone. Envelope status: {0}. Envelope ID: {1}", result, result);


                //EnvelopeDefinition envDef = new EnvelopeDefinition();
                //envDef.EmailSubject = "[DocuSign C# SDK] - Please sign this doc";

                //// assign recipient to template role by setting name, email, and role name.  Note that the
                //// template role name must match the placeholder role name saved in your account template.  
                //TemplateRole tRole = new TemplateRole();
                //tRole.Email = "Rafiberger613@gmail.com";
                //tRole.Name = "{USER_NAME}";
                //tRole.RoleName = "{ROLE}";

                //List<TemplateRole> rolesList = new List<TemplateRole>() { tRole };

                //// add the role to the envelope and assign valid templateId from your account
                //envDef.TemplateRoles = rolesList;
                //envDef.TemplateId = "294c257f-f19f-4bd0-aefb-37b26ae37806";

                //// set envelope status to "sent" to immediately send the signature request
                //envDef.Status = "sent";
                //string accountId = "9597916";
                //// |EnvelopesApi| contains methods related to creating and sending Envelopes (aka signature requests)
                //EnvelopesApi envelopesApi = new EnvelopesApi();
                //EnvelopeSummary envelopeSummary = envelopesApi.CreateEnvelope(accountId, envDef);

                Console.WriteLine("\n\nList the envelopes in the account...");
                EnvelopesInformation envelopesList = new ListEnvelopes(apiClient).List();
                List<Envelope> envelopes = envelopesList.Envelopes;
             //   294c257f - f19f - 4bd0 - aefb - 37b26ae37806
                if (envelopesList != null && envelopes.Count > 2)
                {
                    Console.WriteLine("Results for {0} envelopes were returned. Showing the first two: ", envelopes.Count);
                    envelopesList.Envelopes = new List<Envelope>() {
                        envelopes[0],
                        envelopes[1]
                    };
                }

                DSHelper.PrintPrettyJSON(envelopesList);
            }
            catch (ApiException e)
            {
                Console.WriteLine("\nDocuSign Exception!");

                // Special handling for consent_required
                String message = e.Message;
                if (!String.IsNullOrWhiteSpace(message) && message.Contains("consent_required"))
                {
                    String consent_url = String.Format("\n    {0}/oauth/auth?response_type=code&scope={1}&client_id={2}&redirect_uri={3}",
                        DSConfig.AuthenticationURL, DSConfig.PermissionScopes, DSConfig.ClientID, DSConfig.OAuthRedirectURI);

                    Console.WriteLine("C O N S E N T   R E Q U I R E D");
                    Console.WriteLine("Ask the user who will be impersonated to run the following url: ");
                    Console.WriteLine(consent_url);
                    Console.WriteLine("\nIt will ask the user to login and to approve access by your application.");
                    Console.WriteLine("Alternatively, an Administrator can use Organization Administration to");
                    Console.WriteLine("pre-approve one or more users.");
                }
                else
                {
                    Console.WriteLine("    Reason: {0}", e.ErrorCode);
                    Console.WriteLine("    Error Reponse: {0}", e.ErrorContent);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("Done. Hit enter to exit...");
            Console.ReadKey();
        }
    }
}
