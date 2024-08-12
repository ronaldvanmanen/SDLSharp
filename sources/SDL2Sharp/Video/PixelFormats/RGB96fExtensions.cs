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

using static System.Math;

namespace SDL2Sharp.Video.PixelFormats
{
    public static class RGB96fExtensions
    {
        public static Color ToColor(this RGB96f color)
        {
            var clampedColor = RGB96f.Clamp(color);
            var scaledColor = clampedColor * 255f;
            var r = (byte)Round(scaledColor.R);
            var g = (byte)Round(scaledColor.G);
            var b = (byte)Round(scaledColor.B);
            return new Color(r, g, b, 255);
        }

        public static RGBA8888 ToRgba8888(this RGB96f color)
        {
            var clampedColor = RGB96f.Clamp(color);
            var scaledColor = clampedColor * 255f;
            var r = (byte)Round(scaledColor.R);
            var g = (byte)Round(scaledColor.G);
            var b = (byte)Round(scaledColor.B);
            return new RGBA8888(r, g, b, 255);
        }

        public static ARGB8888 ToArgb8888(this RGB96f color)
        {
            var clampedColor = RGB96f.Clamp(color);
            var scaledColor = clampedColor * 255f;
            var r = (byte)Round(scaledColor.R);
            var g = (byte)Round(scaledColor.G);
            var b = (byte)Round(scaledColor.B);
            return new ARGB8888(255, r, g, b);
        }
    }
}
