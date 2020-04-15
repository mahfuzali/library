using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.Common.Responses
{
    public class AuthSuccessResponse
    {
        public string Token { get; set; }

        public string Expiration { get; set; }
    }
}
