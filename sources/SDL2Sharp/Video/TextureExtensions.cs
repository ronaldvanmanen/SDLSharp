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

using SDL2Sharp.Video.PixelFormats;

namespace SDL2Sharp.Video
{
    public static class TextureExtensions
    {
        public static PackedTexture<TPackedPixel> AsPacked<TPackedPixel>(this Texture texture)
            where TPackedPixel : struct, IPackedPixel<TPackedPixel>
        {
            return new PackedTexture<TPackedPixel>(texture);
        }

        public static YUVTexture<TYUVFormat> AsYUV<TYUVFormat>(this Texture texture)
            where TYUVFormat : IYUVFormat
        {
            return new YUVTexture<TYUVFormat>(texture);
        }

        public static NV12Texture AsNV12(this Texture texture)
        {
            return new NV12Texture(texture);
        }

        public static NV21Texture AsNV21(this Texture texture)
        {
            return new NV21Texture(texture);
        }
    }
}
