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
using System.Collections.Generic;
using SDL2Sharp.Interop;
using SDL2Sharp.Video.Colors;

namespace SDL2Sharp.Video
{
    public sealed class Surface<TPackedPixel> : IDisposable
        where TPackedPixel : struct, IPackedPixel<TPackedPixel>
    {
        private Surface _surface;

        public PixelFormatDescriptor Format => _surface.Format;

        public int Width => _surface.Width;

        public int Height => _surface.Height;

        public int Pitch => _surface.Pitch;

        public IEnumerable<object>? Size { get; internal set; }

        public Surface(int width, int height)
        : this(new Surface(width, height, TPackedPixel.Format))
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

        public void Blit(Surface<TPackedPixel> surface)
        {
            ThrowWhenDisposed();

            ArgumentNullException.ThrowIfNull(surface);

            _surface.Blit(surface._surface);
        }

        public Surface<TTargetPackedPixel> Convert<TTargetPackedPixel>()
            where TTargetPackedPixel : struct, IPackedPixel<TTargetPackedPixel>
        {
            ThrowWhenDisposed();

            var targetPixelFormat = TTargetPackedPixel.Format;
            var targetSurface = _surface.ConvertTo(targetPixelFormat);
            return new Surface<TTargetPackedPixel>(targetSurface);
        }

        public void FillRect(uint color)
        {
            ThrowWhenDisposed();

            _surface.FillRect(color);
        }

        public void WithLock(SurfaceLockCallback<TPackedPixel> callback)
        {
            _surface.WithLock(callback);
        }

        private void ThrowWhenDisposed()
        {
            ObjectDisposedException.ThrowIf(_surface is null, this);
        }

        public static implicit operator Surface(Surface<TPackedPixel> surface)
        {
            ArgumentNullException.ThrowIfNull(surface);

            return surface._surface;
        }
    }
}
