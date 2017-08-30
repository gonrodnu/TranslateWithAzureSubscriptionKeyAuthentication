﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AzureSubscriptionKeySample
{
    class Program
    {
        /// Header name used to pass the subscription key to translation service
        private const string OcpApimSubscriptionKeyHeader = "Ocp-Apim-Subscription-Key";
        
        /// Url template to make translate call
        private const string TranslateUrlTemplate = "http://api.microsofttranslator.com/v2/http.svc/translate?text={0}&from={1}&to={2}&category={3}";

        private const string AzureSubscriptionKey = "Your Subscription Key";   //Enter here the Key from your Microsoft Translator Text subscription on http://portal.azure.com

        static void Main(string[] args)
        {
            TranslateAsync().Wait();
            Console.ReadKey();
        }

        /// Demonstrates Translate API call using Azure Subscription key authentication.
        private static async Task TranslateAsync()
        {
            try
            {
                var translateResponse = await TranslateRequest(string.Format(TranslateUrlTemplate, "Hello world.", "en", "fr", "general"), AzureSubscriptionKey);
                var translateResponseContent = await translateResponse.Content.ReadAsStringAsync();
                if (translateResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine("Translation result: {0}", translateResponseContent);
                }
                else
                {
                    Console.Error.WriteLine("Failed to translate. Response: {0}", translateResponseContent);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Failed to translate. Exception: {0}", ex.Message);
            }
        }

        public static async Task<HttpResponseMessage> TranslateRequest(string url, string azureSubscriptionKey)
        {
            using (HttpClient client = new HttpClient())
            { 
                client.DefaultRequestHeaders.Add(OcpApimSubscriptionKeyHeader, azureSubscriptionKey);
                return await client.GetAsync(url);
            }
        }
    }
}
