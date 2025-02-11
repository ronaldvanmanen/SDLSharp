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
    [Flags]
    public enum WindowFlags
    {
        None = 0,
        FullScreen = Interop.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN,
        OpenGL = Interop.SDL_WindowFlags.SDL_WINDOW_OPENGL,
        Shown = Interop.SDL_WindowFlags.SDL_WINDOW_SHOWN,
        Hidden = Interop.SDL_WindowFlags.SDL_WINDOW_HIDDEN,
        Borderless = Interop.SDL_WindowFlags.SDL_WINDOW_BORDERLESS,
        Resizable = Interop.SDL_WindowFlags.SDL_WINDOW_RESIZABLE,
        Minimized = Interop.SDL_WindowFlags.SDL_WINDOW_MINIMIZED,
        Maximized = Interop.SDL_WindowFlags.SDL_WINDOW_MAXIMIZED,
        InputGrabbed = Interop.SDL_WindowFlags.SDL_WINDOW_INPUT_GRABBED,
        InputFocus = Interop.SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS,
        MouseFocus = Interop.SDL_WindowFlags.SDL_WINDOW_MOUSE_FOCUS,
        FullScreenDesktop = Interop.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP,
        Foreign = Interop.SDL_WindowFlags.SDL_WINDOW_FOREIGN,
        AllowHighDpi = Interop.SDL_WindowFlags.SDL_WINDOW_ALLOW_HIGHDPI,
        MouseCapture = Interop.SDL_WindowFlags.SDL_WINDOW_MOUSE_CAPTURE,
        AlwaysOnTop = Interop.SDL_WindowFlags.SDL_WINDOW_ALWAYS_ON_TOP,
        SkipTaskbar = Interop.SDL_WindowFlags.SDL_WINDOW_SKIP_TASKBAR,
        Utility = Interop.SDL_WindowFlags.SDL_WINDOW_UTILITY,
        Tooltip = Interop.SDL_WindowFlags.SDL_WINDOW_TOOLTIP,
        PopupMenu = Interop.SDL_WindowFlags.SDL_WINDOW_POPUP_MENU,
        Vulkan = Interop.SDL_WindowFlags.SDL_WINDOW_VULKAN,
        Metal = Interop.SDL_WindowFlags.SDL_WINDOW_METAL,
    }
}
