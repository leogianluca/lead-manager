using System;

namespace LeadManager.Domain.ValueObjects
{
    public class PersonName
    {
        public string Value { get; private set; }

        public PersonName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Nome não pode ser vazio.");

            if (value.Length < 2 || value.Length > 50)
                throw new ArgumentException("Nome deve ter entre 2 e 50 caracteres.");

            foreach (char c in value)
            {
                if (!char.IsLetter(c) && c != ' ')
                    throw new ArgumentException("Nome contém caracteres inválidos.");
            }

            Value = value;
        }

        public override string ToString() => Value;
    }
}
