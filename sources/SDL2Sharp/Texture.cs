﻿// SDL2Sharp
//
// Copyright (C) 2021 Ronald van Manen <rvanmanen@gmail.com>
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
    public sealed unsafe class Texture : IDisposable
    {
        private SDL_Texture* _handle;

        public int Width
        {
            get
            {
                int width;
                Error.ThrowOnFailure(SDL.QueryTexture(_handle, null, null, &width, null));
                return width;
            }
        }

        public int Height
        {
            get
            {
                int height;
                Error.ThrowOnFailure(SDL.QueryTexture(_handle, null, null, null, &height));
                return height;
            }
        }

        internal Texture(SDL_Texture* texture)
        {
            if (texture == null)
            {
                throw new ArgumentNullException(nameof(texture));
            }

            _handle = texture;
        }

        ~Texture()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool _)
        {
            if (_handle == null) return;
            SDL.DestroyTexture(_handle);
            _handle = null;
        }

        public static implicit operator SDL_Texture*(Texture texture)
        {
            if (texture is null)
            {
                throw new ArgumentNullException(nameof(texture));
            }

            return texture._handle;
        }
    }
}
