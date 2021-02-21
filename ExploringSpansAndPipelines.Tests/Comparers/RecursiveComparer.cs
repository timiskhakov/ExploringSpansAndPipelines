using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ExploringSpansAndIOPipelines.Core.Tests.Comparers
{
    internal class RecursiveComparer : IComparer
    {
        private Stack<string> _path;
        private object _expected;
        private object _actual;

        public int Compare(object x, object y)
        {
            _path = new Stack<string>();
            var result = CompareInternal(x, y);
            if (result != 0)
            {
                Console.WriteLine("Difference at <root>{0}", string.Join("", _path.Reverse()));
                Console.WriteLine("Expected: {0}", _expected);
                Console.WriteLine("Actual:   {0}", _actual);
            }

            return result;
        }

        private int CompareInternal(object x, object y)
        {
            _expected = x;
            _actual = y;
            
            if (x == null || y == null)
            {
                return (x == null ? -1 : 0) + (y == null ? 1 : 0);
            }

            if (x.GetType() != y.GetType())
            {
                _expected = $"Type: {x.GetType().FullName}";
                _actual = $"Type: {y.GetType().FullName}";
                return string.CompareOrdinal(x.GetType().FullName, y.GetType().FullName);
            }

            if (ReferenceEquals(x, y))
            {
                return 0;
            }

            switch (x)
            {
                case IComparable xComparable:
                    return xComparable.CompareTo(y);
                case IEnumerable xEnumerable:
                    return CompareEnumerable(xEnumerable, (IEnumerable)y);
                default:
                    return CompareProperties(x, y);
            }
        }

        private int CompareEnumerable(IEnumerable x, IEnumerable y)
        {
            var index = 0;

            var xEnumerator = x.GetEnumerator();
            var yEnumerator = y.GetEnumerator();

            while (xEnumerator.MoveNext())
            {
                if (!yEnumerator.MoveNext())
                {
                    _actual = $"Length {index}";
                    while (xEnumerator.MoveNext())
                    {
                        index++;
                    }
                    _expected = $"Length {index + 1}";

                    return 1;
                }

                _path.Push($"[{index}]");

                var result = CompareInternal(xEnumerator.Current, yEnumerator.Current);
                if (result != 0)
                {
                    return result;
                }

                _path.Pop();

                index++;
            }

            var expectedIndex = index;
            while (yEnumerator.MoveNext())
            {
                index++;
            }

            if (expectedIndex == index)
            {
                return 0;
            }

            _expected = $"Length {expectedIndex}";
            _actual = $"Length {index}";

            return -1;
        }

        private int CompareProperties(object x, object y)
        {
            foreach (var property in x.GetType().GetProperties())
            {
                _path.Push($".{property.Name}");

                var result = CompareInternal(property.GetValue(x), property.GetValue(y));
                if (result != 0)
                {
                    return result;
                }

                _path.Pop();
            }

            return 0;
        }
    }
}