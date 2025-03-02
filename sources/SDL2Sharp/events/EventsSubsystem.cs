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
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL2Sharp.Interop;

namespace SDL2Sharp
{
    internal sealed unsafe class EventsSubsystem : FinalizableObject, IEventsSubsystem
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int EventWatchCallbackDelegate(void* userdata, SDL_Event* @event);

        private const uint InitSubsystemFlags = Interop.SDL.SDL_INIT_EVENTS;

        private GCHandle _watchCallbackUserData = default;

        private readonly HashSet<Action<Event>> _watchCallbacks = [];

        public EventsSubsystem()
        {
            Error.ThrowLastErrorIfNegative(
                Interop.SDL.InitSubSystem(InitSubsystemFlags)
            );

            _watchCallbackUserData = GCHandle.Alloc(this, GCHandleType.Normal);
            var watchUserDataPointer = (void*)(IntPtr)_watchCallbackUserData;
            Interop.SDL.AddEventWatch(&OnEventWatchCallback, watchUserDataPointer);
        }

        protected override void Dispose(bool disposing)
        {
            var watchUserDataPointer = (void*)(IntPtr)_watchCallbackUserData;
            Interop.SDL.DelEventWatch(&OnEventWatchCallback, watchUserDataPointer);

            if (_watchCallbackUserData.IsAllocated)
            {
                _watchCallbackUserData.Free();
            }

            Interop.SDL.QuitSubSystem(InitSubsystemFlags);
        }

        public Event? PollEvent()
        {
            var @event = new SDL_Event();
            if (Interop.SDL.PollEvent(&@event) == 0)
            {
                return null;
            }
            return WrapEvent(@event);
        }

        public void PushEvent(Event @event)
        {
            var eventHandle = @event.Handle;
            var result = Interop.SDL.PushEvent(&eventHandle);
            Error.ThrowLastErrorIfNegative(result);
        }

        public void AddWatch(Action<Event> callback)
        {
            _watchCallbacks.Add(callback);
        }

        public void DeleteWatch(Action<Event> callback)
        {
            _watchCallbacks.Remove(callback);
        }

