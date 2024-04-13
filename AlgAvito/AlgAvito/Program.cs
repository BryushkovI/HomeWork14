using System;
using System.Collections.Generic;

namespace AlgAvito
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] array = { { 1, 3 }, { 100, 200 }, { 2, 4 } };

        }
        void AlgAvito1()
        {
            int[] nums = new[] { 4, 5, 2, 1, 3, 7, 23, 0, 11, 12, 24 };
            Array.Sort(nums);
            int start_group = nums[0];
            string result = string.Empty;
            for (int i = 0; i < nums.Length; i++)
            {
                if (i == nums.Length - 1)
                {
                    if (start_group == nums[i])
                    {
                        result += ',' + start_group.ToString();
                    }
                    else
                    {
                        result += ',' + start_group.ToString() + '-' + nums[i].ToString();
                    }
                }
                else
                {
                    if (nums[i + 1] - 1 == nums[i]) continue;
                    if (start_group == nums[i])
                    {
                        result += ',' + start_group.ToString();
                        start_group = nums[i + 1];
                    }
                    else
                    {
                        result += ',' + start_group.ToString() + '-' + nums[i].ToString();
                        start_group = nums[i + 1];
                    }
                }
            }
            result = result.Trim(',');
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
