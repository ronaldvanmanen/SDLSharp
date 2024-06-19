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
using System.Runtime.InteropServices;
using SDL2Sharp.Interop;

namespace SDL2Sharp
{
    public sealed unsafe class Texture : IDisposable
    {
        private SDL_Texture* _handle;

        public PixelFormatEnum Format
        {
            get
            {
                uint format;
                Error.ThrowOnFailure(SDL.QueryTexture(_handle, &format, null, null, null));
                return (PixelFormatEnum)format;
            }
        }

        public TextureAccess Access
        {
            get
            {
                int access;
                Error.ThrowOnFailure(SDL.QueryTexture(_handle, null, &access, null, null));
                return (TextureAccess)access;
            }
        }

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

        public BlendMode BlendMode
        {
            get
            {
                SDL_BlendMode blendMode;
                Error.ThrowOnFailure(
                    SDL.GetTextureBlendMode(_handle, &blendMode)
                );
                return (BlendMode)blendMode;
            }
            set
            {
                Error.ThrowOnFailure(
                    SDL.SetTextureBlendMode(_handle, (SDL_BlendMode)value)
                );
            }
        }

        public bool IsValid => 0 == SDL.QueryTexture(_handle, null, null, null, null);

        internal Texture(SDL_Texture* texture)
        {
            if (texture is null)
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
            if (_handle is null) return;
            SDL.DestroyTexture(_handle);
            _handle = null;
        }

        public void WithLock<TPackedColor>(WithLockPackedImageCallback<TPackedColor> callback)
            where TPackedColor : struct
        {
            WithLock(0, 0, Width, Height, callback);
        }

        public void WithLock<TPackedColor>(Rectangle rectangle, WithLockPackedImageCallback<TPackedColor> callback)
            where TPackedColor : struct
        {
            WithLock(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, callback);
        }

        public void WithLock<TPackedColor>(int x, int y, int width, int height, WithLockPackedImageCallback<TPackedColor> callback)
            where TPackedColor : struct
        {
            ThrowWhenDisposed();

            var rect = new SDL_Rect { x = x, y = y, w = width, h = height };
            void* pixels;
            int pitchInBytes;
            Error.ThrowOnFailure(
                SDL.LockTexture(_handle, &rect, &pixels, &pitchInBytes)
            );

            var bytesPerPixel = Marshal.SizeOf<TPackedColor>();
            var pitch = pitchInBytes / bytesPerPixel;
            var image = new PackedImage<TPackedColor>(pixels, height, width, pitch);
            callback.Invoke(image);
            SDL.UnlockTexture(_handle);
        }

        public void WithLock<TPackedColor>(WithLockSurfaceCallback<TPackedColor> callback)
            where TPackedColor : struct
        {
            WithLock(0, 0, Width, Height, callback);
        }

        public void WithLock<TPackedColor>(Rectangle rectangle, WithLockSurfaceCallback<TPackedColor> callback)
            where TPackedColor : struct
        {
            WithLock(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, callback);
        }

        public void WithLock<TPackedColor>(int x, int y, int width, int height, WithLockSurfaceCallback<TPackedColor> callback)
            where TPackedColor : struct
        {
            ThrowWhenDisposed();

            var rect = new SDL_Rect { x = x, y = y, w = width, h = height };
            SDL_Surface* surfaceHandle;
            Error.ThrowOnFailure(
                SDL.LockTextureToSurface(_handle, &rect, &surfaceHandle)
            );
            var surface = new Surface<TPackedColor>(surfaceHandle, false);
            callback.Invoke(surface);
            SDL.UnlockTexture(_handle);
        }

        private void ThrowWhenDisposed()
        {
            if (_handle is null)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
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
