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

        using var window = videoSubystem.CreateWindow("Tunnel Effect", 640, 480, WindowFlags.Shown | WindowFlags.Resizable);
        using var renderer = window.CreateRenderer(RendererFlags.Accelerated | RendererFlags.PresentVSync);
        using var screenTexture = renderer.CreateTexture<Argb8888>(TextureAccess.Streaming, renderer.OutputSize);
        using var lazyFont = fontSubsystem.OpenFont("lazy.ttf", 28);

        var screenSize = renderer.OutputSize;
        var sourceImageSize = NextPowerOfTwo(Max(screenSize.Width, screenSize.Height));
        var sourceImage = GenerateXorImage(sourceImageSize);
        var transformTable = GenerateTransformTable(sourceImageSize);

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

                screenTexture.WithLock(screenImage =>
                {
                    var screenWidth = screenImage.Width;
                    var screenHeight = screenImage.Height;

                    var sourceWidth = sourceImage.Width;
                    var sourceHeight = sourceImage.Height;
                    var sourceWidthMask = sourceWidth - 1;
                    var sourceHeightMask = sourceHeight - 1;

                    var shiftX = (int)(screenWidth * 1.0 * currentFrameTime.TotalSeconds);
                    var shiftY = (int)(screenHeight * 0.25 * currentFrameTime.TotalSeconds);
                    var lookX = (sourceWidth - screenWidth) / 2;
                    var lookY = (sourceHeight - screenHeight) / 2;
                    var shiftLookX = shiftX + lookX;
                    var shiftLookY = shiftY + lookY;

                    for (var screenY = 0; screenY < screenHeight; ++screenY)
                    {
                        var transformY = screenY + lookY;
                        for (var screenX = 0; screenX < screenWidth; ++screenX)
                        {
                            var transformX = screenX + lookX;
                            var transform = transformTable[transformY, transformX];
                            var sourceX = (transform.Distance + shiftLookX) & sourceWidthMask;
                            var sourceY = (transform.Angle + shiftLookY) & sourceHeightMask;
                            screenImage[screenY, screenX] = sourceImage[sourceY, sourceX];
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

    private static MemoryImage<Argb8888> GenerateXorImage(int size)
    {
        return GenerateXorImage(size, size);
    }

    private static MemoryImage<Argb8888> GenerateXorImage(int width, int height)
    {
        var image = new MemoryImage<Argb8888>(width, height);
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                image[y, x] = new Argb8888(
                    a: 0xFF,
                    r: 0x00,
                    g: 0x00,
                    b: (byte)((x * 256 / width) ^ (y * 256 / height))
                );
            }
        }
        return image;
    }

    private static MemoryImage<Transform> GenerateTransformTable(int size)
    {
        return GenerateTransformTable(size, size);
    }

    private static MemoryImage<Transform> GenerateTransformTable(int width, int height)
    {
        const double ratio = 32d;
        var transformTable = new MemoryImage<Transform>(width, height);
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                unchecked
                {
                    var angle = (int)(0.5 * width * Atan2(y - height / 2.0, x - width / 2.0) / PI);
                    var distance = (int)(ratio * height / Sqrt((x - width / 2d) * (x - width / 2d) + (y - height / 2d) * (y - height / 2d))) % height;
                    transformTable[y, x] = new Transform(angle, distance);
                }
            }
        }
        return transformTable;
    }
}
