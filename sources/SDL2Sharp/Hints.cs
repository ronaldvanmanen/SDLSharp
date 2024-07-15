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

using SDL2Sharp.Internals;
using SDL2Sharp.Interop;

namespace SDL2Sharp
{
    public static class Hints
    {
        public static readonly string FramebufferAcceleration = SDL.SDL_HINT_FRAMEBUFFER_ACCELERATION.AsString();

        public static readonly string RenderDriver = SDL.SDL_HINT_RENDER_DRIVER.AsString();

        public static readonly string OpenGlShaders = SDL.SDL_HINT_RENDER_OPENGL_SHADERS.AsString();

        public static readonly string Direct3DThreadSafe = SDL.SDL_HINT_RENDER_DIRECT3D_THREADSAFE.AsString();

        public static readonly string Direct3D11Debug = SDL.SDL_HINT_RENDER_DIRECT3D11_DEBUG.AsString();

        public static readonly string LogicalSizeMode = SDL.SDL_HINT_RENDER_LOGICAL_SIZE_MODE.AsString();

        public static readonly string RenderScaleQuality = SDL.SDL_HINT_RENDER_SCALE_QUALITY.AsString();

        public static readonly string RenderVSync = SDL.SDL_HINT_RENDER_VSYNC.AsString();

        public static readonly string VideoAllowScreensaver = SDL.SDL_HINT_VIDEO_ALLOW_SCREENSAVER.AsString();

        public static readonly string VideoExternalContext = SDL.SDL_HINT_VIDEO_EXTERNAL_CONTEXT.AsString();

        public static readonly string VideoX11XVidMode = SDL.SDL_HINT_VIDEO_X11_XVIDMODE.AsString();

        public static readonly string VideoX11Xinerama = SDL.SDL_HINT_VIDEO_X11_XINERAMA.AsString();

        public static readonly string VideoX11XRandr = SDL.SDL_HINT_VIDEO_X11_XRANDR.AsString();

        public static readonly string VideoX11WindowVisualId = SDL.SDL_HINT_VIDEO_X11_WINDOW_VISUALID.AsString();

        public static readonly string VideoX11NetWmPing = SDL.SDL_HINT_VIDEO_X11_NET_WM_PING.AsString();

        public static readonly string VideoX11NetWmBypassCompositor = SDL.SDL_HINT_VIDEO_X11_NET_WM_BYPASS_COMPOSITOR.AsString();

        public static readonly string VideoX11ForceEgl = SDL.SDL_HINT_VIDEO_X11_FORCE_EGL.AsString();

        public static readonly string WindowFrameUsableWhileCursorHidden = SDL.SDL_HINT_WINDOW_FRAME_USABLE_WHILE_CURSOR_HIDDEN.AsString();

        public static readonly string WindowsIntResourceIcon = SDL.SDL_HINT_WINDOWS_INTRESOURCE_ICON.AsString();

        public static readonly string WindowsIntResourceIconSmall = SDL.SDL_HINT_WINDOWS_INTRESOURCE_ICON_SMALL.AsString();

        public static readonly string WindowsEnableMessageLoop = SDL.SDL_HINT_WINDOWS_ENABLE_MESSAGELOOP.AsString();

        public static readonly string GrabKeyboard = SDL.SDL_HINT_GRAB_KEYBOARD.AsString();

        public static readonly string MouseDoubleClickTime = SDL.SDL_HINT_MOUSE_DOUBLE_CLICK_TIME.AsString();

        public static readonly string MouseDoubleClickRadius = SDL.SDL_HINT_MOUSE_DOUBLE_CLICK_RADIUS.AsString();

        public static readonly string MouseNormalSpeedScale = SDL.SDL_HINT_MOUSE_NORMAL_SPEED_SCALE.AsString();

        public static readonly string MouseRelativeSpeedScale = SDL.SDL_HINT_MOUSE_RELATIVE_SPEED_SCALE.AsString();

        public static readonly string MouseRelativeScaling = SDL.SDL_HINT_MOUSE_RELATIVE_SCALING.AsString();

        public static readonly string MouseRelativeModeWarp = SDL.SDL_HINT_MOUSE_RELATIVE_MODE_WARP.AsString();

