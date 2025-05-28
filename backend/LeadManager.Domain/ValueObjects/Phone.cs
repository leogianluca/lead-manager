using System;
using System.Text.RegularExpressions;

namespace LeadManager.Domain.ValueObjects
{
    public class Phone
    {
        private static readonly Regex PhoneRegex = new(@"^\+?[\d\s\-\(\)]{8,15}$", RegexOptions.Compiled);

        public string Number { get; private set; }

        public Phone(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("Telefone inválido.");

            var cleaned = number.Trim();

            if (!PhoneRegex.IsMatch(cleaned))
                throw new ArgumentException("Formato de telefone inválido.");

            Number = cleaned;
        }

        public override string ToString() => Number;
    }
}
