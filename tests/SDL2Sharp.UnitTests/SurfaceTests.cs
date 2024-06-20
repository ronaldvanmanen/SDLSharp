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
    public static class SurfaceTests
    {
        [Fact]
        public static void CreateSurfaceOfArgb8888()
        {
            var color = new Argb8888(255, 255, 255, 255);
            using var surface = new Surface<Argb8888>(512, 512);
            surface.WithLock(pixels => pixels.Fill(color));
        }

        [Fact]
        public static void CreateSurfaceOfYuy2()
        {
            Assert.Throws<Error>(() => new Surface<Yuy2>(512, 512));
        }

        [Fact]
        public static void CreateSurfaceOfYvyu()
        {
            Assert.Throws<Error>(() => new Surface<Yvyu>(512, 512));
        }

        [Fact]
        public static void CreateSurfaceOfUyvy()
        {
            Assert.Throws<Error>(() => new Surface<Uyvy>(512, 512));
        }
    }
}
