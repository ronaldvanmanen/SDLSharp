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

using static SDL2Sharp.Interop.SDL_PackedLayout;

namespace SDL2Sharp
{
    public enum PackedLayout
    {
        None = SDL_PACKEDLAYOUT_NONE,
        _332 = SDL_PACKEDLAYOUT_332,
        _4444 = SDL_PACKEDLAYOUT_4444,
        _1555 = SDL_PACKEDLAYOUT_1555,
        _5551 = SDL_PACKEDLAYOUT_5551,
        _565 = SDL_PACKEDLAYOUT_565,
        _8888 = SDL_PACKEDLAYOUT_8888,
        _2101010 = SDL_PACKEDLAYOUT_2101010,
        _1010102 = SDL_PACKEDLAYOUT_1010102,
    }
}
