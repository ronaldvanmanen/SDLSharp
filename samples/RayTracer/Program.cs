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
using SDL2Sharp;
using SDL2Sharp.Input;
using SDL2Sharp.Fonts;
using SDL2Sharp.Video;
using SDL2Sharp.Video.Colors;

internal static class Program
{
    public static void Main()
    {
        using var mainSystem = new MainSystem();
        using var videoSubystem = new VideoSubsystem();
        using var eventSubsystem = new EventSubsystem();
        using var fontSubsystem = new FontSubsystem();

        using var window = videoSubystem.CreateWindow("Ray Tracer", 640, 480, WindowFlags.Shown | WindowFlags.Resizable);
        using var renderer = window.CreateRenderer(RendererFlags.Accelerated | RendererFlags.PresentVSync);
        using var screenTexture = renderer.CreateTexture<Argb8888>(TextureAccess.Streaming, renderer.OutputSize);
        using var lazyFont = fontSubsystem.OpenFont("lazy.ttf", 28);

        var world = new World
        {
            Ambient = new Rgb32f(0.55f, 0.44f, 0.47f),
            Objects =
            {
                 // Backdrop Plane
                 new Plane
                 {
                     Position = new Vector3(0f, 0f, 0f),
                     Normal = new Vector3(0f, 1f, 0f),
                     Surface = new MatteSurface
                     {
                         DiffuseColor = new Rgb32f(1f, 1f, 1f),
                     }
                 },
                // Large center orange Sphere
                new Sphere
                {
                    Position = new Vector3(0f, 5.25f, 0f),
                    Radius = 10.5f / 2f,
                    Surface = new MatteSurface
                    {
                        DiffuseColor = new Rgb32f(0.89f, 0.48f, 0.42f)
                    }
                },
                // Small center yellow Sphere
                new Sphere
                {
                    Position = new Vector3(-3.5f, 1.6f, -6.7f),
                    Radius = 3.2f / 2f,
                    Surface = new MatteSurface
                    {
                        DiffuseColor = new Rgb32f(0.95f, 0.93f, 0.31f)
                    }
                },
                // Large back right pink Sphere
                new Sphere
                {
                    Position = new Vector3(14f, 7f, 6.5f),
                    Radius = 14f / 2f,
                    Surface = new MatteSurface
                    {
                        DiffuseColor = new Rgb32f(1f, 0.44f, 0.64f)
                    }
                },
                // Small front right orange Sphere
                new Sphere
                {
                    Position = new Vector3(8.2f, 3.5f, -6.5f),
                    Radius = 7f / 2f,
                    Surface = new MatteSurface
                    {
                        DiffuseColor = new Rgb32f(0.89f, 0.48f, 0.42f)
                    }
                },
                // Large back left pink Sphere
                new Sphere
                {
                    Position = new Vector3(-16.6f, 6.5f, 0f),
                    Radius = 13f / 2f,
                    Surface = new MatteSurface
                    {
                        DiffuseColor = new Rgb32f(1f, 0.44f, 0.64f)
                    }
                },
                // Medium front back left pink Sphere
                new Sphere
                {
                    Position = new Vector3(-9.5f, 3f, -6f),
                    Radius = 6f / 2f,
                    Surface = new MatteSurface
                    {
                        DiffuseColor = new Rgb32f(1f, 0.44f, 0.64f)
                    }
                },
                // Back left yellow Sphere
                new Sphere
                {
                    Position = new Vector3(-15f, 3f, 12f),
                    Radius = 6f / 2f,
                    Surface = new MatteSurface
                    {
                        DiffuseColor = new Rgb32f(0.95f, 0.93f, 0.31f)
                    }
                },
                // Far Back right blue Sphere
                new Sphere
                {
                    Position = new Vector3(40f, 10f, 175f),
                    Radius = 20f / 2f,
                    Surface = new MatteSurface
                    {
                        DiffuseColor = new Rgb32f(0.18f, 0.31f, 0.68f)
                    }
                },
            },
            Lights =
            {
                new PointLight
                {
                    Position = new Vector3(-300f, 350f, 10f),
                    Color = new Rgb32f(0.70f, 0.689f, 0.6885f)
                },
            },
        };

        var camera = new Camera();
        camera.Roll(180f * MathF.PI / 180f);
        camera.Pitch(-6f * MathF.PI / 180f);
        camera.Yaw(180f * MathF.PI / 180f);
        camera.MoveUp(-8.5f);
        camera.MoveBackward(26f);

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

                    case KeyUpEvent keyEvent:
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

                var keyboardState = Keyboard.State;
                if (keyboardState.IsPressed(Scancode.Up) || keyboardState.IsPressed(Scancode.Keypad8))
                {
                    camera.Pitch(1f * MathF.PI / 180f);
                }

                if (keyboardState.IsPressed(Scancode.Down) || keyboardState.IsPressed(Scancode.Keypad2))
                {
                    camera.Pitch(-1f * MathF.PI / 180f);
                }

                if (keyboardState.IsPressed(Scancode.Left) || keyboardState.IsPressed(Scancode.Keypad4))
                {
                    camera.Yaw(1f * MathF.PI / 180f);
                }

                if (keyboardState.IsPressed(Scancode.Right) || keyboardState.IsPressed(Scancode.Keypad6))
                {
                    camera.Yaw(-1f * MathF.PI / 180f);
                }

                if (keyboardState.IsPressed(Scancode.Keypad7))
                {
                    camera.Roll(1f * MathF.PI / 180f);
                }

                if (keyboardState.IsPressed(Scancode.Keypad9))
                {
                    camera.Roll(-1f * MathF.PI / 180f);
                }

                if (keyboardState.IsPressed(Scancode.W))
                {
                    camera.MoveForward(1);
                }

                if (keyboardState.IsPressed(Scancode.S))
                {
                    camera.MoveBackward(1);
                }

                if (keyboardState.IsPressed(Scancode.A))
                {
                    camera.MoveLeft(1);
                }

                if (keyboardState.IsPressed(Scancode.D))
                {
                    camera.MoveRight(1);
                }

                screenTexture.Update(camera.TakeSnapshot(world));

                renderer.BlendMode = BlendMode.None;
                renderer.DrawColor = Color.Black;
                renderer.Clear();
                renderer.Copy(screenTexture);
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
