using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace Client.UserPassword
{
    /*
     * When you send the token to the identity API endpoint, you will notice one small but important 
     * difference compared to the client credentials grant. The access token will now contain a sub claim which 
     * uniquely identifies the user. This “sub” claim can be seen by examining the content variable after the call 
     * to the API and also will be displayed on the screen by the console application.
     * 
     * The presence (or absence) of the sub claim lets the API distinguish between calls on behalf of clients and 
     * calls on behalf of users.
     */
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () => await MainAsync(args)).GetAwaiter().GetResult();
        }

        public static async Task MainAsync(string[] args)
        {
            var token = await GetToken();
            Console.WriteLine("Press any key to call api");
            Console.ReadLine();
            await CallApi(token);
            Console.ReadLine();
        }

        static async Task CallApi(TokenResponse tokenResponse)
        {
            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5001/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }

        static async Task<TokenResponse> GetToken()
        {
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("alice", "password", "api1");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                throw new Exception(tokenResponse.Error);
            }

            Console.WriteLine(tokenResponse.Json);
            return tokenResponse;
        }
    }
}
