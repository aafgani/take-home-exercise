using System.Collections.Generic;
using System.Linq;

namespace API.Core.Models
{
    public class BasePaged
    {
        public int Offset { get; set; }

        public int Total { get; set; }
    }

    public class PagedResult<T> : BasePaged
    {
        public int Count => Data.Count();

        public IEnumerable<T> Data { get; set; }

        public PagedResult() { }

        public PagedResult(int offset, int total, IEnumerable<T> results)
        {
            Offset = offset;
            Total = total;
            Data = results ?? new List<T>();
        }
    }
}
