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

using SDL2Sharp.Video;
using SDL2Sharp.Video.Colors;
using Xunit;

namespace SDL2Sharp.Tests
{
    public static class PlanarTextureTests
    {
        [Fact]
        public static void WriteAndReadYV12() => WriteAndRead<Yv12>();

        [Fact]
        public static void WriteAndReadIYUV() => WriteAndRead<Iyuv>();

        private static void WriteAndRead<TYuvFormat>()
            where TYuvFormat : struct, IYuvFormat
        {
            using var mainSystem = new MainSystem();
            using var videoSystem = new VideoSubsystem();
            using var window = videoSystem.CreateWindow("PlanarTextureTests", 640, 480, WindowFlags.Hidden);
            using var renderer = window.CreateRenderer();
            using var texture = renderer.CreateYuvTexture<TYuvFormat>(TextureAccess.Streaming, renderer.OutputSize);

            var y = new Y8(255);
            var u = new U8(128);
            var v = new V8(128);
            texture.WithLock(pixels =>
            {
                pixels.Y.Fill(y);
                pixels.U.Fill(u);
                pixels.V.Fill(v);
            });
            renderer.Copy(texture);
            renderer.Present();
        }
    }
}
