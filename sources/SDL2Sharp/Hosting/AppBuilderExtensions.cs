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

using Microsoft.Extensions.DependencyInjection.Extensions;
using SDL2Sharp.Audio;
using SDL2Sharp.Fonts;
using SDL2Sharp.Input;
using SDL2Sharp.Video;

namespace SDL2Sharp.Hosting
{
    public static class AppBuilderExtensions
    {
        public static AppBuilder UseMainSystem(this AppBuilder appBuilder)
        {
            appBuilder.Services.TryAddSingleton<IMainSystem, MainSystem>();
            return appBuilder;
        }

        public static AppBuilder UseAudioSubsystem(this AppBuilder appBuilder)
        {
            appBuilder.Services.TryAddSingleton<IMainSystem, MainSystem>();
            appBuilder.Services.TryAddSingleton<IAudioSubsystem, AudioSubsystem>();
            return appBuilder;
        }

        public static AppBuilder UseVideoSubsystem(this AppBuilder appBuilder)
        {
            appBuilder.Services.TryAddSingleton<IMainSystem, MainSystem>();
            appBuilder.Services.TryAddSingleton<IVideoSubsystem, VideoSubsystem>();
            appBuilder.Services.TryAddSingleton<IEventSubsystem, EventSubsystem>();
            return appBuilder;
        }

        public static AppBuilder UseEventSubsystem(this AppBuilder appBuilder)
        {
            appBuilder.Services.TryAddSingleton<IMainSystem, MainSystem>();
            appBuilder.Services.TryAddSingleton<IEventSubsystem, EventSubsystem>();
            return appBuilder;
        }

        public static AppBuilder UseFontSubsystem(this AppBuilder appBuilder)
        {
            appBuilder.Services.TryAddSingleton<IMainSystem, MainSystem>();
            appBuilder.Services.TryAddSingleton<IFontSubsystem, FontSubsystem>();
            return appBuilder;
        }
    }
}
