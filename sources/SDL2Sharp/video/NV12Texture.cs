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

namespace SDL2Sharp
{
    public sealed unsafe class NV12Texture : DisposableObject
    {
        public delegate void LockCallback(NV12Image pixels);

        private readonly Texture _texture;

        public PixelFormat Format
        {
            get
            {
                ThrowIfDisposed();

                return _texture.Format;
            }
        }

        public TextureAccess Access
        {
            get
            {
                ThrowIfDisposed();

                return _texture.Access;
            }
        }

        public int Width
        {
            get
            {
                ThrowIfDisposed();

                return _texture.Width;
            }
        }

        public int Height
        {
            get
            {
                ThrowIfDisposed();

                return _texture.Height;
            }
        }

        public Size Size
        {
            get
            {
                ThrowIfDisposed();

                return _texture.Size;
            }
        }

        public BlendMode BlendMode
        {
            get
            {
                ThrowIfDisposed();

                return _texture.BlendMode;
            }
            set
            {
                ThrowIfDisposed();

                _texture.BlendMode = value;
            }
        }

        public bool IsValid
        {
            get
            {
                ThrowIfDisposed();

                return _texture.IsValid;
            }
        }

        internal NV12Texture(Texture texture)
        {
            ArgumentNullException.ThrowIfNull(texture);

            if (texture.Format != PixelFormat.NV12)
            {
                throw new ArgumentException("Texture is not in a NV12 format", nameof(texture));
            }

            _texture = texture;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _texture?.Dispose();
            }
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
            ArgumentNullException.ThrowIfNull(callback);

            ThrowIfDisposed();

            var rect = new SDL_Rect { x = x, y = y, w = width, h = height };
            void* pixels;
            int pitch;
            Error.ThrowLastErrorIfNegative(
                Interop.SDL.LockTexture(_texture.Handle, &rect, &pixels, &pitch)
            );

            var image = new NV12Image(pixels, width, height, pitch);
            callback.Invoke(image);
            Interop.SDL.UnlockTexture(_texture.Handle);
        }

        public void Update(NV12Image image)
        {
            ThrowIfDisposed();

            Error.ThrowLastErrorIfNegative(
                Interop.SDL.UpdateNVTexture(_texture.Handle, null,
                    (byte*)image.Y, image.Y.Pitch,
                    (byte*)image.UV, image.UV.Pitch)
            );
        }

        public void Update(NV12MemoryImage image)
        {
            ArgumentNullException.ThrowIfNull(image);

            ThrowIfDisposed();

            Error.ThrowLastErrorIfNegative(
                Interop.SDL.UpdateNVTexture(_texture.Handle, null,
                    (byte*)image.Y, image.Y.Pitch,
                    (byte*)image.UV, image.UV.Pitch
                )
            );
        }

        public static implicit operator Texture(NV12Texture texture)
        {
            ArgumentNullException.ThrowIfNull(texture);

            return texture._texture;
        }
    }
}
