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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL2Sharp.Interop;

namespace SDL2Sharp
{
    public sealed unsafe class AudioDevice : IDisposable
    {
        private uint _deviceID = 0;

        private AudioDeviceCallback _callback = null!;

        private GCHandle _callbackUserData = default;

        private bool _disposed = false;

        public bool IsDisposed => _disposed;

        public bool IsOpen => _deviceID != 0;

        public int Frequency { get; private set; }

        public AudioFormat Format { get; private set; }

        public AudioChannelLayout Channels { get; private set; }

        public byte Silence { get; private set; }

        public ushort Samples { get; private set; }

        public uint Size { get; private set; }

        public AudioStatus Status => (AudioStatus)Interop.SDL.GetAudioDeviceStatus(_deviceID);

        internal AudioDevice()
        { }

        internal AudioDevice(int frequency, AudioFormat format, AudioChannelLayout channels, ushort samples)
        : this(frequency, format, channels, samples, null!)
        { }

        internal AudioDevice(int frequency, AudioFormat format, AudioChannelLayout channels, ushort samples, AudioDeviceCallback callback)
        : this(frequency, format, channels, samples, callback, AudioDeviceAllowedChanges.None)
        { }

        internal AudioDevice(int frequency, AudioFormat format, AudioChannelLayout channels, ushort samples, AudioDeviceCallback callback, AudioDeviceAllowedChanges allowedChanges)
        {
            Open(frequency, format, channels, samples, callback, allowedChanges);
        }

        ~AudioDevice()
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
            if (!_disposed)
            {
                try
                {
                    Close();
                }
                finally
                {
                    _disposed = true;
                }
            }
        }

        public void Open(int frequency, AudioFormat format, AudioChannelLayout channels, ushort samples)
        {
            Open(frequency, format, channels, samples, null!);
        }

        public void Open(int frequency, AudioFormat format, AudioChannelLayout channels, ushort samples, AudioDeviceCallback callback)
        {
            Open(frequency, format, channels, samples, callback, AudioDeviceAllowedChanges.None);
        }

        public void Open(int frequency, AudioFormat format, AudioChannelLayout channels, ushort samples, AudioDeviceCallback callback, AudioDeviceAllowedChanges allowedChanges)
        {
            if (!TryOpen(frequency, format, channels, samples, callback, allowedChanges, out var error))
            {
                throw error;
            }
        }

        public bool TryOpen(int frequency, AudioFormat format, AudioChannelLayout channels, ushort samples, out Error error)
        {
            return TryOpen(frequency, format, channels, samples, null!, out error);
        }

        public bool TryOpen(int frequency, AudioFormat format, AudioChannelLayout channels, ushort samples, AudioDeviceCallback callback, out Error error)
        {
            return TryOpen(frequency, format, channels, samples, callback, AudioDeviceAllowedChanges.None, out error);
        }

        public bool TryOpen(int frequency, AudioFormat format, AudioChannelLayout channels, ushort samples, AudioDeviceCallback callback, AudioDeviceAllowedChanges allowedChanges, out Error error)
        {
            ThrowIfDisposed();
            ThrowIfOpen();

            var desiredSpec = new SDL_AudioSpec
            {
                freq = frequency,
                format = (ushort)format,
                channels = (byte)channels,
                silence = 0,
                samples = samples,
                padding = 0,
                size = 0
            };

            if (callback != null)
            {
                _callback = callback;
                _callbackUserData = GCHandle.Alloc(this, GCHandleType.Normal);
                desiredSpec.callback = &OnAudioDeviceCallback;
                desiredSpec.userdata = (void*)(IntPtr)_callbackUserData;
            }

            var obtainedSpec = new SDL_AudioSpec();
            _deviceID = Interop.SDL.OpenAudioDevice(null, 0, &desiredSpec, &obtainedSpec, (int)allowedChanges);
            if (_deviceID == 0)
            {
                error = Error.GetLastError();
                return false;
            }
            else
            {
                Frequency = obtainedSpec.freq;
                Format = (AudioFormat)obtainedSpec.format;
                Channels = (AudioChannelLayout)obtainedSpec.channels;
                Silence = obtainedSpec.silence;
                Samples = obtainedSpec.samples;
                Size = obtainedSpec.size;
                error = null!;
                return true;
            }
        }

        public void Close()
        {
            ThrowIfDisposed();

            if (_deviceID != 0)
            {
                Interop.SDL.CloseAudioDevice(_deviceID);
                _deviceID = 0;
                _callback = null!;
                if (_callbackUserData.IsAllocated)
                {
                    _callbackUserData.Free();
                }
            }
        }

        public void Pause()
        {
            ThrowIfDisposed();
            ThrowIfClosed();
            Interop.SDL.PauseAudioDevice(_deviceID, 1);
        }

        public void Unpause()
        {
            ThrowIfDisposed();
            ThrowIfClosed();
            Interop.SDL.PauseAudioDevice(_deviceID, 0);
        }

        public void Lock()
        {
            ThrowIfDisposed();
            ThrowIfClosed();
            Interop.SDL.LockAudioDevice(_deviceID);
        }

        public void Unlock()
        {
            ThrowIfDisposed();
            ThrowIfClosed();
            Interop.SDL.UnlockAudioDevice(_deviceID);
        }

        public void Queue(Span<byte> buffer)
        {
            ThrowIfDisposed();
            ThrowIfClosed();
            fixed (void* data = &buffer[0])
            {
                Error.ThrowLastErrorIfNegative(
                    Interop.SDL.QueueAudio(_deviceID, data, (uint)buffer.Length)
                );
            }
        }

        private void ThrowIfOpen()
        {
            if (IsOpen)
            {
                throw new InvalidOperationException("The Audio Device is open");
            }
        }

        private void ThrowIfClosed()
        {
            if (!IsOpen)
            {
                throw new InvalidOperationException("The Audio Device is closed");
            }
        }

        private void ThrowIfDisposed()
        {
            ObjectDisposedException.ThrowIf(IsDisposed, this);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        private static void OnAudioDeviceCallback(void* userdata, byte* stream, int len)
        {
            var audioDeviceHandle = GCHandle.FromIntPtr((IntPtr)userdata);
            if (audioDeviceHandle.Target is AudioDevice audioDevice)
            {
                audioDevice._callback(new Span<byte>(stream, len));
            }
        }
    }
}
