﻿// SDL2Sharp
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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Toolkit.HighPerformance;
using SDL2Sharp.Interop;
using SDL2Sharp.Video.Colors;

namespace SDL2Sharp.Video
{
    public sealed unsafe class PackedTexture<TPackedPixel> : IDisposable
        where TPackedPixel : struct, IPackedPixel<TPackedPixel>
    {
        public delegate void LockCallback(PackedImage<TPackedPixel> pixels);

        public delegate void LockToSurfaceCallback(Surface<TPackedPixel> surface);

        private Texture _texture;

        public PixelFormat Format => _texture.Format;

        public TextureAccess Access => _texture.Access;

        public int Width => _texture.Width;

        public int Height => _texture.Height;

        public Size Size => _texture.Size;

        public BlendMode BlendMode
        {
            get => _texture.BlendMode;

            set => _texture.BlendMode = value;
        }

        public bool IsValid => _texture.IsValid;

        internal PackedTexture(Texture texture)
        {
            if (!texture.Format.IsPacked())
            {
                throw new ArgumentException("Texture is not in a packed color format.", nameof(texture));
            }

            _texture = texture ?? throw new ArgumentNullException(nameof(texture));
        }

        ~PackedTexture()
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
            if (_texture is null) return;
            _texture.Dispose();
            _texture = null!;
        }

        public void WithLock(LockCallback callback)
        {
            WithLock(0, 0, Width, Height, callback);
        }

        public void WithLock(Rectangle rectangle, LockCallback callback)
        {
            WithLock(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, callback);
        }

        public void WithLock(int x, int y, int width, int height, LockCallback callback)
        {
            ThrowWhenDisposed();

            var rect = new SDL_Rect { x = x, y = y, w = width, h = height };
            void* pixels;
            int pitchInBytes;
            Error.ThrowLastErrorIfNegative(
                SDL.LockTexture(_texture.Handle, &rect, &pixels, &pitchInBytes)
            );

            var bytesPerPixel = Marshal.SizeOf<TPackedPixel>();
            var pitch = pitchInBytes / bytesPerPixel;
            var image = new PackedImage<TPackedPixel>(pixels, width, height, pitch);
            callback.Invoke(image);
            SDL.UnlockTexture(_texture.Handle);
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
                SDL.LockTextureToSurface(_texture.Handle, &rect, &surfaceHandle)
            );
            var surface = new Surface<TPackedPixel>(surfaceHandle, false);
            callback.Invoke(surface);
            SDL.UnlockTexture(_texture.Handle);
        }

        public void Update(PackedMemoryImage<TPackedPixel> image)
        {
            ThrowWhenDisposed();

            var pointer = Unsafe.AsPointer(ref image.DangerousGetReference());
            var pitch = image.Width * Marshal.SizeOf<TPackedPixel>();
            Update(null, pointer, pitch);
        }

        public void Update(PackedImage<TPackedPixel> pixels)
        {
            ThrowWhenDisposed();

            var pointer = Unsafe.AsPointer(ref pixels.DangerousGetReference());
            var pitch = pixels.Width * Marshal.SizeOf<TPackedPixel>();
            Update(null, pointer, pitch);
        }

        public void Update(TPackedPixel[,] pixels)
        {
            ThrowWhenDisposed();

            var pointer = Unsafe.AsPointer(ref pixels.DangerousGetReference());
            var width = pixels.GetLength(1);
            var pitch = width * Marshal.SizeOf<TPackedPixel>();
            Update(null, pointer, pitch);
        }

        private void Update(SDL_Rect* rect, void* pixels, int pitch)
        {
            Error.ThrowLastErrorIfNegative(
                SDL.UpdateTexture(_texture.Handle, rect, pixels, pitch)
            );
        }

        private void ThrowWhenDisposed()
        {
            ObjectDisposedException.ThrowIf(_texture is null, this);
        }

        public static implicit operator Texture(PackedTexture<TPackedPixel> texture)
        {
            ArgumentNullException.ThrowIfNull(texture);

            return texture._texture;
        }
    }
}
