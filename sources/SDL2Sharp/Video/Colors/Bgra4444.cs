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
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 2)]
    [PackedColor(PackedPixelFormat.BGRA4444)]
    public readonly record struct Bgra4444
    {
        private readonly ushort _value;

        public byte B => (byte)(_value >> 12 & 0xF);

        public byte G => (byte)(_value >> 8 & 0xF);

        public byte R => (byte)(_value >> 4 & 0xF);

        public byte A => (byte)(_value & 0xFF);

        public Bgra4444(byte b, byte g, byte r, byte a)
        {
            _value = (ushort)((b & 0xF) << 12 | (g & 0xF) << 8 | (r & 0xF) << 4 | a & 0xF);
        }
    }
}
