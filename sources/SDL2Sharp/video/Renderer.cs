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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL2Sharp.Interop;

namespace SDL2Sharp
{
    public sealed unsafe class Renderer : FinalizableObject
    {
        private SDL_Renderer* _handle;

        private Texture? _renderTarget;

        public RendererInfo Info
        {
            get
            {
                ThrowIfDisposed();

                var rendererInfo = new SDL_RendererInfo();
                Error.ThrowLastErrorIfNegative(
                    Interop.SDL.GetRendererInfo(_handle, &rendererInfo)
                );
                return new RendererInfo(rendererInfo);
            }
        }

        public Size OutputSize
        {
            get
            {
                ThrowIfDisposed();

                int width, height;
                Error.ThrowLastErrorIfNegative(
                    Interop.SDL.GetRendererOutputSize(_handle, &width, &height)
                );
                return new Size(width, height);
            }
        }

        public int OutputWidth
        {
            get
            {
                ThrowIfDisposed();

                int width;
                Error.ThrowLastErrorIfNegative(
                    Interop.SDL.GetRendererOutputSize(_handle, &width, null)
                );
                return width;
            }
        }

        public int OutputHeight
        {
            get
            {
                ThrowIfDisposed();

                int height;
                Error.ThrowLastErrorIfNegative(
                    Interop.SDL.GetRendererOutputSize(_handle, null, &height)
                );
                return height;
            }
        }

        public Color DrawColor
        {
            get
            {
                ThrowIfDisposed();

                byte r, g, b, a;
                Error.ThrowLastErrorIfNegative(
                    Interop.SDL.GetRenderDrawColor(_handle, &r, &g, &b, &a)
                );
                return new Color(r, g, b, a);
            }
            set
            {
                ThrowIfDisposed();

                Error.ThrowLastErrorIfNegative(
                    Interop.SDL.SetRenderDrawColor(_handle, value.R, value.G, value.B, value.A)
                );
            }
        }

        public BlendMode BlendMode
        {
            get
            {
                ThrowIfDisposed();

                SDL_BlendMode blendMode;
                Error.ThrowLastErrorIfNegative(
                    Interop.SDL.GetRenderDrawBlendMode(_handle, &blendMode)
                );
                return (BlendMode)blendMode;
            }
            set
            {
                ThrowIfDisposed();

                Error.ThrowLastErrorIfNegative(
                    Interop.SDL.SetRenderDrawBlendMode(_handle, (SDL_BlendMode)value)
                );
            }
        }

        public Size LogicalViewSize
        {
            get
            {
                ThrowIfDisposed();

                int width, height;
                Interop.SDL.RenderGetLogicalSize(_handle, &width, &height);
                return new Size(width, height);
            }
            set
            {
                ThrowIfDisposed();

                Error.ThrowLastErrorIfNegative(
                    Interop.SDL.RenderSetLogicalSize(_handle, value.Width, value.Height)
                );
            }
        }

        public Scale Scale
        {
            get
            {

                ThrowIfDisposed();

                float scaleX, scaleY;
                Interop.SDL.RenderGetScale(_handle, &scaleX, &scaleY);
                return new Scale(scaleX, scaleY);
            }
            set
            {
                ThrowIfDisposed();

                Error.ThrowLastErrorIfNegative(
                    Interop.SDL.RenderSetScale(_handle, value.X, value.Y)
                );
            }
        }

        public Rectangle ViewPort
        {
            get
            {
                ThrowIfDisposed();

                var rect = new SDL_Rect();
                Interop.SDL.RenderGetViewport(_handle, &rect);
                return new Rectangle(rect.x, rect.y, rect.w, rect.h);
            }
            set
            {
                ThrowIfDisposed();

                var rect = new SDL_Rect { x = value.X, y = value.Y, w = value.Width, h = value.Height };
                Error.ThrowLastErrorIfNegative(
                    Interop.SDL.RenderSetViewport(_handle, &rect)
                );
            }
        }

        public Texture? Target
        {
            get
            {
                ThrowIfDisposed();

                return _renderTarget;
            }
            set
            {
                ThrowIfDisposed();

                if (value is null)
                {
                    Error.ThrowLastErrorIfNegative(
                        Interop.SDL.SetRenderTarget(_handle, null)
                    );
                }
                else
                {
                    Error.ThrowLastErrorIfNegative(
                        Interop.SDL.SetRenderTarget(_handle, value.Handle)
                    );
                }

                _renderTarget = value;
            }
        }

        internal Renderer(SDL_Renderer* renderer)
        {
            ArgumentNullException.ThrowIfNull(renderer);

            _handle = renderer;
        }

        protected override void Dispose(bool disposing)
        {
            if (_handle is null) return;
            Interop.SDL.DestroyRenderer(_handle);
            _handle = null;
        }

        public Texture CreateTexture(PixelFormat pixelFormat, TextureAccess access, Size size)
        {
            return CreateTexture(pixelFormat, access, size.Width, size.Height);
        }

