using System;
using System.Collections.Generic;
using DiscountModule.Config;
using System.Text;

namespace DiscountModule.Models
{
    public class PricingPlan
    {
        /// <summary>
        /// Pricings this instance.
        /// </summary>
        /// <returns></returns>
        public  List<Providers> Pricing()
        {
            var Pricing = new List<Providers>();
            Pricing.Add(new Providers() { Provider = ProvidersEnum.LP.ToString(), PackageSize = PackageEnum.S.ToString(), Price = 1.50m });
            Pricing.Add(new Providers() { Provider = ProvidersEnum.LP.ToString(), PackageSize = PackageEnum.M.ToString(), Price = 4.90m });
            Pricing.Add(new Providers() { Provider = ProvidersEnum.LP.ToString(), PackageSize = PackageEnum.L.ToString(), Price = 6.90m });
            Pricing.Add(new Providers() { Provider = ProvidersEnum.MR.ToString(), PackageSize = PackageEnum.S.ToString(), Price = 2 });
            Pricing.Add(new Providers() { Provider = ProvidersEnum.MR.ToString(), PackageSize = PackageEnum.M.ToString(), Price = 3 });
            Pricing.Add(new Providers() { Provider = ProvidersEnum.MR.ToString(), PackageSize = PackageEnum.L.ToString(), Price = 4 });
            return Pricing;
        }
    }
}
