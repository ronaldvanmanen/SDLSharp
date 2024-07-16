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

using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace SDL2Sharp.Video
{
    public readonly ref struct PackedImage<TPackedPixelFormat> where TPackedPixelFormat : struct
    {
        private readonly ImagePlane<TPackedPixelFormat> _plane;

        public int Width
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _plane.Width;
        }

        public int Height
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _plane.Height;
        }

        public Size Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _plane.Size;
        }

        public ref TPackedPixelFormat this[int x, int y]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref _plane[x, y];
        }

        public unsafe PackedImage(void* pixels, int width, int height, int pitch)
        {
            _plane = new ImagePlane<TPackedPixelFormat>(pixels, width, height, pitch);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref TPackedPixelFormat DangerousGetReference()
        {
            return ref _plane.DangerousGetReference();
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref TPackedPixelFormat DangerousGetReferenceAt(int x, int y)
        {
            return ref _plane.DangerousGetReferenceAt(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Fill(TPackedPixelFormat value)
        {
            _plane.Fill(value);
        }
    }
}
