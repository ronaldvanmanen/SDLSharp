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

namespace SDL2Sharp.Video
{
    public sealed unsafe class PixelFormat : IDisposable
    {
        private SDL_PixelFormat* _handle;

        private readonly bool _ownsHandle;

        public PixelFormatEnum Format => (PixelFormatEnum)_handle->format;

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

        internal PixelFormat(SDL_PixelFormat* handle, bool ownsHandle)
        {
            if (handle == null)
            {
                throw new ArgumentNullException(nameof(handle));
            }

            _handle = handle;
            _ownsHandle = ownsHandle;
        }

        public PixelFormat(PixelFormatEnum pixelFormat)
        : this(Error.ReturnOrThrowOnFailure(SDL.AllocFormat((uint)pixelFormat)), true)
        { }

        ~PixelFormat()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool _)
        {
            if (!_ownsHandle || _handle == null) return;
            SDL.FreeFormat(_handle);
            _handle = null;
        }
    }
}
