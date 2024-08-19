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
using SDL2Sharp.Video;
using SDL2Sharp.Video.PixelFormats;
using Xunit;

namespace SDL2Sharp.Tests
{
    public sealed class TextureTests
    {
        [Fact]
        public void CreateTextureOfArgb8888()
        {
            WithRenderer(renderer =>
            {
                using var texture = renderer.CreateTexture(PixelFormat.ARGB8888, TextureAccess.Streaming, renderer.OutputSize);
                using var packedTexture = texture.AsPacked<ARGB8888>();
                var color = new ARGB8888(255, 255, 255, 255);
                packedTexture.WithLock(pixels => pixels.Fill(color));
                renderer.Copy(texture);
                renderer.Present();
            });
        }

        [Fact]
        public void CreateTextureOfIYUV()
        {
            WithRenderer(renderer =>
            {
                using var texture = renderer.CreateTexture(PixelFormat.IYUV, TextureAccess.Streaming, renderer.OutputSize);
                using var planarTexture = texture.AsYUV<IYUV>();
                var y = new Y8(255);
                var u = new U8(128);
                var v = new V8(128);
                planarTexture.WithLock(pixels =>
                {
                    pixels.Y.Fill(y);
                    pixels.U.Fill(u);
                    pixels.V.Fill(v);
                });
                renderer.Copy(texture);
                renderer.Present();
            });
        }

        [Fact]
        public void CreateTextureOfNV12()
        {
            WithRenderer(renderer =>
            {
                using var texture = renderer.CreateTexture(PixelFormat.NV12, TextureAccess.Streaming, renderer.OutputSize);
                using var planarTexture = texture.AsNV12();
                var y = new Y8(255);
                var uv = new UV88(128, 128);
                planarTexture.WithLock(pixels =>
                {
                    pixels.Y.Fill(y);
                    pixels.UV.Fill(uv);
                });
                renderer.Copy(texture);
                renderer.Present();
            });
        }

        [Fact]
        public void CreateTextureOfNV21()
        {
            WithRenderer(renderer =>
            {
                using var texture = renderer.CreateTexture(PixelFormat.NV21, TextureAccess.Streaming, renderer.OutputSize);
                using var planarTexture = texture.AsNV21();
                var y = new Y8(255);
                var vu = new VU88(128, 128);
                planarTexture.WithLock(pixels =>
                {
                    pixels.Y.Fill(y);
                    pixels.UV.Fill(vu);
                });
                renderer.Copy(texture);
                renderer.Present();
            });
        }

        [Fact]
        public void CreateTextureOfYV12()
        {
            WithRenderer(renderer =>
            {
                using var texture = renderer.CreateTexture(PixelFormat.YV12, TextureAccess.Streaming, renderer.OutputSize);
                using var planarTexture = texture.AsYUV<YV12>();
                var y = new Y8(255);
                var u = new U8(128);
                var v = new V8(128);
                planarTexture.WithLock(pixels =>
                {
                    pixels.Y.Fill(y);
                    pixels.V.Fill(v);
                    pixels.U.Fill(u);
                });
                renderer.Copy(texture);
                renderer.Present();
            });
        }

        private static void WithRenderer(Action<Renderer> test)
        {
            using var mainSystem = new MainSystem();
            using var videoSystem = new VideoSubsystem();
            using var window = videoSystem.CreateWindow("TextureTests", 640, 480, WindowFlags.Hidden);
            using var renderer = window.CreateRenderer();
            test(renderer);
        }
    }
}
