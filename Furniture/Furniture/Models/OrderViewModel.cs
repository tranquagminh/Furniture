using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Furniture.Models
{
    public class OrderViewModel
    {
        [Required(ErrorMessage = "Customer's name must fill")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Customer's phone must fill")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Customer's address must fill")]
        public string Addres { get; set; }
        [Required(ErrorMessage = "Customer's email must fill")]
        public string Email { get; set; }
    }
}