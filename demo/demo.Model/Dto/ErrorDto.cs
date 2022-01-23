using System;
using System.Collections.Generic;
using System.Text;

namespace demo.Model.Dto
{
    public class ErrorDto
    {
        public List<string> Errors { get; set; }
        public int StatusCode { get; set; }
        public ErrorDto()
        {
            Errors = new List<string>();
        }
    }
}
