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
using SDL2Sharp.Video.Colors;

namespace SDL2Sharp.Video
{
    public readonly ref struct Nv21Image
    {
        private readonly ImagePlane<Y8> _yPlane;

        private readonly ImagePlane<VU88> _uvPlane;

        public ImagePlane<Y8> Y => _yPlane;

        public ImagePlane<VU88> UV => _uvPlane;

        public readonly int Width
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _yPlane.Width;
        }

        public readonly int Height
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _yPlane.Height;
        }

        public unsafe Nv21Image(void* pixels, int width, int height, int pitch)
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

            _yPlane = new ImagePlane<Y8>(pixels, width, height, pitch);
            var uvPlanePixels = Unsafe.Add<Y8>(pixels, height * pitch);
            _uvPlane = new ImagePlane<VU88>(uvPlanePixels, width / 2, height / 2, pitch / 2);
        }
    }
}
