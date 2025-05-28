using System;

namespace LeadManager.Domain.ValueObjects
{
    public class Suburb
    {
        public string Value { get; private set; }

        public Suburb(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Bairro não pode ser vazio.");

            if (value.Length < 2 || value.Length > 100)
                throw new ArgumentException("Bairro deve ter entre 2 e 100 caracteres.");

            Value = value;
        }

        public override string ToString() => Value;
    }
}
