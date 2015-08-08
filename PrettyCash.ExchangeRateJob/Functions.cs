using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using System.Net.Http;

namespace PrettyCash.ExchangeRateJob
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("queue")] string message, TextWriter log)
        {
            using (var client = new HttpClient())
            {
                client.GetAsync("http://prettycash.azurewebsites.net/api/ExchangeRates");
            }
            log.WriteLine("OK");
        }
    }
}
