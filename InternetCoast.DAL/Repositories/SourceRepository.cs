using System.Linq;
using InternetCoast.Infrastructure.Data.EF;
using InternetCoast.Model.Context;
using InternetCoast.Model.Entities;

namespace InternetCoast.DAL.Repositories
{
    public class SourceRepository : GenericRepository<User>
    {
        private readonly AppDbContext _context;

        public SourceRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public IQueryable<User> GetByFilter(string filter, int? take, int? skip = 0)
        {
            IQueryable<User> query = _context.User.OrderBy(e => e.FirstName).ThenBy(e => e.LastName).ThenBy(e => e.Email);

            if (!string.IsNullOrEmpty(filter))
                query = query.Where(e => e.FirstName.Contains(filter)
                                         || e.FirstName.Contains(filter)
                                         || e.LastName.Contains(filter)
                                         || e.MiddleName.Contains(filter)
                                         || e.Email.Contains(filter));

            if (skip.HasValue && skip.Value > 0)
                query = query.Skip(skip.Value);

            if (take.HasValue && take.Value > 0)
                query = query.Take(take.Value);

            return query;
        }

        public User GetUserByEmail(string email)
        {
            return _context.User.SingleOrDefault(e => e.Email.Equals(email));
        }
    }
}
