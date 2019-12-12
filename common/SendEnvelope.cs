using System;
using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSign.eSign.Model;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using DocuSign.eSign.Client.Auth;

namespace eg_01_csharp_jwt
{
    /// <summary>
    /// Send an envelope with a signer and cc recipient; with three docs:
    /// an HTML, a Word, and a PDF doc.
    /// Anchor text positioning is used for the fields.
    /// </summary>
    internal class SendEnvelope : ExampleBase
    {
        private const String DOC_2_DOCX = "World_Wide_Corp_Battle_Plan_Trafalgar.docx";
        private const String DOC_3_PDF = "World_Wide_Corp_lorem.pdf";

        public static string ENVELOPE_1_DOCUMENT_1
        {
            get => "<!DOCTYPE html>" +
            "<html>" +
            "    <head>" +
            "      <meta charset=\"UTF-8\">" +
            "    </head>" +
            "    <body style=\"font-family:sans-serif;margin-left:2em;\">" +
            "    <h1 style=\"font-family: 'Trebuchet MS', Helvetica, sans-serif;" +
            "         color: darkblue;margin-bottom: 0;\">World Wide Corp</h1>" +
            "    <h2 style=\"font-family: 'Trebuchet MS', Helvetica, sans-serif;" +
            "         margin-top: 0px;margin-bottom: 3.5em;font-size: 1em;" +
            "         color: darkblue;\">Order Processing Division</h2>" +
            "  <h4>Ordered by " + DSConfig.Signer1Name + "</h4>" +
            "    <p style=\"margin-top:0em; margin-bottom:0em;\">Email: " + DSConfig.Signer1Email + "</p>" +
            "    <p style=\"margin-top:0em; margin-bottom:0em;\">Copy to: " + DSConfig.Cc1Name + ", " + DSConfig.Cc1Email + "</p>" +
            "    <p style=\"margin-top:3em;\">" +
            "  Candy bonbon pastry jujubes lollipop wafer biscuit biscuit. Topping brownie sesame snaps" +
            " sweet roll pie. Croissant danish biscuit soufflé caramels jujubes jelly. Dragée danish caramels lemon" +
            " drops dragée. Gummi bears cupcake biscuit tiramisu sugar plum pastry." +
            " Dragée gummies applicake pudding liquorice. Donut jujubes oat cake jelly-o. Dessert bear claw chocolate" +
            " cake gummies lollipop sugar plum ice cream gummies cheesecake." +
            "    </p>" +
            "    <!-- Note the anchor tag for the signature field is in white. -->" +
            "    <h3 style=\"margin-top:3em;\">Agreed: <span style=\"color:white;\">**signature_1**</span></h3>" +
            "    </body>" +
            "</html>";
        }

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

            EnvelopeDefinition envelope = this.CreateEvelope();
            EnvelopesApi envelopeApi = new EnvelopesApi(ApiClient.Configuration);
            EnvelopeSummary results = envelopeApi.CreateEnvelope(AccountID, envelope);
            
