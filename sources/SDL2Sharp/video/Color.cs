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

using SDL2Sharp.Interop;

namespace SDL2Sharp
{
    public readonly struct Color
    {
        public static readonly Color Black = new(0, 0, 0, 255);

        public static readonly Color White = new(255, 255, 255, 255);

        public static readonly Color Red = new(255, 0, 0, 255);

        public static readonly Color Green = new(0, 255, 0, 255);

        public static readonly Color Blue = new(0, 0, 255, 255);

        public static readonly Color Yellow = new(255, 255, 0, 255);

        private readonly SDL_Color _handle;

        public Color(byte r, byte g, byte b, byte a)
        {
            _handle = new SDL_Color
            {
                r = r,
                g = g,
                b = b,
                a = a
            };
        }

        public byte R => _handle.r;

        public byte G => _handle.g;

        public byte B => _handle.b;

        public byte A => _handle.a;


        public static implicit operator SDL_Color(Color color)
        {
            return color._handle;
        }
    }
}
