using SolarLiner.ComplexNumber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLiner.ComplexNumber
{
    public static class ComplexMath
    {
        public static Complex Pow(Complex val, int pow)
        {
            if (pow < 0) return 1.0 / Pow(val, -pow);
            else if (pow == 0) return 1.0;

            Complex res = val;
            for(int i=1; i<pow; i++)
            {
                res *= val;
            }

            return res;
        }

        static public Complex Exp(Complex val)
        {
            return Math.Exp(val.Real) *
                new Complex(Math.Cos(val.Imaginary), Math.Sin(val.Imaginary));
        }

        public static Complex Sqrt(Complex val)
        {
            if(val.Real == 0 && val.Imaginary == 0) return new Complex(0, 0);

            double ar = Math.Abs(val.Real);
            double ai = Math.Abs(val.Imaginary);
            double temp, w;
            if(ar<ai)
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

        public static Complex Pow(Complex val, double pow)
        {
            double real = pow * Math.Log(val.R);
            double imag = pow * val.R;
            double scal = Math.Exp(real);

            return new Complex(scal * Math.Cos(imag), scal * Math.Sin(imag));
        }
        public static Complex Pow(Complex val, Complex pow)
        {
            if (pow.IsReal) return Pow(val, pow.Real);

            double real = Math.Log(val.R);
            double imag = val.R;
            double r2 = (real * pow.Real) - (imag * pow.Imaginary);
            double i2 = (real * pow.Imaginary) + (imag * pow.Real);
            double scal = Math.Exp(r2);

            return new Complex(scal, i2, true);
        }
    }
}
