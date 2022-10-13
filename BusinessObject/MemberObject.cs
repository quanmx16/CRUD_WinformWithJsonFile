namespace BusinessObject
{
    public class MemberObject
    {
        //MemberID, MemberName, Email, Password, City, Country
        public int MemberID { get; set; }
        public string MemberName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public MemberObject(int memberID, string memberName, string email, string password, string city, string country)
        {
            MemberID = memberID;
            MemberName = memberName;
            Email = email;
            Password = password;
            City = city;
            Country = country;
        }

        public MemberObject()
        {
        }

    }
}