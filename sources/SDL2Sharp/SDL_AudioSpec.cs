using System;

namespace SDL2Sharp
{
    public unsafe partial struct SDL_AudioSpec
    {
        public int freq;

        [NativeTypeName("SDL_AudioFormat")]
        public ushort format;

        [NativeTypeName("Uint8")]
        public byte channels;

        [NativeTypeName("Uint8")]
        public byte silence;

        [NativeTypeName("Uint16")]
        public ushort samples;

        [NativeTypeName("Uint16")]
        public ushort padding;

        [NativeTypeName("Uint32")]
        public uint size;

        [NativeTypeName("SDL_AudioCallback")]
        public IntPtr callback;

        public void* userdata;
    }
}
