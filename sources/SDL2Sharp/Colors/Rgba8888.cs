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

namespace SDL2Sharp.Colors
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 4)]
    [PackedColor(PackedPixelFormat.RGBA8888)]
    public readonly record struct Rgba8888
    {
        private readonly uint _value;

        public byte R => (byte)(_value >> 24 & 0xFF);

        public byte G => (byte)(_value >> 16 & 0xFF);

        public byte B => (byte)(_value >> 8 & 0xFF);

        public byte A => (byte)(_value & 0xFF);

        public Rgba8888(byte r, byte g, byte b, byte a)
        {
            unchecked
            {
                _value = (uint)(r << 24 | g << 16 | b << 8 | a);
            }
        }
    }
}
