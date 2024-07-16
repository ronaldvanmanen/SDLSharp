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

using System;

namespace SDL2Sharp.Video
{
    public static class TextureExtensions
    {
        public static PackedTexture<TPacketColor> AsPacked<TPacketColor>(this Texture texture)
            where TPacketColor : struct
        {
            return new PackedTexture<TPacketColor>(texture);
        }

        public static YuvTexture<Iyuv> AsIYUV(this Texture texture)
        {
            if (texture.Format != PixelFormat.IYUV)
            {
                throw new ArgumentException("Texture is not in IYUV color format.", nameof(texture));
            }
            return new YuvTexture<Iyuv>(texture);
        }

        public static Nv12Texture AsNV12(this Texture texture)
        {
            if (texture.Format != PixelFormat.NV12)
            {
                throw new ArgumentException("Texture is not in NV12 color format.", nameof(texture));
            }
            return new Nv12Texture(texture);
        }

        public static Nv21Texture AsNV21(this Texture texture)
        {
            if (texture.Format != PixelFormat.NV21)
            {
                throw new ArgumentException("Texture is not in NV21 color format.", nameof(texture));
            }
            return new Nv21Texture(texture);
        }

        public static YuvTexture<Yv12> AsYV12(this Texture texture)
        {
            if (texture.Format != PixelFormat.YV12)
            {
                throw new ArgumentException("Texture is not in YV12 color format.", nameof(texture));
            }
            return new YuvTexture<Yv12>(texture);
        }
    }
}
