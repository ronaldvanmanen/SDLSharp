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

namespace SDL2Sharp.Video.Colors
{
    public interface IYuvPixel
    {
        static abstract PixelFormat Format { get; }

        static abstract int GetYPlaneWidth(int imageWidth);

        static abstract int GetYPlaneHeight(int imageHeight);

        static abstract int GetYPlanePitch(int imagePitch);

        static abstract int GetYPlaneOffset(int imageWidth, int imageHeight, int imagePitch);

        static abstract int GetUPlaneWidth(int imageWidth);

        static abstract int GetUPlaneHeight(int imageHeight);

        static abstract int GetUPlanePitch(int imagePitch);

        static abstract int GetUPlaneOffset(int imageWidth, int imageHeight, int imagePitch);

        static abstract int GetVPlaneWidth(int imageWidth);

        static abstract int GetVPlaneHeight(int imageHeight);

        static abstract int GetVPlanePitch(int imagePitch);

        static abstract int GetVPlaneOffset(int imageWidth, int imageHeight, int imagePitch);
    }
}
