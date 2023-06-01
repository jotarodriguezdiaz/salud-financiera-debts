using Debts.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Debts.Domain
{
    public class Simulation : BaseDomainModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SimulationId { get; set; }        

        public decimal Import { get; set; }
        public decimal TypeInterest { get; set; }
        public int Time { get; set; }

        public decimal AnnualProfitability { get; set; }
        public decimal Taxes { get; set; }
        public decimal MonthlySaving { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}