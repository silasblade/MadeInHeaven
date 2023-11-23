using System.ComponentModel.DataAnnotations;

namespace MadeinHeavenBookStore.Models.OrderDetail
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string IdUser { get; set; }
        public string street { get; set; }
        public string wards { get; set; }
        public string district { get; set; }
        public string city { get; set; }
		public int shipmethodprice { get; set; }

        public int discount { get; set; }
		public string telephonenumber { get; set; }
        public DateTime DateTime { get; set; }
        public int TotalCost { get; set; }


    }
}
