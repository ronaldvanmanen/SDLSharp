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
using System.Diagnostics.CodeAnalysis;
using SDL2Sharp.Internals;
using SDL2Sharp.Interop;

namespace SDL2Sharp
{
    public unsafe sealed class Window : IDisposable
    {
        private SDL_Window* _window;

        public string Title
        {
            get
            {
                return new string(SDL.GetWindowTitle(_window));
            }
            set
            {
                using (var marshaledValue = new MarshaledString(value))
                {
                    SDL.SetWindowTitle(_window, marshaledValue);
                }
            }
        }

        public Point Position
        {
            get
            {
                int x, y;
                SDL.GetWindowPosition(_window, &x, &y);
                return new Point(x, y);
            }
            set
            {
                SDL.SetWindowPosition(_window, value.X, value.Y);
            }
        }

        public Size Size
        {
            get
            {
                int width, height;
                SDL.GetWindowSize(_window, &width, &height);
                return new Size(width, height);
            }
            set
            {
                SDL.SetWindowSize(_window, value.Width, value.Height);
            }
        }

        public Size MinimumSize
        {
            get
            {
                int width, height;
                SDL.GetWindowMinimumSize(_window, &width, &height);
                return new Size(width, height);
            }
            set
            {
                SDL.SetWindowMinimumSize(_window, value.Width, value.Height);
            }
        }

        public Size MaximumSize
        {
            get
            {
                int width, height;
                SDL.GetWindowMaximumSize(_window, &width, &height);
                return new Size(width, height);
            }
            set
            {
                SDL.SetWindowMaximumSize(_window, value.Width, value.Height);
            }
        }

        public Size ClientSize
        {
            get
            {
                int top, left, bottom, right;
                Error.ThrowOnFailure(
                    SDL.GetWindowBordersSize(_window, &top, &left, &bottom, &right)
                );
                return new Size(right - left, bottom - top);
            }
        }

        public bool IsBordered
        {
            get
            {
                const uint flag = (uint)SDL_WindowFlags.SDL_WINDOW_RESIZABLE;
                var flags = SDL.GetWindowFlags(_window);
                return (flags & flag) != 0;
            }
            set
            {
                SDL.SetWindowBordered(_window, value ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE);
            }
        }

        public bool IsResizable
        {
            get
            {
                const uint flag = (uint)SDL_WindowFlags.SDL_WINDOW_RESIZABLE;
                var flags = SDL.GetWindowFlags(_window);
                return (flags & flag) != 0;
            }
            set
            {
                SDL.SetWindowResizable(_window, value ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE);
            }
        }

        public bool IsFullScreen
        {
            get
            {
                const uint flag = (uint)SDL_WindowFlags.SDL_WINDOW_FULLSCREEN;
                var flags = SDL.GetWindowFlags(_window);
                return (flags & flag) != 0;
            }
            set
            {
                const uint flag = (uint)SDL_WindowFlags.SDL_WINDOW_FULLSCREEN;
                Error.ThrowOnFailure(
                    SDL.SetWindowFullscreen(_window, value ? flag : 0u)
                );
            }
        }

        public bool IsFullScreenDesktop
        {
            get
            {
                const uint flag = (uint)SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP;
                var flags = SDL.GetWindowFlags(_window);
                return (flags & flag) != 0;
            }
            set
            {
                const uint flag = (uint)SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP;
                Error.ThrowOnFailure(
                    SDL.SetWindowFullscreen(_window, value ? flag : 0u)
                );
            }
        }

        public bool IsVisible
        {
            get
            {
                const uint flag = (uint)SDL_WindowFlags.SDL_WINDOW_SHOWN;
                var flags = SDL.GetWindowFlags(_window);
                return (flags & flag) != 0;
            }
        }

        public Window(string title, int width, int height)
        : this(title, (int)SDL.SDL_WINDOWPOS_UNDEFINED, (int)SDL.SDL_WINDOWPOS_UNDEFINED, width, height, (uint)0)
        { }

        public Window(string title, int width, int height, WindowFlags flags)
        : this(title, (int)SDL.SDL_WINDOWPOS_UNDEFINED, (int)SDL.SDL_WINDOWPOS_UNDEFINED, width, height, (uint)flags)
        { }

        public Window(string title, int x, int y, int width, int height)
        : this(title, x, y, width, height, (uint)0)
        { }

        public Window(string title, int x, int y, int width, int height, WindowFlags flags)
        : this(title, x, y, width, height, (uint)flags)
        { }

        private Window(string title, int x, int y, int width, int height, uint flags)
        {
            using (var marshaledTitle = new MarshaledString(title))
            {
                _window = Error.ReturnOrThrowOnFailure(
                    SDL.CreateWindow(marshaledTitle, x, y, width, height, flags)
                );
            }
        }

        ~Window()
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
            if (_window == null) return;
            SDL.DestroyWindow(_window);
            _window = null;
        }

        public void Show()
        {
            SDL.ShowWindow(_window);
        }

        public void Hide()
        {
            SDL.HideWindow(_window);
        }

        public void Raise()
        {
            SDL.RaiseWindow(_window);
        }

        public void Maximize()
        {
            SDL.MaximizeWindow(_window);
        }

        public void Minimize()
        {
            SDL.MinimizeWindow(_window);
        }

        public void Restore()
        {
            SDL.RestoreWindow(_window);
        }

        public Renderer CreateRenderer()
        {
            return CreateRenderer(RendererFlags.None);
        }

        public Renderer CreateRenderer(RendererFlags flags)
        {
            if (!TryCreateRenderer(flags, out var renderer, out var error))
            {
                throw error;
            }

            return renderer;
        }

        public bool TryCreateRenderer([NotNullWhen(true)] out Renderer renderer)
        {
            return TryCreateRenderer(RendererFlags.None, out renderer);
        }

        public bool TryCreateRenderer(RendererFlags flags, [NotNullWhen(true)] out Renderer renderer)
        {
            return TryCreateRenderer(flags, out renderer, out _);
        }

        public bool TryCreateRenderer(RendererFlags flags, [NotNullWhen(true)] out Renderer renderer, [NotNullWhen(false)] out Error error)
        {
            ThrowWhenDisposed();

            var renderPointer = SDL.CreateRenderer(_window, -1, (uint)flags);
            if (renderPointer == null)
            {
                error = new Error(new string(SDL.GetError()));
                renderer = null!;
                return false;
            }
            else
            {
                error = null!;
                renderer = new Renderer(renderPointer);
                return true;
            }
        }

        private void ThrowWhenDisposed()
        {
            if (_window == null)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }
    }
}
