//
// ComplexMath.cs
//
// Author:
//       Nathan Graule <solarliner@gmail.com>
//
// Copyright (c) 2017 SolarLiner
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using SolarLiner.ComplexNumber;
using System;

namespace SolarLiner.ComplexNumber
{
    /// <summary>
    /// Handles math in the Complex plane.
    /// </summary>
    public static class ComplexMath
    {
        /// <summary>
        /// Returns e raised to the specified complex power.
        /// </summary>
        /// <param name="val">A complex number specifying a power.</param>
        static public Complex Exp(Complex val)
        {
            return Math.Exp(val.Real) *
            new Complex(Math.Cos(val.Imaginary), Math.Sin(val.Imaginary));
        }

        /// <summary>
        /// Returns the square root of the specified complex number.
        /// </summary>
        /// <param name="val">The complex number is to be found..</param>
        public static Complex Sqrt(Complex val)
        {
            if (Math.Abs(val.Real) < double.Epsilon && Math.Abs(val.Imaginary) < double.Epsilon)
                return new Complex(0, 0);

            double ar = Math.Abs(val.Real);
            double ai = Math.Abs(val.Imaginary);
            double temp, w;
            if (ar < ai)
            {
                temp = ar / ai;
                w = Math.Sqrt(ai) *
                Math.Sqrt(0.5d * (temp + Math.Sqrt(1d + (temp * temp))));
            }
            else
            {
                temp = ai / ar;
                w = Math.Sqrt(ar) *
                Math.Sqrt(0.5d * (1d + Math.Sqrt(1d + (temp * temp))));
            }

            if (val.Real > 0d)
            {
                return new Complex(w, val.Imaginary / (2d * w));
            }
            else
            {
                double r = (val.Imaginary >= 0d) ? w : -w;
                return new Complex(r, val.Imaginary / (2d * r));
            }
        }

        /// <summary>
        /// Returns a specified complex number to the specified power.
        /// </summary>
        /// <param name="val">The complex number to be elevated to a power.</param>
        /// <param name="pow">The power with which to elevate the complex number.</param>
        public static Complex Pow(Complex val, double pow)
        {
            double real = pow * Math.Log(val.R);
            double imag = pow * val.R;
            double scal = Math.Exp(real);
            return new Complex(scal * Math.Cos(imag), scal * Math.Sin(imag));
        }

        /// <summary>
        /// Returns a specified complex number to the specified complex power.
        /// </summary>
        /// <param name="val">The complex number to be elevated to a power.</param>
        /// <param name="pow">The complex power with which to elevate the complex number.</param>
        public static Complex Pow(Complex val, Complex pow)
        {
            if (pow.IsReal)
                return Pow(val, pow.Real);

            double real = Math.Log(val.R);
            double imag = val.R;
            double r2 = (real * pow.Real) - (imag * pow.Imaginary);
            double i2 = (real * pow.Imaginary) + (imag * pow.Real);
            double scal = Math.Exp(r2);

            return new Complex(scal, i2, true);
        }
    }
}
