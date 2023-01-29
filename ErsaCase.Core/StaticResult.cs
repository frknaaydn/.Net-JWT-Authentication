using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ErsaCase.Core
{
    public class StaticResult
    {
        public bool Succeeded { get; set; } = false;
        public string ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public StaticResult(bool succeeded, string error, HttpStatusCode statusCode)
        {
            Succeeded = succeeded;
            ErrorMessage = error;
            StatusCode = statusCode;
        }

        // hadi bos gondermek istedigim de olsun
        public static StaticResult Fail()
        {
            return new StaticResult(false, "", HttpStatusCode.BadRequest);
        }

        public static StaticResult Fail(string error)
        {
            return new StaticResult(false, error, HttpStatusCode.BadRequest);
        }

        public static StaticResult Ok()
        {
            return new StaticResult(true, "", HttpStatusCode.OK);
        }

        public static StaticResult NoContent()
        {
            return new StaticResult(true, "", HttpStatusCode.NoContent);
        }

        public static StaticResult Accepted(string error)
        {
            return new StaticResult(true, error, HttpStatusCode.Accepted);
        }
    }


    //where T : class // ben classs demek istiyorum ama tuple nesnesinde problem cikariyor
    //public class StaticResult<T1,T2> : Tuple<Mattress, object> // bu zoum olabilir
    public class StaticResult<T>
    {
        public bool Succeeded { get; set; } = false;
        public string ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public T Data { get; }

        public StaticResult(T data, bool succeeded, string error, HttpStatusCode statusCode)
        {
            Succeeded = succeeded;
            ErrorMessage = error;
            Data = data;
            StatusCode = statusCode;
        }

        // hadi tip girdigi halde bos giden de olsun
        public static StaticResult<T> Fail()
        {
            return new StaticResult<T>(default(T), false, "", HttpStatusCode.BadRequest);
        }

        public static StaticResult<T> Fail(string error)
        {
            return new StaticResult<T>(default(T), false, error, HttpStatusCode.BadRequest);
        }

        // hadi tip girdigi halde bos giden de olsun
        public static StaticResult<T> Ok()
        {
            return new StaticResult<T>(default(T), true, "", HttpStatusCode.OK);
        }

        public static StaticResult<T> Ok(T data)
        {
            return new StaticResult<T>(data, true, "", HttpStatusCode.OK);
        }

        // nocontent ile data gondermedim zaten IActionResult da dondurmuyor
        // ama getAll da getirmeye calisip veri bulamadiginda hata vermesin yok desin
        public static StaticResult<T> NoContent()
        {
            return new StaticResult<T>(default(T), true, "", HttpStatusCode.NoContent);
        }
    }

}
