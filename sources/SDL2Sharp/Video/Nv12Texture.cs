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
using SDL2Sharp.Interop;
using SDL2Sharp.Video.Colors;

namespace SDL2Sharp.Video
{
    public unsafe sealed class Nv12Texture : IDisposable
    {
        public delegate void LockCallback(Nv12Image pixels);

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

        internal Nv12Texture(Texture texture)
        {
            _texture = texture ?? throw new ArgumentNullException(nameof(texture));
        }

        ~Nv12Texture()
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
            Error.ThrowOnFailure(
                SDL.LockTexture(_texture, &rect, &pixels, &pitch)
            );

            var image = new Nv12Image(pixels, width, height, pitch);
            callback.Invoke(image);
            SDL.UnlockTexture(_texture);
        }

        public void Update(Nv12Image image)
        {
            var yPlane = (byte*)Unsafe.AsPointer(ref image.Y.DangerousGetReference());
            var yPitch = image.Y.Width * Marshal.SizeOf<Y8>();
            var uvPlane = (byte*)Unsafe.AsPointer(ref image.UV.DangerousGetReference());
            var uvPitch = image.UV.Width * Marshal.SizeOf<UV88>();
            SDL.UpdateNVTexture(_texture, null, yPlane, yPitch, uvPlane, uvPitch);
        }

        public void Update(Nv12MemoryImage image)
        {
            var yPlane = (byte*)Unsafe.AsPointer(ref image.Y.DangerousGetReference());
            var yPitch = image.Y.Width * Marshal.SizeOf<Y8>();
            var uvPlane = (byte*)Unsafe.AsPointer(ref image.UV.DangerousGetReference());
            var uvPitch = image.UV.Width * Marshal.SizeOf<UV88>();
            SDL.UpdateNVTexture(_texture, null, yPlane, yPitch, uvPlane, uvPitch);
        }

        private void ThrowWhenDisposed()
        {
            if (_texture is null)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }

        public static implicit operator Texture(Nv12Texture texture)
        {
            if (texture is null)
            {
                throw new ArgumentNullException(nameof(texture));
            }

            return texture._texture;
        }
    }
}
