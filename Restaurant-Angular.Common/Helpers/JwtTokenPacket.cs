using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Angular.Common.Helpers
{
    public class JwtTokenPacket
    {
        public string Token { get; set; }
        public string Expiretion { get; set; }
        public string UserName { get; set; }
    }
}
