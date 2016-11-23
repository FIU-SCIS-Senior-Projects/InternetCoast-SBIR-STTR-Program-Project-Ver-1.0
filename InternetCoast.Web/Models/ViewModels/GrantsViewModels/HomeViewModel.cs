using System.Collections.Generic;
using InternetCoast.Model.Entities;

namespace InternetCoast.Web.Models.ViewModels.GrantsViewModels
{
    public class HomeViewModel
    {
        public List<Fund> Funds { get; set; }

        public Fund Fund { get; set; }

        public HomeViewModel()
        {
            Funds = new List<Fund>();
        }
    }
}