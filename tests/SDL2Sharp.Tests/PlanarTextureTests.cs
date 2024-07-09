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
using Xunit;

namespace SDL2Sharp.Tests
{
    public sealed class PlanarTextureTests
    {
        [Fact]
        public void CreateYv12Texture()
        {
            using var mainSystem = new MainSystem();
            using var videoSystem = new VideoSubsystem();
            using var window = videoSystem.CreateWindow("CreateYv12Texture", 640, 480, WindowFlags.Hidden);
            using var renderer = window.CreateRenderer(RendererFlags.Software);
            using var texture = renderer.CreateYv12Texture(TextureAccess.Streaming, renderer.OutputSize);
            texture.WithLock(pixels =>
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