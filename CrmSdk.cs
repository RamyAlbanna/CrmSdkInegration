using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;

namespace CrmSdkInegration
{
    public class CrmSdk
    {
        private readonly CrmServiceClient svc;
        public CrmSdk()
        {
            this.svc = Helper.CreateServiceClient(false);
        }

        public Guid CreateContactAndBindWithAccount(Guid accountId)
        {
            var contact = new Entity("contact");
            contact["firstname"] = "Donia";
            contact["lastname"] = "Elmalky";
            contact["parentcustomerid"] = new EntityReference("account",accountId);
            return this.svc.Create(contact);
        }

        public Guid CreateSingleContact()
        {
            var contact = new Entity("contact");
            contact["firstname"] = "Donia";
            contact["lastname"] = "Elmalky";
            return this.svc.Create(contact);
        }

        public Guid CreateAccount()
        {
            var account = new Entity("account");
            account["telephone1"] = "01033842262";
            account["address1_city"] = "Alexandria";
            account["address1_country"] = "Egypt";
            account["name"] = "Elbanna Account";
            return this.svc.Create(account);
        }
    }
}
