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
using SDL2Sharp.Audio;
using SDL2Sharp.Input;
using SDL2Sharp.Fonts;
using SDL2Sharp.Video;
using static System.Math;

internal static class Program
{
    public static void Main()
    {
        using var mainSystem = new MainSystem();
        using var videoSubsystem = new VideoSubsystem();
        using var audioSubsystem = new AudioSubsystem();
        using var eventSubsystem = new EventSubsystem();
        using var fontSubsystem = new FontSubsystem();

        using var window = videoSubsystem.CreateWindow("Wave Player", 640, 480, WindowFlags.Shown | WindowFlags.Resizable);
        using var renderer = window.CreateRenderer(RendererFlags.Accelerated | RendererFlags.PresentVSync);
        using var lazyFont = fontSubsystem.OpenFont("lazy.ttf", 28);
        using var audioFile = audioSubsystem.OpenWaveFile(Environment.GetCommandLineArgs()[1]);

        var audioPlaybackPosition = 0;
        var audioChannelCount = (int)audioFile.Channels;
        var audioSampleSize = audioFile.Format.BitSize() / 8;

        using var audioDevice = audioSubsystem.OpenDevice(
            audioFile.Frequency,
            audioFile.Format,
            audioFile.Channels,
            audioFile.Samples,
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

                        if (audioSampleOffset < audioFile.Buffer.Length)
                        {
                            var normalizedSample = audioFile.Buffer.ToNormalizedSingle(audioSampleOffset, audioFile.Format);
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
            stream.Fill(audioFile.Silence);
            var sliceLength = (int)Min(audioFile.Length - audioPlaybackPosition, stream.Length);
            if (sliceLength <= 0)
            {
                return;
            }
            var slice = audioFile.Buffer.Slice(audioPlaybackPosition, sliceLength);
            stream.MixAudioFormat(slice, audioFile.Format, AudioSubsystem.MixMaxVolume);
            audioPlaybackPosition += sliceLength;
        }
    }
}
