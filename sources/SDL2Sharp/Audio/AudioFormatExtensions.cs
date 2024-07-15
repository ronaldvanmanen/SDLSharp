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

using SDL2Sharp.Interop;

namespace SDL2Sharp.Audio
{
    public static class AudioFormatExtensions
    {
        public static int BitSize(this AudioFormat format)
        {
            return (ushort)format & SDL.SDL_AUDIO_MASK_BITSIZE;
        }

        public static bool IsFloat(this AudioFormat format)
        {
            return ((ushort)format & SDL.SDL_AUDIO_MASK_DATATYPE) != 0;
        }

        public static bool IsBigEndian(this AudioFormat format)
        {
            return ((ushort)format & SDL.SDL_AUDIO_MASK_ENDIAN) != 0;
        }

        public static bool IsSigned(this AudioFormat format)
        {
            return ((ushort)format & SDL.SDL_AUDIO_MASK_SIGNED) != 0;
        }

        public static bool IsInt(this AudioFormat format)
        {
            return !format.IsFloat();
        }

        public static bool IsLittleEndian(this AudioFormat format)
        {
            return !format.IsBigEndian();
        }

        public static bool IsUnsigned(this AudioFormat format)
        {
            return !format.IsSigned();
        }
    }
}
