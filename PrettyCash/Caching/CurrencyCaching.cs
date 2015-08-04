using PrettyCash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrettyCash.Caching
{
    public class CurrencyCaching
    {
        public static List<Currency> GetSet()
        {
            var db = new ApplicationDbContext();
            return new InMemoryCache().GetOrSet<List<Currency>>("Currencies", () => db.Currencies.OrderBy(c => c.ISO).ToList());
        }
    }
}