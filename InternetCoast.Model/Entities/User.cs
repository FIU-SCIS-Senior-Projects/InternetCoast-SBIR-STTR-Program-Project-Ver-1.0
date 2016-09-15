using System;
using System.ComponentModel.DataAnnotations;
using InternetCoast.Infrastructure.Data.EF.Entities.TrackingInformation;

namespace InternetCoast.Model.Entities
{
    public class User : ITrackableEntity
    {
        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; }

        public int? PantherId { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

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
