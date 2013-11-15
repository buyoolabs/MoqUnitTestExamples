using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTestExample
{
    public class WhereToBuyFilterService
    {
        ILog _log = null;
        List<Product> _products = null;
        IFilterConfiguration _config = null;
        List<Product> ProductWithoutPrice = null;

        public WhereToBuyFilterService(IFilterConfiguration config, ILog log)
        {
            
            _config = config;
            _log = log;
        }

        public void Filter(ref List<Product> products)
        {
            _products = products;

            if (_config.ShowWithoutPrice == true)
            {
                ProductWithoutPrice = _products.Where(x => (!(x.PriceAmount.HasValue) || (x.PriceAmount.HasValue && x.PriceAmount.Value == 0))).ToList();
            }

            if (_config.ShowByTextIfExistGtin == false)
            {
                RemoveProductsByText();
            }

            if (_config.NumMaxPerMerchant > 0)
            {
                FilterLowestPricePerMerchant();
            }

            _products = _products.OrderBy(p => p.PriceAmount).ToList();

            if (_config.ShowWithoutPrice == true)
            {
                _products.AddRange(ProductWithoutPrice); 
            }

            products = _products;
        }

        private void Log(string message)
        {
            if (_log == null) return;

            _log.Log(message);
        }

        private void RemoveProductsByText ()
        {
            // Remove results with Text if there are results with identifier
            if (_products.Any(p => (p.ProductApiStep.ToLower() != "upc") && (p.ProductApiStep != string.Empty)))
            {
                _products.RemoveAll(p => p.ProductApiStep == "text");
            }
        }

        private void FilterLowestPricePerMerchant()
        {
            //if the products are different merchants, show a maximum of 3 per api
            var merchantNames = _products.Select(p => p.Merchant).Distinct();

            if (merchantNames.Count() > 1)
            {
                //products Groupby merchant name and with price
                var productsByMerchantName = _products.Where(x => x.PriceAmount.HasValue && x.PriceAmount.Value > 0).GroupBy(p => p.Merchant);

                //take 3 lowest price products per merchan name 
                _products = productsByMerchantName.SelectMany(g => g.OrderBy(x => x.PriceAmount.Value).Take(_config.NumMaxPerMerchant)).ToList();
            }
        }
    }
}
