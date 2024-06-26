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
using System.Linq;
using System.Numerics;
using SDL2Sharp;
using SDL2Sharp.Colors;

namespace RayTracer
{
    internal sealed class Camera
    {
        private Matrix4x4 _viewMatrix = Matrix4x4.Identity;

        private float _fieldOfView;

        public Vector3 Position => _viewMatrix.Translation;

        public Quaternion Orientation => Quaternion.CreateFromRotationMatrix(_viewMatrix);

        public Resolution Resolution { get; }

        public float PixelAspectRatio { get; }

        public Frustum Frustum { get; }

        public float FieldOfView
        {
            get
            {
                return _fieldOfView;
            }
            set
            {
                _fieldOfView = value;
                FocalLength = (float)(Resolution.Width / Resolution.Height / MathF.Tan(FieldOfView * MathF.PI / 180f / 2f));
            }
        }

        public float FocalLength { get; private set; }

        public Camera()
        {
            Resolution = new Resolution(640, 480);
            PixelAspectRatio = 1f;
            Frustum = new Frustum(-4f / 3f, 4f / 3f, -1f, +1f, float.Epsilon, float.PositiveInfinity);
            FieldOfView = 90f;
            FocalLength = (float)(Resolution.Width / Resolution.Height / MathF.Tan(FieldOfView * MathF.PI / 180f / 2f));
        }

        public void LookAt(Vector3 position, Vector3 target, Vector3 up)
        {
            _viewMatrix = Matrix4x4.CreateLookAt(position, target, up);
        }

        public void Move(Vector3 distance)
        {
            _viewMatrix *= Matrix4x4.CreateTranslation(distance);
        }

        public void Move(float x, float y, float z)
        {
            _viewMatrix *= Matrix4x4.CreateTranslation(x, y, z);
        }

        public void RotateX(float radians)
        {
            _viewMatrix *= Matrix4x4.CreateRotationX(radians);
        }

        public void RotateY(float radians)
        {
            _viewMatrix *= Matrix4x4.CreateRotationY(radians);
        }

        public void RotateZ(float radians)
        {
            _viewMatrix *= Matrix4x4.CreateRotationZ(radians);
        }

        public void Rotate(float yaw, float pitch, float roll)
        {
            _viewMatrix *= Matrix4x4.CreateFromYawPitchRoll(yaw, pitch, roll);
        }

        public void Shoot(World world, MemoryImage<Argb8888> image)
        {
            if (world is null)
            {
                throw new ArgumentNullException(nameof(world));
            }

            for (var imageY = 0; imageY < image.Height; ++imageY)
            {
                for (var imageX = 0; imageX < image.Width; ++imageX)
                {
                    var rayDirection = Vector3.Normalize(
                        new Vector3(
                            Frustum.Left + imageX * Frustum.Width / image.Width,
                            Frustum.Bottom + imageY * Frustum.Height / image.Height,
                            -FocalLength
                        )
                    );

                    var ray = new Ray(Vector3.Zero, Vector3.Normalize(rayDirection));
                    var rayWorld = Ray.Transform(ray, _viewMatrix);
                    var color = Trace(world, rayWorld, 0, 1f);
                    image[imageY, imageX] = color.ToArgb8888();
                }
            }
        }

        private static Rgb32f Trace(World world, Ray ray, int level, float weight)
        {
            var intersection = ray.Intersect(world.Objects).MinBy(e => e.Distance);
            if (intersection != null)
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
                //var illumination = Vector3.Dot(n, l);
                //if (illumination > 0f)
                //{
                //    var shadowRay = new Ray(p, -l);
                //    if (Shadow(world, shadowRay, Vector3.Distance(p, light.Position)) > 0f)
                //    {
                //        shade += illumination * light.Color;
                //    }
                //}
                var i = intersection.Object.Surface.Shade(world.Ambient, n, l, light.Color);
                shade += i;
            }
            return shade;
        }

        private const float RoundOffErrorTolerance = 1e-7f;

        private static float Shadow(World world, Ray ray, float tmax)
        {
            var intersection = ray.Intersect(world.Objects).MinBy(e => e.Distance);
            if (intersection == null || intersection.Distance > tmax - RoundOffErrorTolerance)
            {
                return 1f;
            }
            return 0f;
        }
    }
}
