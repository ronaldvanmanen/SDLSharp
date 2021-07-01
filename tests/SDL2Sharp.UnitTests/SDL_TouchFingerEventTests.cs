using System.Runtime.InteropServices;
using Xunit;

namespace SDL2Sharp.UnitTests
{
    /// <summary>Provides validation of the <see cref="SDL_TouchFingerEvent" /> struct.</summary>
    public static unsafe class SDL_TouchFingerEventTests
    {
        /// <summary>Validates that the <see cref="SDL_TouchFingerEvent" /> struct is blittable.</summary>
        [Fact]
        public static void IsBlittableTest()
        {
            Assert.Equal(sizeof(SDL_TouchFingerEvent), Marshal.SizeOf<SDL_TouchFingerEvent>());
        }

        /// <summary>Validates that the <see cref="SDL_TouchFingerEvent" /> struct has the right <see cref="LayoutKind" />.</summary>
        [Fact]
        public static void IsLayoutSequentialTest()
        {
            Assert.True(typeof(SDL_TouchFingerEvent).IsLayoutSequential);
        }

        /// <summary>Validates that the <see cref="SDL_TouchFingerEvent" /> struct has the correct size.</summary>
        [Fact]
        public static void SizeOfTest()
        {
            Assert.Equal(48, sizeof(SDL_TouchFingerEvent));
        }
    }
}
