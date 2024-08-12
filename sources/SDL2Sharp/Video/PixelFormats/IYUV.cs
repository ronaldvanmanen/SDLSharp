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

using System.Runtime.CompilerServices;

namespace SDL2Sharp.Video.PixelFormats
{
    public readonly struct IYUV : IYUVFormat
    {
        public static PixelFormat PixelFormat => PixelFormat.IYUV;

        public static unsafe ImagePlane<Y8> CreateYPlane(void* pixels, int imageWidth, int imageHeight, int imagePitch)
        {
            return new ImagePlane<Y8>(pixels, imageWidth, imageHeight, imagePitch);
        }

        public static unsafe ImagePlane<U8> CreateUPlane(void* pixels, int imageWidth, int imageHeight, int imagePitch)
        {
            var uPlaneWidth = imageWidth / 2;
            var uPlaneHeight = imageHeight / 2;
            var uPlanePitch = imagePitch / 2;
            var uPlaneOffset = imageHeight * imagePitch;
            var uPlanePixels = Unsafe.Add<U8>(pixels, uPlaneOffset);
            return new ImagePlane<U8>(uPlanePixels, uPlaneWidth, uPlaneHeight, uPlanePitch);
        }

        public static unsafe ImagePlane<V8> CreateVPlane(void* pixels, int imageWidth, int imageHeight, int imagePitch)
        {
            var vPlaneWidth = imageWidth / 2;
            var vPlaneHeight = imageHeight / 2;
            var vPlanePitch = imagePitch / 2;
            var vPlaneOffset = imageHeight * imagePitch + imageHeight * imagePitch / 4;
            var vPlanePixels = Unsafe.Add<V8>(pixels, vPlaneOffset);
            return new ImagePlane<V8>(vPlanePixels, vPlaneWidth, vPlaneHeight, vPlanePitch);
        }
    }
}
