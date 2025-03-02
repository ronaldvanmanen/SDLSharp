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

using System;
using SDL2Sharp.Interop;

namespace SDL2Sharp
{
    public sealed unsafe class PixelFormatDescriptor : FinalizableObject
    {
        private SDL_PixelFormat* _handle;

        private readonly bool _ownsHandle;

        internal SDL_PixelFormat* Handle => _handle;

        public PixelFormat Format => (PixelFormat)_handle->format;

        public byte BitsPerPixel => _handle->BitsPerPixel;

        public byte BytesPerPixel => _handle->BytesPerPixel;

        public uint RedMask => _handle->Rmask;

        public uint GreenMask => _handle->Gmask;

        public uint BlueMask => _handle->Bmask;

        public uint AlphaMask => _handle->Amask;

        public byte RedLoss => _handle->Rloss;

        public byte GreenLoss => _handle->Gloss;

        public byte BlueLoss => _handle->Bloss;

        public byte AlphaLoss => _handle->Aloss;

        public byte RedShift => _handle->Rshift;

        public byte GreenShift => _handle->Gshift;

        public byte BlueShift => _handle->Bshift;

        public byte AlphaShift => _handle->Ashift;

        internal PixelFormatDescriptor(SDL_PixelFormat* handle, bool ownsHandle)
        {
            ArgumentNullException.ThrowIfNull(handle);
            _handle = handle;
            _ownsHandle = ownsHandle;
        }

        public PixelFormatDescriptor(PixelFormat pixelFormat)
        : this(Error.ThrowLastErrorIfNull(Interop.SDL.AllocFormat((uint)pixelFormat)), true)
        { }

        protected override void Dispose(bool disposing)
        {
            if (!_ownsHandle || _handle == null) return;
            Interop.SDL.FreeFormat(_handle);
            _handle = null;
        }

        internal uint MapRGB(byte r, byte g, byte b)
        {
            return Interop.SDL.MapRGB(_handle, r, g, b);
        }

        public uint MapRGBA(byte r, byte g, byte b, byte a)
        {
            return Interop.SDL.MapRGBA(_handle, r, g, b, a);
        }

        public (byte r, byte g, byte b) GetRGB(uint value)
        {
            byte r, g, b;
            Interop.SDL.GetRGB(value, _handle, &r, &g, &b);
            return (r, g, b);
        }

        public (byte r, byte g, byte b, byte a) GetRGBA(uint value)
        {
            byte r, g, b, a;
            Interop.SDL.GetRGBA(value, _handle, &r, &g, &b, &a);
            return (r, g, b, a);
        }
    }
}
