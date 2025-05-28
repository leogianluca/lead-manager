using System;
using System.Text.RegularExpressions;

namespace LeadManager.Domain.ValueObjects
{
    public class Email
    {
        private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public string Address { get; private set; }

        public Email(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Email não pode ser vazio.");

            if (!EmailRegex.IsMatch(address))
                throw new ArgumentException("Email inválido.");

            Address = address;
        }

        public override string ToString() => Address;
    }
}
