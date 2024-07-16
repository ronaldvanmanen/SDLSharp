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
using System;
using System.Collections.Generic;

namespace SDL2Sharp.Video
{
    public sealed unsafe class RendererInfo
    {
        private readonly SDL_RendererInfo _handle;

        private readonly Lazy<IReadOnlyList<PixelFormat>> _textureFormats;

        public RendererInfo(SDL_RendererInfo handle)
        {
            _handle = handle;
            _textureFormats = new Lazy<IReadOnlyList<PixelFormat>>(() =>
            {
                var numTextureFormats = (int)_handle.num_texture_formats;
                var textureFormats = new List<PixelFormat>(numTextureFormats);
                for (var index = 0; index < numTextureFormats; ++index)
                {
                    textureFormats.Add((PixelFormat)_handle.texture_formats[index]);
                }
                return textureFormats.AsReadOnly();
            });
        }

        public string Name => new(_handle.name);

        public RendererFlags Flags => (RendererFlags)_handle.flags;

        public IReadOnlyList<PixelFormat> TextureFormats => _textureFormats.Value;

        public int MaxTextureWidth => _handle.max_texture_width;

        public int MaxTextureHeight => _handle.max_texture_height;
    }
}
