using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanLaptop.Models;

namespace WebBanLaptop.ViewModel
{
    public class ProductViewModel
    {
        public product product;
        public List<category> categories;
        public List<product> listProduct;
        public ProductViewModel(List<category> listCate, product p = null)
        {
            this.product = p;
            this.categories = listCate;
        }
        public ProductViewModel() { }
        public ProductViewModel(List<product> list, List<category> listCate) { this.listProduct = list; this.categories = listCate; }
    }
}