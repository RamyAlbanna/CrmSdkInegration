using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CrmSdkInegration
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IntegrateUsingSdk();
        }

        public static void IntegrateUsingSdk()
        {
            CrmSdk crmSdk = new CrmSdk();
            Guid accountId = crmSdk.CreateAccount();
            crmSdk.CreateContactAndBindWithAccount(accountId);
        }

        public static async Task IntegrateUsingHttpClient()
        {
            var accountData = new
            {
                name = "Elbanna Account",
                telephone1 = "01033842262",
                address1_city = "Alexandria",
                address1_country = "Egypt"
            };

            var contactData = new
            {
                firstname = "Donia",
                lastname = "Elmalky",
            };

            var accountId = await CrmHttpClient.CreateEntityAsync("accounts", accountData);
            var contactId = await CrmHttpClient.CreateEntityAsync ("contacts", contactData);

            JObject jprimaryContact = new JObject
            {
                { "parentcustomerid_account@odata.bind", $"/accounts({accountId})" }
            };


            await CrmHttpClient.UpdateContactAsync(jprimaryContact.ToString(), contactId);
        }
    }
}
