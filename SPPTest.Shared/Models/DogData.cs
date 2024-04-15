using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPTest.Shared.Models
{
    public class DogData
    {
        public string Message { get; set; }
        public string Status { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (DogData)obj;
            return Message == other.Message && Status == other.Status;
        }
      
        public override int GetHashCode()
        {
            return HashCode.Combine(Message, Status);
        }
    }

}
