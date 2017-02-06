using Plugins.Interfaces;
using System.ComponentModel.Composition;

namespace Plugins
{
    [Export(typeof(IOperation))]
    [ExportMetadata("Country", "Hungary")]
    public class HungaryTax : IOperation
    {
        public decimal Calculate(decimal price)
        {
            return price * 1.16m;
        }
    }
}