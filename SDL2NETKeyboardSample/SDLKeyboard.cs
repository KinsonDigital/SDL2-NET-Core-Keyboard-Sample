using SDL2;
using System.Collections.Generic;
using System.Linq;

namespace SDL2NETKeyboardSample
{
    public class SDLKeyboard : IKeyboard
    {
        #region Private Vars
        /// <summary>
        /// Holds a list of all the keys and there current state this frame.
        /// </summary>
        private readonly static Dictionary<SDL.SDL_Keycode, bool> _currentStateKeys =
            new Dictionary<SDL.SDL_Keycode, bool>((from k in KeyboardKeyMapper.SDLToStandardMappings select new KeyValuePair<SDL.SDL_Keycode, bool>(k.Key, false)).ToArray());

        /// <summary>
        /// Holds a list of all the keys and there state the previous frame.
        /// </summary>
        private readonly static Dictionary<SDL.SDL_Keycode, bool> _prevStateKeys =
            new Dictionary<SDL.SDL_Keycode, bool>((from k in KeyboardKeyMapper.SDLToStandardMappings select new KeyValuePair<SDL.SDL_Keycode, bool>(k.Key, false)).ToArray());
        #endregion


        #region Props
        public bool CapsLockOn => (SDL.SDL_GetModState() & SDL.SDL_Keymod.KMOD_CAPS) == SDL.SDL_Keymod.KMOD_CAPS;

        public bool NumLockOn => (SDL.SDL_GetModState() & SDL.SDL_Keymod.KMOD_NUM) == SDL.SDL_Keymod.KMOD_NUM;

        public bool IsLeftShiftDown => (SDL.SDL_GetModState() & SDL.SDL_Keymod.KMOD_LSHIFT) == SDL.SDL_Keymod.KMOD_LSHIFT;

        public bool IsRightShiftDown => (SDL.SDL_GetModState() & SDL.SDL_Keymod.KMOD_RSHIFT) == SDL.SDL_Keymod.KMOD_RSHIFT;

        public bool IsLeftCtrlDown => (SDL.SDL_GetModState() & SDL.SDL_Keymod.KMOD_LCTRL) == SDL.SDL_Keymod.KMOD_LCTRL;

        public bool IsRightCtrlDown => (SDL.SDL_GetModState() & SDL.SDL_Keymod.KMOD_RCTRL) == SDL.SDL_Keymod.KMOD_RCTRL;

        public bool IsLeftAltDown => (SDL.SDL_GetModState() & SDL.SDL_Keymod.KMOD_LALT) == SDL.SDL_Keymod.KMOD_LALT;

        public bool IsRightAltDown => (SDL.SDL_GetModState() & SDL.SDL_Keymod.KMOD_RALT) == SDL.SDL_Keymod.KMOD_RALT;
        #endregion


        #region Public Methods       
        public bool AreAnyKeysDown() => _currentStateKeys.Any(k => k.Value);


        public KeyCodes[] GetCurrentPressedKeys()
        {
            return (from k in _currentStateKeys
                    where k.Value
                    select KeyboardKeyMapper.ToStandardKeyCode(k.Key)).ToArray();
        }


        public KeyCodes[] GetPreviousPressedKeys()
        {
            return (from k in _prevStateKeys
                    where k.Value
                    select KeyboardKeyMapper.ToStandardKeyCode(k.Key)).ToArray();
        }


        public bool IsAnyKeyDown(KeyCodes[] keys)
        {
            var downKeys = (from k in _currentStateKeys where k.Value select k).ToArray();


            return keys.Any(k => downKeys.Any(dk => dk.Key == KeyboardKeyMapper.ToSDLKeyCode((KeyCodes)k)));
        }


        public bool IsKeyDown(KeyCodes key) => _currentStateKeys[KeyboardKeyMapper.ToSDLKeyCode(key)];


        public bool IsKeyPressed(KeyCodes key) => _currentStateKeys[KeyboardKeyMapper.ToSDLKeyCode(key)] == false &&
            _prevStateKeys[KeyboardKeyMapper.ToSDLKeyCode(key)] == true;


        public bool IsKeyUp(KeyCodes key) => !IsKeyDown(key);


        public void UpdateCurrentState()
        {
            while (SDL.SDL_PollEvent(out var e) != 0)
            {
                if (e.type == SDL.SDL_EventType.SDL_KEYDOWN)
                {
                    _currentStateKeys[e.key.keysym.sym] = true;
                }
                else if (e.type == SDL.SDL_EventType.SDL_KEYUP)
                {
                    _currentStateKeys[e.key.keysym.sym] = false;
                }
            }
        }


        public void UpdatePreviousState()
        {
            foreach (var currentKey in _currentStateKeys)
            {
                _prevStateKeys[currentKey.Key] = currentKey.Value;
            }
        }
        #endregion
    }
}
