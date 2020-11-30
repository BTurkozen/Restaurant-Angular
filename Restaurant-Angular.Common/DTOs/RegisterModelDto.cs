using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Angular.Common.DTOs
{
   public class RegisterModelDto
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
