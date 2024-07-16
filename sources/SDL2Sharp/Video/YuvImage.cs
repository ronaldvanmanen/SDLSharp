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
    public readonly ref struct YuvImage<TYuvPixelFormat> where TYuvPixelFormat : IYuvPixelFormat, new()
    {
        private static readonly TYuvPixelFormat _format = new();

        private readonly ImagePlane<Y8> _yPlane;

        private readonly ImagePlane<U8> _uPlane;

        private readonly ImagePlane<V8> _vPlane;

        public ImagePlane<Y8> Y => _yPlane;

        public ImagePlane<U8> U => _uPlane;

        public ImagePlane<V8> V => _vPlane;

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

        public unsafe YuvImage(void* pixels, int width, int height, int pitch)
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

            var yPlaneHeight = _format.GetYPlaneHeight(height);
            var yPlaneWidth = _format.GetYPlaneWidth(width);
            var yPlanePitch = _format.GetYPlanePitch(pitch);
            var yPlaneOffset = _format.GetYPlaneOffset(width, height, pitch);
            var yPlanePixels = Unsafe.Add<Y8>(pixels, yPlaneOffset);
            _yPlane = new ImagePlane<Y8>(yPlanePixels, yPlaneWidth, yPlaneHeight, yPlanePitch);

            var uPlaneHeight = _format.GetUPlaneHeight(height);
            var uPlaneWidth = _format.GetUPlaneWidth(width);
            var uPlanePitch = _format.GetUPlanePitch(pitch);
            var uPlaneOffset = _format.GetUPlaneOffset(width, height, pitch);
            var uPlanePixels = Unsafe.Add<U8>(pixels, uPlaneOffset);
            _uPlane = new ImagePlane<U8>(uPlanePixels, uPlaneWidth, uPlaneHeight, uPlanePitch);

            var vPlaneHeight = _format.GetVPlaneHeight(height);
            var vPlaneWidth = _format.GetVPlaneWidth(width);
            var vPlanePitch = _format.GetVPlanePitch(pitch);
            var vPlaneOffset = _format.GetVPlaneOffset(width, height, pitch);
            var vPlanePixels = Unsafe.Add<V8>(pixels, vPlaneOffset);
            _vPlane = new ImagePlane<V8>(vPlanePixels, vPlaneWidth, vPlaneHeight, vPlanePitch);
        }
    }
}
