using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_and_Launch.Interfaces
{
    public interface IValidator
    {
        bool CompareWithRequest(string request, object data);
        bool CheckWithSettings(object data);
        bool Validate(string request, object data);
    }
}
