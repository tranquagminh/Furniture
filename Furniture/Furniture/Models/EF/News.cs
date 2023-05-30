namespace Furniture.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
        public int id { get; set; }

        public string name { get; set; }

        [StringLength(100)]
        public string img { get; set; }

        public string description { get; set; }

        public string link { get; set; }

        [Column(TypeName = "ntext")]
        public string detail { get; set; }

        public string meta { get; set; }

        public bool? hide { get; set; }

        public int? order { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? datebegin { get; set; }
    }
}
