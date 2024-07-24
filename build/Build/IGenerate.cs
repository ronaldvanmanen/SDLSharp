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

using System.Linq;
using System.Runtime.InteropServices;
using Nuke.Common;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.ClangSharpPInvokeGenerator;
using static System.Runtime.InteropServices.RuntimeInformation;
using static Nuke.Common.Tools.ClangSharpPInvokeGenerator.ClangSharpPInvokeGeneratorTasks;
using static Nuke.Common.Tools.ClangSharpPInvokeGenerator.ClangSharpPInvokeGeneratorConfigOption;

interface IGenerate : IBuild
{
    public Target Generate => _ => _
        .DependsOn<IRestore>(target => target.Restore)
        .Before<ICompile>(target => target.Compile)
        .OnlyWhenStatic(() => IsOSPlatform(OSPlatform.Windows))
        .Produces(ArtifactsDirectory / "log" / "*.*")
        .Executes(() =>
        {
            GenerateBindingsForSDL2();
            GenerateBindingsForSDL2Image();
            GenerateBindingsForSDL2TTF();
        });

    private void GenerateBindingsForSDL2()
    {
        var headerFile = RootDirectory / "build" / "Build" / "Header.txt";
        var outputDirectory = RootDirectory / "sources" / "SDL2Sharp.Interop";
        var testOutputDirectory = RootDirectory / "tests" / "SDL2Sharp.Interop.Tests";

        const string packageId = "SDL2";
        var packageFolder = packageId.ToLower();
        var packageReferenceVersion = Solution.AllProjects
            .Where(e => e.Name.Equals("SDL2Sharp.Interop"))
            .Select(e => e.GetPackageReferenceVersion(packageId))
            .Single(e => e is not null);
        var fileDirectory = GlobalPackagesFolder / packageFolder / packageReferenceVersion / "lib" / "native" / "include";

        ClangSharpPInvokeGenerator(settings => settings
            .SetConfig
            (
                latest_codegen,
                generate_aggressive_inlining,
                generate_macro_bindings,
                generate_tests_xunit,
                multi_file
            )
            .SetHeaderFile(headerFile)
            .SetNamespace("SDL2Sharp.Interop")
            .SetOutput(outputDirectory)
            .SetTestOutput(testOutputDirectory)
            .SetWithType
            (
                "SDL_EventType=uint",
                "SDL_PixelFormatEnum=uint",
                "SDL_RendererFlags=uint"
            )
            .SetExclude
            (
                "__builtin_add_overflow",
                "__builtin_mul_overflow",
                "_SDL_HAS_BUILTIN",
                "_SDL_size_add_overflow_builtin",
                "_SDL_size_mul_overflow_builtin",
                "SDL_abs",
                "SDL_acos",
                "SDL_acosf",
                "SDL_arraysize",
                "SDL_asin",
                "SDL_asinf",
                "SDL_asprintf",
                "SDL_atan",
                "SDL_atan2",
                "SDL_atan2f",
                "SDL_atanf",
                "SDL_atof",
                "SDL_atoi",
                "SDL_AUDIO_BITSIZE",
                "SDL_AUDIO_ISBIGENDIAN",
                "SDL_AUDIO_ISFLOAT",
                "SDL_AUDIO_ISINT",
                "SDL_AUDIO_ISLITTLEENDIAN",
                "SDL_AUDIO_ISSIGNED",
                "SDL_AUDIO_ISUNSIGNED",
                "SDL_BITSPERPIXEL",
                "SDL_BlitScaled",
                "SDL_BlitSurface",
                "SDL_bsearch",
                "SDL_BUTTON",
                "SDL_BYTESPERPIXEL",
                "SDL_ceil",
                "SDL_ceilf",
                "SDL_clamp",
                "SDL_COMPILE_TIME_ASSERT",
                "SDL_const_cast",
                "SDL_copyp",
                "SDL_copysign",
                "SDL_copysignf",
                "SDL_cos",
                "SDL_cosf",
                "SDL_crc16",
                "SDL_crc32",
                "SDL_DEFINE_PIXELFORMAT",
                "SDL_DEFINE_PIXELFOURCC",
                "SDL_EncloseFPoints",
                "SDL_EnclosePoints",
                "SDL_exp",
                "SDL_expf",
                "SDL_fabs",
                "SDL_fabsf",
                "SDL_floor",
                "SDL_floorf",
                "SDL_fmod",
                "SDL_fmodf",
                "SDL_FOURCC",
                "SDL_FRectEmpty",
                "SDL_FRectEquals",
                "SDL_FRectEqualsEpsilon",
                "SDL_getenv",
                "SDL_GetEventState",
                "SDL_HasIntersection",
                "SDL_HasIntersectionF",
                "SDL_iconv_utf8_locale",
                "SDL_iconv_utf8_ucs2",
                "SDL_iconv_utf8_ucs4",
                "SDL_iconv_wchar_utf8",
                "SDL_IN_BYTECAP",
                "SDL_INOUT_Z_CAP",
                "SDL_IntersectFRect",
                "SDL_IntersectFRectAndLine",
                "SDL_IntersectRect",
                "SDL_IntersectRectAndLine",
                "SDL_InvalidParamError",
                "SDL_isalnum",
                "SDL_isalpha",
                "SDL_isblank",
                "SDL_iscntrl",
                "SDL_isdigit",
                "SDL_isgraph",
                "SDL_islower",
                "SDL_ISPIXELFORMAT_ALPHA",
                "SDL_ISPIXELFORMAT_ARRAY",
                "SDL_ISPIXELFORMAT_FOURCC",
                "SDL_ISPIXELFORMAT_INDEXED",
                "SDL_ISPIXELFORMAT_PACKED",
                "SDL_isprint",
                "SDL_ispunct",
                "SDL_isspace",
                "SDL_isupper",
                "SDL_isxdigit",
                "SDL_itoa",
                "SDL_lltoa",
                "SDL_LoadBMP",
                "SDL_LoadWAV",
                "SDL_log",
                "SDL_Log",
                "SDL_log10",
                "SDL_log10f",
                "SDL_LogCritical",
                "SDL_LogDebug",
                "SDL_LogError",
                "SDL_logf",
                "SDL_LogGetOutputFunction",
                "SDL_LogGetPriority",
                "SDL_LogInfo",
                "SDL_LogMessage",
                "SDL_LogMessageV",
                "SDL_LogResetPriorities",
                "SDL_LogSetAllPriority",
                "SDL_LogSetOutputFunction",
                "SDL_LogSetPriority",
                "SDL_LogVerbose",
                "SDL_LogWarn",
                "SDL_lround",
                "SDL_lroundf",
                "SDL_ltoa",
                "SDL_MAX_LOG_MESSAGE",
                "SDL_max",
                "SDL_memcmp",
                "SDL_memcpy",
                "SDL_memcpy4",
                "SDL_memmove",
                "SDL_memset",
                "SDL_memset4",
                "SDL_min",
                "SDL_MUSTLOCK",
                "SDL_OUT_BYTECAP",
                "SDL_OUT_CAP",
                "SDL_OUT_Z_BYTECAP",
                "SDL_OUT_Z_CAP",
                "SDL_OutOfMemory",
                "SDL_PIXELFLAG",
                "SDL_PIXELLAYOUT",
                "SDL_PIXELORDER",
                "SDL_PIXELTYPE",
                "SDL_PointInFRect",
                "SDL_PointInRect",
                "SDL_pow",
                "SDL_powf",
                "SDL_PRINTF_VARARG_FUNC",
                "SDL_qsort",
                "SDL_RectEmpty",
                "SDL_RectEquals",
                "SDL_reinterpret_cast",
                "SDL_round",
                "SDL_roundf",
                "SDL_SaveBMP",
                "SDL_scalbn",
                "SDL_scalbnf",
                "SDL_SCANCODE_TO_KEYCODE",
                "SDL_SCANF_VARARG_FUNC",
                "SDL_setenv",
                "SDL_sin",
                "SDL_sinf",
                "SDL_size_add_overflow",
                "SDL_size_mul_overflow",
                "SDL_snprintf",
                "SDL_sqrt",
                "SDL_sqrtf",
                "SDL_sscanf",
                "SDL_stack_alloc",
                "SDL_stack_free",
                "SDL_static_cast",
                "SDL_strcasecmp",
                "SDL_strchr",
                "SDL_strcmp",
                "SDL_strdup",
                "SDL_STRINGIFY_ARG",
                "SDL_strlcat",
                "SDL_strlcpy",
                "SDL_strlen",
                "SDL_strlwr",
                "SDL_strncasecmp",
                "SDL_strncmp",
                "SDL_strrchr",
                "SDL_strrev",
                "SDL_strstr",
                "SDL_strtod",
                "SDL_strtokr",
                "SDL_strtol",
                "SDL_strtoll",
                "SDL_strtoul",
                "SDL_strtoull",
                "SDL_strupr",
                "SDL_TABLESIZE",
                "SDL_tan",
                "SDL_tanf",
                "SDL_tolower",
                "SDL_toupper",
                "SDL_trunc",
                "SDL_truncf",
                "SDL_uitoa",
                "SDL_ulltoa",
                "SDL_ultoa",
                "SDL_UnionFRect",
                "SDL_UnionRect",
                "SDL_Unsupported",
                "SDL_utf8strlcpy",
                "SDL_utf8strlen",
                "SDL_utf8strnlen",
                "SDL_vasprintf",
                "SDL_VERSION_ATLEAST",
                "SDL_VERSION",
                "SDL_VERSIONNUM",
                "SDL_vsnprintf",
                "SDL_vsscanf",
                "SDL_wcscasecmp",
                "SDL_wcscmp",
                "SDL_wcsdup",
                "SDL_wcslcat",
                "SDL_wcslcpy",
                "SDL_wcslen",
                "SDL_wcsncasecmp",
                "SDL_wcsncmp",
                "SDL_wcsstr",
                "SDL_WINDOWPOS_CENTERED_DISPLAY",
                "SDL_WINDOWPOS_ISCENTERED",
                "SDL_WINDOWPOS_ISUNDEFINED",
                "SDL_WINDOWPOS_UNDEFINED_DISPLAY",
                "SDL_zero",
                "SDL_zeroa",
                "SDL_zerop"
            )
            .SetFile
            (
                $"SDL.h",
                $"SDL_audio.h",
                $"SDL_blendmode.h",
                $"SDL_error.h",
                $"SDL_events.h",
                $"SDL_guid.h",
                $"SDL_hints.h",
                $"SDL_joystick.h",
                $"SDL_keyboard.h",
                $"SDL_keycode.h",
                $"SDL_log.h",
                $"SDL_mouse.h",
                $"SDL_pixels.h",
                $"SDL_rect.h",
                $"SDL_render.h",
                $"SDL_rwops.h",
                $"SDL_scancode.h",
                $"SDL_stdinc.h",
                $"SDL_surface.h",
                $"SDL_system.h",
                $"SDL_version.h",
                $"SDL_video.h"
            )
            .SetLibraryPath("SDL2")
            .SetMethodClassName("SDL")
            .SetPrefixStrip("SDL_")
            .SetFileDirectory(fileDirectory)
        );
    }

