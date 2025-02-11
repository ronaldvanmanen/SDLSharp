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

namespace SDL2Sharp
{
    [StructLayout(LayoutKind.Sequential, Pack = 2, Size = 2)]
    public readonly record struct RGB565 : IPackedPixel<RGB565>
    {
        private static readonly PixelFormatDescriptor _formatDescriptor = new(PixelFormat.RGB565);

        private readonly ushort _value;

        public static PixelFormat Format => _formatDescriptor.Format;

        public static RGB565 FromRGB(byte r, byte g, byte b)
        {
            return new RGB565((ushort)_formatDescriptor.MapRGB(r, g, b));
        }

        public static RGB565 FromRGBA(byte r, byte g, byte b, byte a)
        {
            return new RGB565((ushort)_formatDescriptor.MapRGBA(r, g, b, a));
        }

        private RGB565(ushort value)
        {
            _value = value;
        }

        public (byte r, byte g, byte b) ToRGB()
        {
            return _formatDescriptor.GetRGB(_value);
        }

        public (byte r, byte g, byte b, byte a) ToRGBA()
        {
            return _formatDescriptor.GetRGBA(_value);
        }
    }
}
