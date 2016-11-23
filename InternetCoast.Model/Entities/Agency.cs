using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetCoast.Model.Entities
{
    public class Agency
    {
        [Key]
        public int AgencyId { get; set; }

        [Required]
        [StringLength(100)]
        public string AgencyName { get; set; }

        [MaxLength(50)]
        public string Acronym { get; set; }

        public string Remarks { get; set; }

        public virtual ICollection<Fund> Funds { get; set; }

        //public Agency()
        //{
        //    Funds = new List<Fund>();
        //}
    }
}
