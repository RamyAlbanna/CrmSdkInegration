using System.Net.Http;
using System.Net;
using Microsoft.Xrm.Tooling.Connector;

namespace CrmSdkInegration
{
    public static class Helper
    {
        private static readonly string Username = "administrator";
        private static readonly string Password = "123@qwe";

        public static readonly string BaseCRMApiUrl = "http://192.168.1.176:5555/crmdemo/api/data/v9.0";
        public static readonly string BaseCRMUrl = "http://192.168.1.176:5555/crmdemo";
        public static HttpClientHandler GetClientHandler()
        {
            return new HttpClientHandler()
            {
                Credentials = new NetworkCredential(Username, Password),
                PreAuthenticate = true,
            };
        }

        public static CrmServiceClient CreateServiceClient()
        {
            string connection = $@"Url={BaseCRMUrl};UserName={Username};Password={Password};RequiredNewInstance=True";
            return new CrmServiceClient(connection);
        }
    }
}
