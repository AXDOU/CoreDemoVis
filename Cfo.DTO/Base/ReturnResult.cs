
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cfo.DTO.Base
{
    public class ReturnResult
    {
        public int StatusCode { get; set; }

        public string ErrMsg { get; set; }

        public object Data { get; set; }

    }

    public class ReturnResult<T> where T : class
    {
        public int StatusCode { get; set; }

        public string ErrMsg { get; set; }

        public T data { get; set; }

    }

    public class ReturnData<T> where T : class
    {
        public int StatusCode { get; set; }

        public string ErrMsg { get; set; }

        public List<T> data { get; set; }

    }
}