/*********************************************************************
* floor(lg lg (n))
* CS 212A
* Chan Kim, 9/22/2017
*
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Lg
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Lg(Lg(n)) finder!");
            while (true)
            {
                Console.Write("\nEnter N: ");
                double n = double.Parse(Console.ReadLine());
                double lg = (Lg(n));
                double lglg = (Lg(Lg(n)));
                if (lg < 0) 
                    // If the lg becomes a negative number, lg lg(n) is going to be invalid. For example, if we use n = 0.5, lg n is going to be -2, so the lg lg n becomes lg -2 which is invalid.
                    Console.WriteLine("The value is invalid!"); // ex) lg(0.5) = -2, lg (-2) = invalid
                else if (lglg < 0) 
                    // Although the previous lg does not make invalid value, it still can make an invalid value. For instance, if n = 1, the previous lg n = 0, and it is invalid value.
                    Console.WriteLine("The value is invalid!"); // ex) lg(1) = 0, lg (0) = invalid
                else
                    Console.WriteLine("lg(lg(({0})) = {1}.", n, lglg);
            }
        }

        static double Lg(double n)
        {
        double result = 0;
            // Since we compute lg lg n, we do not need to make out put negative result due to it makes an invalid result. So, use -1 to determine invalid value.
            if (n <= 0)
             return -1;
        // if 0.5 < n < 2, the value is going to be 0 < # < 1.
        else if ((n < 2) & (n > 0.5))
             return 0;
        else 
            // if 0 < n <= 0.5, the output value is going to be a negative number.
            while ((n <= 0.5) & (n > 0)) 
                {
                    n = n * 2;
                    result = result - 1;
                }
            // if n >= 2, the output value is going to be a positive number.
            while (n >= 2)
                {
                    n = n / 2;
                    result = result + 1;
                }
        return result;
        }
    }
}

