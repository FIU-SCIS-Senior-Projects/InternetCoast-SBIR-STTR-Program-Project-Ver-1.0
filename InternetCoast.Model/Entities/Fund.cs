using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InternetCoast.Infrastructure.Data.EF.Entities.TrackingInformation;

namespace InternetCoast.Model.Entities
{
    public class Fund : ITrackableEntity
    {
        [Key]
        public int FundId { get; set; }

        [Required]
        public string FundTitle { get; set; }

        [Required]
        public string FundTopic { get; set; }

        public string Solicitation { get; set; }

        public string Description { get; set; }

        public string KeyWords { get; set; }

        public string Elegibility { get; set; }

        [DataType(DataType.Url)]
        public string Url { get; set; }

        public virtual ICollection<Source> Sources { get; set; }

        public virtual ICollection<Agency> Agencies { get; set; }

        public DateTime? OpenDate { get; set; }

        public DateTime? DeadLine { get; set; }

        public string Awards { get; set; }

        public string Remarks { get; set; }

        //public Fund()
        //{
        //    Sources = new List<Source>();
        //    Agencies = new List<Agency>();
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
