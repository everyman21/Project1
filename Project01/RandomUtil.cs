/* Random Util
 * @Author: Andy Groenenberg
 * @Date: 2013-10-09
 * @Version:1.1 
 * Uses one static refence to a random class to generate and return
 * a reference to a random integer or double.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01
{
  /// <summary>
  /// RandomUtil creates one static reference to Random() allowing for truely random
  /// numbers to be returned without chance of duplication
  /// </summary>
        public class RandomUtil
        {
            // allows only 1 reference to class Random()
            private static Random random = new Random();
            
            // Constructor
            public RandomUtil()
            {
               
            }

            /// <summary>
            /// Returns a random number between the two parameters (min, max)
            /// </summary>
            /// <param name="min">lower boundry for random number</param>
            /// <param name="max">upper boundry for random number</param>
            /// <returns></returns>
            public static int IntWithRange(int min, int max)
            {
               return random.Next(min, max);

            }

            /// <summary>
            /// Returns a random double number within a specified range
            /// </summary>
            /// <param name="min">lower boundary</param>
            /// <param name="max">upper boundary</param>
            /// <returns>a random number of type double between the lower and upper boundary parameters</returns>
            public static double DoubleWithRange(int min,int max)
            {
               double randomNumber = min+random.NextDouble()*((max - min));
               
               return randomNumber;

            }
            
            /// <summary>
            /// Returns a random integer 
            /// see Random
            /// </summary>
            /// <returns>random number of type integer</returns>
            public static int IntRandom()
            {
                return random.Next();
            }

            /// <summary>
            /// Returns a random double 
            /// see Random
            /// </summary>
            /// <returns>random number of type double</returns>
            public static double DoubleRandom()
            {
                return (random.NextDouble()*RandomUtil.IntRandom());
            }

        }

    }

