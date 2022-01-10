using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ServiceLayer.Extenstions
{
    public static class MathExtensions
    {
        public static double Median(this IEnumerable<double> source)
        {
            var sortedSourceValues = source.OrderBy(x => x);

            int midpoint = sortedSourceValues.Count() / 2;
            if (sortedSourceValues.Count() % 2 == 0)
                return (sortedSourceValues.ElementAt(midpoint - 1) + sortedSourceValues.ElementAt(midpoint)) / 2.0;
            else
                return sortedSourceValues.ElementAt(midpoint);
        }
    }
}
