using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bede.Go.Host.Models
{
    public class ApiError
    {
        public string Error { get; set; }

        public object Data { get; set; }
    }
}