using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoreApi.Repository
{
    public class SortingService : ISortNumber
    {
        //ProductStoreEntities _prodentity = new ProductStoreEntities();
        public string[] SortNumber(int[] numberList)
        {
            int n = numberList.Length;
            for (int i = 0; i < n - 1; i++)
            for (int j = 0; j < n - i - 1; j++)
                if (numberList[j] > numberList[j + 1])
                {
                    int temp = numberList[j];
                    numberList[j] = numberList[j + 1];
                    numberList[j + 1] = temp;
                }
            string[] sortedNumbers = numberList.Select(x=>x.ToString()).ToArray();
            return sortedNumbers;
        }
    }
}