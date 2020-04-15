using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Application.Common.Models.Requests
{
    public class LoginModel
    {
        [JsonProperty("username")]
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [JsonProperty("password")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
