using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDiSUBD_WPF.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int IdCustomer { get; set; }
        public int IdWorkshop { get; set; }
        public int Number { get; set; }
        public int IdDetail { get; set; }
        public int IdCarPhoto { get; set; }
        public int IdMaster { get; set; }
        public int IdUser { get; set; }
        public int IdWorkType { get; set; }
        public int IdPaymentType { get; set; }
    }
}
