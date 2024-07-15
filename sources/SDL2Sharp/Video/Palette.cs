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

namespace SDL2Sharp.Video
{
    public sealed class Palette<TColor> where TColor : struct
    {
        private readonly TColor[] _colors;

        private int _offset;

        public int Count => _colors.Length;

        public TColor this[int index]
        {
            get
            {
                if (index < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, "The specified index cannot be less than zero.");
                }

                if (index >= _colors.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, "The specified index cannot be greater than or equal to the number of colors in the palette.");
                }

                var shiftedIndex = _offset + index;
                if (shiftedIndex >= _colors.Length)
                {
                    shiftedIndex -= _colors.Length;
                }

                return _colors[shiftedIndex];
            }
            set
            {
                if (index < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, "The specified index cannot be less than zero.");
                }

                if (index >= _colors.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, "The specified index cannot be greater than or equal to the number of colors in the palette.");
                }

                var shiftedIndex = _offset + index;
                if (shiftedIndex >= _colors.Length)
                {
                    shiftedIndex -= _colors.Length;
                }

                _colors[shiftedIndex] = value;
            }
        }

        public Palette(int size)
        {
            if (size < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), size, "The size of a palette cannot be less than zero");
            }

            _colors = new TColor[size];
            _offset = 0;
        }

        public void RotateLeft()
        {
            --_offset;

            if (_offset < 0)
            {
                _offset += _colors.Length;
            }
        }

        public void RotateRight()
        {
            ++_offset;

            if (_offset >= _colors.Length)
            {
                _offset -= _colors.Length;
            }
        }
    }
}
