using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ConApp.Services
{
    public class RandomStringGenerator
    {

        #region private_readonly_felds
        private readonly int _stringLength;
        private readonly IEnumerable<int> _enumerableRange;
        private readonly ParallelQuery<int> _parallelQuery;
        private readonly Random _rnd;
        private readonly StringBuilder _stringBuilder;
        #endregion

        #region static_fields

        private static StringBuilder _staticBuilder = new StringBuilder();
        private static readonly Random StaticRnd = new Random();
        #endregion
        public RandomStringGenerator(int stringLength)
        {
            _stringLength = stringLength;
            _stringBuilder = new StringBuilder(stringLength);
            _enumerableRange = Enumerable.Range(0, stringLength);
            _parallelQuery = ParallelEnumerable.Range(0, stringLength);
            _rnd = new Random();
        }

        public string Generate()
        {
            foreach (var i in _enumerableRange)
            {
                _stringBuilder.Append((char)_rnd.Next(127 + 1));
            }

            var result = _stringBuilder.ToString();

            _stringBuilder.Clear();

            return result;
        }

        public string GenerateParallel()
        {
            Parallel.ForEach(_parallelQuery, (item) =>
            {
                _stringBuilder.Append((char)StaticRnd.Next(127 + 1));
            });

            var result = _stringBuilder.ToString();

            _stringBuilder.Clear();

            return result;
        }

        public static string Generate(int stringLen)
        {
            var enumRange = Enumerable.Range(0, stringLen);


            foreach (var unused in enumRange)
            {
                _staticBuilder.Append((char)StaticRnd.Next(127 + 1));
            }

            var result = _staticBuilder.ToString();
            _staticBuilder.Clear();

            return result;
        }

        public static string GenerateParallel(int stringLen)
        {
            _staticBuilder = new StringBuilder(stringLen);

            var plEnum = ParallelEnumerable
            .Range(0, stringLen);

            Parallel.ForEach(plEnum, (item) => { _staticBuilder.Append((char)StaticRnd.Next(127 + 1)); });

            var result = _staticBuilder.ToString();
            _staticBuilder.Clear();

            return result;
        }

    }
}
