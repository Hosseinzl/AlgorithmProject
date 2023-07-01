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

        public void Util(int[] input)
        {
            while (input.Length > 0)
            {
                if (AreAllValuesSame(input))
                {
                    score += input.Length * input.Length;
                    return;
                }

                var dictionay = initialInputDetail(input);
                var mostRepeatedNumber = dictionay.Last().Key;

                int firstIndex = Array.IndexOf(input, mostRepeatedNumber);
                int lastIndex = Array.LastIndexOf(input, mostRepeatedNumber);

                var subArray = GetSubarray(input, firstIndex, lastIndex);

                if (firstIndex == 0 && lastIndex + 1 == input.Length)
                {
                    List<int> currentSubarray = new();
                    bool insideSubarray = false;
                    int i = 0;
                    foreach (int num in input)
                    {
                        if (num == mostRepeatedNumber)
                        {
                            if (insideSubarray)
                            {
                                Util(currentSubarray.ToArray());
                                currentSubarray = new();
                            }
                            else
                            {
                                insideSubarray = true;
                            }
                        }
                        else if (insideSubarray)
                        {
                            currentSubarray.Add(num);
                            input = DeleteSubarray(input, i, i).Item1;
                            i--;
                        }
                        i++;
                    }
                }
                else
                {
                    Util(subArray);
                    var result = DeleteSubarray(input, firstIndex, lastIndex);
                    input = result.Item1;
                }
            }
        }

         
        public Dictionary<int, int> initialInputDetail(int[] input)
        {
            Dictionary<int, int> inputDetail = new();

            foreach (var number in input)
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

        static (int[],int) DeleteSubarray(int[] array, int firstIndex, int lastIndex)
        {
            if (firstIndex < 0 || firstIndex >= array.Length || lastIndex < 0 || lastIndex >= array.Length || firstIndex > lastIndex)
            {
                throw new ArgumentException("Invalid indices provided.");
            }

            List<int> newList = new List<int>(array);
            newList.RemoveRange(firstIndex, lastIndex - firstIndex + 1);

            return (newList.ToArray(),lastIndex - firstIndex + 1);
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
            Util(_input);
            Console.WriteLine(score);
        }

    }
}
