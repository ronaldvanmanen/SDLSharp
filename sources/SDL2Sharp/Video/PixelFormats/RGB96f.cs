// SDL2Sharp
//
// Copyright (C) 2021-2024 Ronald van Manen <rvanmanen@gmail.com>
//
// This software is provided 'as-is', without any express or implied
// warranty.  In no event will the authors be held liable for any damages
// arising from the use of this software.
// 
// Permission is granted to anyone to use this software for any purpose,
// including commercial applications, and to alter it and redistribute it
// freely, subject to the following restrictions:
//
// 1. The origin of this software must not be misrepresented; you must not
//    claim that you wrote the original software. If you use this software
//    in a product, an acknowledgment in the product documentation would be
//    appreciated but is not required.
// 2. Altered source versions must be plainly marked as such, and must not be
//    misrepresented as being the original software.
// 3. This notice may not be removed or altered from any source distribution.

using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL2Sharp.Video.PixelFormats
{
    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 12)]
    public readonly struct RGB96f : IEquatable<RGB96f>
    {
        public static RGB96f Black { get; } = new RGB96f(0f, 0f, 0f);

        public static RGB96f White { get; } = new RGB96f(1f, 1f, 1f);

        private readonly Vector3 _components;

        public float R
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _components.X;
            }
        }

        public float G
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _components.Y;
            }
        }

        public float B
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return _components.Z;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGB96f(float r, float g, float b)
        : this(new Vector3(r, g, b))
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGB96f(RGB96f other)
        : this(other._components)
        { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private RGB96f(Vector3 components)
        {
            _components = components;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool Equals(RGB96f other)
        {
            return _components.Equals(other._components);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly bool Equals(object? obj)
        {
            if (obj is RGB96f other)
            {
                return Equals(other);
            }
            return false;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(R, G, B);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGB96f Negate(RGB96f value)
        {
            return new RGB96f(-value._components);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGB96f Add(RGB96f left, RGB96f right)
        {
            return new RGB96f(left._components + right._components);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGB96f Subtract(RGB96f left, RGB96f right)
        {
            return new RGB96f(left._components - right._components);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGB96f Multiply(float left, RGB96f right)
        {
            return new RGB96f(left * right._components);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGB96f Multiply(RGB96f left, float right)
        {
            return new RGB96f(left._components * right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGB96f Multiply(RGB96f left, RGB96f right)
        {
            return new RGB96f(left._components * right._components);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGB96f Divide(RGB96f left, RGB96f right)
        {
            return new RGB96f(left._components / right._components);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGB96f Divide(RGB96f left, float right)
        {
            return new RGB96f(left._components / right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGB96f Clamp(RGB96f value, RGB96f min, RGB96f max)
        {
            return new RGB96f(Vector3.Clamp(value._components, min._components, max._components));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGB96f Clamp(RGB96f value)
        {
            return Clamp(value, Black, White);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGB96f operator +(RGB96f value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGB96f operator -(RGB96f value)
        {
            return Negate(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGB96f operator +(RGB96f left, RGB96f right)
        {
            return Add(left, right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGB96f operator -(RGB96f left, RGB96f right)
        {
            return Subtract(left, right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGB96f operator *(float left, RGB96f right)
        {
            return Multiply(left, right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGB96f operator *(RGB96f left, float right)
        {
            return Multiply(left, right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGB96f operator *(RGB96f left, RGB96f right)
        {
            return Multiply(left, right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGB96f operator /(RGB96f left, RGB96f right)
        {
            return Divide(left, right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGB96f operator /(RGB96f left, float right)
        {
            return Divide(left, right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(RGB96f left, RGB96f right)
        {
            return left.Equals(right);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(RGB96f left, RGB96f right)
        {
            return !left.Equals(right);
        }
    }
}
