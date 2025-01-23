using System.Net.Http;
using System.Net;
using Microsoft.Xrm.Tooling.Connector;
using System;

namespace CrmSdkInegration
{
    public static class Helper
    {
        private static readonly string Username = "administrator";
        private static readonly string Password = "123@qwe";
        private static readonly string onlineUrl = "AuthType=OAuth;Url=https://orgfda64f09.crm4.dynamics.com;ClientId=a010afb6-1c2c-4012-8c24-f639b9362e9b;ClientSecret=Zwg8Q~wdgs6JV7drXNE5Q8tjjYk77.4eSrnkWdyp;RedirectUri=http://localhost";
        private static readonly string onpremisesUrl = $@"Url={BasePremisesCRMUrl};UserName={Username};Password={Password};RequiredNewInstance=True";
        public static readonly string BaseCRMApiPremisesUrl = "http://192.168.1.176:5555/crmdemo/api/data/v9.0";
        public static readonly string BasePremisesCRMUrl = "http://192.168.1.176:5555/crmdemo";

        public static HttpClientHandler GetClientHandler()
        {
            return new HttpClientHandler()
            {
                Credentials = new NetworkCredential(Username, Password),
                PreAuthenticate = true,
            };
        }

        public static CrmServiceClient CreateServiceClient(Boolean isOnlinePlatform)
        {
            string Connection = isOnlinePlatform ? onlineUrl : onpremisesUrl;
            return new CrmServiceClient(Connection);
        }
    }
}
