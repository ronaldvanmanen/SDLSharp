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
using System.Runtime.CompilerServices;

namespace SDL2Sharp.Video
{
    public readonly ref struct IyuvImage
    {
        private readonly ColorPlane _yPlane;

        private readonly ColorPlane _uPlane;

        private readonly ColorPlane _vPlane;

        private readonly int _height;

        private readonly int _width;

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

        public unsafe IyuvImage(void* pixels, int height, int width, int pitch)
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

            _yPlane = new ColorPlane(pixels, height, width, pitch, 0);
            _vPlane = new ColorPlane(pixels, height / 2, width, pitch / 2, width * pitch);
            _uPlane = new ColorPlane(pixels, height / 2, width, pitch / 2, width * pitch * 2);
            _height = height;
            _width = width;
        }

        public ColorPlane Y => _yPlane;

        public ColorPlane U => _uPlane;

        public ColorPlane V => _vPlane;
    }
}
