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

using static SDL2Sharp.Interop.SDL_PackedOrder;

namespace SDL2Sharp.Video
{
    public enum PackedOrder
    {
        None = SDL_PACKEDORDER_NONE,
        Xrgb = SDL_PACKEDORDER_XRGB,
        Rgbx = SDL_PACKEDORDER_RGBX,
        Argb = SDL_PACKEDORDER_ARGB,
        Rgba = SDL_PACKEDORDER_RGBA,
        Xbgr = SDL_PACKEDORDER_XBGR,
        Bgrx = SDL_PACKEDORDER_BGRX,
        Abgr = SDL_PACKEDORDER_ABGR,
        Bgra = SDL_PACKEDORDER_BGRA,
    }
}
