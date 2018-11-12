using Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider
{
    public class StockSina : IDomainModel
    {
        [Key]
        public long Id { get; set; }
    }
}
