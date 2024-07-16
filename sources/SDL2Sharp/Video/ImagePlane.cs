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
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL2Sharp.Video
{
    public readonly ref struct ImagePlane<TPackedPixelFormat> where TPackedPixelFormat : struct
    {
        private readonly Span<TPackedPixelFormat> _pixels;

        private readonly int _width;

        private readonly int _height;

        private readonly int _pitch;

        public readonly int Width
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _width;
        }

        public readonly int Height
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _height;
        }

        public Size Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new(_width, _height);
        }

        public ref TPackedPixelFormat this[int x, int y]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (x < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(x),
                        x,
                        "x cannot be less than zero");
                }

                if (x >= _width)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(x),
                        x,
                        "x cannot be greater than or equal to the width of the image");
                }

                if (y < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(y),
                        y,
                        "y cannot be less than zero");
                }

                if (y >= _height)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(x),
                        x,
                        "y cannot be greater than or equal to the height of the image");
                }

                return ref DangerousGetReferenceAt(x, y);
            }
        }

        public unsafe ImagePlane(void* pixels, int width, int height, int pitch)
        {
            if (height < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(height),
                    height,
                    "height cannot be less than zero");
            }

            if (width < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(width),
                    width,
                    "height cannot be less than zero");
            }

            if (pitch < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(pitch),
                    pitch,
                    "pitch cannot be less than zero");
            }

            _pixels = new Span<TPackedPixelFormat>(pixels, height * pitch);
            _height = height;
            _width = width;
            _pitch = pitch;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref TPackedPixelFormat DangerousGetReference()
        {
            ref var r0 = ref MemoryMarshal.GetReference(_pixels);
            const int index = 0;
            return ref Unsafe.Add(ref r0, index);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref TPackedPixelFormat DangerousGetReferenceAt(int x, int y)
        {
            ref var r0 = ref MemoryMarshal.GetReference(_pixels);
            var index = y * _pitch + x;
            return ref Unsafe.Add(ref r0, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly void Fill(TPackedPixelFormat value)
        {
            _pixels.Fill(value);
        }
    }
}
