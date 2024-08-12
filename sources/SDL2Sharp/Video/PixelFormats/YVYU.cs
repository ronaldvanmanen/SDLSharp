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

namespace SDL2Sharp.Video.PixelFormats
{
    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 4)]
    public readonly record struct YVYU
    {
        private readonly uint _value;

        public byte Y0 => (byte)(_value & 0xFF);

        public byte V0 => (byte)(_value >> 8 & 0xFF);

        public byte Y1 => (byte)(_value >> 16 & 0xFF);

        public byte U0 => (byte)(_value >> 24 & 0xFF);

        public YVYU(byte y0, byte v0, byte y1, byte u0)
        {
            unchecked
            {
                _value = (uint)(y0 | v0 << 8 | y1 << 16 | u0 << 24);
            }
        }
    }
}