            return results;
        }
        /// <summary>
        /// This method creates the envelope request body 
        /// </summary>
        /// <returns></returns>
        private EnvelopeDefinition CreateEvelope()
        {
            //EnvelopeDefinition envelopeDefinition = new EnvelopeDefinition
            //{
            //    EmailSubject = "Please sign this document sent from the C# SDK"
            //};

            string accountId = "9597916";
            string templateId = "212fcb4c-75b2-41a3-9f40-1056373a5c6e";
            // Add it to the inlineTemplatesList
          
            // Create the definition for the envelope
            EnvelopeDefinition envDefinition = new EnvelopeDefinition();
            // Add the Composite Template list
     
            envDefinition.EmailSubject = "CompositeTemplate Example";
            envDefinition.Status = "sent";
            CarbonCopy cc1 = new CarbonCopy
            {
                Email = "RafiBerger613@gmail.com",
                Name = "Rafael Berger",
                RecipientId = "2",
                RoutingOrder = "2"
            };
            string signerEmail = "berger.aaronl@gmail.com";
            Signer signer1 = new Signer
            {
                Email = signerEmail,
                Name = "Aaron Berger",
                RecipientId = "1",
                RoutingOrder = "1"
            };
            Signer signer2 = new Signer
            {
                Email = "shaniberger12@gmail.com",
                Name = "Shani Berger",
                RecipientId = "1",
                RoutingOrder = "1"
            };
            SignHere signHere1 = new SignHere
            {
                AnchorString = "**signature_1**",
                AnchorUnits = "pixels",
                AnchorYOffset = "10",
                AnchorXOffset = "20"
            };

            SignHere signHere2 = new SignHere
            {
                AnchorString = "/sn1/",
                AnchorUnits = "pixels",
                AnchorYOffset = "10",
                AnchorXOffset = "20"
            };

            // Tabs are set per recipient / signer
            Tabs signer1Tabs = new Tabs
            {
                SignHereTabs = new List<SignHere> { signHere1, signHere2 }
            };
            signer1.Tabs = signer1Tabs;

            // Add the recipients to the envelope object
            Recipients recipients = new Recipients
            {
                Signers = new List<Signer> { signer1, signer2 },
                CarbonCopies = new List<CarbonCopy> { cc1 }
            };

            EnvelopeDefinition envDef = new EnvelopeDefinition();
            envDef.EmailSubject = "[DocuSign C# SDK] - Please sign this doc";

            envDef.TemplateId = templateId;
            TemplateRole tRole = new TemplateRole();
            tRole.Email = "rberger@weitzlux.com";
            tRole.Name = "R Berger";
            tRole.RoleName = "signer";
            List<Text> tabsTextList = new List<Text>();
            List<Number> tabsNumberList = new List<Number>();

            tRole.Tabs = new Tabs();
            tRole.Tabs.TextTabs = new List<Text>();
            Text textTab = new Text();
            textTab.TabLabel = "NameTest";
            textTab.Value = "R Berger";
            //textTab.Shared = "true";
            //textTab.PageNumber = "1";


            // TemplateRole tRole1 = new TemplateRole();
            // tRole.Email = "shaniberger12@gmail.com";
            // tRole.Name = "Shani Berger";
            // tRole.RoleName = "signer2";
            // List<Text> tabsTextList1 = new List<Text>();
            // List<Number> tabsNumberList1 = new List<Number>();

            // tRole1.Tabs = new Tabs();
            // tRole1.Tabs.TextTabs = new List<Text>();
            // Text textTab1 = new Text();
            // textTab1.TabLabel = "Name";
            // textTab1.Value = "Shani Berger";
            // textTab1.Shared = "true";
            // textTab1.PageNumber = "2";
            // // envDef.Recipients.CarbonCopies.Add(cc1);

            textTab.XPosition = "100";
            textTab.YPosition = "100";
            // var test = CreateSignHere("/sn1/", "pixels", "20", "10");
            ////tRole.Tabs.SignHereTabs.Add(CreateSignHere("/sn1/", "pixels", "20", "10"));
            // tRole.Tabs.SignHereTabs = new List<SignHere>() { test };
             tRole.Tabs.TextTabs.Add(textTab);

            // TemplateRole tRole2 = new TemplateRole();
            // tRole2.Email = "Rafiberger613@gmail.com";
            // tRole2.Name = "rafael Berger";
            // tRole2.RoleName = "reciever";
            // List<Text> tabsTextList2 = new List<Text>();
            // List<Number> tabsNumberList2 = new List<Number>();

            // tRole2.Tabs = new Tabs();
            // tRole2.Tabs.TextTabs = new List<Text>();
            // Text textTab2 = new Text();
            // textTab2.TabLabel = "Name";
            // textTab2.Value = "Rafael Berger";
            // textTab2.Shared = "true";
            // textTab2.PageNumber = "2";
            List<TemplateRole> rolesList = new List<TemplateRole>() { tRole };

            //add the role to the envelope and assign valid templateId from your account

                envDef.TemplateRoles = rolesList;

            envDef.Status = "sent";
      

            EnvelopesApi envelopesApi = new EnvelopesApi(ApiClient.Configuration);
      

            EnvelopesInformation envelopesList = new ListEnvelopes(ApiClient).List(); String filePath = String.Empty;
            FileStream fs = null;
            //foreach (var envelope in envelopesList.Envelopes)
            //{
            //    EnvelopeDocumentsResult docsList = envelopesApi.ListDocuments(accountId, envelope.EnvelopeId);
            //    foreach (var document in docsList.EnvelopeDocuments)
            //    {
            //        MemoryStream docStream = (MemoryStream)envelopesApi.GetDocument(accountId, envelope.EnvelopeId, document.DocumentId);
            //        filePath = @"C:\wlcollection\WORKAREA RafaelB\Docusign\Envelopes\" + envelope.EnvelopeId + document.DocumentId +  ".pdf";
            //        fs = new FileStream(filePath, FileMode.Create);
            //        docStream.Seek(0, SeekOrigin.Begin);
            //        docStream.CopyTo(fs);
            //        fs.Close();
                  

            //    }
                
            //}
             
            return envDef;
        }
        /// <summary>
        /// This method creates Recipients instance and populates its signers and carbon copies
        /// </summary>
        /// <param name="signer">Signer instance</param>
        /// <param name="cc">CarbonCopy array</param>
        /// <returns></returns>
        private Recipients CreateRecipients(Signer signer, params CarbonCopy[] cc)
        {
            Recipients recipients = new Recipients
            {
                Signers = new List<Signer>() { signer },
                CarbonCopies = new List<CarbonCopy>(cc)
            };

            return recipients;
        }
        /// <summary>
        /// This method create Tabs
        /// </summary>
        /// <param name="signer">Signer instance to be set tabs</param>
        /// <param name="signers">SignHere array</param>
        private void SetSignerTabs(Signer signer, params SignHere[] signers)
        {
            signer.Tabs = new Tabs
            {
                SignHereTabs = new List<SignHere>(signers)
            };
        }
        /// <summary>
        /// This method create SignHere anchor
        /// </summary>
        /// <param name="anchorPattern">anchor pattern</param>
        /// <param name="anchorUnits">anchor units</param>
        /// <param name="anchorXOffset">anchor x offset</param>
        /// <param name="anchorYOffset">anchor y offset</param>
        /// <returns></returns>
        private SignHere CreateSignHere(String anchorPattern, String anchorUnits, String anchorXOffset, String anchorYOffset)
        {
            return new SignHere
            {
                AnchorString = anchorPattern,
                AnchorUnits = anchorUnits,
                AnchorXOffset = anchorXOffset,
                AnchorYOffset = anchorYOffset
            };
        }
        /// <summary>
        /// This method creates CarbonCopy instance and populate its members
        /// </summary>
        /// <returns>CarbonCopy instance</returns>
        private CarbonCopy CreateCarbonCopy()
        {
            return new CarbonCopy
            {
                Email = DSConfig.Cc1Email,
                Name = DSConfig.Cc1Name,
                RoutingOrder = "2",
                RecipientId = "2"
            };
        }
        /// <summary>
        /// This method creates Signer instance and populates its members
        /// </summary>
        /// <returns>Signer instance</returns>
        private Signer CreateSigner()
        {
            return new Signer
            {
                Email = DSConfig.Signer1Email,
                Name = DSConfig.Signer1Name,
                RecipientId = "1",
                RoutingOrder = "1"
            };
        }
        /// <summary>
        /// This method create document from byte array template
        /// </summary>
        /// <param name="documentId">document id</param>
        /// <param name="fileName">file name</param>
        /// <param name="fileExtension">file extention</param>
        /// <param name="templateContent">file content byte array</param>
        /// <returns></returns>
        private Document CreateDocumentFromTemplate(String documentId, String fileName, String fileExtension, byte[] templateContent)
        {
            Document document = new Document();

            String base64Content = Convert.ToBase64String(templateContent);

            document.DocumentBase64 = base64Content;
            // can be different from actual file name
            document.Name = fileName;
            // Source data format. Signed docs are always pdf.
            document.FileExtension = fileExtension;
            // a label used to reference the doc
            document.DocumentId = documentId;

            return document;
        }
    }
}