        public static readonly string MouseFocusClickThrough = SDL.SDL_HINT_MOUSE_FOCUS_CLICKTHROUGH.AsString();

        public static readonly string TouchMouseEvents = SDL.SDL_HINT_TOUCH_MOUSE_EVENTS.AsString();

        public static readonly string MouseTouchEvents = SDL.SDL_HINT_MOUSE_TOUCH_EVENTS.AsString();

        public static readonly string VideoMinimizeOnFocusLoss = SDL.SDL_HINT_VIDEO_MINIMIZE_ON_FOCUS_LOSS.AsString();

        public static readonly string IdleTimerDisabled = SDL.SDL_HINT_IDLE_TIMER_DISABLED.AsString();

        public static readonly string Orientations = SDL.SDL_HINT_ORIENTATIONS.AsString();

        public static readonly string AppleTvControllerUiEvents = SDL.SDL_HINT_APPLE_TV_CONTROLLER_UI_EVENTS.AsString();

        public static readonly string AppleTvRemoteAllowRotation = SDL.SDL_HINT_APPLE_TV_REMOTE_ALLOW_ROTATION.AsString();

        public static readonly string IosHideHomeIndicator = SDL.SDL_HINT_IOS_HIDE_HOME_INDICATOR.AsString();

        public static readonly string AccelerometerAsJoystick = SDL.SDL_HINT_ACCELEROMETER_AS_JOYSTICK.AsString();

        public static readonly string TvRemoveAsJoystick = SDL.SDL_HINT_TV_REMOTE_AS_JOYSTICK.AsString();

        public static readonly string XInputEnabled = SDL.SDL_HINT_XINPUT_ENABLED.AsString();

        public static readonly string XInputUseOldJoystickMapping = SDL.SDL_HINT_XINPUT_USE_OLD_JOYSTICK_MAPPING.AsString();

        public static readonly string GameControllerType = SDL.SDL_HINT_GAMECONTROLLERTYPE.AsString();

        public static readonly string GameControllerConfig = SDL.SDL_HINT_GAMECONTROLLERCONFIG.AsString();

        public static readonly string GameControllerConfigFile = SDL.SDL_HINT_GAMECONTROLLERCONFIG_FILE.AsString();

        public static readonly string GameControllerIgnoreDevices = SDL.SDL_HINT_GAMECONTROLLER_IGNORE_DEVICES.AsString();

        public static readonly string GameControllerIgnoreDevicesExcept = SDL.SDL_HINT_GAMECONTROLLER_IGNORE_DEVICES_EXCEPT.AsString();

        public static readonly string GameControllerUseButtonLabels = SDL.SDL_HINT_GAMECONTROLLER_USE_BUTTON_LABELS.AsString();

        public static readonly string JoystickAllowBackgroundEvents = SDL.SDL_HINT_JOYSTICK_ALLOW_BACKGROUND_EVENTS.AsString();

        public static readonly string JoystickHidApi = SDL.SDL_HINT_JOYSTICK_HIDAPI.AsString();

        public static readonly string JoystickHidApiGameCube = SDL.SDL_HINT_JOYSTICK_HIDAPI_GAMECUBE.AsString();

        public static readonly string JoystickHidApiJoyCons = SDL.SDL_HINT_JOYSTICK_HIDAPI_JOY_CONS.AsString();

        public static readonly string JoystickHidApiLuna = SDL.SDL_HINT_JOYSTICK_HIDAPI_LUNA.AsString();

        public static readonly string JoystickHidApiPS4 = SDL.SDL_HINT_JOYSTICK_HIDAPI_PS4.AsString();

        public static readonly string JoystickHidApiPS4Rumble = SDL.SDL_HINT_JOYSTICK_HIDAPI_PS4_RUMBLE.AsString();

        public static readonly string JoystickHidApiPS5 = SDL.SDL_HINT_JOYSTICK_HIDAPI_PS5.AsString();

        public static readonly string JoystickHidApiPS5PlayerLed = SDL.SDL_HINT_JOYSTICK_HIDAPI_PS5_PLAYER_LED.AsString();

        public static readonly string JoystickHidApiPS5Rumble = SDL.SDL_HINT_JOYSTICK_HIDAPI_PS5_RUMBLE.AsString();

