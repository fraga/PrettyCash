using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PrettyCash.Models;
using System.Text;
using System.IO;

namespace PrettyCash.Controllers
{
    public class ExchangeRatesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public async Task<IHttpActionResult> GetLatestExchangeRate()
        {
            var exchangeApi = "http://api.fixer.io/latest?base=USD";
            var exchangeRate = new ExchangeRate();

            using(var client = new HttpClient())
            {
                var result = await client.GetAsync(exchangeApi);
                using(var stream = new StreamReader(await result.Content.ReadAsStreamAsync()))
                {
                    exchangeRate.Data = Convert.ToBase64String(Encoding.UTF8.GetBytes(stream.ReadToEnd()));
                }
                
            }

            exchangeRate.DateTime = DateTime.UtcNow;

            var curEx = db.ExchangeRates.Where(e => e.Data == exchangeRate.Data);
            if (curEx.Any())
                return CreatedAtRoute("DefaultApi", new { id = curEx.First().Id }, curEx.First());


            db.Entry(exchangeRate).State = EntityState.Added;
            db.ExchangeRates.Add(exchangeRate);

            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = exchangeRate.Id }, exchangeRate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExchangeRateExists(int id)
        {
            return db.ExchangeRates.Count(e => e.Id == id) > 0;
        }
    }
}