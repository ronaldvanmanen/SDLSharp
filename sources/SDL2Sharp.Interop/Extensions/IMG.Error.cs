﻿// SDL2Sharp
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
    public static unsafe partial class IMG
    {
#if NET8_0_OR_GREATER
        [NativeTypeName("#define IMG_SetError SDL_SetError")]
        public static int SetError(sbyte* fmt) => SDL.SetError(fmt, __arglist());

        [NativeTypeName("#define IMG_GetError SDL_GetError")]
        public static sbyte* GetError() => SDL.GetError();
#endif
    }
}