        public static readonly string JoystickHidApiStadia = SDL.SDL_HINT_JOYSTICK_HIDAPI_STADIA.AsString();

        public static readonly string JoystickHidApiSteam = SDL.SDL_HINT_JOYSTICK_HIDAPI_STEAM.AsString();

        public static readonly string JoystickHidApiSwitch = SDL.SDL_HINT_JOYSTICK_HIDAPI_SWITCH.AsString();

        public static readonly string JoystickHidApiSwitchHomeLed = SDL.SDL_HINT_JOYSTICK_HIDAPI_SWITCH_HOME_LED.AsString();

        public static readonly string JoystickHidApiXbox = SDL.SDL_HINT_JOYSTICK_HIDAPI_XBOX.AsString();

        public static readonly string EnableSteamControllers = SDL.SDL_HINT_ENABLE_STEAM_CONTROLLERS.AsString();

        public static readonly string JoystickRawInput = SDL.SDL_HINT_JOYSTICK_RAWINPUT.AsString();

        public static readonly string JoystickThread = SDL.SDL_HINT_JOYSTICK_THREAD.AsString();

        public static readonly string LinuxJoystickDeadzones = SDL.SDL_HINT_LINUX_JOYSTICK_DEADZONES.AsString();

        public static readonly string AllowTopMost = SDL.SDL_HINT_ALLOW_TOPMOST.AsString();

        public static readonly string TimerResolution = SDL.SDL_HINT_TIMER_RESOLUTION.AsString();

        public static readonly string QtWaylandContentOrientation = SDL.SDL_HINT_QTWAYLAND_CONTENT_ORIENTATION.AsString();

        public static readonly string QtWaylandWindowFlags = SDL.SDL_HINT_QTWAYLAND_WINDOW_FLAGS.AsString();

        public static readonly string ThreadStackSize = SDL.SDL_HINT_THREAD_STACK_SIZE.AsString();

        public static readonly string ThreadPriorityPolicy = SDL.SDL_HINT_THREAD_PRIORITY_POLICY.AsString();

        public static readonly string ThreadForceRealtimeTimeCritical = SDL.SDL_HINT_THREAD_FORCE_REALTIME_TIME_CRITICAL.AsString();

        public static readonly string VideoHighDpiDisabled = SDL.SDL_HINT_VIDEO_HIGHDPI_DISABLED.AsString();

        public static readonly string MacCtrlClickEmulateRightClick = SDL.SDL_HINT_MAC_CTRL_CLICK_EMULATE_RIGHT_CLICK.AsString();

        public static readonly string VideoWinD3DCompiler = SDL.SDL_HINT_VIDEO_WIN_D3DCOMPILER.AsString();

        public static readonly string VideoWindowSharePixelFormat = SDL.SDL_HINT_VIDEO_WINDOW_SHARE_PIXEL_FORMAT.AsString();

        public static readonly string WinrtPrivacyPolicyUrl = SDL.SDL_HINT_WINRT_PRIVACY_POLICY_URL.AsString();

        public static readonly string WinrtPrivacyPolicyLabel = SDL.SDL_HINT_WINRT_PRIVACY_POLICY_LABEL.AsString();

        public static readonly string WinrtHandleBackButton = SDL.SDL_HINT_WINRT_HANDLE_BACK_BUTTON.AsString();

        public static readonly string VideoMacFullScreenSpaces = SDL.SDL_HINT_VIDEO_MAC_FULLSCREEN_SPACES.AsString();

        public static readonly string MacBackgroundApp = SDL.SDL_HINT_MAC_BACKGROUND_APP.AsString();

        public static readonly string AndroidApkExpansionMainFileVersion = SDL.SDL_HINT_ANDROID_APK_EXPANSION_MAIN_FILE_VERSION.AsString();

        public static readonly string AndroidApkExpansionPatchFileVersion = SDL.SDL_HINT_ANDROID_APK_EXPANSION_PATCH_FILE_VERSION.AsString();

        public static readonly string ImeInternalEditing = SDL.SDL_HINT_IME_INTERNAL_EDITING.AsString();

        public static readonly string AndroidTrapBackButton = SDL.SDL_HINT_ANDROID_TRAP_BACK_BUTTON.AsString();

