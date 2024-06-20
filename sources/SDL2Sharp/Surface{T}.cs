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
    public sealed class Surface<TPackedColor> : IDisposable where TPackedColor : struct
    {
        private Surface _surface;

        public PixelFormat Format => _surface.Format;

        public int Width => _surface.Width;

        public int Height => _surface.Height;

        public int Pitch => _surface.Pitch;

        public Surface(int width, int height)
        : this(new Surface(width, height, (PixelFormatEnum)PackedColorAttribute.GetPixelFormatOf<TPackedColor>()))
        { }

        internal unsafe Surface(SDL_Surface* surface)
        : this(new Surface(surface))
        { }

        internal unsafe Surface(SDL_Surface* surface, bool freeHandle)
        : this(new Surface(surface, freeHandle))
        { }

        internal Surface(Surface surface)
        {
            _surface = surface ?? throw new ArgumentNullException(nameof(surface));
        }

        ~Surface()
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
            if (_surface is null) return;
            _surface.Dispose();
            _surface = null!;
        }

        public void Blit(Surface<TPackedColor> surface)
        {
            ThrowWhenDisposed();

            if (surface is null)
            {
                throw new ArgumentNullException(nameof(surface));
            }

            _surface.Blit(surface._surface);
        }

        public Surface<TTargetColor> Convert<TTargetColor>() where TTargetColor : struct
        {
            ThrowWhenDisposed();

            var targetPixelFormat = PackedColorAttribute.GetPixelFormatOf<TTargetColor>();
            var targetSurface = _surface.ConvertTo((PixelFormatEnum)targetPixelFormat);
            return new Surface<TTargetColor>(targetSurface);
        }

        public void FillRect(uint color)
        {
            ThrowWhenDisposed();

            _surface.FillRect(color);
        }

        public void WithLock(WithLockPackedImageCallback<TPackedColor> callback)
        {
            _surface.WithLock(callback);
        }

        private void ThrowWhenDisposed()
        {
            if (_surface is null)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }

        public static implicit operator Surface(Surface<TPackedColor> surface)
        {
            if (surface is null)
            {
                throw new ArgumentNullException(nameof(surface));
            }

            return surface._surface;
        }
    }
}
