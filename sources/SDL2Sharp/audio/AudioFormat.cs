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

namespace SDL2Sharp
{
    public enum AudioFormat : ushort
    {
#pragma warning disable CA1069 // Enums values should not be duplicated
        U8 = Interop.SDL.AUDIO_U8,
        S8 = Interop.SDL.AUDIO_S8,
        U16LSB = Interop.SDL.AUDIO_U16LSB,
        S16LSB = Interop.SDL.AUDIO_S16LSB,
        U16MSB = Interop.SDL.AUDIO_U16MSB,
        S16MSB = Interop.SDL.AUDIO_S16MSB,
        U16 = Interop.SDL.AUDIO_U16,
        S16 = Interop.SDL.AUDIO_S16,
        S32LSB = Interop.SDL.AUDIO_S32LSB,
        S32MSB = Interop.SDL.AUDIO_S32MSB,
        S32 = Interop.SDL.AUDIO_S32,
        F32LSB = Interop.SDL.AUDIO_F32LSB,
        F32MSB = Interop.SDL.AUDIO_F32MSB,
        F32 = Interop.SDL.AUDIO_F32,
        U16SYS = Interop.SDL.AUDIO_U16SYS,
        S16SYS = Interop.SDL.AUDIO_S16SYS,
        S32SYS = Interop.SDL.AUDIO_S32SYS,
        F32SYS = Interop.SDL.AUDIO_F32SYS
#pragma warning restore CA1069 // Enums values should not be duplicated
    }
}
