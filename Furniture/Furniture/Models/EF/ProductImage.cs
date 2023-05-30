namespace Furniture.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductImage
    {
        public int id { get; set; }

        public int? productID { get; set; }

        [StringLength(1000)]
        public string img { get; set; }

        public bool? Avatar { get; set; }

        public virtual Product Product { get; set; }
    }
}
