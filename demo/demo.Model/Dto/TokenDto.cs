using System;
using System.Collections.Generic;
using System.Text;

namespace demo.Model.Dto
{
    public class TokenDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