    private void GenerateBindingsForSDL2Image()
    {
        var headerFile = RootDirectory / "build" / "Build" / "Header.txt";
        var outputDirectory = RootDirectory / "sources" / "SDL2Sharp.Interop";
        var testOutputDirectory = RootDirectory / "tests" / "SDL2Sharp.Interop.Tests";

        const string basePackageId = "SDL2";
        var basePackageFolder = basePackageId.ToLower();
        var basePackageReferenceVersion = Solution.AllProjects
            .Where(e => e.Name.Equals("SDL2Sharp.Interop"))
            .Select(e => e.GetPackageReferenceVersion(basePackageId))
            .Single(e => e is not null);
        var includeDirectory = GlobalPackagesFolder / basePackageFolder / basePackageReferenceVersion / "lib" / "native" / "include";

        const string packageId = "SDL2_image";
        var packageFolder = packageId.ToLower();
        var packageReferenceVersion = Solution.AllProjects
            .Select(e => e.GetPackageReferenceVersion(packageId))
            .Single(e => e is not null);
        var fileDirectory = GlobalPackagesFolder / packageFolder / packageReferenceVersion / "lib" / "native" / "include";

        ClangSharpPInvokeGenerator(settings => settings
            .SetConfig
            (
                compatible_codegen,
                generate_aggressive_inlining,
                generate_macro_bindings,
                generate_tests_xunit, multi_file
            )
            .SetHeaderFile(headerFile)
            .SetNamespace("SDL2Sharp.Interop")
            .SetOutput(outputDirectory)
            .SetTestOutput(testOutputDirectory)
            .SetWithType
            (
                "SDL_EventType=uint",
                "SDL_PixelFormatEnum=uint",
                "SDL_RendererFlags=uint"
            )
            .SetExclude
            (
                "IMG_GetError",
                "IMG_SetError",
                "SDL_IMAGE_VERSION",
                "SDL_IMAGE_VERSION_ATLEAST"
            )
            .SetFile("SDL_image.h")
            .SetLibraryPath("SDL2_image")
            .SetMethodClassName("IMG")
            .SetPrefixStrip("IMG_")
            .SetFileDirectory(fileDirectory)
            .SetIncludeDirectory(includeDirectory)
        );
    }

