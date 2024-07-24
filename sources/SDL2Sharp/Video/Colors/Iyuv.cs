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

namespace SDL2Sharp.Video.Colors
{
    public readonly struct Iyuv : IYuvPixel
    {
        public static PixelFormat Format => PixelFormat.IYUV;

        public static int GetYPlaneWidth(int imageWidth)
        {
            return imageWidth;
        }

        public static int GetYPlaneHeight(int imageHeight)
        {
            return imageHeight;
        }

        public static int GetYPlanePitch(int imagePitch)
        {
            return imagePitch;
        }

        public static int GetYPlaneOffset(int imageWidth, int imageHeight, int imagePitch)
        {
            return 0;
        }

        public static int GetUPlaneWidth(int imageWidth)
        {
            return GetYPlaneWidth(imageWidth) / 2;
        }

        public static int GetUPlaneHeight(int imageHeight)
        {
            return GetYPlaneHeight(imageHeight) / 2;
        }

        public static int GetUPlanePitch(int imagePitch)
        {
            return GetYPlanePitch(imagePitch) / 2;
        }

        public static int GetUPlaneOffset(int imageWidth, int imageHeight, int imagePitch)
        {
            return GetYPlaneOffset(imageWidth, imageHeight, imagePitch)
                 + GetYPlaneHeight(imageHeight) * GetYPlanePitch(imagePitch);
        }

        public static int GetVPlaneWidth(int imageWidth)
        {
            return GetUPlaneHeight(imageWidth);
        }

        public static int GetVPlaneHeight(int imageHeight)
        {
            return GetUPlaneHeight(imageHeight);
        }

        public static int GetVPlanePitch(int imagePitch)
        {
            return GetUPlaneHeight(imagePitch);
        }

        public static int GetVPlaneOffset(int imageWidth, int imageHeight, int imagePitch)
        {
            return GetUPlaneOffset(imageWidth, imageHeight, imagePitch)
                 + GetUPlaneHeight(imageHeight) * GetUPlanePitch(imagePitch);
        }
    }
}
