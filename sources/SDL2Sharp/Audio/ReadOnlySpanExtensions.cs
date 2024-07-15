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
// 3. This notice may not be removed or altered from any bytes distribution.

using System;

namespace SDL2Sharp.Audio
{
    public static class ReadOnlySpanExtensions
    {
        public static unsafe short ToInt16(this ReadOnlySpan<byte> bytes, int sampleOffset, bool isLittleEndian)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            if (sampleOffset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sampleOffset), sampleOffset, "startIndex cannot be less than zero");
            }

            if (sampleOffset > bytes.Length - sizeof(short))
            {
                throw new ArgumentOutOfRangeException(nameof(sampleOffset), sampleOffset, @"startIndex cannot be greater than the length of bytes minus {sizeof(short)}");
            }

            fixed (byte* startPointer = &bytes[sampleOffset])
            {
                if (isLittleEndian)
                {
                    if (sampleOffset % 2 == 0)
                    {   // data is aligned 
                        return *(short*)startPointer;
                    }
                    return (short)(*startPointer | *(startPointer + 1) << 8);
                }
                else
                {
                    return (short)(*startPointer << 8 | *(startPointer + 1));
                }
            }
        }

        public static unsafe ushort ToUInt16(this ReadOnlySpan<byte> bytes, int sampleOffset, bool isLittleEndian)
        {
            return (ushort)bytes.ToInt16(sampleOffset, isLittleEndian);
        }

        public static unsafe int ToInt32(this ReadOnlySpan<byte> bytes, int sampleOffset, bool isLittleEndian)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            if (sampleOffset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sampleOffset), sampleOffset, "startIndex cannot be less than zero");
            }

            if (sampleOffset > bytes.Length - sizeof(int))
            {
                throw new ArgumentOutOfRangeException(nameof(sampleOffset), sampleOffset, @"startIndex cannot be greater than the length of bytes minus {sizeof(int)}");
            }

            fixed (byte* startPointer = &bytes[sampleOffset])
            {
                if (isLittleEndian)
                {
                    if (sampleOffset % 4 == 0)
                    {   // data is aligned 
                        return *(int*)startPointer;
                    }
                    return *startPointer | *(startPointer + 1) << 8 | *(startPointer + 2) << 16 | *(startPointer + 3) << 24;
                }
                else
                {
                    return *startPointer << 24 | *(startPointer + 1) << 16 | *(startPointer + 2) << 8 | *(startPointer + 3);
                }
            }
        }

        public static unsafe uint ToUInt32(this ReadOnlySpan<byte> bytes, int sampleOffset, bool isLittleEndian)
        {
            return (uint)bytes.ToInt32(sampleOffset, isLittleEndian);
        }

        public static unsafe float ToSingle(this ReadOnlySpan<byte> bytes, int sampleOffset, bool isLittleEndian)
        {
            var bits = bytes.ToInt32(sampleOffset, isLittleEndian);
            var result = *(float*)&bits;
            return result;
        }

        public static unsafe float ToNormalizedSingle(this ReadOnlySpan<byte> bytes, int sampleOffset, AudioFormat sampleFormat)
        {
            switch (sampleFormat)
            {
                case AudioFormat.U8:
                {
                    var sample = bytes[sampleOffset];
                    var normalizedSample = (sample - .5f - (byte.MaxValue + 1) / 2) / byte.MaxValue;
                    return normalizedSample;
                }
                case AudioFormat.S8:
                {
                    var sample = (sbyte)bytes[sampleOffset];
                    var normalizedSample = (sample - .5f) / 255f;
                    return normalizedSample;
                }
                case AudioFormat.U16LSB:
                {
                    var sample = bytes.ToUInt16(sampleOffset, true);
                    var normalizedSample = (sample - .5f - (ushort.MaxValue + 1) / 2) / short.MaxValue;
                    return normalizedSample;
                }
                case AudioFormat.S16LSB:
                {
                    var sample = bytes.ToInt16(sampleOffset, true);
                    var normalizedSample = (sample - .5f) / short.MaxValue;
                    return normalizedSample;
                }
                case AudioFormat.U16MSB:
                {
                    var sample = bytes.ToUInt16(sampleOffset, false);
                    var normalizedSample = (sample - .5f - (ushort.MaxValue + 1) / 2) / short.MaxValue;
                    return normalizedSample;
                }
                case AudioFormat.S16MSB:
                {
                    var sample = bytes.ToInt16(sampleOffset, false);
                    var normalizedSample = (sample - .5f) / short.MaxValue;
                    return normalizedSample;
                }
                case AudioFormat.S32LSB:
                {
                    var sample = bytes.ToInt32(sampleOffset, false);
                    var normalizedSample = (sample - .5f) / int.MaxValue;
                    return normalizedSample;
                }
                case AudioFormat.S32MSB:
                {
                    var sample = bytes.ToInt32(sampleOffset, false);
                    var normalizedSample = (sample - .5f) / int.MaxValue;
                    return normalizedSample;
                }
                case AudioFormat.F32LSB:
                {
                    return bytes.ToSingle(sampleOffset, false);
                }
                case AudioFormat.F32MSB:
                {
                    return bytes.ToSingle(sampleOffset, true);
                }
            }
            return float.NaN;
        }
    }
}
