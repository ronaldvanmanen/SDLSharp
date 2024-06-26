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
using SDL2Sharp.Colors;
using static System.Math;
using static SDL2Sharp.Math;

namespace PlasmaFractal
{
    internal sealed class App : Application
    {
        private static readonly TimeSpan HideCursorDelay = TimeSpan.FromSeconds(1);

        private static readonly Font _frameRateFont = new("lazy.ttf", 28);

        private static readonly Color _frameRateColor = new(255, 255, 255, 255);

        private static readonly Color _backgroundColor = new(0, 0, 0, 255);

        private Window _window = null!;

        private Renderer _renderer = null!;

        private PackedTexture<Argb8888> _screenImage = null!;

        private MemoryImage<byte> _sourceImage = null!;

        private Palette<Argb8888> _palette = null!;

        private Stopwatch _realTime = null!;

        private Stopwatch _frameTime = null!;

        private int _frameCount;

        private double _frameRate;

        private volatile bool _reversePaletteRotation = false;

        private DateTime _cursorLastActive = DateTime.UtcNow;

        protected override void OnInitialized()
        {
            _window = new Window("Plasma Fractal", 512, 512, WindowFlags.Shown | WindowFlags.Resizable);
            _window.KeyDown += OnWindowKeyDown;
            _window.SizeChanged += OnWindowSizeChanged;
            _window.MouseMotion += OnWindowMouseMotion;
            _renderer = _window.CreateRenderer(RendererFlags.Accelerated | RendererFlags.PresentVSync);
            _screenImage = _renderer.CreateTexture<Argb8888>(TextureAccess.Streaming, _renderer.OutputSize);
            _sourceImage = GenerateDiamondSquareImage(_renderer.OutputSize);
            _palette = GeneratePalette();
            _realTime = new Stopwatch();
            _frameTime = new Stopwatch();
            _frameCount = 0;
            _frameRate = double.NaN;
            _realTime.Start();
        }

        protected override void OnQuiting()
        {
            _realTime.Stop();
            _renderer?.Dispose();
            _window?.Dispose();
        }

        protected override void OnIdle()
        {
            if (Cursor.Shown)
            {
                var cursorInactivity = DateTime.UtcNow - _cursorLastActive;
                if (cursorInactivity >= HideCursorDelay)
                {
                    Cursor.Hide();
                }
            }

            Render(_realTime.Elapsed);
        }

        private void Render(TimeSpan realTime)
        {
            _frameTime.Start();

            _screenImage.WithLock(screenImage =>
            {
                for (var y = 0; y < screenImage.Height; ++y)
                {
                    for (var x = 0; x < screenImage.Width; ++x)
                    {
                        screenImage[y, x] = _palette[_sourceImage[y, x]];
                    }
                }
            });

            _renderer.BlendMode = BlendMode.None;
            _renderer.DrawColor = _backgroundColor;
            _renderer.Clear();
            _renderer.Copy(_screenImage);
            _renderer.DrawColor = _frameRateColor;
            _renderer.DrawTextBlended(8, 8, _frameRateFont, $"FPS: {_frameRate:0.0}");
            _renderer.Present();

            if (_reversePaletteRotation)
            {
                _palette.RotateRight();
            }
            else
            {
                _palette.RotateLeft();
            }

            _frameTime.Stop();

            if (_frameTime.ElapsedMilliseconds >= 1000d)
            {
                _frameRate = _frameCount * 1000d / _frameTime.ElapsedMilliseconds;
                _frameCount = 0;
                _frameTime.Reset();
            }
            else
            {
                ++_frameCount;
            }
        }

        private void OnWindowKeyDown(object? sender, KeyEventArgs e)
        {
            if (sender is Window window)
            {
                if (e.KeyCode == KeyCode.F11 || (e.KeyCode == KeyCode.Return && e.Alt))
                {
                    window.IsFullScreenDesktop = !window.IsFullScreenDesktop;
                }

                if (e.KeyCode == KeyCode.R)
                {
                    _reversePaletteRotation = !_reversePaletteRotation;
                }
            }
        }

        private void OnWindowSizeChanged(object? sender, WindowSizeChangedEventArgs e)
        {
            _screenImage?.Dispose();
            _renderer?.Dispose();
            _renderer = _window.CreateRenderer(RendererFlags.Accelerated | RendererFlags.PresentVSync);
            _screenImage = _renderer.CreateTexture<Argb8888>(TextureAccess.Streaming, _renderer.OutputSize);
            _sourceImage = GenerateDiamondSquareImage(_renderer.OutputSize);
        }

        private void OnWindowMouseMotion(object? sender, MouseMotionEventArgs e)
        {
            if (Cursor.Hidden)
            {
                Cursor.Show();
            }

            _cursorLastActive = DateTime.UtcNow;
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

        private static int Main()
        {
            var app = new App();
            var exitCode = app.Run();
            return exitCode;
        }
    }
}
