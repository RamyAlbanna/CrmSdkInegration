using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CrmSdkInegration
{
    public class CrmHttpClient
    {
        public static async Task<string> CreateEntityAsync(string entitySetName, object entityData)
        {
            string jsonData = JsonConvert.SerializeObject(entityData);

            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpClient authenticatedClient = new HttpClient(Helper.GetClientHandler());

            HttpResponseMessage response = await authenticatedClient.PostAsync($"{Helper.BaseCRMApiPremisesUrl}/{entitySetName}", content);

            if (response.IsSuccessStatusCode)
            {
                string locationHeader = response.Headers.Location?.AbsolutePath;
                return locationHeader?.Split('(')[1].Split(')')[0];
            }
            else
            {
                Console.WriteLine("Error creating entity: " + response.ReasonPhrase);
                return null;
            }
        }

        public static async Task<string> UpdateContactAsync(string jsonObject, string contactId)
        {
            HttpClient client = new HttpClient(Helper.GetClientHandler());

            string url = $"{Helper.BaseCRMApiPremisesUrl}/contacts({contactId})";
            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("patch"), url)
            {
                Content = new StringContent(jsonObject, Encoding.UTF8, "application/json")
            };
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string locationHeader = response.Headers.Location?.AbsolutePath;
                return locationHeader?.Split('(')[1].Split(')')[0];
            }
            else
            {
                Console.WriteLine("Error creating entity: " + response.ReasonPhrase);
                return null;
            }
        }
    }
}
