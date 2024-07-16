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
    [PixelFormat(PixelFormat.IYUV)]
    public readonly struct Iyuv : IYuvPixelFormat
    {
        public int GetYPlaneWidth(int imageWidth)
        {
            return imageWidth;
        }

        public int GetYPlaneHeight(int imageHeight)
        {
            return imageHeight;
        }

        public int GetYPlanePitch(int imagePitch)
        {
            return imagePitch;
        }

        public int GetYPlaneOffset(int imageWidth, int imageHeight, int imagePitch)
        {
            return 0;
        }

        public int GetUPlaneWidth(int imageWidth)
        {
            return GetYPlaneWidth(imageWidth) / 2;
        }

        public int GetUPlaneHeight(int imageHeight)
        {
            return GetYPlaneHeight(imageHeight) / 2;
        }

        public int GetUPlanePitch(int imagePitch)
        {
            return GetYPlanePitch(imagePitch) / 2;
        }

        public int GetUPlaneOffset(int imageWidth, int imageHeight, int imagePitch)
        {
            return GetYPlaneOffset(imageWidth, imageHeight, imagePitch)
                 + GetYPlaneHeight(imageHeight) * GetYPlanePitch(imagePitch);
        }

        public int GetVPlaneWidth(int imageWidth)
        {
            return GetUPlaneHeight(imageWidth);
        }

        public int GetVPlaneHeight(int imageHeight)
        {
            return GetUPlaneHeight(imageHeight);
        }

        public int GetVPlanePitch(int imagePitch)
        {
            return GetUPlaneHeight(imagePitch);
        }

        public int GetVPlaneOffset(int imageWidth, int imageHeight, int imagePitch)
        {
            return GetUPlaneOffset(imageWidth, imageHeight, imagePitch)
                 + GetUPlaneHeight(imageHeight) * GetUPlanePitch(imagePitch);
        }
    }
}
