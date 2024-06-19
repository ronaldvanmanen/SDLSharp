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
    [PackedColor(PackedPixelFormat.ARGB2101010)]
    public readonly record struct Argb2101010
    {
        private readonly uint _value;

        public ushort A => (ushort)(_value >> 30 & 0x3);

        public ushort R => (ushort)(_value >> 20 & 0x3FF);

        public ushort G => (ushort)(_value >> 10 & 0x3FF);

        public ushort B => (ushort)(_value & 0x3FF);

        public Argb2101010(byte a, ushort r, ushort g, ushort b)
        {
            unchecked
            {
                _value = (uint)(a << 30 & 0x3 | r << 20 & 0x3FF | g << 10 & 0x3FF | b & 0x3FF);
            }
        }
    }
}
