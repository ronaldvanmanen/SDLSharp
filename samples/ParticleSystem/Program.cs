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
using SDL2Sharp;
using SDL2Sharp.Input;
using SDL2Sharp.Fonts;
using SDL2Sharp.Video;

internal static class Program
{
    public static void Main()
    {
        using var mainSystem = new MainSystem();
        using var videoSubsystem = new VideoSubsystem();
        using var eventSubsystem = new EventSubsystem();
        using var fontSubsystem = new FontSubsystem();

        using var window = videoSubsystem.CreateWindow("Particle System", 640, 480, WindowFlags.Shown | WindowFlags.Resizable);
        using var renderer = window.CreateRenderer(RendererFlags.Accelerated | RendererFlags.PresentVSync);
        using var lazyFont = fontSubsystem.OpenFont("lazy.ttf", 28);

        var particleEmitterColor = new Color(255, 0, 0, 255);
        var particleEmmiterPosition = new Point(window.Width / 2, window.Height / 2);
        var particleEmitter = new ParticleEmitter(particleEmitterColor, particleEmmiterPosition, 15);

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

                    case MouseMotionEvent motionEvent:
                        particleEmitter.MoveTo(motionEvent.X, motionEvent.Y);
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
                particleEmitter.Update(elapsedFrameTime);
                particleEmitter.Render(renderer);
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
}
