using Plugins.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Plugins.Exports
{
    [Export(typeof(ICore))]
    public class Core : ICore
    {
        [ImportMany]
        private IEnumerable<Lazy<IOperation, IOperationCountry>> _calculations;

        public decimal CalculateTax(decimal price, string country)
        {
            foreach (var c in _calculations)
            {
                if (c.Metadata.Country.Equals(country))
                {
                    return c.Value.Calculate(price);
                }
            }
            return price;
        }
    }
}