using System.Runtime.InteropServices;
using Xunit;

namespace SDL2Sharp.Interop.UnitTests
{
    /// <summary>Provides validation of the <see cref="SDL_JoyButtonEvent" /> struct.</summary>
    public static unsafe class SDL_JoyButtonEventTests
    {
        /// <summary>Validates that the <see cref="SDL_JoyButtonEvent" /> struct is blittable.</summary>
        [Fact]
        public static void IsBlittableTest()
        {
            Assert.Equal(sizeof(SDL_JoyButtonEvent), Marshal.SizeOf<SDL_JoyButtonEvent>());
        }

        /// <summary>Validates that the <see cref="SDL_JoyButtonEvent" /> struct has the right <see cref="LayoutKind" />.</summary>
        [Fact]
        public static void IsLayoutSequentialTest()
        {
            Assert.True(typeof(SDL_JoyButtonEvent).IsLayoutSequential);
        }

        /// <summary>Validates that the <see cref="SDL_JoyButtonEvent" /> struct has the correct size.</summary>
        [Fact]
        public static void SizeOfTest()
        {
            Assert.Equal(16, sizeof(SDL_JoyButtonEvent));
        }
    }
}