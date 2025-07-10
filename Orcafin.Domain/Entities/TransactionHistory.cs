using Orcafin.Domain.Enums;
using System;

namespace Orcafin.Domain.Entities
{
    public class TransactionHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } // Propriedade de navegação
        public string Identifier { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } // Propriedade de navegação
        public DateTime TransactionAt { get; set; }
    }
}