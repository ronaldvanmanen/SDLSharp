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
using System.Threading.Tasks;
using SDL2Sharp.Video;
using SDL2Sharp.Video.Colors;

internal sealed class Camera
{
    private Vector3 _position = Vector3.Zero;

    private Quaternion _orientation = Quaternion.Identity;

    public Resolution Resolution { get; }

    public float PixelAspectRatio { get; }

    public Frustum Frustum { get; }

    public float FieldOfView { get; }

    public float FocalLength { get; private set; }

    public MemoryImage<Argb8888> Snapshot { get; }

    public Camera()
    {
        Resolution = new Resolution(640, 480);
        PixelAspectRatio = 1f;
        Frustum = new Frustum(-4f / 3f, 4f / 3f, -1f, +1f, float.Epsilon, float.PositiveInfinity);
        FieldOfView = 90f;
        FocalLength = (float)(Resolution.Width / Resolution.Height / MathF.Tan(FieldOfView * MathF.PI / 180f / 2f));
        Snapshot = new MemoryImage<Argb8888>(Resolution.Width, Resolution.Height);
    }

    public void LookAt(Vector3 position, Vector3 target, Vector3 up)
    {
        var lookAtMatrix = Matrix4x4.CreateLookAt(position, target, up);
        _position = lookAtMatrix.Translation;
        _orientation = Quaternion.CreateFromRotationMatrix(lookAtMatrix);
    }

    public void MoveForward(float distance)
    {
        var forwardVector = Vector3.Transform(Vector3.UnitZ, _orientation);
        var translation = forwardVector * -distance;
        _position += translation;
    }

    public void MoveBackward(float distance)
    {
        var forwardVector = Vector3.Transform(Vector3.UnitZ, _orientation);
        var translation = forwardVector * distance;
        _position += translation;
    }

    public void MoveLeft(float distance)
    {
        var rightVector = Vector3.Transform(Vector3.UnitX, _orientation);
        var translation = rightVector * -distance;
        _position += translation;
    }

    public void MoveRight(float distance)
    {
        var rightVector = Vector3.Transform(Vector3.UnitX, _orientation);
        var translation = rightVector * distance;
        _position += translation;
    }

    public void MoveUp(float distance)
    {
        var upVector = Vector3.Transform(Vector3.UnitY, _orientation);
        var translation = upVector * distance;
        _position += translation;
    }

    public void MoveDown(float distance)
    {
        var upVector = Vector3.Transform(Vector3.UnitY, _orientation);
        var translation = upVector * -distance;
        _position += translation;
    }

    public void Yaw(float radians)
    {
        var upVector = Vector3.Transform(Vector3.UnitY, _orientation);
        var rotation = Quaternion.CreateFromAxisAngle(upVector, radians);
        _orientation = rotation * _orientation;
    }

    public void Pitch(float radians)
    {
        var rightVector = Vector3.Transform(Vector3.UnitX, _orientation);
        var rotation = Quaternion.CreateFromAxisAngle(rightVector, radians);
        _orientation = rotation * _orientation;
    }

    public void Roll(float radians)
    {
        var forwardVector = Vector3.Transform(Vector3.UnitZ, _orientation);
        var rotation = Quaternion.CreateFromAxisAngle(forwardVector, radians);
        _orientation = rotation * _orientation;
    }

    public MemoryImage<Argb8888> TakeSnapshot(World world)
    {
        if (world is null)
        {
            throw new ArgumentNullException(nameof(world));
        }

        var rotationMatrix = Matrix4x4.CreateFromQuaternion(_orientation);
        var translationMatrix = Matrix4x4.CreateTranslation(_position);
        var viewMatrix = rotationMatrix * translationMatrix;
        Parallel.For(0, Snapshot.Height, y =>
        {
            for (var x = 0; x < Snapshot.Width; ++x)
            {
                var rayDirection = Vector3.Normalize(
                    new Vector3(
                        Frustum.Left + x * Frustum.Width / Snapshot.Width,
                        Frustum.Bottom + y * Frustum.Height / Snapshot.Height,
                        -FocalLength
                    )
                );

                var ray = new Ray(Vector3.Zero, Vector3.Normalize(rayDirection));
                var rayWorld = Ray.Transform(ray, viewMatrix);
                var color = Trace(world, rayWorld, 0, 1f);
                Snapshot[y, x] = color.ToArgb8888();
            }
        });

        return Snapshot;
    }

    private static Rgb32f Trace(World world, Ray ray, int level, float weight)
    {
        var intersection = ray.Intersect(world.Objects).Min;
        if (intersection is not null)
        {
            return Shade(world, intersection, level, weight);
        }
        return Rgb32f.Black;
    }

    private static Rgb32f Shade(World world, Intersection intersection, int level, float weight)
    {
        var shade = Rgb32f.Black;
        var n = intersection.Normal;
        var p = intersection.Point;

        foreach (var light in world.Lights)
        {
            var l = Vector3.Normalize(light.Position - p);
            var illumination = Vector3.Dot(n, l);
            if (illumination > 0f)
            {
                var shadowRay = new Ray(p, -l);
                if (Shadow(world, shadowRay, Vector3.Distance(p, light.Position)) > 0f)
                {
                    shade += illumination * light.Color;
                }
            }
            var i = intersection.Object.Surface.Shade(world.Ambient, n, l, light.Color);
            shade += i;
        }
        return shade;
    }

    private const float RoundOffErrorTolerance = 1e-7f;

    private static float Shadow(World world, Ray ray, float tmax)
    {
        var nearestIntersection = ray.Intersect(world.Objects).Min;
        if (nearestIntersection is null || nearestIntersection.Distance > tmax - RoundOffErrorTolerance)
        {
            return 1f;
        }
        return 0f;
    }
}
