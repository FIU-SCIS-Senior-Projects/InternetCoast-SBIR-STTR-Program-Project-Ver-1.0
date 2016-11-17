using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InternetCoast.Infrastructure.Data.EF.Entities.TrackingInformation;

namespace InternetCoast.Model.Entities
{
    public class Source : ITrackableEntity
    {
        [Key]
        public int SourceId { get; set; }

        [Required]
        [StringLength(100)]
        public string SourceName { get; set; }

        public string Remarks { get; set; }

        public virtual ICollection<Fund> Funds { get; set; }

        //public Source()
        //{
        //    Funds = new List<Fund>();
        //}

        #region Tracking
        public bool Active { get; set; }

        public DateTime DateCreated { get; set; }
        public int CreatedBy { get; set; }

        public DateTime? DateLastModified { get; set; }
        public int? LastModifiedBy { get; set; }
        #endregion
    }
}
