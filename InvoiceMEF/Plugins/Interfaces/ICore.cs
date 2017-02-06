namespace Plugins.Interfaces
{
    public interface ICore
    {
        decimal CalculateTax(decimal price, string country);
    }
}