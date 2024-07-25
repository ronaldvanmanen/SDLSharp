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
using SDL2Sharp.Internals;
using SDL2Sharp.Interop;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SDL2Sharp.Video
{
    public sealed unsafe class Window : IDisposable
    {
        private SDL_Window* _handle;

        public uint Id
        {
            get
            {
                ThrowWhenDisposed();

                return SDL.GetWindowID(_handle);
            }
        }

        public string Title
        {
            get
            {
                ThrowWhenDisposed();

                return new string(SDL.GetWindowTitle(_handle));
            }
            set
            {
                ThrowWhenDisposed();

                using var marshaledValue = new MarshaledString(value);
                SDL.SetWindowTitle(_handle, marshaledValue);
            }
        }

        public Point Position
        {
            get
            {
                ThrowWhenDisposed();

                int x, y;
                SDL.GetWindowPosition(_handle, &x, &y);
                return new Point(x, y);
            }
            set
            {
                ThrowWhenDisposed();

                SDL.SetWindowPosition(_handle, value.X, value.Y);
            }
        }

        public int Width
        {
            get
            {
                ThrowWhenDisposed();

                int width;
                SDL.GetWindowSize(_handle, &width, null);
                return width;
            }
        }

        public int Height
        {
            get
            {
                ThrowWhenDisposed();

                int height;
                SDL.GetWindowSize(_handle, null, &height);
                return height;
            }
        }

        public Size Size
        {
            get
            {
                ThrowWhenDisposed();

                int width, height;
                SDL.GetWindowSize(_handle, &width, &height);
                return new Size(width, height);
            }
            set
            {
                ThrowWhenDisposed();

                SDL.SetWindowSize(_handle, value.Width, value.Height);
            }
        }

        public Size MinimumSize
        {
            get
            {
                ThrowWhenDisposed();

                int width, height;
                SDL.GetWindowMinimumSize(_handle, &width, &height);
                return new Size(width, height);
            }
            set
            {
                ThrowWhenDisposed();

                SDL.SetWindowMinimumSize(_handle, value.Width, value.Height);
            }
        }

        public Size MaximumSize
        {
            get
            {
                ThrowWhenDisposed();

                int width, height;
                SDL.GetWindowMaximumSize(_handle, &width, &height);
                return new Size(width, height);
            }
            set
            {
                ThrowWhenDisposed();

                SDL.SetWindowMaximumSize(_handle, value.Width, value.Height);
            }
        }

        public Size ClientSize
        {
            get
            {
                ThrowWhenDisposed();

                int borderTop, borderLeft, borderBottom, borderRight;
                Error.ThrowLastErrorIfNegative(
                    SDL.GetWindowBordersSize(_handle, &borderTop, &borderLeft, &borderBottom, &borderRight)
                );
                int windowWidth, windowHeight;
                SDL.GetWindowSize(_handle, &windowWidth, &windowHeight);
                var clientWidth = windowWidth - borderRight - borderLeft;
                var clientHeight = windowHeight - borderBottom - borderTop;
                return new Size(clientWidth, clientHeight);
            }
        }

        public PixelFormat PixelFormat
        {
            get
            {
                ThrowWhenDisposed();

                var pixelFormat = SDL.GetWindowPixelFormat(_handle);
                Error.ThrowLastErrorIfZero(pixelFormat);
                return (PixelFormat)pixelFormat;
            }
        }

        public Surface Surface
        {
            get
            {
                ThrowWhenDisposed();

                var surfaceHandle = SDL.GetWindowSurface(_handle);
                Error.ThrowLastErrorIfNull(surfaceHandle);
                return new Surface(surfaceHandle, false);
            }
        }

        public bool IsBordered
        {
            get
            {
                ThrowWhenDisposed();

                return HasWindowFlag(SDL_WindowFlags.SDL_WINDOW_BORDERLESS);
            }
            set
            {
                ThrowWhenDisposed();

                SDL.SetWindowBordered(_handle, value ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE);
            }
        }

        public bool IsResizable
        {
            get
            {
                ThrowWhenDisposed();

                return HasWindowFlag(SDL_WindowFlags.SDL_WINDOW_RESIZABLE);
            }
            set
            {
                ThrowWhenDisposed();

                SDL.SetWindowResizable(_handle, value ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE);
            }
        }

        public bool IsFullScreen
        {
            get
            {
                ThrowWhenDisposed();

                return HasWindowFlag(SDL_WindowFlags.SDL_WINDOW_FULLSCREEN);
            }
            set
            {
                ThrowWhenDisposed();

                var flags = value ? SDL_WindowFlags.SDL_WINDOW_FULLSCREEN : 0;
                Error.ThrowLastErrorIfNegative(
                    SDL.SetWindowFullscreen(_handle, (uint)flags)
                );
            }
        }

        public bool IsFullScreenDesktop
        {
            get
            {
                ThrowWhenDisposed();

                return HasWindowFlag(SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP);
            }
            set
            {
                ThrowWhenDisposed();

                var flags = value ? SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP : 0;
                Error.ThrowLastErrorIfNegative(
                    SDL.SetWindowFullscreen(_handle, (uint)flags)
                );
            }
        }

        public bool IsVisible
        {
            get
            {
                ThrowWhenDisposed();

                return HasWindowFlag(SDL_WindowFlags.SDL_WINDOW_SHOWN);
            }
        }

        internal Window(string title, int width, int height)
        : this(title, (int)SDL.SDL_WINDOWPOS_UNDEFINED, (int)SDL.SDL_WINDOWPOS_UNDEFINED, width, height, (uint)0)
        { }

        internal Window(string title, int width, int height, WindowFlags flags)
        : this(title, (int)SDL.SDL_WINDOWPOS_UNDEFINED, (int)SDL.SDL_WINDOWPOS_UNDEFINED, width, height, (uint)flags)
        { }

        internal Window(string title, int x, int y, int width, int height)
        : this(title, x, y, width, height, (uint)0)
        { }

        internal Window(string title, int x, int y, int width, int height, WindowFlags flags)
        : this(title, x, y, width, height, (uint)flags)
        { }

        private Window(string title, int x, int y, int width, int height, uint flags)
        {
            using var marshaledTitle = new MarshaledString(title);
            _handle = Error.ThrowLastErrorIfNull(
                SDL.CreateWindow(marshaledTitle, x, y, width, height, flags)
            );

            if (!IsBordered)
            {
                Error.ThrowLastErrorIfNegative(
                    SDL.SetWindowHitTest(_handle, &HitTestCallback, null)
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
            if (_handle is null) return;
            SDL.DestroyWindow(_handle);
            _handle = null;
        }

        public void Show()
        {
            ThrowWhenDisposed();

            SDL.ShowWindow(_handle);
        }

        public void Hide()
        {
            ThrowWhenDisposed();

            SDL.HideWindow(_handle);
        }

        public void Raise()
        {
            ThrowWhenDisposed();

            SDL.RaiseWindow(_handle);
        }

        public void Maximize()
        {
            ThrowWhenDisposed();

            SDL.MaximizeWindow(_handle);
        }

        public void Minimize()
        {
            ThrowWhenDisposed();

            SDL.MinimizeWindow(_handle);
        }

        public void Restore()
        {
            ThrowWhenDisposed();

            SDL.RestoreWindow(_handle);
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

        public bool TryCreateRenderer(out Renderer renderer)
        {
            return TryCreateRenderer(RendererFlags.None, out renderer);
        }

        public bool TryCreateRenderer(RendererFlags flags, out Renderer renderer)
        {
            return TryCreateRenderer(flags, out renderer, out _);
        }

        public bool TryCreateRenderer(RendererFlags flags, out Renderer renderer, out Error error)
        {
            ThrowWhenDisposed();

            var handle = SDL.CreateRenderer(_handle, -1, (uint)flags);
            if (handle is null)
            {
                error = new Error(new string(SDL.GetError()));
                renderer = null!;
                return false;
            }
            else
            {
                error = null!;
                renderer = new Renderer(handle);
                return true;
            }
        }

        public void UpdateSurface()
        {
            ThrowWhenDisposed();

            Error.ThrowLastErrorIfNegative(
                SDL.UpdateWindowSurface(_handle)
            );
        }

        private bool HasWindowFlag(SDL_WindowFlags flag)
        {
            var flags = (SDL_WindowFlags)SDL.GetWindowFlags(_handle);
            var hasFlag = flags.HasFlag(flag);
            return hasFlag;
        }

        private void ThrowWhenDisposed()
        {
            ObjectDisposedException.ThrowIf(_handle is null, this);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        private static unsafe SDL_HitTestResult HitTestCallback(SDL_Window* win, SDL_Point* area, void* data)
        {
            var x = area->x;
            var y = area->y;

            int windowWidth = 0, windowHeight = 0;
            SDL.GetWindowSize(win, &windowWidth, &windowHeight);

            var windowTop = 0;
            var windowLeft = 0;
            var windowBottom = windowTop + windowHeight;
            var windowRight = windowLeft + windowWidth;

            var borderTop = 0;
            var borderLeft = 0;
            var borderBottom = 0;
            var borderRight = 0;
#pragma warning disable CA1806 // Do not ignore method results
            SDL.GetWindowBordersSize(win, &borderTop, &borderLeft, &borderBottom, &borderRight);
#pragma warning restore CA1806 // Do not ignore method results

            var clientAreaTop = windowTop + borderTop;
            var clientAreaLeft = windowLeft + borderLeft;
            var clientAreaBottom = windowBottom - borderBottom;
            var clientAreaRight = windowRight - borderRight;

            if (y > windowTop && y < clientAreaTop)
            {
                return SDL_HitTestResult.SDL_HITTEST_DRAGGABLE;
            }

            if (y >= windowTop && y <= clientAreaTop)
            {
                if (x >= windowLeft && x <= clientAreaLeft)
                {
                    return SDL_HitTestResult.SDL_HITTEST_RESIZE_TOPLEFT;
                }

                if (x >= clientAreaRight && x <= windowRight)
                {
                    return SDL_HitTestResult.SDL_HITTEST_RESIZE_TOPRIGHT;
                }

                return SDL_HitTestResult.SDL_HITTEST_RESIZE_TOP;
            }

            if (y >= clientAreaBottom && y <= windowBottom)
            {
                if (x >= windowLeft && x <= clientAreaLeft)
                {
                    return SDL_HitTestResult.SDL_HITTEST_RESIZE_BOTTOMLEFT;
                }

                if (x >= clientAreaRight && x <= windowRight)
                {
                    return SDL_HitTestResult.SDL_HITTEST_RESIZE_BOTTOMRIGHT;
                }

                return SDL_HitTestResult.SDL_HITTEST_RESIZE_BOTTOM;
            }

            if (x >= windowLeft && x <= clientAreaLeft)
            {
                return SDL_HitTestResult.SDL_HITTEST_RESIZE_LEFT;
            }

            if (x >= clientAreaRight && x <= windowRight)
            {
                return SDL_HitTestResult.SDL_HITTEST_RESIZE_RIGHT;
            }

            return SDL_HitTestResult.SDL_HITTEST_NORMAL;
        }
    }
}
