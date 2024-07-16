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

using System.Collections.Generic;
using SDL2Sharp.Interop;

namespace SDL2Sharp.Video
{
    public unsafe sealed class Display
    {
        private readonly int _displayIndex;

        public string Name
        {
            get
            {
                return new string(
                    Error.ReturnOrThrowOnFailure(
                        SDL.GetDisplayName(_displayIndex)
                    )
                );
            }
        }

        public Rectangle Bounds
        {
            get
            {
                var rect = new SDL_Rect();
                Error.ThrowOnFailure(
                    SDL.GetDisplayBounds(_displayIndex, &rect)
                );
                return new Rectangle(rect.x, rect.y, rect.w, rect.h);
            }
        }

        public DisplayMode CurrentMode
        {
            get
            {
                var displayMode = new SDL_DisplayMode();
                Error.ThrowOnFailure(
                    SDL.GetCurrentDisplayMode(_displayIndex, &displayMode)
                );
                return new DisplayMode(
                    (PixelFormat)displayMode.format,
                    displayMode.w,
                    displayMode.h,
                    displayMode.refresh_rate);
            }
        }

        public DisplayMode DesktopMode
        {
            get
            {
                var displayMode = new SDL_DisplayMode();
                Error.ThrowOnFailure(
                    SDL.GetDesktopDisplayMode(_displayIndex, &displayMode)
                );
                return new DisplayMode(
                    (PixelFormat)displayMode.format,
                    displayMode.w,
                    displayMode.h,
                    displayMode.refresh_rate);
            }
        }

        public IReadOnlyList<DisplayMode> Modes
        {
            get
            {
                var modeCount = SDL.GetNumDisplayModes(_displayIndex);
                var modes = new List<DisplayMode>(modeCount);
                for (var modeIndex = 0; modeIndex < modeCount; modeIndex++)
                {
                    var displayMode = new SDL_DisplayMode();
                    Error.ThrowOnFailure(
                        SDL.GetDisplayMode(_displayIndex, modeIndex, &displayMode)
                    );
                    modes.Add(new DisplayMode(
                        (PixelFormat)displayMode.format,
                        displayMode.w,
                        displayMode.h,
                        displayMode.refresh_rate));
                }
                return modes.AsReadOnly();
            }
        }

        public DisplayOrientation Orientation
        {
            get
            {
                return (DisplayOrientation)SDL.GetDisplayOrientation(_displayIndex);
            }
        }

        public Display(int displayIndex)
        {
            _displayIndex = displayIndex;
        }

    }
}
