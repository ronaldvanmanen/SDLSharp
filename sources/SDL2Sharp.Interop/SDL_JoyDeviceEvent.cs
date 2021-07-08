namespace SDL2Sharp.Interop
{
    public partial struct SDL_JoyDeviceEvent
    {
        [NativeTypeName("Uint32")]
        public uint type;

        [NativeTypeName("Uint32")]
        public uint timestamp;

        [NativeTypeName("Sint32")]
        public int which;
    }
}