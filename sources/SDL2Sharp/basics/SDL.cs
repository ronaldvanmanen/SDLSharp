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
    public sealed class SDL : IDisposable
    {
        private VideoSubsystem? _videoSubsystem;

        private AudioSubsystem? _audioSubsystem;

        private EventsSubsystem? _eventsSubsystem;

        private FontSubsystem? _fontSubsystem;

        private bool _disposed = false;

        public IVideoSubsystem Video => _videoSubsystem ??= new VideoSubsystem();

        public IAudioSubsystem Audio => _audioSubsystem ??= new AudioSubsystem();

        public IEventsSubsystem Events => _eventsSubsystem ??= new EventsSubsystem();

        public IFontSubsystem Fonts => _fontSubsystem ??= new FontSubsystem();

        public bool IsDisposed => _disposed;

        public SDL()
        {
            Error.ThrowLastErrorIfNegative(
                Interop.SDL.Init(0)
            );
        }

        ~SDL()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                SafeDispose(ref _videoSubsystem);
                SafeDispose(ref _audioSubsystem);
                SafeDispose(ref _eventsSubsystem);
                SafeDispose(ref _fontSubsystem);
            }

            Interop.SDL.Quit();

            _disposed = true;
        }

        private static void SafeDispose<TDisposable>(ref TDisposable? disposable) where TDisposable : IDisposable
        {
            var temp = disposable;
            disposable = default;
            temp?.Dispose();
        }
    }
}
