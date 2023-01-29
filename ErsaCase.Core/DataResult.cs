using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ErsaCase.Core
{
    public class DataResult<T>
    {
        public bool Succeeded { get; set; }
        public string ErrorCode { get; set; }
        public int? ErrorCodeInt { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string? ErrorDefination { get; set; }
        public string? Message { get; set; }
        public string? Log { get; set; }
        public T? Data { get; set; }

        public DataResult()
        {
            Succeeded = false;
        }

        public DataResult(T? data)
        {
            Succeeded = true;
            Data = data;
        }

        public DataResult(bool succeeded, T? data)
        {
            Succeeded = succeeded;
            Data = data;
        }

        public DataResult(string errorDefination)
        {
            Succeeded = false;
            ErrorDefination = errorDefination;
        }
        //public DataResult(string errorDefination, T? data)
        //{
        //    Succeeded = false;
        //    Data = data;
        //    ErrorDefination = errorDefination;
        //}

        public DataResult(string errorCode, string errorDefination)
        {
            Succeeded = false;
            ErrorCode = errorCode;
            ErrorDefination = errorDefination;
        }

        public DataResult(bool succeeded, T? data, string errorCode, string errorDefination)
        {
            if (!string.IsNullOrEmpty(errorCode) || !string.IsNullOrEmpty(errorDefination))
            {
                Succeeded = false;
                ErrorCode = errorCode;
                ErrorDefination = errorDefination;
                Data = data;
            }
            else
            {
                Succeeded = true;
                Data = data;
            }
        }

        public DataResult(bool succeeded, T? data, int errorCode, string errorDefination)
        {
            Succeeded = false;
            ErrorCodeInt = errorCode;
            ErrorDefination = errorDefination;
            Data = data;
        }


        //for resullt builder
        public DataResult(HttpStatusCode HttpStatusCode, string message)
        {
            StatusCode = HttpStatusCode;
            Message = message;
        }

        public DataResult(HttpStatusCode statusCode, T? data)
        {
            StatusCode = statusCode;
            Data = data;
        }
    }
}
