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

using static SDL2Sharp.Interop.SDL_PixelType;

namespace SDL2Sharp.Video
{
    public enum PixelType
    {
        Unknown = SDL_PIXELTYPE_UNKNOWN,
        Index1 = SDL_PIXELTYPE_INDEX1,
        Index4 = SDL_PIXELTYPE_INDEX4,
        Index8 = SDL_PIXELTYPE_INDEX8,
        Packed8 = SDL_PIXELTYPE_PACKED8,
        Packed16 = SDL_PIXELTYPE_PACKED16,
        Packet32 = SDL_PIXELTYPE_PACKED32,
        ArrayU8 = SDL_PIXELTYPE_ARRAYU8,
        ArrayU16 = SDL_PIXELTYPE_ARRAYU16,
        ArrayU32 = SDL_PIXELTYPE_ARRAYU32,
        ArrayF16 = SDL_PIXELTYPE_ARRAYF16,
        ArrayF32 = SDL_PIXELTYPE_ARRAYF32,
    }
}
