using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPTest.Domain.Models
{
    public class Cache<T>
    {
        public string Key { get; set; }
        public T Value { get; set; }
    }
}
