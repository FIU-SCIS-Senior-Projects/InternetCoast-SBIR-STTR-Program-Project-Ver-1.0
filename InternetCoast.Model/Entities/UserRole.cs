using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InternetCoast.Infrastructure.Data.EF.Entities.TrackingInformation;

namespace InternetCoast.Model.Entities
{
    public class UserRole : ITrackableEntity
    {
        [Key]
        public int UserRoleId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { set; get; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        #region Tracking
        public bool Active { get; set; }

        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }

        public DateTime? DateLastModified { get; set; }
        public int? LastModifiedBy { get; set; }
        #endregion
    }
}
