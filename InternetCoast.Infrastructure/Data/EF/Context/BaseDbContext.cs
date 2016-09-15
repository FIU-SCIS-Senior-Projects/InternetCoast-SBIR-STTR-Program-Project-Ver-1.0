using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using InternetCoast.Infrastructure.Data.EF.Entities.TrackingInformation;

namespace InternetCoast.Infrastructure.Data.EF.Context
{
    public class BaseDbContext : DbContext
    {
        private readonly UiContext _uiContext;

        public BaseDbContext(string connectionName, UiContext uiContext)
            : base(connectionName) { _uiContext = uiContext; }

        public override int SaveChanges()
        {
            try
            {
                var manager = ((IObjectContextAdapter) this).ObjectContext;

                var date = DateTime.Now;

                foreach (
                    var entry in
                        manager.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified))
                {
                    var trackable = entry.Entity as ITrackableEntity;

                    if (trackable == null) continue;

                    var currentUserId = _uiContext.UserId;

                    trackable.DateLastModified = date;
                    trackable.LastModifiedBy = currentUserId;

                    if (entry.State != EntityState.Added) continue;

                    trackable.DateCreated = date;
                    trackable.CreatedBy = currentUserId;
                    trackable.Active = true;
                }

                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            catch (DbUpdateException dbu)
            {
                var builder = new StringBuilder("A DbUpdateException was caught while saving changes. ");

                try
                {
                    foreach (var result in dbu.Entries)
                    {
                        builder.AppendFormat("Type: {0} was part of the problem. ", result.Entity.GetType().Name);
                    }
                }
                catch (Exception e)
                {
                    builder.Append("Error parsing DbUpdateException: " + e);
                }

                var message = builder.ToString();
                throw new DbEntityValidationException(message, dbu);
            }
        }
    }
}
