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

namespace SDL2Sharp.Video
{
    public static class PixelFormatEnumExtensions
    {
        public static bool IsFourCC(this PixelFormatEnum format)
        {
            return (format != PixelFormatEnum.Unknown) && (GetPixelFlag(format) != 1);
        }

        public static uint GetPixelFlag(this PixelFormatEnum format)
        {
            return (((uint)format) >> 28) & 0x0FU;
        }

        public static PixelType GetPixelType(this PixelFormatEnum format)
        {
            return (PixelType)((((uint)format) >> 24) & 0x0F);
        }

        public static uint GetBitsPerPixel(this PixelFormatEnum format)
        {
            return (((uint)format) >> 8) & 0xFFU;
        }

        public static uint GetBytesPerPixel(this PixelFormatEnum pixelFormat)
        {
            return IsFourCC(pixelFormat) ?
                (((pixelFormat == PixelFormatEnum.YUY2) ||
                  (pixelFormat == PixelFormatEnum.UYVY) ||
                  (pixelFormat == PixelFormatEnum.YVYU)) ? 2u : 1u) : ((uint)pixelFormat) & 0xFF;
        }

        public static bool IsPacked(this PixelFormatEnum pixelFormat)
        {
            if (IsFourCC(pixelFormat))
            {
                return false;
            }

            var pixelType = GetPixelType(pixelFormat);
            return (pixelType == PixelType.Packed8)
                || (pixelType == PixelType.Packed16)
                || (pixelType == PixelType.Packet32);
        }
    }
}
