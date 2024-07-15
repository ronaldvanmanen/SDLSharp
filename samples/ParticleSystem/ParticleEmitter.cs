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
using SDL2Sharp.Video;
using static System.Math;

internal sealed class ParticleEmitter
{
    public ParticleEmitter(Color color, Point position, int radius)
    {
        Color = color;
        Position = position;
        Radius = radius;
        Particles = new List<Particle>(GenerateParticles(250));
    }

    public Color Color { get; }

    public Point Position { get; private set; }

    public int Radius { get; }

    private List<Particle> Particles { get; }

    public void MoveTo(int mouseX, int mouseY)
    {
        Position = new Point(mouseX, mouseY);
    }

    public void Update(TimeSpan elapsedTime)
    {
        foreach (var particle in Particles)
        {
            if (particle.IsDead)
            {
                var angle = _randomizer.NextDouble() * PI * 2;
                var x = Position.X + Cos(angle) * Radius * 2;
                var y = Position.Y + Sin(angle) * Radius * 2;
                var position = new Point((int)x, (int)y);

                var r = (byte)(_randomizer.NextDouble() * byte.MaxValue);
                var g = (byte)(_randomizer.NextDouble() * byte.MaxValue);
                var b = (byte)(_randomizer.NextDouble() * byte.MaxValue);
                var color = new Color(r, g, b, byte.MaxValue);

                var lifespan = TimeSpan.FromSeconds(_randomizer.NextDouble());

                particle.Respawn(lifespan, color, position, 7);
            }

            particle.Update(elapsedTime);
        }
    }

    public void Render(Renderer renderer)
    {
        if (renderer is null)
        {
            throw new ArgumentNullException(nameof(renderer));
        }

        foreach (var particle in Particles)
        {
            particle.Render(renderer);
        }

        renderer.DrawColor = Color;
        renderer.BlendMode = BlendMode.Blend;
        renderer.FillCircle(Position.X, Position.Y, Radius);
    }

    private static readonly Random _randomizer = new();

    private static Particle GenerateParticle()
    {
        return new Particle();
    }

    private static IEnumerable<Particle> GenerateParticles(int max)
    {
        for (var i = 0; i < max; ++i)
        {
            yield return GenerateParticle();
        }
    }
}
