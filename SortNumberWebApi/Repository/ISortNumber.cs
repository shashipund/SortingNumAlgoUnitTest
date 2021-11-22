using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApi.Repository
{
    public interface ISortNumber
    {
        public string[] SortNumber(int[] numberList);
    }
}
