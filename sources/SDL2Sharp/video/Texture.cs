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
    public sealed unsafe class Texture : IDisposable
    {
        public delegate void LockToSurfaceCallback(Surface pixels);

        private SDL_Texture* _handle;

        internal SDL_Texture* Handle => _handle;

        public PixelFormat Format
        {
            get
            {
                uint format;
                Error.ThrowLastErrorIfNegative(Interop.SDL.QueryTexture(_handle, &format, null, null, null));
                return (PixelFormat)format;
            }
        }

        public TextureAccess Access
        {
            get
            {
                int access;
                Error.ThrowLastErrorIfNegative(Interop.SDL.QueryTexture(_handle, null, &access, null, null));
                return (TextureAccess)access;
            }
        }

        public int Width
        {
            get
            {
                int width;
                Error.ThrowLastErrorIfNegative(Interop.SDL.QueryTexture(_handle, null, null, &width, null));
                return width;
            }
        }

        public int Height
        {
            get
            {
                int height;
                Error.ThrowLastErrorIfNegative(Interop.SDL.QueryTexture(_handle, null, null, null, &height));
                return height;
            }
        }

        public Size Size
        {
            get
            {
                int width, height;
                Error.ThrowLastErrorIfNegative(Interop.SDL.QueryTexture(_handle, null, null, &width, &height));
                return new Size(width, height);
            }
        }

        public BlendMode BlendMode
        {
            get
            {
                SDL_BlendMode blendMode;
                Error.ThrowLastErrorIfNegative(
                    Interop.SDL.GetTextureBlendMode(_handle, &blendMode)
                );
                return (BlendMode)blendMode;
            }
            set
            {
                Error.ThrowLastErrorIfNegative(
                    Interop.SDL.SetTextureBlendMode(_handle, (SDL_BlendMode)value)
                );
            }
        }

        public bool IsValid => 0 == Interop.SDL.QueryTexture(_handle, null, null, null, null);

        internal Texture(SDL_Texture* texture)
        {
            ArgumentNullException.ThrowIfNull(texture);

            _handle = texture;
        }

        ~Texture()
        {
            ReleaseHandle();
        }

        public void Dispose()
        {
            ReleaseHandle();
            GC.SuppressFinalize(this);
        }

        private void ReleaseHandle()
        {
            if (_handle is null) return;
            Interop.SDL.DestroyTexture(_handle);
            _handle = null;
        }

        public void WithLock(LockToSurfaceCallback callback)
        {
            WithLock(0, 0, Width, Height, callback);
        }

        public void WithLock(Rectangle rectangle, LockToSurfaceCallback callback)
        {
            WithLock(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, callback);
        }

        public void WithLock(int x, int y, int width, int height, LockToSurfaceCallback callback)
        {
            ThrowWhenDisposed();

            var rect = new SDL_Rect { x = x, y = y, w = width, h = height };
            SDL_Surface* surfaceHandle;
            Error.ThrowLastErrorIfNegative(
                Interop.SDL.LockTextureToSurface(_handle, &rect, &surfaceHandle)
            );
            var surface = new Surface(surfaceHandle, false);
            callback.Invoke(surface);
            Interop.SDL.UnlockTexture(_handle);
        }

        private void ThrowWhenDisposed()
        {
            ObjectDisposedException.ThrowIf(_handle is null, this);
        }
    }
}
