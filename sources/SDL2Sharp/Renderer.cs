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

namespace SDL2Sharp
{
    public sealed unsafe class Renderer : IDisposable
    {
        private SDL_Renderer* _handle;

        private Texture? _renderTarget;

        public RendererInfo Info
        {
            get
            {
                ThrowWhenDisposed();

                var rendererInfo = new SDL_RendererInfo();
                Error.ThrowOnFailure(
                    SDL.GetRendererInfo(_handle, &rendererInfo)
                );
                return new RendererInfo(rendererInfo);
            }
        }

        public Size OutputSize
        {
            get
            {
                ThrowWhenDisposed();

                int width, height;
                Error.ThrowOnFailure(
                    SDL.GetRendererOutputSize(_handle, &width, &height)
                );
                return new Size(width, height);
            }
        }

        public Color DrawColor
        {
            get
            {
                ThrowWhenDisposed();

                byte r, g, b, a;
                Error.ThrowOnFailure(
                    SDL.GetRenderDrawColor(_handle, &r, &g, &b, &a)
                );
                return new Color(r, g, b, a);
            }
            set
            {
                ThrowWhenDisposed();

                Error.ThrowOnFailure(
                    SDL.SetRenderDrawColor(_handle, value.R, value.G, value.B, value.A)
                );
            }
        }

        public BlendMode BlendMode
        {
            get
            {
                ThrowWhenDisposed();

                SDL_BlendMode blendMode;
                Error.ThrowOnFailure(
                    SDL.GetRenderDrawBlendMode(_handle, &blendMode)
                );
                return (BlendMode)blendMode;
            }
            set
            {
                ThrowWhenDisposed();

                Error.ThrowOnFailure(
                    SDL.SetRenderDrawBlendMode(_handle, (SDL_BlendMode)value)
                );
            }
        }

        public Size LogicalViewSize
        {
            get
            {
                ThrowWhenDisposed();

                int width, height;
                SDL.RenderGetLogicalSize(_handle, &width, &height);
                return new Size(width, height);
            }
            set
            {
                ThrowWhenDisposed();

                Error.ThrowOnFailure(
                    SDL.RenderSetLogicalSize(_handle, value.Width, value.Height)
                );
            }
        }

        public Scale Scale
        {
            get
            {

                ThrowWhenDisposed();

                float scaleX, scaleY;
                SDL.RenderGetScale(_handle, &scaleX, &scaleY);
                return new Scale(scaleX, scaleY);
            }
            set
            {
                ThrowWhenDisposed();

                Error.ThrowOnFailure(
                    SDL.RenderSetScale(_handle, value.X, value.Y)
                );
            }
        }

        public Rectangle ViewPort
        {
            get
            {
                ThrowWhenDisposed();

                var rect = new SDL_Rect();
                SDL.RenderGetViewport(_handle, &rect);
                return new Rectangle(rect.x, rect.y, rect.w, rect.h);
            }
            set
            {
                ThrowWhenDisposed();

                var rect = new SDL_Rect { x = value.X, y = value.Y, w = value.Width, h = value.Height };
                Error.ThrowOnFailure(
                    SDL.RenderSetViewport(_handle, &rect)
                );
            }
        }

        public Texture? Target
        {
            get
            {
                ThrowWhenDisposed();

                return _renderTarget;
            }
            set
            {
                ThrowWhenDisposed();

                if (value is null)
                {
                    Error.ThrowOnFailure(
                        SDL.SetRenderTarget(_handle, null)
                    );
                }
                else
                {
                    Error.ThrowOnFailure(
                        SDL.SetRenderTarget(_handle, value)
                    );
                }

                _renderTarget = value;
            }
        }

        internal Renderer(SDL_Renderer* renderer)
        {
            if (renderer is null)
            {
                throw new ArgumentNullException(nameof(renderer));
            }

            _handle = renderer;
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
            if (_handle is null) return;
            SDL.DestroyRenderer(_handle);
            _handle = null;
        }

        public Texture CreateTexture(PixelFormatEnum pixelFormat, TextureAccess access, Size size)
        {
            return CreateTexture(pixelFormat, access, size.Width, size.Height);
        }

        public Texture CreateTexture(PixelFormatEnum pixelFormat, TextureAccess access, int width, int height)
        {
            ThrowWhenDisposed();

            var texture = SDL.CreateTexture(_handle, (uint)pixelFormat, (int)access, width, height);
            Error.ThrowOnFailure(texture);
            return new Texture(texture);
        }


        public PackedTexture<TPackedColor> CreateTexture<TPackedColor>(TextureAccess access, Size size) where TPackedColor : struct
        {
            return CreateTexture<TPackedColor>(access, size.Width, size.Height);
        }

