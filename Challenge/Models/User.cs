namespace Challenge.Models
{
    public class User
    {
        public class LoginSet : IEquatable<LoginSet>
        {
            public string User { get; set; }
            public string Pass { get; set; }

            public bool Equals(LoginSet? other)
            {
                if(other == null)
                {
                    return false;
                }
                else if(this.User != other.User)
                {
                    return false;
                }
                else if(this.Pass != other.Pass)
                {
                    return false;
                }

                return true;
            }
        }

        public class LoginResult : Interfaces.IResult
        {
            public string Token { get; set; }
            public int IdResponse { get; set; }
            public string Response { get; set; }
        }
    }
}
