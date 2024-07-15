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
using SDL2Sharp.Video.Colors;
using static System.Math;
using static SDL2Sharp.Math;

internal static class Program
{
    public static void Main()
    {
        using var mainSystem = new MainSystem();
        using var videoSubystem = new VideoSubsystem();
        using var eventSubsystem = new EventSubsystem();
        using var fontSubsystem = new FontSubsystem();

        using var window = videoSubystem.CreateWindow("Plasma Fractal", 640, 480, WindowFlags.Shown | WindowFlags.Resizable);
        using var renderer = window.CreateRenderer(RendererFlags.Accelerated | RendererFlags.PresentVSync);
        using var screenTexture = renderer.CreateTexture<Argb8888>(TextureAccess.Streaming, renderer.OutputSize);
        using var lazyFont = fontSubsystem.OpenFont("lazy.ttf", 28);

        var sourceImage = GenerateDiamondSquareImage(renderer.OutputSize);
        var palette = GeneratePalette();
        var reversePaletteRotation = false;

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

                            case KeyCode.R:
                                reversePaletteRotation = !reversePaletteRotation;
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

                screenTexture.WithLock(screenImage =>
                {
                    for (var y = 0; y < screenImage.Height; ++y)
                    {
                        for (var x = 0; x < screenImage.Width; ++x)
                        {
                            screenImage[y, x] = palette[sourceImage[y, x]];
                        }
                    }
                });

                renderer.BlendMode = BlendMode.None;
                renderer.DrawColor = Color.Black;
                renderer.Clear();
                renderer.Copy(screenTexture);
                renderer.DrawColor = Color.White;
                renderer.DrawTextBlended(8, 8, lazyFont, $"FPS: {frameRate:0.0}");
                renderer.Present();

                if (reversePaletteRotation)
                {
                    palette.RotateRight();
                }
                else
                {
                    palette.RotateLeft();
                }

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

    private static readonly Random _random = new();

    private static Palette<Argb8888> GeneratePalette()
    {
        var palette = new Palette<Argb8888>(256);
        for (var i = 0; i < 32; ++i)
        {
            var lo = (byte)(i * 255 / 31);
            var hi = (byte)(255 - lo);
            palette[i] = new Argb8888(0xFF, lo, 0, 0);
            palette[i + 32] = new Argb8888(0xFF, hi, 0, 0);
            palette[i + 64] = new Argb8888(0xFF, 0, lo, 0);
            palette[i + 96] = new Argb8888(0xFF, 0, hi, 0);
            palette[i + 128] = new Argb8888(0xFF, 0, 0, lo);
            palette[i + 160] = new Argb8888(0xFF, 0, 0, hi);
            palette[i + 192] = new Argb8888(0xFF, lo, 0, lo);
            palette[i + 224] = new Argb8888(0xFF, hi, 0, hi);
        }
        return palette;
    }

    private static MemoryImage<byte> GenerateDiamondSquareImage(Size size)
    {
        return GenerateDiamondSquareImage(size.Width, size.Height);
    }

    private static MemoryImage<byte> GenerateDiamondSquareImage(int width, int height)
    {
        var size = NextPowerOfTwo(Max(width, height)) + 1;
        var image = GenerateDiamondSquareImage(size);
        return image.Crop(0, 0, height, width);
    }

    private static MemoryImage<byte> GenerateDiamondSquareImage(int size)
    {
        var image = new MemoryImage<byte>(size, size);

        var randomness = 256;

        image[0, 0] = (byte)_random.Next(0, randomness);
        image[0, size - 1] = (byte)_random.Next(0, randomness);
        image[size - 1, 0] = (byte)_random.Next(0, randomness);
        image[size - 1, size - 1] = (byte)_random.Next(0, randomness);

        randomness /= 2;

        for (var stepSize = size - 1; stepSize > 1; stepSize /= 2)
        {
            var halfStepSize = stepSize / 2;

            for (var y = halfStepSize; y < image.Height; y += stepSize)
            {
                for (var x = halfStepSize; x < image.Width; x += stepSize)
                {
                    Diamond(image, x, y, halfStepSize, randomness);
                }
            }

            for (var y = 0; y < image.Height; y += halfStepSize)
            {
                for (var x = (y % stepSize) == 0 ? halfStepSize : 0; x < image.Width; x += stepSize)
                {
                    Square(image, x, y, halfStepSize, randomness);
                }
            }

            randomness /= 2;
        }
        return image;
    }

    private static void Diamond(MemoryImage<byte> map, int centerX, int centerY, int distance, int randomness)
    {
        var sum = 0;
        var count = 0;
        var top = centerY - distance;
        if (top >= 0 && top < map.Height)
        {
            var left = centerX - distance;
            if (left >= 0 && left < map.Width)
            {
                sum += map[top, left];
                count++;
            }

            var right = centerX + distance;
            if (right >= 0 && right < map.Height)
            {
                sum += map[top, right];
                count++;
            }
        }

        var bottom = centerY + distance;
        if (bottom >= 0 && bottom < map.Height)
        {
            var left = centerX - distance;
            if (left >= 0 && left < map.Width)
            {
                sum += map[bottom, left];
                count++;
            }

            var right = centerX + distance;
            if (right >= 0 && right < map.Height)
            {
                sum += map[bottom, right];
                count++;
            }
        }

        var average = sum / count;
        var random = _random.Next(-randomness, randomness);
        var value = Clamp(average + random, 0, 255);

        map[centerY, centerX] = (byte)value;
    }

    private static void Square(MemoryImage<byte> map, int centerX, int centerY, int distance, int randomness)
    {
        var sum = 0;
        var count = 0;
        var top = centerY - distance;
        if (top >= 0 && top < map.Height)
        {
            sum += map[top, centerX];
            count++;
        }

        var left = centerX - distance;
        if (left >= 0 && left < map.Width)
        {
            sum += map[centerY, left];
            count++;
        }

        var bottom = centerY + distance;
        if (bottom >= 0 && bottom < map.Height)
        {
            sum += map[bottom, centerX];
            count++;
        }

        var right = centerX + distance;
        if (right >= 0 && right < map.Height)
        {
            sum += map[centerY, right];
            count++;
        }

        var average = sum / count;
        var random = _random.Next(-randomness, randomness);
        var value = Clamp(average + random, 0, 255);

        map[centerY, centerX] = (byte)value;
    }
}
