using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
   public class BackDate
    {
        public string DataContext { get; set; }
        public object ErrorCode { get; set; }
    }
   public class ErrorCodeclass {
       public string Code { get; set; }
       public string Message { get; set; }
   }
}
