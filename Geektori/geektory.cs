using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geektori
{

    public class geektory
    {
        private readonly int[] _input;
        private int score;
        //private Dictionary<int, int> inputDetail = new();
        public geektory(int[] input)
        {
            _input = input;
        }

        public void geektoryUtil(int[] input)
        {

            if (AreAllValuesSame(input))
            {
                score = input.Length * input.Length;
            }

            while (input.Length > 0)
            {
                var dictionay = initialInputDetail(input);
                var mostRepeatedNumber = dictionay.Last().Key;

                var firstIndex = Array.IndexOf(input, mostRepeatedNumber);
                int nextIndex;
                for(int i = firstIndex; i < input.Length; ++i)
                {
                    if (input[i] == mostRepeatedNumber)
                    {

                        var subarray = GetSubarray(input, firstIndex, i);

                        if (subarray.Length > 0)
                        {
                            geektoryUtil(subarray);
                            DeleteSubarray(input, firstIndex, i);
                        }

                        firstIndex = i + 1;

                    } 
                }
                
            }
        }
         
        public Dictionary<int, int> initialInputDetail(int[] input)
        {
            Dictionary<int, int> inputDetail = new();

            foreach (var number in _input)
            {
                if (inputDetail.ContainsKey(number))
                {
                    inputDetail[number]++;
                }
                else
                {
                    inputDetail.Add(number, 1);
                }
            }

            return sortDictionary(inputDetail);
        }

        public Dictionary<int, int> sortDictionary(Dictionary<int, int> input)
        {
            List<KeyValuePair<int, int>> sortedList = input.ToList();
            sortedList.Sort((x, y) => x.Value.CompareTo(y.Value));

            return sortedList.ToDictionary(x => x.Key, x => x.Value);
        }

        static bool AreAllValuesSame(int[] array)
        {
            if (array.Length == 0)
            {
                return true; // Empty array is considered to have all values the same
            }

            int firstElement = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] != firstElement)
                {
                    return false;
                }
            }

            return true;
        }

        static int[] DeleteSubarray(int[] array, int firstIndex, int lastIndex)
        {
            if (firstIndex < 0 || firstIndex >= array.Length || lastIndex < 0 || lastIndex >= array.Length || firstIndex > lastIndex)
            {
                throw new ArgumentException("Invalid indices provided.");
            }

            List<int> newList = new List<int>(array);
            newList.RemoveRange(firstIndex, lastIndex - firstIndex + 1);

            return newList.ToArray();
        }

        static int[] GetSubarray(int[] array, int firstIndex, int lastIndex)
        {
            if (firstIndex < 0 || firstIndex >= array.Length || lastIndex < 0 || lastIndex >= array.Length || firstIndex > lastIndex)
            {
                throw new ArgumentException("Invalid indices provided.");
            }

            int length = lastIndex - firstIndex + 1;
            return new ArraySegment<int>(array, firstIndex, length).ToArray();
        }

        public void main()
        {
            geektoryUtil(_input);
        }

    }
}
