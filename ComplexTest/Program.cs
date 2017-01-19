using System;
using SolarLiner.ComplexNumber;

namespace ComplexTest
{
	class Program
	{
		static void Main(string[] args)
		{
            Complex a = new Complex(3, 2);

            Complex Pow = ComplexMath.Pow(a, 3);

            Console.WriteLine("{0}^{1} = {2}", a, 3, Pow);
			Console.ReadKey();
		}
	}
}
