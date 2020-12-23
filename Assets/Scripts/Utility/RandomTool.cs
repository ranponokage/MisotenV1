using UnityEngine;

namespace Utility
{
    public class RandomTool
    {
        public static int[] GetIndexRandomNum(int minValue, int maxValue)
        {
            System.Random random = new System.Random();
            int sum = Mathf.Abs(maxValue - minValue);//计算数组范围
            int site = sum;//设置索引范围
            int[] index = new int[sum];
            int[] result = new int[sum];
            int temp = 0;
            for (int i = minValue; i < maxValue; i++)
            {
                index[temp] = i;
                temp++;
            }
            for (int i = 0; i < sum; i++)
            {
                int id = random.Next(0, site - 1);
                result[i] = index[id];
                index[id] = index[site - 1];//因id随机到的数已经获取到了，用最后的一个数来替换它
                site--;//缩小索引范围
            }
            return result;
        }
    }
}

