namespace InternetCoast.Infrastructure.UI
{
    public class UserProfile
    {
        public int UserId { get; set; }

        public int? PantherId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string[] Roles { get; set; }

        public string[] AccessRights { get; set; }

        public string FullName
        {
            get { return string.Join(" ", FirstName, MiddleName != null ? MiddleName[0].ToString() : "", LastName); }
        }
    }
}
