using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InternetCoast.Model.Entities;

namespace InternetCoast.Web.Models.ViewModels.CompetitiveProgramsViewModels
{
    public class HomeViewModel
    {
        public List<Fund> Funds { get; set; }

        public Fund NewFund { get; set; }

        public HomeViewModel()
        {
            Funds = new List<Fund>();
        }
    }
}