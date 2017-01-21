//
// Complex.cs
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


using System;
using System.Drawing;

namespace SolarLiner.ComplexNumber
{
    /// <summary>
    /// Class handling complex numbers.
    /// </summary>
    public struct Complex
    {
        /// <summary>
        /// Enumerates the different kinds of string output used in ToString().
        /// </summary>
        public enum ComplexStyle
        {
            /// <summary>
            /// Trigonometric form (r*e^{i*theta*pi}).
            /// </summary>
            TRIGONOMETRIC,
            /// <summary>
            /// Cartesian form (a+bi).
            /// </summary>
            CARTESIAN,
            /// <summary>
            /// Component form ([r, theta]).
            /// </summary>
            COMPONENT,
            /// <summary>
            /// Prints complex number as a sum of sin and cos functions (r*(cos(theta)+ i*sin(theta))).
            /// </summary>
            PHASOR
        }

        /// <summary>
        /// Gets or sets the real part of the complex number.
        /// </summary>
        /// <value>The real part.</value>
        public double Real
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the imaginary part of the complex number.
        /// </summary>
        /// <value>The imaginary part.</value>
        public double Imaginary
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the radius, or length, or magnitude, of the complex number.
        /// </summary>
        public double R
        {
            get
            {
                return Math.Sqrt(Real * Real + Imaginary * Imaginary);
            }
        }

        /// <summary>
        /// Gets the angle of the complex number.
        /// </summary>
        public double Theta
        {
            get
            {
                if (IsReal)
                    return 0;
                else if (IsImaginary)
                    return Math.PI / 2;
                else
                    return Math.Atan2(Imaginary, Real);
            }
        }

        /// <summary>
        /// Returns whether the complex number is imaginary or not (no real part).
        /// </summary>
        /// <value><c>true</c> if this instance is imaginary; otherwise, <c>false</c>.</value>
        public bool IsImaginary { get { return Math.Abs(Real) < double.Epsilon; } }

        /// <summary>
        /// Returns whether the complex number is real or not (no imaginary part).
        /// </summary>
        /// <value><c>true</c> if this instance is real; otherwise, <c>false</c>.</value>
        public bool IsReal { get { return Math.Abs(Imaginary) < double.Epsilon; } }

        /// <summary>
        /// Returns the complex conjugate (a-bi).
        /// </summary>
        /// <value>The complex conjugate.</value>
        public Complex Conjugate
        {
            get
            {
                return new Complex(Real, -Imaginary);
            }
        }

