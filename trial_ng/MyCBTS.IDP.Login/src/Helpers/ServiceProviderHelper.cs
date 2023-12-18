using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rsk.Saml;
using Rsk.Saml.Models;

namespace MyCBTS.IDP.Login.Helpers
{
    public class ServiceProviderHelper
    {
        private List<Service> _singleLogoutServices;
        private List<Service> _assertionConsumerServices;

        public string EntityId
        {
            get; set;
        }

        public string SigningCertificates
        {
            get; set;
        }

        public string SigningCertificatesPaths
        {
            get; set;
        }

        public string EncryptionCertificate
        {
            get; set;
        }

        public string EncryptionCertificatePath
        {
            get; set;
        }

        public List<Service> SingleLogoutServices
        {
            get
            {
                return this._singleLogoutServices;
            }

            set

            {
                this._singleLogoutServices = value;
                this._singleLogoutServices.ForEach(x => { x.Binding = GetBindings(x.Binding); });
            }
        }

        public List<Service> AssertionConsumerServices
        {
            get
            {
                return this._assertionConsumerServices;
            }
            set

            {
                this._assertionConsumerServices = value;
                this._assertionConsumerServices.ForEach(x => { x.Binding = GetBindings(x.Binding); });
            }
        }

        public bool SignAssertions { get; set; }
        public bool EncryptAssertions { get; set; }
        public bool RequireSamlRequestDestination { get; set; }

        private string GetBindings(string binding)
        {
            return binding switch
            {
                "HTTPPOST" => SamlConstants.BindingTypes.HttpPost,
                "HTTPREDRIECT" => SamlConstants.BindingTypes.HttpRedirect,
                "HTTPARTIFACT" => SamlConstants.BindingTypes.HttpArtifact,
                _ => binding,
            };
        }
    }
}
