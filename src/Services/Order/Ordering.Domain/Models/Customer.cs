using Ordering.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Models
{
    public class Customer : Entity<CustomerId>
    {
        //public Customer(string name,  string email)
        //{
        //    Name = name;
        //    Email = email;
        //}

        public string Name { get; private set; } = default!;


        public string Email { get; private set; } = default!;

        public static Customer Create(CustomerId customerId,string name, string email)
        {
            ArgumentNullException.ThrowIfNull(email, nameof(email));
            ArgumentNullException.ThrowIfNull(name, nameof(name));

            return new Customer
            {
                Id = customerId,
                Name = name,
                Email = email
            };
        }



    }
}
