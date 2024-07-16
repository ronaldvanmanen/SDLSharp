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
using System.Diagnostics;
using System.Numerics;
using System.Collections.Generic;
using SDL2Sharp;
using SDL2Sharp.Video;
using SDL2Sharp.Fonts;
using SDL2Sharp.Video.Colors;
using SDL2Sharp.Input;

internal static class Program
{
    public static void Main()
    {
        using var mainSystem = new MainSystem();
        using var videoSubsystem = new VideoSubsystem();
        using var eventSubsystem = new EventSubsystem();
        using var fontSubsystem = new FontSubsystem();

        using var window = videoSubsystem.CreateWindow("Swirl Stars", 640, 480, WindowFlags.Shown | WindowFlags.Resizable);
        using var renderer = window.CreateRenderer(RendererFlags.Accelerated | RendererFlags.PresentVSync);
        using var lazyFont = fontSubsystem.OpenFont("lazy.ttf", 28);

        var stars = new List<Star>(GenerateStars(256));

        var lastFrameTime = TimeSpan.Zero;
        var accumulatedFrameTime = TimeSpan.Zero;
        var frameCounter = 0;
        var frameRate = 0d;

        var programClock = Stopwatch.StartNew();

        while (true)
        {
            var @event = eventSubsystem.PollEvent();
            if (@event is not null)
            {
                switch (@event)
                {
                    case QuitEvent:
                        return;

                    case KeyUpEvent keyEvent when !keyEvent.Repeat:
                        switch (keyEvent.KeyCode)
                        {
                            case KeyCode.Return when keyEvent.Modifiers.HasFlag(KeyModifiers.Alt):
                            case KeyCode.F11:
                                window.IsFullScreenDesktop = !window.IsFullScreenDesktop;
                                break;
                        }
                        break;
                }
            }
            else
            {
                var currentFrameTime = programClock.Elapsed;
                var elapsedFrameTime = currentFrameTime - lastFrameTime;
                accumulatedFrameTime += elapsedFrameTime;

                renderer.DrawColor = Color.Black;
                renderer.BlendMode = BlendMode.None;
                renderer.Clear();

                var screenSize = renderer.OutputSize;
                var screenCenterX = screenSize.Width / 2f;
                var screenCenterY = screenSize.Height / 2f;
                const int maxSubFrameCount = 20;
                for (var subFrame = 0; subFrame < maxSubFrameCount; subFrame++)
                {
                    foreach (var star in stars)
                    {
                        star.Position += star.Velocity / maxSubFrameCount;

                        var screenX = star.Position.X / star.Position.Z * 100f + screenCenterX;
                        var screenY = star.Position.Y / star.Position.Z * 100f + screenCenterY;
                        if (screenX < 0f || screenX >= screenSize.Width ||
                            screenY < 0f || screenY >= screenSize.Height ||
                            star.Position.Z < 0f || star.Position.Z > 1000f)
                        {
                            ResetStar(star);
                        }

                        var starBrightness = 1f - Vector3.Distance(Vector3.Zero, star.Position) / 1000f;
                        var starDimmedColor = star.Color * starBrightness;
                        var starDrawColor = starDimmedColor.ToColor();
                        renderer.DrawColor = starDrawColor;
                        renderer.DrawPoint(screenX, screenY);
                    }
                }

                renderer.DrawColor = Color.White;
                renderer.DrawTextBlended(8, 8, lazyFont, $"FPS: {frameRate:0.0}");
                renderer.Present();

                if (accumulatedFrameTime >= TimeSpan.FromSeconds(1))
                {
                    frameRate = frameCounter / accumulatedFrameTime.TotalSeconds;
                    accumulatedFrameTime = TimeSpan.Zero;
                    frameCounter = 0;
                }
                else
                {
                    ++frameCounter;
                }

                lastFrameTime = currentFrameTime;
            }
        }
    }

    private static readonly Random _randomizer = new();

    private static IEnumerable<Star> GenerateStars(int count)
    {
        for (var i = 0; i < count; ++i)
        {
            var star = new Star();
            ResetStar(star);
            yield return star;
        }
    }

    private static void ResetStar(Star star)
    {
        star.Position = new Vector3(
            x: -500f + 1000f * _randomizer.NextSingle(),
            y: -500f + 1000f * _randomizer.NextSingle(),
            z: 100f + 900f * _randomizer.NextSingle());
        star.Velocity = new Vector3(
            x: 0f,
            y: 0f,
            z: -(.5f + 4.5f * _randomizer.NextSingle()));
        star.Color = new Rgb96f(
            r: _randomizer.NextSingle(),
            g: _randomizer.NextSingle(),
            b: _randomizer.NextSingle()
        );
    }
}
