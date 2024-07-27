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

using System.Runtime.CompilerServices;

namespace SDL2Sharp.Interop
{
    public unsafe partial struct SDL_AudioCVT
    {
        public int needed;

        [NativeTypeName("SDL_AudioFormat")]
        public ushort src_format;

        [NativeTypeName("SDL_AudioFormat")]
        public ushort dst_format;

        public double rate_incr;

        [NativeTypeName("Uint8 *")]
        public byte* buf;

        public int len;

        public int len_cvt;

        public int len_mult;

        public double len_ratio;

        [NativeTypeName("SDL_AudioFilter[10]")]
        public _filters_e__FixedBuffer filters;

        public int filter_index;

        public unsafe partial struct _filters_e__FixedBuffer
        {
            public delegate* unmanaged[Cdecl]<SDL_AudioCVT*, ushort, void> e0;
            public delegate* unmanaged[Cdecl]<SDL_AudioCVT*, ushort, void> e1;
            public delegate* unmanaged[Cdecl]<SDL_AudioCVT*, ushort, void> e2;
            public delegate* unmanaged[Cdecl]<SDL_AudioCVT*, ushort, void> e3;
            public delegate* unmanaged[Cdecl]<SDL_AudioCVT*, ushort, void> e4;
            public delegate* unmanaged[Cdecl]<SDL_AudioCVT*, ushort, void> e5;
            public delegate* unmanaged[Cdecl]<SDL_AudioCVT*, ushort, void> e6;
            public delegate* unmanaged[Cdecl]<SDL_AudioCVT*, ushort, void> e7;
            public delegate* unmanaged[Cdecl]<SDL_AudioCVT*, ushort, void> e8;
            public delegate* unmanaged[Cdecl]<SDL_AudioCVT*, ushort, void> e9;

            public ref delegate* unmanaged[Cdecl]<SDL_AudioCVT*, ushort, void> this[int index]
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    fixed (delegate* unmanaged[Cdecl]<SDL_AudioCVT*, ushort, void>* pThis = &e0)
                    {
                        return ref pThis[index];
                    }
                }
            }
        }
    }
}
