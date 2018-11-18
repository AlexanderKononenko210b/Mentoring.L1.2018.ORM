using System;
using System.ComponentModel.DataAnnotations;

namespace EF.Infrastructure.Models
{
    /// <summary>
    /// Represents a model <see cref="CreditCard"/> class.
    /// </summary>
    public class CreditCard
    {
        public int CreditCardID { get; set; }

        [StringLength(13)]
        public string CardNumber { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(25)]
        public string CardHolder { get; set; }

        public string Note { get; set; }

        [Required]
        public int EmployeeID { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
