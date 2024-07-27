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
    public unsafe partial struct SDL_VirtualJoystickDesc
    {
        [NativeTypeName("Uint16")]
        public ushort version;

        [NativeTypeName("Uint16")]
        public ushort type;

        [NativeTypeName("Uint16")]
        public ushort naxes;

        [NativeTypeName("Uint16")]
        public ushort nbuttons;

        [NativeTypeName("Uint16")]
        public ushort nhats;

        [NativeTypeName("Uint16")]
        public ushort vendor_id;

        [NativeTypeName("Uint16")]
        public ushort product_id;

        [NativeTypeName("Uint16")]
        public ushort padding;

        [NativeTypeName("Uint32")]
        public uint button_mask;

        [NativeTypeName("Uint32")]
        public uint axis_mask;

        [NativeTypeName("const char *")]
        public sbyte* name;

        public void* userdata;

        [NativeTypeName("void (*)(void *) __attribute__((cdecl))")]
        public delegate* unmanaged[Cdecl]<void*, void> Update;

        [NativeTypeName("void (*)(void *, int) __attribute__((cdecl))")]
        public delegate* unmanaged[Cdecl]<void*, int, void> SetPlayerIndex;

        [NativeTypeName("int (*)(void *, Uint16, Uint16) __attribute__((cdecl))")]
        public delegate* unmanaged[Cdecl]<void*, ushort, ushort, int> Rumble;

        [NativeTypeName("int (*)(void *, Uint16, Uint16) __attribute__((cdecl))")]
        public delegate* unmanaged[Cdecl]<void*, ushort, ushort, int> RumbleTriggers;

        [NativeTypeName("int (*)(void *, Uint8, Uint8, Uint8) __attribute__((cdecl))")]
        public delegate* unmanaged[Cdecl]<void*, byte, byte, byte, int> SetLED;

        [NativeTypeName("int (*)(void *, const void *, int) __attribute__((cdecl))")]
        public delegate* unmanaged[Cdecl]<void*, void*, int, int> SendEffect;
    }
}
