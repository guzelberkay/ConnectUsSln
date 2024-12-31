using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            public Builder Email(string email)
            {
                _email = email;
                return this;
            }

            public Builder Code(string code)
            {
                _code = code;
                return this;
            }

            public MailModel Build()
            {
                return new MailModel(_email, _code);
            }
        }
    }
}