        public PackedTexture<TPackedColor> CreateTexture<TPackedColor>(TextureAccess access, int width, int height) where TPackedColor : struct
        {
            ThrowWhenDisposed();

            var pixelFormat = PackedColorAttribute.GetPixelFormatOf<TPackedColor>();
            var texture = SDL.CreateTexture(_handle, (uint)pixelFormat, (int)access, width, height);
            Error.ThrowOnFailure(texture);
            return new PackedTexture<TPackedColor>(texture);
        }

        public Texture CreateTextureFromSurface(Surface surface)
        {
            ThrowWhenDisposed();

            var texture = SDL.CreateTextureFromSurface(_handle, surface);
            Error.ThrowOnFailure(texture);
            return new Texture(texture);
        }


        public PackedTexture<TPackedColor> CreateTextureFromSurface<TPackedColor>(Surface<TPackedColor> surface) where TPackedColor : struct
        {
            ThrowWhenDisposed();

            var texture = SDL.CreateTextureFromSurface(_handle, surface);
            Error.ThrowOnFailure(texture);
            return new PackedTexture<TPackedColor>(texture);
        }

        public void Clear()
        {
            ThrowWhenDisposed();

            Error.ThrowOnFailure(
                SDL.RenderClear(_handle)
            );
        }

        public void Copy(Texture texture)
        {
            ThrowWhenDisposed();

            Error.ThrowOnFailure(
                SDL.RenderCopy(_handle, texture, null, null)
            );
        }

        public void Copy(Texture texture, Rectangle destination)
        {
            ThrowWhenDisposed();

            var dest = new SDL_Rect
            {
                x = destination.X,
                y = destination.Y,
                w = destination.Width,
                h = destination.Height
            };

            Error.ThrowOnFailure(
                SDL.RenderCopy(_handle, texture, null, &dest)
            );
        }

        public void Copy(Texture texture, Rectangle source, Rectangle destination)
        {
            ThrowWhenDisposed();

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

            Error.ThrowOnFailure(
                SDL.RenderCopy(_handle, texture, &src, &dest)
            );
        }

        public void Copy<TPackedColor>(PackedTexture<TPackedColor> texture) where TPackedColor : struct
        {
            ThrowWhenDisposed();

            Error.ThrowOnFailure(
                SDL.RenderCopy(_handle, texture, null, null)
            );
        }

        public void Copy<TPackedColor>(PackedTexture<TPackedColor> texture, Rectangle destination) where TPackedColor : struct
        {
            ThrowWhenDisposed();

            var dest = new SDL_Rect
            {
                x = destination.X,
                y = destination.Y,
                w = destination.Width,
                h = destination.Height
            };

            Error.ThrowOnFailure(
                SDL.RenderCopy(_handle, texture, null, &dest)
            );
        }

        public void Copy<TPackedColor>(PackedTexture<TPackedColor> texture, Rectangle source, Rectangle destination) where TPackedColor : struct
        {
            ThrowWhenDisposed();

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

            Error.ThrowOnFailure(
                SDL.RenderCopy(_handle, texture, &src, &dest)
            );
        }

        public void DrawLine(int x1, int y1, int x2, int y2)
        {
            ThrowWhenDisposed();

            Error.ThrowOnFailure(
                SDL.RenderDrawLine(_handle, x1, y1, x2, y2)
            );
        }

        public void DrawLines(Point[] points)
        {
            ThrowWhenDisposed();

            fixed (Point* point = &points[0])
            {
                Error.ThrowOnFailure(
                    SDL.RenderDrawLines(_handle, (SDL_Point*)point, points.Length)
                );
            }
        }

        public void DrawLine(float x1, float y1, float x2, float y2)
        {
            ThrowWhenDisposed();

            Error.ThrowOnFailure(
                SDL.RenderDrawLineF(_handle, x1, y1, x2, y2)
            );
        }

        public void DrawPoint(int x, int y)
        {
            ThrowWhenDisposed();

            Error.ThrowOnFailure(
                SDL.RenderDrawPoint(_handle, x, y)
            );
        }

        public void DrawPoint(float x, float y)
        {
            ThrowWhenDisposed();

            Error.ThrowOnFailure(
                SDL.RenderDrawPointF(_handle, x, y)
            );
        }

        public void DrawPoints(Point[] points)
        {
            ThrowWhenDisposed();

            fixed (Point* point = &points[0])
            {
                Error.ThrowOnFailure(
                    SDL.RenderDrawPoints(_handle, (SDL_Point*)point, points.Length)
                );
            }
        }

        public void FillRect(int x, int y, int width, int height)
        {
            ThrowWhenDisposed();

            var rect = new SDL_Rect { x = x, y = y, w = width, h = height };
            Error.ThrowOnFailure(
                SDL.RenderFillRect(_handle, &rect)
            );
        }

        public void FillRect(Rectangle rectangle)
        {
            FillRect(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        public void Present()
        {
            ThrowWhenDisposed();

            SDL.RenderPresent(_handle);
        }

        private void ThrowWhenDisposed()
        {
            if (_handle is null)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }
    }
}
