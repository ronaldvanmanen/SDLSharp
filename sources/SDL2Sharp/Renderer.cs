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
    public unsafe sealed class Renderer : IDisposable
    {
        private SDL_Renderer* _renderer;

        internal Renderer(SDL_Renderer* renderer)
        {
            if (renderer == null)
            {
                throw new ArgumentNullException(nameof(renderer));
            }

            _renderer = renderer;
        }

        ~Renderer()
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
            if (_renderer == null) return;
            SDL.DestroyRenderer(_renderer);
            _renderer = null;
        }

        public Texture CreateTexture(uint format, int access, int width, int height)
        {
            ThrowWhenDisposed();

            var texture = SDL.CreateTexture(_renderer, format, access, width, height);
            Error.ThrowOnFailure(texture);
            return new Texture(texture);
        }

        public Texture CreateTextureFromSurface(Surface surface)
        {
            ThrowWhenDisposed();

            var texture = SDL.CreateTextureFromSurface(_renderer, surface);
            Error.ThrowOnFailure(texture);
            return new Texture(texture);
        }

        public void Clear()
        {
            ThrowWhenDisposed();

            Error.ThrowOnFailure(
                SDL.RenderClear(_renderer)
            );
        }

        public void Copy(Texture texture)
        {
            ThrowWhenDisposed();

            Error.ThrowOnFailure(
                SDL.RenderCopy(_renderer, texture, null, null)
            );
        }

        public void Present()
        {
            ThrowWhenDisposed();

            SDL.RenderPresent(_renderer);
        }

        private void ThrowWhenDisposed()
        {
            if (_renderer == null)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }
    }
}