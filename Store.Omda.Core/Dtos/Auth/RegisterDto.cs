﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Omda.Core.Dtos.Auth
{
    public class RegisterDto
    {
        [Required(ErrorMessage ="Email Is Required !!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Name Is Required !!")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Phone Number Is Required !!")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password Is Required !!")]
        public string Password { get; set; }
    }
}
