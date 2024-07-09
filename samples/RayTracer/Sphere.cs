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

using System;
using System.Numerics;
using SDL2Sharp.Video.Colors;

internal sealed class Sphere : IObject
{
    public Vector3 Position { get; set; } = new Vector3(0f, 0f, 0f);

    public float Radius { get; set; }

    public float AmbientCoefficient { get; set; } = 1f;

    public float DiffuseCoefficient { get; set; } = 1f;

    public Rgb32f DiffuseColor { get; set; } = Rgb32f.Black;

    public Vector3 NormalAt(Vector3 point)
    {
        return Vector3.Normalize((point - Position) / Radius);
    }

    public Intersection? Intersect(Ray ray)
    {
        var v = Position - ray.Origin;
        var b = Vector3.Dot(v, ray.Direction);
        var discriminant = b * b - Vector3.Dot(v, v) + Radius * Radius;
        if (discriminant <= 0f)
        {
            return null;
        }

        discriminant = MathF.Sqrt(discriminant);

        var t2 = b + discriminant;
        if (t2 <= Ray.Epsilon)
        {
            return null;
        }

        var t1 = b - discriminant;
        if (t1 > Ray.Epsilon)
        {
            return new Intersection(this, ray, t1);
        }

        return new Intersection(this, ray, t2);
    }
}
