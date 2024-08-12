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
using System.Collections.Generic;
using System.Numerics;
using SDL2Sharp.Video.PixelFormats;

internal sealed class World
{
    public RGB96f Ambient { get; set; }

    public ICollection<IObject> Objects { get; } = [];

    public ICollection<PointLight> Lights { get; } = [];

    public RGB96f Trace(Ray ray, int level, float weight)
    {
        var nearestIntersection = ray.Intersect(Objects);
        if (nearestIntersection is not null)
        {
            return Shade(nearestIntersection, level, weight);
        }
        return RGB96f.Black;
    }

    private RGB96f Shade(Intersection intersection, int level, float weight)
    {
        var color = RGB96f.Black;
        var @object = intersection.Object;
        var surfaceNormal = intersection.Normal;
        var surfacePoint = intersection.Point;

        foreach (var light in Lights)
        {
            var lightVector = Vector3.Normalize(light.Position - surfacePoint);
            var illumination = Vector3.Dot(surfaceNormal, lightVector);
            var shadowRay = new Ray(surfacePoint, lightVector);
            var visibility = Shadow(shadowRay, MathF.Abs(Vector3.Distance(surfacePoint, light.Position)));
            if (illumination > 0f && visibility > 0f)
            {
                var ambientCoefficient = intersection.Object.AmbientCoefficient;
                var diffuseCoefficient = intersection.Object.DiffuseCoefficient;
                color += Ambient * ambientCoefficient + light.Color * diffuseCoefficient * @object.DiffuseColor * illumination;
            }
        }
        return color;
    }

    private float Shadow(Ray ray, float maxDistance)
    {
        var nearestIntersection = ray.Intersect(Objects);
        if (nearestIntersection is null || nearestIntersection.Distance > (maxDistance - Ray.Epsilon))
        {
            return 1f;
        }
        return 0f;
    }
}
