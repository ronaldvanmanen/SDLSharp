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

namespace SDL2Sharp.Interop
{
    public unsafe partial struct SDL_PixelFormat
    {
        [NativeTypeName("Uint32")]
        public uint format;

        public SDL_Palette* palette;

        [NativeTypeName("Uint8")]
        public byte BitsPerPixel;

        [NativeTypeName("Uint8")]
        public byte BytesPerPixel;

        [NativeTypeName("Uint8[2]")]
        public fixed byte padding[2];

        [NativeTypeName("Uint32")]
        public uint Rmask;

        [NativeTypeName("Uint32")]
        public uint Gmask;

        [NativeTypeName("Uint32")]
        public uint Bmask;

        [NativeTypeName("Uint32")]
        public uint Amask;

        [NativeTypeName("Uint8")]
        public byte Rloss;

        [NativeTypeName("Uint8")]
        public byte Gloss;

        [NativeTypeName("Uint8")]
        public byte Bloss;

        [NativeTypeName("Uint8")]
        public byte Aloss;

        [NativeTypeName("Uint8")]
        public byte Rshift;

        [NativeTypeName("Uint8")]
        public byte Gshift;

        [NativeTypeName("Uint8")]
        public byte Bshift;

        [NativeTypeName("Uint8")]
        public byte Ashift;

        public int refcount;

        [NativeTypeName("struct SDL_PixelFormat *")]
        public SDL_PixelFormat* next;
    }
}
