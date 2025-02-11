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

namespace SDL2Sharp
{
    internal sealed class AudioSubsystem : IAudioSubsystem, IDisposable
    {
        private const uint InitSubsystemFlags = Interop.SDL.SDL_INIT_AUDIO;

        public AudioSubsystem()
        {
            Error.ThrowLastErrorIfNegative(
                Interop.SDL.InitSubSystem(InitSubsystemFlags)
            );
        }

        public void Dispose()
        {
            Interop.SDL.QuitSubSystem(InitSubsystemFlags);
        }

        public AudioDevice CreateDevice()
        {
            return new AudioDevice();
        }

        public AudioDevice OpenDevice(int frequency, AudioFormat format, AudioChannelLayout channels, ushort samples)
        {
            return new AudioDevice(frequency, format, channels, samples);
        }

        public AudioDevice OpenDevice(int frequency, AudioFormat format, AudioChannelLayout channels, ushort samples, AudioDeviceCallback callback)
        {
            return new AudioDevice(frequency, format, channels, samples, callback);
        }

        public AudioDevice OpenDevice(int frequency, AudioFormat format, AudioChannelLayout channels, ushort samples, AudioDeviceCallback callback, AudioDeviceAllowedChanges allowedChanges)
        {
            return new AudioDevice(frequency, format, channels, samples, callback, allowedChanges);
        }

        public WaveFile OpenWaveFile(string filename)
        {
            return new WaveFile(filename);
        }
    }
}
