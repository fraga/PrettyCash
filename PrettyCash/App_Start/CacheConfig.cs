using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PrettyCash.Models;
using PrettyCash.Caching;

namespace PrettyCash
{
    public class CacheConfig
    {
        public static void RegisterCurrencyCache()
        {
            CurrencyCaching.GetSet();
        }
    }
}