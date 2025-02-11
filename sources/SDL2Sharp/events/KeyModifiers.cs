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
using SDL2Sharp.Interop;

namespace SDL2Sharp
{
    [Flags]
    public enum KeyModifiers
    {
        None = SDL_Keymod.KMOD_NONE,
        LeftShift = SDL_Keymod.KMOD_LSHIFT,
        RightShift = SDL_Keymod.KMOD_RSHIFT,
        LeftCtrl = SDL_Keymod.KMOD_LCTRL,
        RightCtrl = SDL_Keymod.KMOD_RCTRL,
        LeftAlt = SDL_Keymod.KMOD_LALT,
        RightAlt = SDL_Keymod.KMOD_RALT,
        LeftGui = SDL_Keymod.KMOD_LGUI,
        RightGui = SDL_Keymod.KMOD_RGUI,
        NumLock = SDL_Keymod.KMOD_NUM,
        CapsLock = SDL_Keymod.KMOD_CAPS,
        Mode = SDL_Keymod.KMOD_MODE,
        Reserved = SDL_Keymod.KMOD_RESERVED,
        Ctrl = SDL_Keymod.KMOD_CTRL,
        Shift = SDL_Keymod.KMOD_SHIFT,
        Alt = SDL_Keymod.KMOD_ALT,
        Gui = SDL_Keymod.KMOD_GUI,
    }
}
