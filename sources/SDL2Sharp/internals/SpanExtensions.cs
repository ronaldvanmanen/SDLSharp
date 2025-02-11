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
using System.Text;

namespace SDL2Sharp
{
    internal static unsafe class SpanExtensions
    {
        public static string AsString(this Span<byte> self) => AsString((ReadOnlySpan<byte>)self);

        public static string AsString(this ReadOnlySpan<byte> self)
        {
            if (self.IsEmpty)
            {
                return string.Empty;
            }

            fixed (byte* pSelf = self)
            {
                return Encoding.UTF8.GetString(pSelf, self.Length);
            }
        }

        public static string AsString(this ReadOnlySpan<ushort> self)
        {
            if (self.IsEmpty)
            {
                return string.Empty;
            }

            fixed (ushort* pSelf = self)
            {
                return Encoding.Unicode.GetString((byte*)pSelf, self.Length * 2);
            }
        }

        public static string AsString(this ReadOnlySpan<uint> self)
        {
            if (self.IsEmpty)
            {
                return string.Empty;
            }

            fixed (uint* pSelf = self)
            {
                return Encoding.UTF32.GetString((byte*)pSelf, self.Length * 4);
            }
        }
    }
}
