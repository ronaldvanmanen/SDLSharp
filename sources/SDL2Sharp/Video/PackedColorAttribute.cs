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
using System.Reflection;

namespace SDL2Sharp.Video
{
    [AttributeUsage(AttributeTargets.Struct)]
    internal sealed class PackedColorAttribute : Attribute
    {
        public PackedPixelFormat PixelFormat { get; }

        public PackedColorAttribute(PackedPixelFormat pixelFormat)
        {
            PixelFormat = pixelFormat;
        }

        public static PackedPixelFormat GetPixelFormatOf<TPackedColor>()
        {
            var pixelFormatType = typeof(TPackedColor);
            var pixelFormatAttribute = pixelFormatType.GetCustomAttribute<PackedColorAttribute>();
            if (pixelFormatAttribute == null)
            {
                throw new NotSupportedException($"The type {pixelFormatType} does not have a {nameof(PackedColorAttribute)}.");
            }
            return pixelFormatAttribute.PixelFormat;
        }
    }
}