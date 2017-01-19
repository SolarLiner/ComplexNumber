using System;
using System.Drawing;

namespace SolarLiner.ComplexNumber
{
    public struct Complex
    {
        public enum ComplexStyle
        {
            TRIGONOMETRIC,
            CARTESIAN,
			COMPONENT,
			PHASOR
        }

        private double a;
        private double b;

        public double Real { get => a; set => a = value; }
        public double Imaginary { get => b; set => a = value; }

		public double R => Math.Sqrt(a * a + b * b);
        public double Theta
        {
            get
            {
                if (a == 0)
                    if (b == 0) return 0;
                    else return Math.PI / 2;
                return Math.Atan2(b, a);
            }
        }
		

        public bool IsImaginary => a == 0;
        public bool IsReal => b == 0;

		public Complex Conjugate => new Complex(Real, -Imaginary);
        public Complex Normalized
        {
            get
            {
                if (R < double.Epsilon) return new Complex(0, 0);
                else return new Complex(Real / R, Imaginary / R);
            }
        }

        public Complex (double a, double b, bool IsTrigonometric=false)
        {
            this.a = a;
            this.b = b;

            if (IsTrigonometric)
			{
				SetRTheta(a, b);
				return;
			}
        }
		public Complex(double Theta):this(1,Theta, true)
		{
		}
		public Complex (PointF Point):this(Point.X, Point.Y)
		{
		}
        
        public void SetRTheta(double r, double theta)
        {
            a = Math.Abs(r) * Math.Cos(theta);
            b = Math.Abs(r) * Math.Sin(theta);
        }

        public override string ToString()
        {
            return ToString(ComplexStyle.CARTESIAN);
        }
        public string ToString(IFormatProvider format)
        {
            throw new NotImplementedException();
        }

        public string ToString(ComplexStyle Style)
        {
			switch (Style)
			{
				case ComplexStyle.TRIGONOMETRIC:
					return string.Format("{0}*e^(i{1}*pi)", R, Theta/Math.PI);
				case ComplexStyle.COMPONENT:
					return string.Format("({0}, {1})", Real, Imaginary);
				case ComplexStyle.PHASOR:
                    if (IsReal) return Real.ToString();
                    else if (IsImaginary) return string.Format("cos({0}*pi)+i sin({0}*pi)", Theta / Math.PI);
					return string.Format("{0}*(cos({1}*pi)+i sin({1}*pi))", R, Theta/Math.PI);
				case ComplexStyle.CARTESIAN:
				default:
                    if (b == 0.0) return string.Format("{0}", a);
                    else if (a == 0.0) return string.Format("{0}i", b);
					return string.Format("({0} + {1}i)", a, b);
			}
        }

		public PointF ToPoint()
		{
			return new PointF((float)a, (float)b);
		}


        static public implicit operator Complex(double value)
        {
            return new Complex(value, 0);
        }
        static public implicit operator double(Complex val)
        {
            return val.Real;
        }
        static public implicit operator int(Complex val)
        {
            return (int)val.Real;
        }

        static public Complex operator +(Complex a, Complex b)
        {
            return new Complex(a.a + b.a, a.b + b.b);
        }
		static public Complex operator +(Complex a, double b)
		{
			return new Complex(a.a + b, a.b);
		}

        static public Complex operator -(Complex a)
        {
            return new Complex(-a.a, -a.b);
        }

		static public Complex operator -(Complex a, double b)
		{
			return a + (-b);
		}
        static public Complex operator -(Complex a, Complex b)
        {
            return a + (-b);
        }

        static public Complex operator *(Complex a, double b)
        {
            return new Complex(a.a * b, a.b * b);
        }
        static public Complex operator *(double a, Complex b)
        {
            return b * a;
        }

        public static Complex operator *(Complex a, Complex b)
        {
            // (a+bi)(c+di) => ac+adi+cbi-bd => (ac-bd) + (ad+cb)i

            return new Complex(a.a * b.a - a.b * b.b, a.a * b.b + b.a * a.b);
        }

        public static Complex operator /(Complex a, Complex b)
        {
            // (a+bi)/(c+di) => (ac+bd)/(c*c+d*d) + i(bc-ad)/(c*c+d*d)

            return new Complex((a.a*b.a+a.b*b.b)/(b.a*b.a+b.b*b.b),
                               (a.b*b.a-a.a*b.b)/(b.a*b.a+b.b*b.b));
        }

        public static Complex operator /(Complex a, double b)
        {
            return new Complex(a.a/b, a.b/b);
        }
        public static Complex operator /(double a, Complex b)
        {
            return new Complex(a/b.a, a/b.b);
        }

        public static bool operator ==(Complex a, Complex b)
        {
            return a.Equals(b);
        }
        public static bool operator ==(Complex a, object b)
        {
            return a.Equals(b);
        }
        public static bool operator ==(object a, Complex b)
        {
            return b.Equals(a);
        }

        public static bool operator !=(Complex a, Complex b)
        {
            return !(a == b);
        }
        public static bool operator !=(Complex a, object b)
        {
            return !a.Equals(b);
        }
        public static bool operator !=(object a, Complex b)
        {
            return !b.Equals(a);
        }

        public override bool Equals(object obj)
        {
            if (obj is Complex c)
            {
                return a == c.a && b == c.b;
            }
            else if (obj is double)
            {
                return IsReal && Real == (double)obj;
            }
            else if (obj is float)
            {
                return IsReal && (float)Real == (float)obj;
            }
            else if (obj is int)
            {
                return IsReal && Real == (int)obj;
            }
            else return false;
        }
        public override int GetHashCode()
        {
            return Real.GetHashCode() ^ Imaginary.GetHashCode();
        }

        static readonly Complex I = new Complex(0,1);
        static readonly Complex Epsilon = new Complex(double.Epsilon, double.Epsilon);
    }
}
