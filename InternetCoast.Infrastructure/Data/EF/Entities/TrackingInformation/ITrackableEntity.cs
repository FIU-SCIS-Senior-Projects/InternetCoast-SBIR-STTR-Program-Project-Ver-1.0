using System;

namespace InternetCoast.Infrastructure.Data.EF.Entities.TrackingInformation
{
    public interface ITrackableEntity
    {
        //string CreatedFrom { get; set; }
        //string ModifiedFrom { get; set; }
        
        bool Active { get; set; }
        
        DateTime DateCreated { get; set; }
        int CreatedBy { get; set; }
        
        DateTime? DateLastModified { get; set; }
        int? LastModifiedBy { get; set; }
    }
}
