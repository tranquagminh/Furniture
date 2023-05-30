using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Furniture.Models
{
    public class ShoppingCart
    {
        public List<ShoppingCartItem> Items { get;set; }
        public ShoppingCart()
        {
            this.Items = new List<ShoppingCartItem>();
        }
        public void AddToCart(ShoppingCartItem item,int Quantity)
        {
            var checkExist = Items.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (checkExist != null)
            {
                checkExist.Quantity += Quantity;
                checkExist.TotalPrice = checkExist.Quantity*checkExist.Price;
            }
            else
            {
                Items.Add(item);    
            }
        }
        public void Remove (int id)
        {
                var checkExist = Items.FirstOrDefault(x => x.ProductId == id);
            if (checkExist != null) 
            {
                Items.Remove(checkExist);
            }
        }
        public void UpdateQuantity(int id, int quantity)
        {
            var checkExist = Items.FirstOrDefault(x => x.ProductId == id);
            if (checkExist != null)
            {
                checkExist.Quantity = quantity;
                checkExist.TotalPrice = checkExist.Price * checkExist.Quantity;
            }
        }
        public double getTotalPrice()
        {
            return Items.Sum(x => x.TotalPrice);
        }
        public double getTotalQuantity()
        {
            return Items.Sum(x => x.Quantity);
        }
        public void ClearCart()
        {
            Items.Clear();
        }

    }
    public class ShoppingCartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Meta { get; set; }
        public string ProductCategory { get; set; }
        public string ProductImage { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
    }
}