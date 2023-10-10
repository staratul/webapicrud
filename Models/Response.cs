using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapicrud.Models
{
    public class Response
    {
        public string ResponseCode { get; set; }

        public string ResponseMessage { get; set; }

        public Employee employee { get; set; }

        public List<Employee> lstEmployees { get; set; }
    }
}