using LeadManager.Domain.Enums;
using LeadManager.Domain.Events;
using LeadManager.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace LeadManager.Domain.Entities
{
    public class Lead
    {
        public Guid Id { get; private set; }
        public PersonName FirstName { get; private set; }
        public PersonName LastName { get; private set; }
        public Email Email { get; private set; }
        public Phone Phone { get; private set; }
        public Suburb Suburb { get; private set; }
        public CategoryEnum Category { get; private set; }
        public string Description { get; private set; }
        public Price Price { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public LeadStatusEnum Status { get; private set; }

        private Lead() { } // para EF Core

        public Lead(PersonName firstName, PersonName lastName, Email email, Phone phone, Suburb suburb, CategoryEnum category, string description, Price price)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Suburb = suburb;
            Category = category;
            Description = description;
            Price = price;
            CreatedAt = DateTime.UtcNow;
            Status = LeadStatusEnum.Invited;
        }

        public void Accept()
        {
            if (Status != LeadStatusEnum.Invited)
                throw new InvalidOperationException("Lead já está processado.");

            Status = LeadStatusEnum.Accepted;

            if (Price.Value > 500m)
                Price = new Price(Price.Value * 0.9m); // aplica 10% de desconto criando um novo Price
        }

        public void Reject()
        {
            if (Status != LeadStatusEnum.Invited)
                throw new InvalidOperationException("Lead já está processado.");

            Status = LeadStatusEnum.Rejected;
        }
    }
}
