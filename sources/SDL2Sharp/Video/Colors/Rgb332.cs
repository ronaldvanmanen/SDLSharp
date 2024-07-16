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

using System.Runtime.InteropServices;

namespace SDL2Sharp.Video.Colors
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 1)]
    [PixelFormat(PixelFormat.RGB332)]
    public readonly record struct Rgb332
    {
        private static readonly PixelFormatDescriptor _pixelFormat = new(PixelFormat.RGB332);

        private readonly byte _value;

        public static Rgb332 FromRGB(byte r, byte g, byte b)
        {
            return new Rgb332((byte)_pixelFormat.MapRGB(r, g, b));
        }

        public static Rgb332 FromRGBA(byte r, byte g, byte b, byte a)
        {
            return new Rgb332((byte)_pixelFormat.MapRGBA(r, g, b, a));
        }

        private Rgb332(byte value)
        {
            _value = value;
        }

        public (byte r, byte g, byte b) ToRGB()
        {
            return _pixelFormat.GetRGB(_value);
        }

        public (byte r, byte g, byte b, byte a) ToRGBA()
        {
            return _pixelFormat.GetRGBA(_value);
        }
    }
}
