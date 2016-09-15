using System;
using System.ComponentModel.DataAnnotations;
using InternetCoast.Infrastructure.Data.EF.Entities.TrackingInformation;

namespace InternetCoast.Model.Entities
{
    public class Role : ITrackableEntity
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        [StringLength(20)]
        public string RoleName { get; set; }

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
