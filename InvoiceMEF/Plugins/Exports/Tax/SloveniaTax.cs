using System.ComponentModel.Composition;
using Plugins.Interfaces;

namespace Plugins
{
    [Export(typeof(IOperation))]
    [ExportMetadata("Country", "Slovenia")]
    public class SloveniaTax : IOperation
    {
        public decimal Calculate(decimal price)
        {
            return price * 1.18m;
        }
    }
}