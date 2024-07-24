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

    [StructLayout(LayoutKind.Sequential, Pack = 2, Size = 2)]
    public readonly record struct Abgr1555 : IPackedPixel<Abgr1555>
    {
        private static readonly PixelFormatDescriptor _formatDescriptor = new(PixelFormat.ABGR1555);

        private readonly ushort _value;

        public static PixelFormat Format => _formatDescriptor.Format;

        public static Abgr1555 FromRGB(byte r, byte g, byte b)
        {
            return new Abgr1555((ushort)_formatDescriptor.MapRGB(r, g, b));
        }

        public static Abgr1555 FromRGBA(byte r, byte g, byte b, byte a)
        {
            return new Abgr1555((ushort)_formatDescriptor.MapRGBA(r, g, b, a));
        }

        private Abgr1555(ushort value)
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
