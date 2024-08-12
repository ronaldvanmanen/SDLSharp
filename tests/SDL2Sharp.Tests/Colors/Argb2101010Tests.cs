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

using SDL2Sharp.Video.PixelFormats;
using Xunit;

namespace SDL2Sharp.Tests.Colors
{
    public static class Argb2101010Tests
    {
        [Fact]
        public static void SelfEquality()
        {
#pragma warning disable CS1718 // Comparison made to same variable
            var color = ARGB2101010.FromRGBA(r: 63, g: 127, b: 191, a: 255);
            Assert.True(color == color);
            Assert.False(color != color);
#pragma warning restore CS1718 // Comparison made to same variable
        }

        [Fact]
        public static void Equality()
        {
            var a = ARGB2101010.FromRGBA(r: 63, g: 127, b: 191, a: 255);
            var b = ARGB2101010.FromRGBA(r: 63, g: 127, b: 191, a: 255);
            Assert.True(a == b);
            Assert.False(a != b);
        }
    }
}
