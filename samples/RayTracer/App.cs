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
using SDL2Sharp.Colors;

namespace RayTracer
{
    internal sealed class App : Application
    {
        private static readonly Font _frameRateFont = new("lazy.ttf", 28);

        private static readonly Color _frameRateTextColor = new(255, 255, 255, 255);

        private static readonly Color _backgroundColor = new(0, 0, 0, 255);

        private Window _window = null!;

        private Renderer _renderer = null!;

        private PackedTexture<Argb8888> _screenTexture = null!;

        private MemoryImage<Argb8888> _screenImage = null!;

        private Stopwatch _frameTime = null!;

        private int _framesCounted = 0;

        private double _framesPerSecond = double.NaN;

        private World _world = null!;

        private Camera _camera = null!;

        protected override void OnInitialized()
        {
            _window = new Window("Ray Tracer", 640, 480, WindowFlags.Resizable);
            _window.KeyDown += OnWindowKeyDown;
            _window.SizeChanged += OnWindowSizeChanged;
            _window.MouseWheel += OnMouseWheel;
            _renderer = _window.CreateRenderer(RendererFlags.Accelerated | RendererFlags.PresentVSync);
            _screenTexture = _renderer.CreateTexture<Argb8888>(TextureAccess.Streaming, _renderer.OutputSize);
            _screenImage = new MemoryImage<Argb8888>(_renderer.OutputSize);
            _frameTime = new Stopwatch();
            _world = new World
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
                    //// Large back right pink Sphere
                    //new Sphere
                    //{
                    //    Position = new Vector3(14f, 7f, 6.5f),
                    //    Radius = 14f / 2f,
                    //    Surface = new MatteSurface
                    //    {
                    //        DiffuseColor = new Rgb32f(1f, 0.44f, 0.64f)
                    //    }
                    //},
                    //// Small front right orange Sphere
                    //new Sphere
                    //{
                    //    Position = new Vector3(8.2f, 3.5f, -6.5f),
                    //    Radius = 7f / 2f,
                    //    Surface = new MatteSurface
                    //    {
                    //        DiffuseColor = new Rgb32f(0.89f, 0.48f, 0.42f)
                    //    }
                    //},
                    //// Large back left pink Sphere
                    //new Sphere
                    //{
                    //    Position = new Vector3(-16.6f, 6.5f, 0f),
                    //    Radius = 13f / 2f,
                    //    Surface = new MatteSurface
                    //    {
                    //        DiffuseColor = new Rgb32f(1f, 0.44f, 0.64f)
                    //    }
                    //},
                    //// Medium front back left pink Sphere
                    //new Sphere
                    //{
                    //    Position = new Vector3(-9.5f, 3f, -6f),
                    //    Radius = 6f / 2f,
                    //    Surface = new MatteSurface
                    //    {
                    //        DiffuseColor = new Rgb32f(1f, 0.44f, 0.64f)
                    //    }
                    //},
                    //// Back left yellow Sphere
                    //new Sphere
                    //{
                    //    Position = new Vector3(-15f, 3f, 12f),
                    //    Radius = 6f / 2f,
                    //    Surface = new MatteSurface
                    //    {
                    //        DiffuseColor = new Rgb32f(0.95f, 0.93f, 0.31f)
                    //    }
                    //},
                    //// Far Back right blue Sphere
                    //new Sphere
                    //{
                    //    Position = new Vector3(40f, 10f, 175f),
                    //    Radius = 20f / 2f,
                    //    Surface = new MatteSurface
                    //    {
                    //        DiffuseColor = new Rgb32f(0.18f, 0.31f, 0.68f)
                    //    }
                    //},
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

            _camera = new Camera();
            _camera.Rotate(
                -6f * MathF.PI / 180f,
                180f * MathF.PI / 180f,
                0f
            );
            _camera.Move(0f, 8.5f, -26f);
        }

        protected override void OnQuiting()
        {
            _window?.Dispose();
        }

        protected override void OnIdle()
        {
            _frameTime.Start();
            Render();
            _frameTime.Stop();
            _framesCounted++;

            if (_frameTime.ElapsedMilliseconds >= 1000)
            {
                _framesPerSecond = _framesCounted / _frameTime.Elapsed.TotalMilliseconds * 1000;
                _framesCounted = 0;
                _frameTime.Reset();
            }
        }

        private void Render()
        {
            _camera.Shoot(_world, _screenImage);
            _screenTexture.Update(_screenImage);
            _renderer.BlendMode = BlendMode.None;
            _renderer.DrawColor = _backgroundColor;
            _renderer.Clear();
            _renderer.Copy(_screenTexture);
            _renderer.DrawColor = _frameRateTextColor;
            _renderer.DrawTextBlended(8, 8, _frameRateFont, $"FPS: {_framesPerSecond:0.0}");
            _renderer.Present();
        }

        private void OnWindowKeyDown(object? sender, KeyEventArgs e)
        {
            if (sender is not Window window)
            {
                return;
            }

            switch (e.KeyCode)
            {
                case KeyCode.F11:
                    window.IsFullScreenDesktop = !window.IsFullScreenDesktop;
                    break;

                case KeyCode.Return:
                    if (e.Alt)
                    {
                        window.IsFullScreenDesktop = !window.IsFullScreenDesktop;
                    }
                    break;

                case KeyCode.W:
                {
                    _camera.Move(0, 0, 1);
                    break;
                }

                case KeyCode.S:
                {
                    _camera.Move(0, 0, -1);
                    break;
                }

                case KeyCode.A:
                {
                    _camera.Move(-1, 0, 0);
                    break;
                }

                case KeyCode.D:
                {
                    _camera.Move(1, 0, 0);
                    break;
                }

                case KeyCode.Up:
                {
                    _camera.RotateX(1f * MathF.PI / 180f);
                    break;
                }

                case KeyCode.Down:
                {
                    _camera.RotateX(-1f * MathF.PI / 180f);
                    break;
                }

                case KeyCode.Left:
                {
                    _camera.RotateY(1f * MathF.PI / 180f);
                    break;
                }

                case KeyCode.Right:
                {
                    _camera.RotateY(-1f * MathF.PI / 180f);
                    break;
                }
            }
        }

        private void OnWindowSizeChanged(object? sender, WindowSizeChangedEventArgs e)
        {
            _screenTexture?.Dispose();
            _renderer?.Dispose();
            _renderer = _window.CreateRenderer(RendererFlags.Accelerated | RendererFlags.PresentVSync);
            _screenTexture = _renderer.CreateTexture<Argb8888>(TextureAccess.Streaming, _renderer.OutputSize);
            _screenImage = new MemoryImage<Argb8888>(_renderer.OutputSize);
        }

        private void OnMouseWheel(object? sender, MouseWheelEventArgs e)
        {
            _camera.FieldOfView -= e.Y;
        }

        private static int Main()
        {
            var app = new App();
            var exitCode = app.Run();
            return exitCode;
        }
    }
}
