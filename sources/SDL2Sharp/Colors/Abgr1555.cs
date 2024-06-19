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
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 1)]
    [PackedColor(PackedPixelFormat.ABGR1555)]
    public readonly record struct Abgr1555
    {
        private readonly ushort _value;

        public byte A => (byte)(_value >> 15 & 0x1);

        public byte B => (byte)(_value >> 10 & 0x1F);

        public byte G => (byte)(_value >> 5 & 0x1F);

        public byte R => (byte)(_value & 0x1F);

        public Abgr1555(byte a, byte b, byte g, byte r)
        {
            _value = (ushort)((a & 0x1) << 15 | (b & 0x1F) << 10 | (g & 0x1F) << 5 | r & 0x1F);
        }
    }
}
