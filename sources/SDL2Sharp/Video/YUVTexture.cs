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
using SDL2Sharp.Interop;
using SDL2Sharp.Video.PixelFormats;

namespace SDL2Sharp.Video
{
    public sealed unsafe partial class YUVTexture<TYUVFormat> : IDisposable where TYUVFormat : IYUVFormat
    {
        public delegate void LockCallback(YUVImage<TYUVFormat> pixels);

        private Texture _texture;

        public PixelFormat Format => _texture.Format;

        public TextureAccess Access => _texture.Access;

        public int Width => _texture.Width;

        public int Height => _texture.Height;

        public BlendMode BlendMode
        {
            get => _texture.BlendMode;

            set => _texture.BlendMode = value;
        }

        public bool IsValid => _texture.IsValid;

        internal YUVTexture(Texture texture)
        {
            ArgumentNullException.ThrowIfNull(texture);

            if (texture.Format != TYUVFormat.PixelFormat)
            {
                throw new ArgumentException($"Texture is not in a {TYUVFormat.PixelFormat} format", nameof(texture));
            }

            _texture = texture;
        }

        ~YUVTexture()
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
            int pitch;
            Error.ThrowLastErrorIfNegative(
                SDL.LockTexture(_texture.Handle, &rect, &pixels, &pitch)
            );

            var image = new YUVImage<TYUVFormat>(pixels, width, height, pitch);
            callback.Invoke(image);
            SDL.UnlockTexture(_texture.Handle);
        }

        public void Update(YUVImage<TYUVFormat> image)
        {
            Error.ThrowLastErrorIfNegative(
                SDL.UpdateYUVTexture(_texture.Handle, null,
                    (byte*)image.Y, image.Y.Pitch,
                    (byte*)image.U, image.U.Pitch,
                    (byte*)image.V, image.V.Pitch
                )
            );
        }

        public void Update(YUVMemoryImage image)
        {
            Error.ThrowLastErrorIfNegative(
                SDL.UpdateYUVTexture(_texture.Handle, null,
                    (byte*)image.Y, image.Y.Pitch,
                    (byte*)image.U, image.U.Pitch,
                    (byte*)image.V, image.V.Pitch
                )
            );
        }

        private void ThrowWhenDisposed()
        {
            ObjectDisposedException.ThrowIf(_texture is null, this);
        }

        public static implicit operator Texture(YUVTexture<TYUVFormat> texture)
        {
            ArgumentNullException.ThrowIfNull(texture);

            return texture._texture;
        }
    }
}
