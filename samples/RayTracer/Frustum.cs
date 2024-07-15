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

internal sealed class Frustum
{
    public Frustum(float left, float right, float bottom, float top, float near, float far)
    {
        Left = left;
        Right = right;
        Bottom = bottom;
        Top = top;
        Near = near;
        Far = far;
    }

    public float Left { get; }

    public float Right { get; }

    public float Bottom { get; }

    public float Top { get; }

    public float Near { get; }

    public float Far { get; }

    public float Width => Right - Left;

    public float Height => Top - Bottom;
}
