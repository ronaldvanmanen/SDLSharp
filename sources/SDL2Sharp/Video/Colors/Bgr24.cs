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
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 3)]
    [PackedColor(PackedPixelFormat.BGR24)]
    public readonly record struct Bgr24
    {
        private readonly byte _r, _g, _b;

        public byte B => _b;

        public byte G => _g;

        public byte R => _r;

        public Bgr24(byte b, byte g, byte r)
        {
            _b = b;
            _g = g;
            _r = r;
        }
    }
}