namespace Furniture.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order_tb
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order_tb()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int id { get; set; }

        [StringLength(1000)]
        public string Code { get; set; }

        [StringLength(1000)]
        public string CustomerName { get; set; }

        [StringLength(100)]
        public string Phone { get; set; }

        [StringLength(1000)]
        public string Addres { get; set; }

        public double? TotalAmount { get; set; }

        public int? Quantity { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
