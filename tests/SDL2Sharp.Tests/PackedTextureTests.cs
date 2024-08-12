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
using SDL2Sharp.Video;
using SDL2Sharp.Video.PixelFormats;
using Xunit;

namespace SDL2Sharp.Tests
{
    public static class PackedTextureTests
    {
        private static readonly Random _random = new();

        [Fact]
        public static void WriteAndReadAbgr1555() => WriteAndRead
        (
            () => ABGR1555.FromRGBA(
                r: (byte)_random.Next(0, 256),
                g: (byte)_random.Next(0, 256),
                b: (byte)_random.Next(0, 256),
                a: (byte)_random.Next(0, 256)
            )
        );

        [Fact]
        public static void WriteAndReadAbgr4444() => WriteAndRead
        (
            () => ABGR4444.FromRGBA(
                r: (byte)_random.Next(0, 256),
                g: (byte)_random.Next(0, 256),
                b: (byte)_random.Next(0, 256),
                a: (byte)_random.Next(0, 256)
            )
        );

        [Fact]
        public static void WriteAndReadAbgr8888() => WriteAndRead
        (
            () => ABGR8888.FromRGBA(
                r: (byte)_random.Next(0, 256),
                g: (byte)_random.Next(0, 256),
                b: (byte)_random.Next(0, 256),
                a: (byte)_random.Next(0, 256)
            )
        );

        [Fact]
        public static void WriteAndReadArgb1555() => WriteAndRead
        (
            () => ARGB1555.FromRGBA(
                r: (byte)_random.Next(0, 256),
                g: (byte)_random.Next(0, 256),
                b: (byte)_random.Next(0, 256),
                a: (byte)_random.Next(0, 256)
            )
        );

        [Fact]
        public static void WriteAndReadArgb2101010() => WriteAndRead
        (
            () => ARGB2101010.FromRGBA(
                r: (byte)_random.Next(0, 256),
                g: (byte)_random.Next(0, 256),
                b: (byte)_random.Next(0, 256),
                a: (byte)_random.Next(0, 256)
            )
        );

        [Fact]
        public static void WriteAndReadArgb4444() => WriteAndRead
        (
            () => ARGB4444.FromRGBA(
                r: (byte)_random.Next(0, 256),
                g: (byte)_random.Next(0, 256),
                b: (byte)_random.Next(0, 256),
                a: (byte)_random.Next(0, 256)
            )
        );

        [Fact]
        public static void WriteAndReadArgb8888() => WriteAndRead(
            () => ARGB8888.FromRGBA(
                r: (byte)_random.Next(0, 256),
                g: (byte)_random.Next(0, 256),
                b: (byte)_random.Next(0, 256),
                a: (byte)_random.Next(0, 256)
            )
        );

        [Fact]
        public static void WriteAndReadRgba8888() => WriteAndRead(
            () => new RGBA8888(
                a: (byte)_random.Next(0, 256),
                r: (byte)_random.Next(0, 256),
                g: (byte)_random.Next(0, 256),
                b: (byte)_random.Next(0, 256)
            )
        );

        [Fact]
        public static void WriteAndReadBgr565() => WriteAndRead(
            () => BGR565.FromRGB(
                r: (byte)_random.Next(0, 256),
                g: (byte)_random.Next(0, 256),
                b: (byte)_random.Next(0, 256)
            )
        );

        [Fact]
        public static void WriteAndReadBgra4444() => WriteAndRead(
            () => BGRA4444.FromRGBA(
                r: (byte)_random.Next(0, 256),
                g: (byte)_random.Next(0, 256),
                b: (byte)_random.Next(0, 256),
                a: (byte)_random.Next(0, 256)
            )
        );

        [Fact]
        public static void WriteAndReadBgra5551() => WriteAndRead(
            () => BGRA4444.FromRGBA(
                r: (byte)_random.Next(0, 256),
                g: (byte)_random.Next(0, 256),
                b: (byte)_random.Next(0, 256),
                a: (byte)_random.Next(0, 256)
            )
        );

        [Fact]
        public static void WriteAndReadBgra8888() => WriteAndRead(
            () => new BGRA8888(
                b: (byte)_random.Next(0, 256),
                g: (byte)_random.Next(0, 256),
                r: (byte)_random.Next(0, 256),
                a: (byte)_random.Next(0, 256)
            )
        );

        //[Fact]
        //public static void WriteAndReadRgb332() => WriteAndRead(
        //    () => Rgb332.FromRGB(
        //        r: (byte)_random.Next(0, 256),
        //        g: (byte)_random.Next(0, 256),
        //        b: (byte)_random.Next(0, 256)
        //    )
        //);

        [Fact]
        public static void WriteAndReadRgb565() => WriteAndRead(
            () => RGB565.FromRGB(
                r: (byte)_random.Next(0, 256),
                g: (byte)_random.Next(0, 256),
                b: (byte)_random.Next(0, 256)
            )
        );

        private static void WriteAndRead<TPackedPixelFormat>(Func<TPackedPixelFormat> colorGenerator)
            where TPackedPixelFormat : struct, IPackedPixel<TPackedPixelFormat>
        {
            using var mainSystem = new MainSystem();
            using var videoSystem = new VideoSubsystem();
            using var window = videoSystem.CreateWindow("PackedTextureTests", 640, 480, WindowFlags.Hidden);
            using var renderer = window.CreateRenderer(RendererFlags.Software | RendererFlags.TargetTexture);
            using var sourceTexture = renderer.CreatePackedTexture<TPackedPixelFormat>(TextureAccess.Streaming, renderer.OutputSize);
            using var targetTexture = renderer.CreatePackedTexture<TPackedPixelFormat>(TextureAccess.Target, renderer.OutputSize);
            var sourceImage = GenerateImage(renderer.OutputSize, colorGenerator);

            sourceTexture.Update(sourceImage);

            //sourceTexture.WithLock(pixels =>
            //{
            //    for (var y = 0; y < pixels.Height; y++)
            //    {
            //        for (var x = 0; x < pixels.Width; x++)
            //        {
            //            pixels[x, y] = sourceImage[x, y];
            //        }
            //    }
            //});

            renderer.Target = targetTexture;
            renderer.Copy(sourceTexture);
            renderer.Present();

            var targetImage = renderer.ReadPixels<TPackedPixelFormat>();

            Assert.Equal(sourceImage.Size, targetImage.Size);
            Assert.Equal(sourceImage.Height, targetImage.Height);
            Assert.Equal(sourceImage.Width, targetImage.Width);
            Assert.Equal(sourceImage, targetImage, (expectedImage, actualImage) =>
            {
                if (expectedImage is null && actualImage is null)
                {
                    return true;
                }

                if (expectedImage is null || actualImage is null)
                {
                    return false;
                }

                if (expectedImage.Size != actualImage.Size)
                {
                    return false;
                }

                for (var y = 0; y < expectedImage.Height; y++)
                {
                    for (var x = 0; x < expectedImage.Width; x++)
                    {
                        if (!expectedImage[x, y].Equals(actualImage[x, y]))
                        {
                            return false;
                        }
                    }
                }

                return true;
            });
        }

        private static PackedMemoryImage<TColor> GenerateImage<TColor>(Size size, Func<TColor> createRandomColor) where TColor : struct
        {
            var image = new PackedMemoryImage<TColor>(size.Width, size.Height);
            for (var y = 0; y < image.Height; y++)
            {
                for (var x = 0; x < image.Width; x++)
                {
                    image[x, y] = createRandomColor();
                }
            }
            return image;
        }
    }
}
