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

using System.Runtime.InteropServices;

namespace SDL2Sharp.Interop
{
    public static unsafe partial class IMG
    {
        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_Linked_Version", ExactSpelling = true)]
        [return: NativeTypeName("const SDL_version *")]
        public static extern SDL_version* Linked_Version();

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_Init", ExactSpelling = true)]
        public static extern int Init(int flags);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_Quit", ExactSpelling = true)]
        public static extern void Quit();

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_LoadTyped_RW", ExactSpelling = true)]
        public static extern SDL_Surface* LoadTyped_RW(SDL_RWops* src, int freesrc, [NativeTypeName("const char *")] sbyte* type);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_Load", ExactSpelling = true)]
        public static extern SDL_Surface* Load([NativeTypeName("const char *")] sbyte* file);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_Load_RW", ExactSpelling = true)]
        public static extern SDL_Surface* Load_RW(SDL_RWops* src, int freesrc);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_LoadTexture", ExactSpelling = true)]
        public static extern SDL_Texture* LoadTexture(SDL_Renderer* renderer, [NativeTypeName("const char *")] sbyte* file);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_LoadTexture_RW", ExactSpelling = true)]
        public static extern SDL_Texture* LoadTexture_RW(SDL_Renderer* renderer, SDL_RWops* src, int freesrc);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_LoadTextureTyped_RW", ExactSpelling = true)]
        public static extern SDL_Texture* LoadTextureTyped_RW(SDL_Renderer* renderer, SDL_RWops* src, int freesrc, [NativeTypeName("const char *")] sbyte* type);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_isAVIF", ExactSpelling = true)]
        public static extern int isAVIF(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_isICO", ExactSpelling = true)]
        public static extern int isICO(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_isCUR", ExactSpelling = true)]
        public static extern int isCUR(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_isBMP", ExactSpelling = true)]
        public static extern int isBMP(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_isGIF", ExactSpelling = true)]
        public static extern int isGIF(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_isJPG", ExactSpelling = true)]
        public static extern int isJPG(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_isJXL", ExactSpelling = true)]
        public static extern int isJXL(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_isLBM", ExactSpelling = true)]
        public static extern int isLBM(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_isPCX", ExactSpelling = true)]
        public static extern int isPCX(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_isPNG", ExactSpelling = true)]
        public static extern int isPNG(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_isPNM", ExactSpelling = true)]
        public static extern int isPNM(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_isSVG", ExactSpelling = true)]
        public static extern int isSVG(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_isQOI", ExactSpelling = true)]
        public static extern int isQOI(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_isTIF", ExactSpelling = true)]
        public static extern int isTIF(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_isXCF", ExactSpelling = true)]
        public static extern int isXCF(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_isXPM", ExactSpelling = true)]
        public static extern int isXPM(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_isXV", ExactSpelling = true)]
        public static extern int isXV(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_isWEBP", ExactSpelling = true)]
        public static extern int isWEBP(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_LoadAVIF_RW", ExactSpelling = true)]
        public static extern SDL_Surface* LoadAVIF_RW(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_LoadICO_RW", ExactSpelling = true)]
        public static extern SDL_Surface* LoadICO_RW(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_LoadCUR_RW", ExactSpelling = true)]
        public static extern SDL_Surface* LoadCUR_RW(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_LoadBMP_RW", ExactSpelling = true)]
        public static extern SDL_Surface* LoadBMP_RW(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_LoadGIF_RW", ExactSpelling = true)]
        public static extern SDL_Surface* LoadGIF_RW(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_LoadJPG_RW", ExactSpelling = true)]
        public static extern SDL_Surface* LoadJPG_RW(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_LoadJXL_RW", ExactSpelling = true)]
        public static extern SDL_Surface* LoadJXL_RW(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_LoadLBM_RW", ExactSpelling = true)]
        public static extern SDL_Surface* LoadLBM_RW(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_LoadPCX_RW", ExactSpelling = true)]
        public static extern SDL_Surface* LoadPCX_RW(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_LoadPNG_RW", ExactSpelling = true)]
        public static extern SDL_Surface* LoadPNG_RW(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_LoadPNM_RW", ExactSpelling = true)]
        public static extern SDL_Surface* LoadPNM_RW(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_LoadSVG_RW", ExactSpelling = true)]
        public static extern SDL_Surface* LoadSVG_RW(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_LoadQOI_RW", ExactSpelling = true)]
        public static extern SDL_Surface* LoadQOI_RW(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_LoadTGA_RW", ExactSpelling = true)]
        public static extern SDL_Surface* LoadTGA_RW(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_LoadTIF_RW", ExactSpelling = true)]
        public static extern SDL_Surface* LoadTIF_RW(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_LoadXCF_RW", ExactSpelling = true)]
        public static extern SDL_Surface* LoadXCF_RW(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_LoadXPM_RW", ExactSpelling = true)]
        public static extern SDL_Surface* LoadXPM_RW(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_LoadXV_RW", ExactSpelling = true)]
        public static extern SDL_Surface* LoadXV_RW(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_LoadWEBP_RW", ExactSpelling = true)]
        public static extern SDL_Surface* LoadWEBP_RW(SDL_RWops* src);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_LoadSizedSVG_RW", ExactSpelling = true)]
        public static extern SDL_Surface* LoadSizedSVG_RW(SDL_RWops* src, int width, int height);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_ReadXPMFromArray", ExactSpelling = true)]
        public static extern SDL_Surface* ReadXPMFromArray([NativeTypeName("char **")] sbyte** xpm);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_ReadXPMFromArrayToRGB888", ExactSpelling = true)]
        public static extern SDL_Surface* ReadXPMFromArrayToRGB888([NativeTypeName("char **")] sbyte** xpm);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_SavePNG", ExactSpelling = true)]
        public static extern int SavePNG(SDL_Surface* surface, [NativeTypeName("const char *")] sbyte* file);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_SavePNG_RW", ExactSpelling = true)]
        public static extern int SavePNG_RW(SDL_Surface* surface, SDL_RWops* dst, int freedst);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_SaveJPG", ExactSpelling = true)]
        public static extern int SaveJPG(SDL_Surface* surface, [NativeTypeName("const char *")] sbyte* file, int quality);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_SaveJPG_RW", ExactSpelling = true)]
        public static extern int SaveJPG_RW(SDL_Surface* surface, SDL_RWops* dst, int freedst, int quality);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_LoadAnimation", ExactSpelling = true)]
        public static extern IMG_Animation* LoadAnimation([NativeTypeName("const char *")] sbyte* file);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_LoadAnimation_RW", ExactSpelling = true)]
        public static extern IMG_Animation* LoadAnimation_RW(SDL_RWops* src, int freesrc);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_LoadAnimationTyped_RW", ExactSpelling = true)]
        public static extern IMG_Animation* LoadAnimationTyped_RW(SDL_RWops* src, int freesrc, [NativeTypeName("const char *")] sbyte* type);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_FreeAnimation", ExactSpelling = true)]
        public static extern void FreeAnimation(IMG_Animation* anim);

        [DllImport("SDL2_image", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IMG_LoadGIFAnimation_RW", ExactSpelling = true)]
        public static extern IMG_Animation* LoadGIFAnimation_RW(SDL_RWops* src);

        [NativeTypeName("#define SDL_IMAGE_MAJOR_VERSION 2")]
        public const int SDL_IMAGE_MAJOR_VERSION = 2;

        [NativeTypeName("#define SDL_IMAGE_MINOR_VERSION 6")]
        public const int SDL_IMAGE_MINOR_VERSION = 6;

        [NativeTypeName("#define SDL_IMAGE_PATCHLEVEL 3")]
        public const int SDL_IMAGE_PATCHLEVEL = 3;

        [NativeTypeName("#define SDL_IMAGE_COMPILEDVERSION SDL_VERSIONNUM(SDL_IMAGE_MAJOR_VERSION, SDL_IMAGE_MINOR_VERSION, SDL_IMAGE_PATCHLEVEL)")]
        public const int SDL_IMAGE_COMPILEDVERSION = ((2) * 1000 + (6) * 100 + (3));
    }
}