        public Texture CreateTexture(PixelFormat pixelFormat, TextureAccess access, int width, int height)
        {
            ThrowIfDisposed();

            var texture = Interop.SDL.CreateTexture(_handle, (uint)pixelFormat, (int)access, width, height);
            Error.ThrowLastErrorIfNull(texture);
            return new Texture(texture);
        }

        public Texture CreateTextureFromSurface(Surface surface)
        {
            ThrowIfDisposed();

            var texture = Interop.SDL.CreateTextureFromSurface(_handle, surface.Handle);
            Error.ThrowLastErrorIfNull(texture);
            return new Texture(texture);
        }

        public void Clear()
        {
            ThrowIfDisposed();

            Error.ThrowLastErrorIfNegative(
                Interop.SDL.RenderClear(_handle)
            );
        }

        public void Copy(Texture texture)
        {
            ThrowIfDisposed();

            Error.ThrowLastErrorIfNegative(
                Interop.SDL.RenderCopy(_handle, texture.Handle, null, null)
            );
        }

        public void Copy(Texture texture, Rectangle destination)
        {
            ThrowIfDisposed();

            var dest = new SDL_Rect
            {
                x = destination.X,
                y = destination.Y,
                w = destination.Width,
                h = destination.Height
            };

            Error.ThrowLastErrorIfNegative(
                Interop.SDL.RenderCopy(_handle, texture.Handle, null, &dest)
            );
        }

        public void Copy(Texture texture, Rectangle source, Rectangle destination)
        {
            ThrowIfDisposed();

            var src = new SDL_Rect
            {
                x = source.X,
                y = source.Y,
                w = source.Width,
                h = source.Height
            };

            var dest = new SDL_Rect
            {
                x = destination.X,
                y = destination.Y,
                w = destination.Width,
                h = destination.Height
            };

            Error.ThrowLastErrorIfNegative(
                Interop.SDL.RenderCopy(_handle, texture.Handle, &src, &dest)
            );
        }

        public void DrawLine(int x1, int y1, int x2, int y2)
        {
            ThrowIfDisposed();

            Error.ThrowLastErrorIfNegative(
                Interop.SDL.RenderDrawLine(_handle, x1, y1, x2, y2)
            );
        }

        public void DrawLines(Point[] points)
        {
            ThrowIfDisposed();

            fixed (Point* point = &points[0])
            {
                Error.ThrowLastErrorIfNegative(
                    Interop.SDL.RenderDrawLines(_handle, (SDL_Point*)point, points.Length)
                );
            }
        }

        public void DrawLine(float x1, float y1, float x2, float y2)
        {
            ThrowIfDisposed();

            Error.ThrowLastErrorIfNegative(
                Interop.SDL.RenderDrawLineF(_handle, x1, y1, x2, y2)
            );
        }

        public void DrawPoint(int x, int y)
        {
            ThrowIfDisposed();

            Error.ThrowLastErrorIfNegative(
                Interop.SDL.RenderDrawPoint(_handle, x, y)
            );
        }

        public void DrawPoint(float x, float y)
        {
            ThrowIfDisposed();

            Error.ThrowLastErrorIfNegative(
                Interop.SDL.RenderDrawPointF(_handle, x, y)
            );
        }

        public void DrawPoints(Point[] points)
        {
            ThrowIfDisposed();

            fixed (Point* point = &points[0])
            {
                Error.ThrowLastErrorIfNegative(
                    Interop.SDL.RenderDrawPoints(_handle, (SDL_Point*)point, points.Length)
                );
            }
        }

        public void FillRect(int x, int y, int width, int height)
        {
            ThrowIfDisposed();

            var rect = new SDL_Rect { x = x, y = y, w = width, h = height };
            Error.ThrowLastErrorIfNegative(
                Interop.SDL.RenderFillRect(_handle, &rect)
            );
        }

        public void FillRect(Rectangle rectangle)
        {
            FillRect(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        public void Present()
        {
            ThrowIfDisposed();

            Interop.SDL.RenderPresent(_handle);
        }

        public PackedMemoryImage<TPackedPixel> ReadPixels<TPackedPixel>()
            where TPackedPixel : struct, IPackedPixel<TPackedPixel>
        {
            ThrowIfDisposed();

            return ReadPixels<TPackedPixel>(
                new Rectangle(0, 0, OutputWidth, OutputHeight)
            );
        }

        public PackedMemoryImage<TPackedPixel> ReadPixels<TPackedPixel>(Rectangle rectangle)
            where TPackedPixel : struct, IPackedPixel<TPackedPixel>
        {
            ThrowIfDisposed();

            var rect = new SDL_Rect { x = rectangle.X, y = rectangle.Y, w = rectangle.Width, h = rectangle.Height };
            var format = (uint)TPackedPixel.Format;
            var image = new PackedMemoryImage<TPackedPixel>(rectangle.Width, rectangle.Height);
            var pixels = Unsafe.AsPointer(ref image.DangerousGetReference());
            var pitch = rectangle.Width * Marshal.SizeOf<TPackedPixel>();
            Error.ThrowLastErrorIfNegative(
                Interop.SDL.RenderReadPixels(_handle,
                    &rect,
                    format,
                    pixels,
                    pitch)
            );
            return image;
        }
    }
}
