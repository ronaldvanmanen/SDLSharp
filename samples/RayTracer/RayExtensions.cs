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

using System.Collections.Generic;

internal static class RayExtensions
{
    private static readonly Comparer<Intersection> _distanceComparer =
        Comparer<Intersection>.Create(
            (a, b) => a.Distance.CompareTo(b.Distance));

    public static SortedSet<Intersection> Intersect(this Ray ray, IEnumerable<IObject> objects)
    {
        var intersections = new SortedSet<Intersection>(_distanceComparer);
        foreach (var @object in objects)
        {
            var intersection = @object.Intersect(ray);
            if (intersection != null)
            {
                intersections.Add(intersection);
            }
        }
        return intersections;
    }
}
