using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanLaptop.Models;

namespace WebBanLaptop.ViewModel
{
    public class ProfileViewModel
    {
        public List<order> myOrder { get; set; }    
        public ProfileViewModel() { }
        public ProfileViewModel(List<order> myOrder)
        {
            this.myOrder = myOrder;
        }
    }
}