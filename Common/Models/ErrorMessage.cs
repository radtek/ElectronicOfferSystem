﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class ErrorMessage
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }

        public ErrorMessage()
        {
            
        }

    }
}
