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
using static System.Math;
using static SDL2Sharp.StaticMethods;

internal static class Program
{
    public static void Main()
    {
#pragma warning disable IDE1006 // Naming Styles
        using var SDL = new SDL();
#pragma warning restore IDE1006 // Naming Styles

        using var window = SDL.Video.CreateWindow("Wave Player", 640, 480, WindowFlags.Shown | WindowFlags.Resizable);
        using var renderer = window.CreateRenderer(RendererFlags.Accelerated | RendererFlags.PresentVSync);
        using var lazyFont = SDL.Fonts.OpenFont("lazy.ttf", 28);
        using var waveFile = SDL.Audio.OpenWaveFile(Environment.GetCommandLineArgs()[1]);

        var audioPlaybackPosition = 0;
        var audioChannelCount = (int)waveFile.Channels;
        var audioSampleSize = waveFile.Format.BitSize() / 8;

        using var audioDevice = SDL.Audio.OpenDevice(
            waveFile.Frequency,
            waveFile.Format,
            waveFile.Channels,
            waveFile.Samples,
            OnAudioDeviceCallback,
            AudioDeviceAllowedChanges.None);

        var lastFrameTime = TimeSpan.Zero;
        var accumulatedFrameTime = TimeSpan.Zero;
        var frameCounter = 0;
        var frameRate = 0d;

        var programClock = Stopwatch.StartNew();

        audioDevice.Unpause();

        while (true)
        {
            var @event = SDL.Events.PollEvent();
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

                            case KeyCode.Space:
                                if (audioDevice.Status == AudioStatus.Paused)
                                {
                                    audioDevice.Unpause();
                                }
                                else
                                {
                                    audioDevice.Pause();
                                }
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
                renderer.Clear();
                renderer.DrawColor = Color.Yellow;

                var audioWavePoints = new Point[renderer.OutputSize.Width];
                var renderChannelHeight = renderer.OutputSize.Height / audioChannelCount;
                var renderHalfGraphHeight = renderChannelHeight * 9 / 20;
                for (var channel = 0; channel < audioChannelCount; ++channel)
                {
                    var renderCenterLine = channel * renderChannelHeight + renderChannelHeight / 2;
                    var audioSampleOffset = audioPlaybackPosition + audioSampleSize * channel;
                    for (var x = 0; x < audioWavePoints.Length; ++x)
                    {
                        var y = renderCenterLine;

                        if (audioSampleOffset < waveFile.Buffer.Length)
                        {
                            var normalizedSample = waveFile.Buffer.ToNormalizedSingle(audioSampleOffset, waveFile.Format);
                            y = (int)(renderCenterLine + normalizedSample * renderHalfGraphHeight);
                        }

                        audioWavePoints[x] = new Point(x, y);

                        audioSampleOffset += audioSampleSize * audioChannelCount;
                    }

                    renderer.DrawLines(audioWavePoints);
                }

                renderer.DrawColor = Color.Blue;

                for (var channel = 0; channel <= audioChannelCount; ++channel)
                {
                    var y = channel * renderChannelHeight;
                    renderer.DrawLine(0, y, renderer.OutputSize.Width - 1, y);
                }

                renderer.DrawColor = Color.Black;
                renderer.DrawTextBlendedCentered(lazyFont, $"Average Frames Per Second: {frameRate:0.0}");
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

        void OnAudioDeviceCallback(Span<byte> stream)
        {
            stream.Fill(waveFile.Silence);
            var sliceLength = (int)Min(waveFile.Length - audioPlaybackPosition, stream.Length);
            if (sliceLength <= 0)
            {
                return;
            }
            var slice = waveFile.Buffer.Slice(audioPlaybackPosition, sliceLength);
            MixAudioFormat(stream, slice, waveFile.Format, AudioConstants.MixMaxVolume);
            audioPlaybackPosition += sliceLength;
        }
    }
}
