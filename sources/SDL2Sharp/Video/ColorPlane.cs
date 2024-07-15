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
    public readonly ref struct ColorPlane
    {
        private readonly Span<byte> _pixels;

        private readonly int _height;

        private readonly int _width;

        private readonly int _pitch;

        private readonly int _offset;

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

        public ref byte this[int row, int column]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (row < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(row),
                        row,
                        "row cannot be less than zero");
                }

                if (row >= _height)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(column),
                        column,
                        "row cannot be greater than or equal to the height of the image");
                }

                if (column < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(column),
                        column,
                        "column cannot be less than zero");
                }

                if (column >= _width)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(column),
                        column,
                        "column cannot be greater than or equal to the width of the image");
                }

                return ref DangerousGetReferenceAt(row, column);
            }
        }

        public unsafe ColorPlane(void* pixels, int height, int width, int pitch, int offset)
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

            if (_offset < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(offset),
                    offset,
                    "offset cannot be less than zero");
            }

            _pixels = new Span<byte>(pixels, height * pitch);
            _height = height;
            _width = width;
            _pitch = pitch;
            _offset = offset;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref byte DangerousGetReference()
        {
            ref var r0 = ref MemoryMarshal.GetReference(_pixels);
            return ref Unsafe.Add(ref r0, _offset);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref byte DangerousGetReferenceAt(int row, int column)
        {
            ref var r0 = ref MemoryMarshal.GetReference(_pixels);
            var index = row * _pitch + column + _offset;
            return ref Unsafe.Add(ref r0, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly void Fill(byte value)
        {
            _pixels.Fill(value);
        }
    }
}
