using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureAppServiceEasyAuth.Models
{
    public class LoggedInUser
    {
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
    }
}
