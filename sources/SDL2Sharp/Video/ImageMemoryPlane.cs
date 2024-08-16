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
using System.Runtime.InteropServices;
using CommunityToolkit.HighPerformance;

namespace SDL2Sharp.Video
{
    public sealed class ImageMemoryPlane<TPackedPixel> where TPackedPixel : struct
    {
        private readonly int _width;

        private readonly int _height;

        private readonly TPackedPixel[] _pixels;

        public int Width => _width;

        public int Height => _height;

        public Size Size => new(_width, _height);

        public int Pitch => _width * Marshal.SizeOf<TPackedPixel>();

        public TPackedPixel this[int x, int y]
        {
            get
            {
                return _pixels[y * _width + x];
            }
            set
            {
                _pixels[y * _width + x] = value;
            }
        }

        public ImageMemoryPlane(int width, int height)
        {
            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(width),
                    width,
                    "The width of the image plane must be a positive integer.");
            }

            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(height),
                    height,
                    "The height of the image plane must be a positive integer.");
            }

            _width = width;
            _height = height;
            _pixels = new TPackedPixel[_height * _width];
        }

        public ImageMemoryPlane(Size size)
        : this(size.Width, size.Height)
        { }

        public ImageMemoryPlane<TPackedPixel> Crop(int top, int left, int bottom, int right)
        {
            if (top < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(top),
                    top,
                    "The top of the crop rectangle cannot be less than zero.");
            }

            if (left < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(left),
                    left,
                    "The left of the crop rectangle cannot be less than zero.");
            }

            if (bottom >= _height)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(bottom),
                    bottom,
                    "The bottom of the crop rectangle cannot be greater than or equal to the width of the image.");
            }

            if (right >= _width)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(right),
                    right,
                    "The right of the crop rectangle cannot be greater than or equal to the width of the image.");
            }

            if (bottom <= top)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(bottom),
                    bottom,
                    "The bottom of the crop rectangle cannot be less than or equal to the top of the crop rectangle.");
            }

            if (right <= left)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(right),
                    right,
                    "The right of the crop rectangle cannot be less than or equal to the left of the crop rectangle.");
            }

            var croppedImage = new ImageMemoryPlane<TPackedPixel>(right - left, bottom - top);
            for (var y = 0; y < croppedImage.Height; ++y)
            {
                for (var x = 0; x < croppedImage.Width; ++x)
                {
                    croppedImage[x, y] = this[x + left, y + top];
                }
            }
            return croppedImage;
        }

        public ref TPackedPixel DangerousGetReference()
        {
            return ref _pixels.DangerousGetReference();
        }

        public static unsafe explicit operator void*(ImageMemoryPlane<TPackedPixel> imagePlane)
        {
            return (void*)imagePlane;
        }
    }
}
