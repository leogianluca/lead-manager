using System;

namespace LeadManager.Domain.ValueObjects
{
    public class Price
    {
        public decimal Value { get; private set; }

        public Price(decimal value)
        {
            if (value < 0)
                throw new ArgumentException("Preço não pode ser negativo.");

            Value = decimal.Round(value, 2);
        }

        public override string ToString() => Value.ToString("C"); // Formato moeda
    }
}
