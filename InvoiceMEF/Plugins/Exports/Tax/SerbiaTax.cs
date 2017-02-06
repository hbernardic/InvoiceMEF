using System.ComponentModel.Composition;
using Plugins.Interfaces;

namespace Plugins
{
    [Export(typeof(IOperation))]
    [ExportMetadata("Country", "Serbia")]
    public class SerbiaTax : IOperation
    {
        public decimal Calculate(decimal price)
        {
            return price * 1.17m;
        }
    }
}