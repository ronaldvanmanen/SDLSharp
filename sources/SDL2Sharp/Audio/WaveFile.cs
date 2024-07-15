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
using SDL2Sharp.Internals;
using SDL2Sharp.Interop;

namespace SDL2Sharp.Audio
{
    public sealed unsafe class WaveFile : IDisposable
    {
        private SDL_AudioSpec _waveSpec;

        private byte* _waveBuffer;

        private uint _waveLength;

        public int Frequency => _waveSpec.freq;

        public AudioFormat Format => (AudioFormat)_waveSpec.format;

        public AudioChannelLayout Channels => (AudioChannelLayout)_waveSpec.channels;

        public byte Silence => _waveSpec.silence;

        public ushort Samples => _waveSpec.samples;

        public uint Size => _waveSpec.size;

        public ReadOnlySpan<byte> Buffer => new(_waveBuffer, (int)_waveLength);

        public uint Length => _waveLength;

        internal WaveFile(string filename)
        {
            using var unmanagedFilename = new MarshaledString(filename);
            using var unmanagedMode = new MarshaledString("rb");
            var fileStream = Error.ReturnOrThrowOnFailure(
                SDL.RWFromFile(unmanagedFilename, unmanagedMode)
            );

            fixed (SDL_AudioSpec* waveSpec = &_waveSpec)
            fixed (byte** waveBuffer = &_waveBuffer)
            fixed (uint* waveLength = &_waveLength)
            {
                Error.ThrowOnFailure(
                    SDL.LoadWAV_RW(fileStream, 1, waveSpec, waveBuffer, waveLength)
                );
            }
        }

        ~WaveFile()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool _)
        {
            if (_waveBuffer != null)
            {
                SDL.FreeWAV(_waveBuffer);
                _waveSpec = default;
                _waveBuffer = null;
                _waveLength = 0;
            }
        }
    }
}
