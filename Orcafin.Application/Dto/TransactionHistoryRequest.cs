using Orcafin.Domain.Enums;
using System;

namespace Orcafin.Application.Dto
{
    public class TransactionHistoryRequest
    {
        public int UserId { get; set; }
        public string Identifier { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public int CategoryId { get; set; }
        public DateTime TransactionAt { get; set; }
    }
}