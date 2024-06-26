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
using SDL2Sharp.Colors;
using Xunit;

namespace SDL2Sharp.Tests
{
    public sealed class TextureTests : IAssemblyFixture<AssemblyFixture>
    {
        [Fact]
        public void CreateTextureOfArgb8888()
        {
            var color = new Argb8888(255, 255, 255, 255);
            using var window = new Window("CreateTextureOfArgb8888", 640, 480, WindowFlags.Hidden);
            using var renderer = window.CreateRenderer();
            using var texture = renderer.CreateTexture(PixelFormatEnum.ARGB8888, TextureAccess.Streaming, renderer.OutputSize);
            texture.WithLock<Argb8888>(pixels => pixels.Fill(color));
            renderer.Copy(texture);
            renderer.Present();
        }

        [Fact]
        public void CreateTextureOfYUY2()
        {
            var color = new Yuy2(255, 255, 255, 255);
            using var window = new Window("CreateTextureOfYUY2", 640, 480, WindowFlags.Hidden);
            using var renderer = window.CreateRenderer();
            using var texture = renderer.CreateTexture(PixelFormatEnum.YUY2, TextureAccess.Streaming, renderer.OutputSize);
            texture.WithLock<Yuy2>(pixels => pixels.Fill(color));
            renderer.Copy(texture);
            renderer.Present();
        }

        [Fact]
        public void CreateTextureOfYVYU()
        {
            var color = new Yvyu(255, 255, 255, 255);
            using var window = new Window("CreateTextureOfYVYU", 640, 480, WindowFlags.Hidden);
            using var renderer = window.CreateRenderer();
            using var texture = renderer.CreateTexture(PixelFormatEnum.YVYU, TextureAccess.Streaming, renderer.OutputSize);
            texture.WithLock<Yvyu>(pixels => pixels.Fill(color));
            renderer.Copy(texture);
            renderer.Present();
        }

        [Fact]
        public void CreateTextureOfUYVY()
        {
            var color = new Uyvy(255, 255, 255, 255);
            using var window = new Window("CreateTextureOfUYVY", 640, 480, WindowFlags.Hidden);
            using var renderer = window.CreateRenderer();
            using var texture = renderer.CreateTexture(PixelFormatEnum.UYVY, TextureAccess.Streaming, renderer.OutputSize);
            texture.WithLock<Uyvy>(pixels => pixels.Fill(color));
            renderer.Copy(texture);
            renderer.Present();
        }

        [Fact]
        public void CreateTextureOfYV12()
        {
            var color = new Uyvy(255, 255, 255, 255);
            using var window = new Window("CreateTextureOfYV12", 640, 480, WindowFlags.Hidden);
            using var renderer = window.CreateRenderer();
            using var texture = renderer.CreateTexture(PixelFormatEnum.YV12, TextureAccess.Streaming, renderer.OutputSize);
            texture.WithLock((Yv12Image pixels) =>
            {
                pixels.Y.Fill(255);
                pixels.U.Fill(255);
                pixels.V.Fill(255);
            });
            renderer.Copy(texture);
            renderer.Present();
        }
    }
}