        private static Event WrapEvent(SDL_Event @event)
        {
            var eventType = (SDL_EventType)@event.type;
            switch (eventType)
            {
                case SDL_EventType.SDL_QUIT:
                    return new QuitEvent(@event);
                case SDL_EventType.SDL_APP_TERMINATING:
                    return new AppTerminatingEvent(@event);
                case SDL_EventType.SDL_APP_LOWMEMORY:
                    return new AppLowMemoryEvent(@event);
                case SDL_EventType.SDL_APP_WILLENTERBACKGROUND:
                    return new AppWillEnterBackgroundEvent(@event);
                case SDL_EventType.SDL_APP_DIDENTERBACKGROUND:
                    return new AppDidEnterBackgroundEvent(@event);
                case SDL_EventType.SDL_APP_WILLENTERFOREGROUND:
                    return new AppWillEnterForegroundEvent(@event);
                case SDL_EventType.SDL_APP_DIDENTERFOREGROUND:
                    return new AppDidEnterForegroundEvent(@event);
                case SDL_EventType.SDL_LOCALECHANGED:
                    return new LocaleChangedEvent(@event);
                case SDL_EventType.SDL_DISPLAYEVENT:
                    return new DisplayEvent(@event);
                case SDL_EventType.SDL_WINDOWEVENT:
                    switch ((SDL_WindowEventID)@event.window.@event)
                    {
                        case SDL_WindowEventID.SDL_WINDOWEVENT_SHOWN:
                            return new WindowShownEvent(@event);
                        case SDL_WindowEventID.SDL_WINDOWEVENT_HIDDEN:
                            return new WindowHiddenEvent(@event);
                        case SDL_WindowEventID.SDL_WINDOWEVENT_EXPOSED:
                            return new WindowExposedEvent(@event);
                        case SDL_WindowEventID.SDL_WINDOWEVENT_MOVED:
                            return new WindowMovedEvent(@event);
                        case SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED:
                            return new WindowResizedEvent(@event);
                        case SDL_WindowEventID.SDL_WINDOWEVENT_SIZE_CHANGED:
                            return new WindowSizeChangedEvent(@event);
                        case SDL_WindowEventID.SDL_WINDOWEVENT_MINIMIZED:
                            return new WindowMinimizedEvent(@event);
                        case SDL_WindowEventID.SDL_WINDOWEVENT_MAXIMIZED:
                            return new WindowMaximizedEvent(@event);
                        case SDL_WindowEventID.SDL_WINDOWEVENT_RESTORED:
                            return new WindowRestoredEvent(@event);
                        case SDL_WindowEventID.SDL_WINDOWEVENT_ENTER:
                            return new WindowEnterEvent(@event);
                        case SDL_WindowEventID.SDL_WINDOWEVENT_LEAVE:
                            return new WindowLeaveEvent(@event);
                        case SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_GAINED:
                            return new WindowFocusGainedEvent(@event);
                        case SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_LOST:
                            return new WindowFocusLostEvent(@event);
                        case SDL_WindowEventID.SDL_WINDOWEVENT_CLOSE:
                            return new WindowCloseEvent(@event);
                        case SDL_WindowEventID.SDL_WINDOWEVENT_TAKE_FOCUS:
                            return new WindowTakeFocusEvent(@event);
                        case SDL_WindowEventID.SDL_WINDOWEVENT_HIT_TEST:
                            return new WindowHitTestEvent(@event);
                        case SDL_WindowEventID.SDL_WINDOWEVENT_ICCPROF_CHANGED:
                            return new WindowIccProfileChangedEvent(@event);
                        case SDL_WindowEventID.SDL_WINDOWEVENT_DISPLAY_CHANGED:
                            return new WindowDisplayChangedEvent(@event);
                    }
                    break;
                case SDL_EventType.SDL_SYSWMEVENT:
                    return new SystemWindowManagerEvent(@event);
                case SDL_EventType.SDL_KEYDOWN:
                    return new KeyDownEvent(@event);
                case SDL_EventType.SDL_KEYUP:
                    return new KeyUpEvent(@event);
                case SDL_EventType.SDL_TEXTEDITING:
                    return new TextEditingEvent(@event);
                case SDL_EventType.SDL_TEXTINPUT:
                    return new TextInputEvent(@event);
                case SDL_EventType.SDL_KEYMAPCHANGED:
                    return new KeyMapChangedEvent(@event);
                case SDL_EventType.SDL_TEXTEDITING_EXT:
                    return new TextEditingExtEvent(@event);
                case SDL_EventType.SDL_MOUSEMOTION:
                    return new MouseMotionEvent(@event);
                case SDL_EventType.SDL_MOUSEBUTTONDOWN:
                    return new MouseButtonDownEvent(@event);
                case SDL_EventType.SDL_MOUSEBUTTONUP:
                    return new MouseButtonUpEvent(@event);
                case SDL_EventType.SDL_MOUSEWHEEL:
                    return new MouseWheelEvent(@event);
                case SDL_EventType.SDL_JOYAXISMOTION:
                    return new JoystickAxisMotionEvent(@event);
                case SDL_EventType.SDL_JOYBALLMOTION:
                    return new JoystickBallMotionEvent(@event);
                case SDL_EventType.SDL_JOYHATMOTION:
                    return new JoystickHatMotionEvent(@event);
                case SDL_EventType.SDL_JOYBUTTONDOWN:
                    return new JoystickButtonDownEvent(@event);
                case SDL_EventType.SDL_JOYBUTTONUP:
                    return new JoystickButtonUpEvent(@event);
                case SDL_EventType.SDL_JOYDEVICEADDED:
                    return new JoystickDeviceAddedEvent(@event);
                case SDL_EventType.SDL_JOYDEVICEREMOVED:
                    return new JoystickDeviceRemovedEvent(@event);
                case SDL_EventType.SDL_JOYBATTERYUPDATED:
                    return new JoystickBatteryUpdatedEvent(@event);
                case SDL_EventType.SDL_CONTROLLERAXISMOTION:
                    return new ControllerAxisMotionEvent(@event);
                case SDL_EventType.SDL_CONTROLLERBUTTONDOWN:
                    return new ControllerButtonDownEvent(@event);
                case SDL_EventType.SDL_CONTROLLERBUTTONUP:
                    return new ControllerButtonUpEvent(@event);
                case SDL_EventType.SDL_CONTROLLERDEVICEADDED:
                    return new ControllerDeviceAddedEvent(@event);
                case SDL_EventType.SDL_CONTROLLERDEVICEREMOVED:
                    return new ControllerDeviceRemovedEvent(@event);
                case SDL_EventType.SDL_CONTROLLERDEVICEREMAPPED:
                    return new ControllerDeviceRemappedEvent(@event);
                case SDL_EventType.SDL_CONTROLLERTOUCHPADDOWN:
                    return new ControllerTouchpadDownEvent(@event);
                case SDL_EventType.SDL_CONTROLLERTOUCHPADMOTION:
                    return new ControllerTouchpadMotionEvent(@event);
                case SDL_EventType.SDL_CONTROLLERTOUCHPADUP:
                    return new ControllerTouchpadUpEvent(@event);
                case SDL_EventType.SDL_CONTROLLERSENSORUPDATE:
                    return new ControllerSensorUpdateEvent(@event);
                case SDL_EventType.SDL_FINGERDOWN:
                    return new FingerDownEvent(@event);
                case SDL_EventType.SDL_FINGERUP:
                    return new FingerUpEvent(@event);
                case SDL_EventType.SDL_FINGERMOTION:
                    return new FingerMotionEvent(@event);
                case SDL_EventType.SDL_DOLLARGESTURE:
                    return new DollarGestureEvent(@event);
                case SDL_EventType.SDL_DOLLARRECORD:
                    return new DollarRecordEvent(@event);
                case SDL_EventType.SDL_MULTIGESTURE:
                    return new MultiGestureEvent(@event);
                case SDL_EventType.SDL_CLIPBOARDUPDATE:
                    return new ClipboardUpdateEvent(@event);
                case SDL_EventType.SDL_DROPFILE:
                    return new DropFileEvent(@event);
                case SDL_EventType.SDL_DROPTEXT:
                    return new DropTextEvent(@event);
                case SDL_EventType.SDL_DROPBEGIN:
                    return new DropBeginEvent(@event);
                case SDL_EventType.SDL_DROPCOMPLETE:
                    return new DropCompleteEvent(@event);
                case SDL_EventType.SDL_AUDIODEVICEADDED:
                    return new AudioDeviceAddedEvent(@event);
                case SDL_EventType.SDL_AUDIODEVICEREMOVED:
                    return new AudioDeviceRemovedEvent(@event);
                case SDL_EventType.SDL_SENSORUPDATE:
                    return new SensorUpdateEvent(@event);
                case SDL_EventType.SDL_RENDER_TARGETS_RESET:
                    return new RenderTargetsResetEvent(@event);
                case SDL_EventType.SDL_RENDER_DEVICE_RESET:
                    return new RenderDeviceResetEvent(@event);
                case SDL_EventType.SDL_POLLSENTINEL:
                    return new PollSentinelEvent(@event);
                case SDL_EventType.SDL_USEREVENT:
                    return new UserEvent(@event);
            }
            throw new NotImplementedException("Event type not implemented");
        }

        private void OnEventWatchCallback(Event @event)
        {
            foreach (var callback in _watchCallbacks)
            {
                callback(@event);
            }
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
        private static int OnEventWatchCallback(void* userdata, SDL_Event* @event)
        {
            var eventSubsystemHandle = GCHandle.FromIntPtr((IntPtr)userdata);
            if (eventSubsystemHandle.Target is EventsSubsystem eventSubsystem)
            {
                eventSubsystem.OnEventWatchCallback(WrapEvent(*@event));
            }
            return 0;
        }
    }
}
