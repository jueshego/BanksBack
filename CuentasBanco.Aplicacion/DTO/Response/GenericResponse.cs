using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuentasBanco.Aplicacion.DTO.Response
{
    public class GenericResponse<T> where T : class
    {
        public GenericResponse()
        {
            Date = DateTime.Now;
        }

        public GenericResponse(string message)
        {
            Succeeded = false;
            Message = message;
            Date = DateTime.Now;
        }

        public GenericResponse(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
            Date = DateTime.Now;
        }

        public bool Succeeded { get; set; }

        public string Message { get; set; }

        public List<string> Errors { get; set; }

        public T Data { get; set; }

        public DateTime Date { get; set; }
    }
}
