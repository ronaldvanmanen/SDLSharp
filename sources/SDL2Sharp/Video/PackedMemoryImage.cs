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

namespace SDL2Sharp.Video
{
    public sealed class PackedMemoryImage<TColor> where TColor : struct
    {
        private readonly ImageMemoryPlane<TColor> _plane;

        public int Width => _plane.Width;

        public int Height => _plane.Height;

        public Size Size => _plane.Size;

        public TColor this[int x, int y]
        {
            get
            {
                return _plane[x, y];
            }
            set
            {
                _plane[x, y] = value;
            }
        }

        public PackedMemoryImage(Size size)
        : this(size.Width, size.Height)
        { }

        public PackedMemoryImage(int width, int height)
        : this(new ImageMemoryPlane<TColor>(width, height))
        { }

        private PackedMemoryImage(ImageMemoryPlane<TColor> plane)
        {
            _plane = plane ?? throw new ArgumentNullException(nameof(plane));
        }

        public PackedMemoryImage<TColor> Crop(int top, int left, int bottom, int right)
        {
            var croppedPlane = _plane.Crop(top, left, bottom, right);
            var croppedImage = new PackedMemoryImage<TColor>(croppedPlane);
            return croppedImage;
        }

        public ref TColor DangerousGetReference()
        {
            return ref _plane.DangerousGetReference();
        }
    }
}