        public static readonly string AndroidBlockOnPause = SDL.SDL_HINT_ANDROID_BLOCK_ON_PAUSE.AsString();

        public static readonly string AndroidBlockOnPausePauseAudio = SDL.SDL_HINT_ANDROID_BLOCK_ON_PAUSE_PAUSEAUDIO.AsString();

        public static readonly string ReturnKeyHidesIme = SDL.SDL_HINT_RETURN_KEY_HIDES_IME.AsString();

        public static readonly string EmscriptenKeyboardElement = SDL.SDL_HINT_EMSCRIPTEN_KEYBOARD_ELEMENT.AsString();

        public static readonly string EmscriptenAsyncify = SDL.SDL_HINT_EMSCRIPTEN_ASYNCIFY.AsString();

        public static readonly string NoSignalHandlers = SDL.SDL_HINT_NO_SIGNAL_HANDLERS.AsString();

        public static readonly string WindowsNoCloseOnAltF4 = SDL.SDL_HINT_WINDOWS_NO_CLOSE_ON_ALT_F4.AsString();

        public static readonly string BmpSaveLegacyFormat = SDL.SDL_HINT_BMP_SAVE_LEGACY_FORMAT.AsString();

        public static readonly string WindowsDisableThreadNaming = SDL.SDL_HINT_WINDOWS_DISABLE_THREAD_NAMING.AsString();

        public static readonly string RpiVideoLayer = SDL.SDL_HINT_RPI_VIDEO_LAYER.AsString();

        public static readonly string VideoDoubleBuffer = SDL.SDL_HINT_VIDEO_DOUBLE_BUFFER.AsString();

        public static readonly string OpenGlEsDriver = SDL.SDL_HINT_OPENGL_ES_DRIVER.AsString();

        public static readonly string AudioResamplingMode = SDL.SDL_HINT_AUDIO_RESAMPLING_MODE.AsString();

        public static readonly string AudioCategory = SDL.SDL_HINT_AUDIO_CATEGORY.AsString();

        public static readonly string RenderBatching = SDL.SDL_HINT_RENDER_BATCHING.AsString();

        public static readonly string AutoUpdateJoysticks = SDL.SDL_HINT_AUTO_UPDATE_JOYSTICKS.AsString();

        public static readonly string AutoUpdateSensors = SDL.SDL_HINT_AUTO_UPDATE_SENSORS.AsString();

        public static readonly string EventLogging = SDL.SDL_HINT_EVENT_LOGGING.AsString();

        public static readonly string WaveRiffChunkSize = SDL.SDL_HINT_WAVE_RIFF_CHUNK_SIZE.AsString();

        public static readonly string WaveTruncation = SDL.SDL_HINT_WAVE_TRUNCATION.AsString();

        public static readonly string WaveFactChunk = SDL.SDL_HINT_WAVE_FACT_CHUNK.AsString();

        public static readonly string DisplayUsableBounds = SDL.SDL_HINT_DISPLAY_USABLE_BOUNDS.AsString();

        public static readonly string AudioDeviceAppName = SDL.SDL_HINT_AUDIO_DEVICE_APP_NAME.AsString();

        public static readonly string AudioDeviceStreamName = SDL.SDL_HINT_AUDIO_DEVICE_STREAM_NAME.AsString();

        public static readonly string PreferredLocales = SDL.SDL_HINT_PREFERRED_LOCALES.AsString();

        public static unsafe bool SetValue(string name, string value)
        {
            using var marshaledName = new MarshaledString(name);
            using var marshaledValue = new MarshaledString(value);
            return 0 != SDL.SetHint(marshaledName, marshaledValue);
        }

        public static unsafe bool SetValue(string name, string value, HintPriority priority)
        {
            using var marshaledName = new MarshaledString(name);
            using var marshaledValue = new MarshaledString(value);
            return 0 != SDL.SetHintWithPriority(marshaledName, marshaledValue, (SDL_HintPriority)priority);
        }

        public static unsafe string GetValue(string name)
        {
            using var marshaledName = new MarshaledString(name);
            var value = SDL.GetHint(marshaledName);
            if (value is null)
            {
                return null!;
            }
            return new string(value);
        }
    }
}
