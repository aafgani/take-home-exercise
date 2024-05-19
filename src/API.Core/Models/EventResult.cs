using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core.Models
{
    public class EventResult<T>
    {
        public EventResult()
        {
            Error = new List<string>();
        }

        public EventResult(List<string> error)
        {
            Error = error;
        }

        public bool IsError
        {
            get
            {
                return (Error == null || Error.Count == 0) ? false : true;
            }
        }
        public T Result { get; set; }
        public List<string> Error { get; set; }
        public static EventResult<T> WithErrors(List<string> errors) => new EventResult<T>(errors);
    }
}
