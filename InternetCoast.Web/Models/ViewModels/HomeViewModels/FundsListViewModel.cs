using System.Collections.Generic;
using InternetCoast.Model.Entities;

namespace InternetCoast.Web.Models.ViewModels.HomeViewModels
{
    public class FundsListViewModel
    {
        public List<Fund> SbirSttr { get; set; }
        public List<Fund> Grants { get; set; }
        public List<Fund> CompetitivePrograms { get; set; }

        public FundsListViewModel()
        {
            SbirSttr = new List<Fund>();
            Grants = new List<Fund>();
            CompetitivePrograms = new List<Fund>();
        }
    }
}