    private void GenerateBindingsForSDL2TTF()
    {
        var headerFile = RootDirectory / "build" / "Build" / "Header.txt";
        var outputDirectory = RootDirectory / "sources" / "SDL2Sharp.Interop";
        var testOutputDirectory = RootDirectory / "tests" / "SDL2Sharp.Interop.Tests";

        const string basePackageId = "SDL2";
        var basePackageFolder = basePackageId.ToLower();
        var basePackageReferenceVersion = Solution.AllProjects
            .Where(e => e.Name.Equals("SDL2Sharp.Interop"))
            .Select(e => e.GetPackageReferenceVersion(basePackageId))
            .Single(e => e is not null);
        var includeDirectory = GlobalPackagesFolder / basePackageFolder / basePackageReferenceVersion / "lib" / "native" / "include";

        const string packageId = "SDL2_ttf";
        var packageFolder = packageId.ToLower();
        var packageReferenceVersion = Solution.AllProjects
            .Select(e => e.GetPackageReferenceVersion(packageId))
            .Single(e => e is not null);
        var fileDirectory = GlobalPackagesFolder / packageFolder / packageReferenceVersion / "lib" / "native" / "include";

        ClangSharpPInvokeGenerator(settings => settings
            .SetConfig
            (
                compatible_codegen,
                generate_aggressive_inlining,
                generate_macro_bindings,
                generate_tests_xunit, multi_file
            )
            .SetHeaderFile(headerFile)
            .SetNamespace("SDL2Sharp.Interop")
            .SetOutput(outputDirectory)
            .SetTestOutput(testOutputDirectory)
            .SetWithType
            (
                "SDL_EventType=uint",
                "SDL_PixelFormatEnum=uint",
                "SDL_RendererFlags=uint"
            )
            .SetExclude
            (
                "SDL_TTF_VERSION",
                "SDL_TTF_VERSION_ATLEAST",
                "TTF_GetError",
                "TTF_RenderText",
                "TTF_RenderUTF8",
                "TTF_RenderUNICODE",
                "TTF_SetError",
                "TTF_VERSION"
            )
            .SetFile("SDL_ttf.h")
            .SetLibraryPath("SDL2_ttf")
            .SetMethodClassName("TTF")
            .SetPrefixStrip("TTF_")
            .SetFileDirectory(fileDirectory)
            .SetIncludeDirectory(includeDirectory)
        );
    }
}
