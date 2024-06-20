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

using SDL2Sharp.Colors;
using Xunit;

namespace SDL2Sharp.UnitTests
{
    public static class PackedTextureTests
    {
        [Fact]
        public static void CreatePackedTextureOfArgb8888()
        {
            var color = new Argb8888(255, 255, 255, 255);
            using var window = new Window("CreatePackedTextureOfArgb8888", 640, 480, WindowFlags.Hidden);
            using var renderer = window.CreateRenderer();
            using var texture = renderer.CreateTexture<Argb8888>(TextureAccess.Streaming, renderer.OutputSize);
            texture.WithLock(pixels => pixels.Fill(color));
            renderer.Copy(texture);
            renderer.Present();
        }

        [Fact]
        public static void CreatePackedTextureOfYUY2()
        {
            var color = new Yuy2(255, 255, 255, 255);
            using var window = new Window("CreatePackedTextureOfYUY2", 640, 480, WindowFlags.Hidden);
            using var renderer = window.CreateRenderer();
            using var texture = renderer.CreateTexture<Yuy2>(TextureAccess.Streaming, renderer.OutputSize);
            texture.WithLock(pixels => pixels.Fill(color));
            renderer.Copy(texture);
            renderer.Present();
        }

        [Fact]
        public static void CreatePackedTextureOfYVYU()
        {
            var color = new Yvyu(255, 255, 255, 255);
            using var window = new Window("CreatePackedTextureOfYVYU", 640, 480, WindowFlags.Hidden);
            using var renderer = window.CreateRenderer();
            using var texture = renderer.CreateTexture<Yvyu>(TextureAccess.Streaming, renderer.OutputSize);
            texture.WithLock(pixels => pixels.Fill(color));
            renderer.Copy(texture);
            renderer.Present();
        }

        [Fact]
        public static void CreatePackedTextureOfUYVY()
        {
            var color = new Uyvy(255, 255, 255, 255);
            using var window = new Window("CreatePackedTextureOfUYVY", 640, 480, WindowFlags.Hidden);
            using var renderer = window.CreateRenderer();
            using var texture = renderer.CreateTexture<Uyvy>(TextureAccess.Streaming, renderer.OutputSize);
            texture.WithLock(pixels => pixels.Fill(color));
            renderer.Copy(texture);
            renderer.Present();
        }
    }
}
