using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanLaptop.Models;

namespace WebBanLaptop.ViewModel
{
    public class HomeViewModel
    {
        public List<category> categoryList;

        public HomeViewModel() { }
        public HomeViewModel(List<category> list)
        {
            this.categoryList = list;
        }
    }
}