using System;

namespace ConnectUs.Entity.Model
{
    public class MailModel
    {
        public string Email { get; set; }
        public string Code { get; set; }

        public MailModel() { }

        public MailModel(string email, string code)
        {
            Email = email;
            Code = code;
        }

        // Builder Deseni
        public class Builder
        {
            private string _email;
            private string _code;

            public Builder WithEmail(string email)
            {
                try
                {
                    var mailAddress = new System.Net.Mail.MailAddress(email);
                    _email = mailAddress.Address;
                }
                catch
                {
                    throw new ArgumentException("Invalid email format.");
                }
                return this;
            }

            public Builder WithCode(string code)
            {
                _code = code;
                return this;
            }

            public MailModel Build()
            {
                if (string.IsNullOrWhiteSpace(_email))
                {
                    throw new ArgumentException("Email cannot be null or empty.");
                }
                if (string.IsNullOrWhiteSpace(_code))
                {
                    throw new ArgumentException("Code cannot be null or empty.");
                }
                return new MailModel(_email, _code);
            }
        }

        public override string ToString()
        {
            return $"Email: {Email}, Code: {Code}";
        }
    }
}
