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
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SDL2Sharp
{

    public sealed unsafe class Window : FinalizableObject
    {
        private SDL_Window* _handle;

        public uint Id
        {
            get
            {
                ThrowIfDisposed();

                return Interop.SDL.GetWindowID(_handle);
            }
        }

        public string Title
        {
            get
            {
                ThrowIfDisposed();

                return new string(Interop.SDL.GetWindowTitle(_handle));
            }
            set
            {
                ThrowIfDisposed();

                using var marshaledValue = new MarshaledString(value);
                Interop.SDL.SetWindowTitle(_handle, marshaledValue);
            }
        }

        public Point Position
        {
            get
            {
                ThrowIfDisposed();

                int x, y;
                Interop.SDL.GetWindowPosition(_handle, &x, &y);
                return new Point(x, y);
            }
            set
            {
                ThrowIfDisposed();

                Interop.SDL.SetWindowPosition(_handle, value.X, value.Y);
            }
        }

        public int Width
        {
            get
            {
                ThrowIfDisposed();

                int width;
                Interop.SDL.GetWindowSize(_handle, &width, null);
                return width;
            }
        }

        public int Height
        {
            get
            {
                ThrowIfDisposed();

                int height;
                Interop.SDL.GetWindowSize(_handle, null, &height);
                return height;
            }
        }

        public Size Size
        {
            get
            {
                ThrowIfDisposed();

                int width, height;
                Interop.SDL.GetWindowSize(_handle, &width, &height);
                return new Size(width, height);
            }
            set
            {
                ThrowIfDisposed();

                Interop.SDL.SetWindowSize(_handle, value.Width, value.Height);
            }
        }

        public Size MinimumSize
        {
            get
            {
                ThrowIfDisposed();

                int width, height;
                Interop.SDL.GetWindowMinimumSize(_handle, &width, &height);
                return new Size(width, height);
            }
            set
            {
                ThrowIfDisposed();

                Interop.SDL.SetWindowMinimumSize(_handle, value.Width, value.Height);
            }
        }

        public Size MaximumSize
        {
            get
            {
                ThrowIfDisposed();

                int width, height;
                Interop.SDL.GetWindowMaximumSize(_handle, &width, &height);
                return new Size(width, height);
            }
            set
            {
                ThrowIfDisposed();

                Interop.SDL.SetWindowMaximumSize(_handle, value.Width, value.Height);
            }
        }

        public Size ClientSize
        {
            get
            {
                ThrowIfDisposed();

                int borderTop, borderLeft, borderBottom, borderRight;
                Error.ThrowLastErrorIfNegative(
                    Interop.SDL.GetWindowBordersSize(_handle, &borderTop, &borderLeft, &borderBottom, &borderRight)
                );
                int windowWidth, windowHeight;
                Interop.SDL.GetWindowSize(_handle, &windowWidth, &windowHeight);
                var clientWidth = windowWidth - borderRight - borderLeft;
                var clientHeight = windowHeight - borderBottom - borderTop;
                return new Size(clientWidth, clientHeight);
            }
        }

        public PixelFormat PixelFormat
        {
            get
            {
                ThrowIfDisposed();

                var pixelFormat = Interop.SDL.GetWindowPixelFormat(_handle);
                Error.ThrowLastErrorIfZero(pixelFormat);
                return (PixelFormat)pixelFormat;
            }
        }

        public Surface Surface
        {
            get
            {
                ThrowIfDisposed();

                var surfaceHandle = Interop.SDL.GetWindowSurface(_handle);
                Error.ThrowLastErrorIfNull(surfaceHandle);
                return new Surface(surfaceHandle, false);
            }
        }

        public bool IsBordered
        {
            get
            {
                ThrowIfDisposed();

                return HasWindowFlag(SDL_WindowFlags.SDL_WINDOW_BORDERLESS);
            }
            set
            {
                ThrowIfDisposed();

                Interop.SDL.SetWindowBordered(_handle, value ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE);
            }
        }

        public bool IsResizable
        {
            get
            {
                ThrowIfDisposed();

                return HasWindowFlag(SDL_WindowFlags.SDL_WINDOW_RESIZABLE);
            }
            set
            {
                ThrowIfDisposed();

                Interop.SDL.SetWindowResizable(_handle, value ? SDL_bool.SDL_TRUE : SDL_bool.SDL_FALSE);
            }
        }

        public bool IsFullScreen
        {
            get
            {
                ThrowIfDisposed();

                return HasWindowFlag(SDL_WindowFlags.SDL_WINDOW_FULLSCREEN);
            }
            set
            {
                ThrowIfDisposed();

                var flags = value ? SDL_WindowFlags.SDL_WINDOW_FULLSCREEN : 0;
                Error.ThrowLastErrorIfNegative(
                    Interop.SDL.SetWindowFullscreen(_handle, (uint)flags)
                );
            }
        }

        public bool IsFullScreenDesktop
        {
            get
            {
                ThrowIfDisposed();

                return HasWindowFlag(SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP);
            }
            set
            {
                ThrowIfDisposed();

                var flags = value ? SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP : 0;
                Error.ThrowLastErrorIfNegative(
                    Interop.SDL.SetWindowFullscreen(_handle, (uint)flags)
                );
            }
        }

        public bool IsVisible
        {
            get
            {
                ThrowIfDisposed();

                return HasWindowFlag(SDL_WindowFlags.SDL_WINDOW_SHOWN);
            }
        }

        internal Window(string title, int width, int height)
        : this(title, (int)Interop.SDL.SDL_WINDOWPOS_UNDEFINED, (int)Interop.SDL.SDL_WINDOWPOS_UNDEFINED, width, height, (uint)0)
        { }

        internal Window(string title, int width, int height, WindowFlags flags)
        : this(title, (int)Interop.SDL.SDL_WINDOWPOS_UNDEFINED, (int)Interop.SDL.SDL_WINDOWPOS_UNDEFINED, width, height, (uint)flags)
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
                Interop.SDL.CreateWindow(marshaledTitle, x, y, width, height, flags)
            );

            if (!IsBordered)
            {
                Error.ThrowLastErrorIfNegative(
                    Interop.SDL.SetWindowHitTest(_handle, &HitTestCallback, null)
                );
            }
        }

        protected override void Dispose(bool _)
        {
            if (_handle is null) return;
            Interop.SDL.DestroyWindow(_handle);
            _handle = null;
        }

        public void Show()
        {
            ThrowIfDisposed();

            Interop.SDL.ShowWindow(_handle);
        }

        public void Hide()
        {
            ThrowIfDisposed();

            Interop.SDL.HideWindow(_handle);
        }

        public void Raise()
        {
            ThrowIfDisposed();

            Interop.SDL.RaiseWindow(_handle);
        }

        public void Maximize()
        {
            ThrowIfDisposed();

            Interop.SDL.MaximizeWindow(_handle);
        }

        public void Minimize()
        {
            ThrowIfDisposed();

            Interop.SDL.MinimizeWindow(_handle);
        }

        public void Restore()
        {
            ThrowIfDisposed();

            Interop.SDL.RestoreWindow(_handle);
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
            ThrowIfDisposed();

            var handle = Interop.SDL.CreateRenderer(_handle, -1, (uint)flags);
            if (handle is null)
            {
                error = new Error(new string(Interop.SDL.GetError()));
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
            ThrowIfDisposed();

            Error.ThrowLastErrorIfNegative(
                Interop.SDL.UpdateWindowSurface(_handle)
            );
        }

        private bool HasWindowFlag(SDL_WindowFlags flag)
        {
            var flags = (SDL_WindowFlags)Interop.SDL.GetWindowFlags(_handle);
            var hasFlag = flags.HasFlag(flag);
            return hasFlag;
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        private static unsafe SDL_HitTestResult HitTestCallback(SDL_Window* win, SDL_Point* area, void* data)
        {
            var x = area->x;
            var y = area->y;

            int windowWidth = 0, windowHeight = 0;
            Interop.SDL.GetWindowSize(win, &windowWidth, &windowHeight);

            var windowTop = 0;
            var windowLeft = 0;
            var windowBottom = windowTop + windowHeight;
            var windowRight = windowLeft + windowWidth;

            var borderTop = 0;
            var borderLeft = 0;
            var borderBottom = 0;
            var borderRight = 0;
#pragma warning disable CA1806 // Do not ignore method results
            Interop.SDL.GetWindowBordersSize(win, &borderTop, &borderLeft, &borderBottom, &borderRight);
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
