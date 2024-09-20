using System.Collections.Generic;

namespace YiJian.BodyParts
{
    public static class CommonUtil
    {
        static readonly Dictionary<char, int> dict = new Dictionary<char, int>
        {
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
            {'D', 500},
            {'M', 1000}
        };

        public static int RomanToInt(string s)
        {
            int sum = 0;
            for(int i = 0;i < s.Length;i++)
            {
                int currentValue = dict[s[i]];
                if(i == s.Length - 1 || dict[s[i + 1]] <= currentValue)
                    sum += currentValue;
                else
                    sum -= currentValue;
            }
            return sum;
        }
        
        public static string intToRoman(int num) {
            string res="";
            int[] key = {1000,900,500,400,100,90,50,40,10,9,5,4,1};
            string[] value = {"M","CM","D","CD","C","XC","L","XL","X","IX","V","IV","I"};
        
            for(int i=0; i<key.Length; i++){
                int count=num/key[i];
                for(int j=0;j<count;j++){
                    res+=value[i];
                }
                num=num%key[i];
            }
            return res;
        }
    }
}