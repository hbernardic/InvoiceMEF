using System;
using System.ComponentModel.Composition;
using Plugins.Interfaces;

namespace Plugins
{
    [Export(typeof(IOperation))]
    [ExportMetadata("Country", "Croatia")]
    public class CroatiaTax : IOperation
    {
        public decimal Calculate(decimal price)
        {
            Console.WriteLine("bok");

            return price * 1.25m;
        }
    }
}