        /// <summary>
        /// Returns the normalized complex number.
        /// </summary>
        /// <value>The normalized complex number with radius=1.</value>
        public Complex Normalized
        {
            get
            {
                if (R < double.Epsilon)
                    return new Complex(0, 0);
                else
                    return new Complex(Real / R, Imaginary / R);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SolarLiner.ComplexNumber.Complex"/> struct.
        /// </summary>
        /// <param name="a">The real or radius component.</param>
        /// <param name="b">The imaginary or angle component.</param>
        /// <param name="isTrigonometric">If set to <c>true</c>, a and b are trigonometric inputs.</param>
        public Complex(double a, double b, bool isTrigonometric = false)
        {
            this.Real = a;
            this.Imaginary = b;

            if (isTrigonometric)
            {
                SetRTheta(Real, Imaginary);
                return;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SolarLiner.ComplexNumber.Complex"/> struct.
        /// </summary>
        /// <param name="theta">Trigonimetric angle component.</param>
        public Complex(double theta)
            : this(1, theta, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SolarLiner.ComplexNumber.Complex"/> struct.
        /// </summary>
        /// <param name="point">Point to convert to a complex.</param>
        public Complex(PointF point)
            : this(point.X, point.Y)
        {
        }

        /// <summary>
        /// Sets the trigonometric components of the complex number
        /// </summary>
        /// <param name="r">The radius component.</param>
        /// <param name="theta">The angle component.</param>
        public void SetRTheta(double r, double theta)
        {
            Real = Math.Abs(r) * Math.Cos(theta);
            Imaginary = Math.Abs(r) * Math.Sin(theta);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="SolarLiner.ComplexNumber.Complex"/> using the Cartesian form.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="SolarLiner.ComplexNumber.Complex"/>.</returns>
        public override string ToString()
        {
            return ToString(ComplexStyle.CARTESIAN);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="SolarLiner.ComplexNumber.Complex"/> using the given style.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="SolarLiner.ComplexNumber.Complex"/>.</returns>
        /// <param name="Style">Chosen complex styling.</param>
        public string ToString(ComplexStyle Style)
        {
            switch (Style)
            {
                case ComplexStyle.TRIGONOMETRIC:
                    return string.Format("{0}*e^(i{1}*pi)", R, Theta / Math.PI);
                case ComplexStyle.COMPONENT:
                    return string.Format("[{0}, {1}*pi]", R, Theta / Math.PI);
                case ComplexStyle.PHASOR:
                    if (IsReal)
                        return Real.ToString();
                    else if (IsImaginary)
                        return string.Format("cos({0}*pi)+i sin({0}*pi)", Theta / Math.PI);
                    return string.Format("{0}*(cos({1}*pi)+i sin({1}*pi))", R, Theta / Math.PI);
                case ComplexStyle.CARTESIAN:
                default:
                    if (Imaginary == 0.0)
                        return string.Format("{0}", Real);
                    else if (Real == 0.0)
                        return string.Format("{0}i", Imaginary);
                    return string.Format("({0} + {1}i)", Real, Imaginary);
            }
        }

        /// <summary>
        /// Converts the current complex number into a <see cref="System.Drawing.PointF"/>.
        /// </summary>
        /// <returns>A new <see cref="System.Drawing.PointF"/> corresponding to the complex number.</returns>
        public PointF ToPoint()
        {
            return new PointF((float)Real, (float)Imaginary);
        }

        static public implicit operator Complex(double value)
        {
            return new Complex(value, 0);
        }

        /// <remarks>May produce a loss of data when <see cref="SolarLiner.ComplexNumber.Complex"/> has an imarinary part.</remarks>
        static public implicit operator double(Complex val)
        {
            #warning "Possible loss of data when casting from Complex to double."
            return val.Real;
        }

        /// <remarks>May produce a loss of data when <see cref="SolarLiner.ComplexNumber.Complex"/> has an imarinary part.</remarks>
        static public implicit operator int(Complex val)
        {
            #warning "Possible loss of data when casting from Complex to double."
            return (int)val.Real;
        }

        static public Complex operator +(Complex a, Complex b)
        {
            return new Complex(a.Real + b.Real, a.Imaginary + b.Imaginary);
        }

        static public Complex operator +(Complex a, double b)
        {
            return new Complex(a.Real + b, a.Imaginary);
        }

        static public Complex operator -(Complex a)
        {
            return new Complex(-a.Real, -a.Imaginary);
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
            return new Complex(a.Real * b, a.Imaginary * b);
        }

        static public Complex operator *(double a, Complex b)
        {
            return b * a;
        }

        public static Complex operator *(Complex a, Complex b)
        {
            // (a+bi)(c+di) => ac+adi+cbi-bd => (ac-bd) + (ad+cb)i

            return new Complex(a.Real * b.Real - a.Imaginary * b.Imaginary, a.Real * b.Imaginary + b.Real * a.Imaginary);
        }

        public static Complex operator /(Complex a, Complex b)
        {
            // (a+bi)/(c+di) => (ac+bd)/(c*c+d*d) + i(bc-ad)/(c*c+d*d)

            return new Complex((a.Real * b.Real + a.Imaginary * b.Imaginary) / (b.Real * b.Real + b.Imaginary * b.Imaginary),
                (a.Imaginary * b.Real - a.Real * b.Imaginary) / (b.Real * b.Real + b.Imaginary * b.Imaginary));
        }

        public static Complex operator /(Complex a, double b)
        {
            return new Complex(a.Real / b, a.Imaginary / b);
        }

        public static Complex operator /(double a, Complex b)
        {
            return new Complex(a / b.Real, a / b.Imaginary);
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
            return !a.Equals(b);
        }

        public static bool operator !=(Complex a, object b)
        {
            return !a.Equals(b);
        }

        public static bool operator !=(object a, Complex b)
        {
            return !b.Equals(a);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="SolarLiner.ComplexNumber.Complex"/>.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="SolarLiner.ComplexNumber.Complex"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
        /// <see cref="SolarLiner.ComplexNumber.Complex"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Complex)
            {
                Complex c = (Complex)obj;
                return Math.Abs(Real - c.Real) < double.Epsilon && Math.Abs(Imaginary - c.Imaginary) < double.Epsilon;
            }
            else if (obj is double)
            {
                return IsReal && Math.Abs(Real - (double)obj) < double.Epsilon;
            }
            else if (obj is float)
            {
                return IsReal && Math.Abs((float)Real - (float)obj) < double.Epsilon;
            }
            else if (obj is int)
            {
                return IsReal && Math.Abs(Real - (int)obj) < double.Epsilon;
            }
            else
                return false;
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="SolarLiner.ComplexNumber.Complex"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
        public override int GetHashCode()
        {
            return Real.GetHashCode() ^ Imaginary.GetHashCode();
        }

        /// <summary>
        /// The Imaginary number i.
        /// </summary>
        public static readonly Complex I = new Complex(0, 1);
        /// <summary>
        /// Smallest positive complex number.
        /// </summary>
        public static readonly Complex Epsilon = new Complex(double.Epsilon, double.Epsilon);
    }
}
