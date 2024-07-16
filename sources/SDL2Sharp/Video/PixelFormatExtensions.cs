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
    public static class PixelFormatExtensions
    {
        public static bool IsFourCC(this PixelFormat format)
        {
            return (format != PixelFormat.Unknown) && (GetPixelFlag(format) != 1);
        }

        public static uint GetPixelFlag(this PixelFormat format)
        {
            return (((uint)format) >> 28) & 0x0FU;
        }

        public static PixelType GetPixelType(this PixelFormat format)
        {
            return (PixelType)((((uint)format) >> 24) & 0x0F);
        }

        public static uint GetBitsPerPixel(this PixelFormat format)
        {
            return (((uint)format) >> 8) & 0xFFU;
        }

        public static uint GetBytesPerPixel(this PixelFormat pixelFormat)
        {
            return IsFourCC(pixelFormat) ?
                (((pixelFormat == PixelFormat.YUY2) ||
                  (pixelFormat == PixelFormat.UYVY) ||
                  (pixelFormat == PixelFormat.YVYU)) ? 2u : 1u) : ((uint)pixelFormat) & 0xFF;
        }

        public static PixelOrder GetPixelOrder(this PixelFormat format)
        {
            return (PixelOrder)((((uint)format) >> 20) & 0x0F);
        }

        public static PackedLayout GetPixelLayout(this PixelFormat format)
        {
            return (PackedLayout)((((uint)format) >> 16) & 0x0F);
        }

        public static bool IsIndexed(this PixelFormat pixelFormat)
        {
            if (IsFourCC(pixelFormat))
            {
                return false;
            }

            var pixelType = GetPixelType(pixelFormat);
            return (pixelType == PixelType.Index1)
                || (pixelType == PixelType.Index4)
                || (pixelType == PixelType.Index8);
        }

        public static bool IsPacked(this PixelFormat pixelFormat)
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

        public static bool IsArray(this PixelFormat pixelFormat)
        {
            if (IsFourCC(pixelFormat))
            {
                return false;
            }

            var pixelType = GetPixelType(pixelFormat);
            return (pixelType == PixelType.ArrayU8)
                || (pixelType == PixelType.ArrayU16)
                || (pixelType == PixelType.ArrayU32)
                || (pixelType == PixelType.ArrayF16)
                || (pixelType == PixelType.ArrayF32);
        }

        public static bool IsAlpha(this PixelFormat pixelFormat)
        {
            if (IsFourCC(pixelFormat))
            {
                return false;
            }

            var pixelOrder = GetPixelOrder(pixelFormat);
            if (IsPacked(pixelFormat))
            {
                var packedOrder = (PackedOrder)pixelOrder;
                return (packedOrder == PackedOrder.Argb)
                    || (packedOrder == PackedOrder.Rgba)
                    || (packedOrder == PackedOrder.Abgr)
                    || (packedOrder == PackedOrder.Bgra);
            }

            if (IsArray(pixelFormat))
            {
                var arrayOrder = (ArrayOrder)pixelOrder;
                return (arrayOrder == ArrayOrder.Argb)
                    || (arrayOrder == ArrayOrder.Rgba)
                    || (arrayOrder == ArrayOrder.Abgr)
                    || (arrayOrder == ArrayOrder.Bgra);
            }

            return false;
        }
    }
}
