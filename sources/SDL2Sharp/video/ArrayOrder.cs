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

using static SDL2Sharp.Interop.SDL_ArrayOrder;

namespace SDL2Sharp
{
    public enum ArrayOrder
    {
        None = SDL_ARRAYORDER_NONE,
        Rgb = SDL_ARRAYORDER_RGB,
        Rgba = SDL_ARRAYORDER_RGBA,
        Argb = SDL_ARRAYORDER_ARGB,
        Bgr = SDL_ARRAYORDER_BGR,
        Bgra = SDL_ARRAYORDER_BGRA,
        Abgr = SDL_ARRAYORDER_ABGR,
    }
}
