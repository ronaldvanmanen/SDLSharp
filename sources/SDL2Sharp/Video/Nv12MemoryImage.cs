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
    public sealed class Nv12MemoryImage
    {
        private readonly ImageMemoryPlane<Y8> _yPlane;

        private readonly ImageMemoryPlane<UV88> _uvPlane;

        public ImageMemoryPlane<Y8> Y => _yPlane;

        public ImageMemoryPlane<UV88> UV => _uvPlane;

        public int Width
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _yPlane.Width;
        }

        public int Height
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _yPlane.Height;
        }

        public Nv12MemoryImage(int width, int height)
        {
            if (width < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(width),
                    width,
                    "height cannot be less than zero");
            }

            if (height < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(height),
                    height,
                    "height cannot be less than zero");
            }

            _yPlane = new ImageMemoryPlane<Y8>(width, height);
            _uvPlane = new ImageMemoryPlane<UV88>(width / 2, height / 2);
        }
    }
}
