using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    public class AuthInfoDto
    {
        public String AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        //public String TokenType { get; set; }
        //public String RefreshToken { get; set; }
        //public String Scope { get; set; }
    }
}
