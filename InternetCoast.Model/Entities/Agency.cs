using System;
using System.ComponentModel.DataAnnotations;
using InternetCoast.Infrastructure.Data.EF.Entities.TrackingInformation;

namespace InternetCoast.Model.Entities
{
    public class Agency : ITrackableEntity
    {
        [Key]
        public int AgencyId { get; set; }

        [Required]
        [StringLength(20)]
        public string AgencyName { get; set; }

        [MaxLength(10)]
        public string Acronym { get; set; }

        public string Remarks { get; set; }

        #region Tracking
        public bool Active { get; set; }
        
        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }
        
        public DateTime? DateLastModified { get; set; }
        public int? LastModifiedBy { get; set; }
        #endregion
    }
}
