using SDL2;
using System.Collections.Generic;
using System.Linq;

namespace SDL2NETKeyboardSample
{
    public class SDLKeyboard : IKeyboard
    {
        #region Private Vars
        /// <summary>
        /// The letter keys on the keyboard.
        /// </summary>
        private static readonly SDL.SDL_Keycode[] _letters = new SDL.SDL_Keycode[]
        {
            SDL.SDL_Keycode.SDLK_a, SDL.SDL_Keycode.SDLK_b, SDL.SDL_Keycode.SDLK_c, SDL.SDL_Keycode.SDLK_d,
            SDL.SDL_Keycode.SDLK_e, SDL.SDL_Keycode.SDLK_f, SDL.SDL_Keycode.SDLK_g,SDL.SDL_Keycode.SDLK_h,
            SDL.SDL_Keycode.SDLK_i, SDL.SDL_Keycode.SDLK_j, SDL.SDL_Keycode.SDLK_k, SDL.SDL_Keycode.SDLK_l,
            SDL.SDL_Keycode.SDLK_m, SDL.SDL_Keycode.SDLK_n, SDL.SDL_Keycode.SDLK_o, SDL.SDL_Keycode.SDLK_p,
            SDL.SDL_Keycode.SDLK_q, SDL.SDL_Keycode.SDLK_r, SDL.SDL_Keycode.SDLK_s, SDL.SDL_Keycode.SDLK_t,
            SDL.SDL_Keycode.SDLK_u, SDL.SDL_Keycode.SDLK_v, SDL.SDL_Keycode.SDLK_w, SDL.SDL_Keycode.SDLK_x,
            SDL.SDL_Keycode.SDLK_y, SDL.SDL_Keycode.SDLK_z
        };

        /// <summary>
        /// The number keys on the keypad.
        /// </summary>
        private readonly SDL.SDL_Keycode[] _numPadKeys = new SDL.SDL_Keycode[]
        {
            SDL.SDL_Keycode.SDLK_KP_0, SDL.SDL_Keycode.SDLK_KP_1,
            SDL.SDL_Keycode.SDLK_KP_2, SDL.SDL_Keycode.SDLK_KP_3,
            SDL.SDL_Keycode.SDLK_KP_4, SDL.SDL_Keycode.SDLK_KP_5,
            SDL.SDL_Keycode.SDLK_KP_6, SDL.SDL_Keycode.SDLK_KP_7,
            SDL.SDL_Keycode.SDLK_KP_8, SDL.SDL_Keycode.SDLK_KP_9
        };

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
        public bool CapsLockOn => _currentStateKeys.Any(k => k.Key == SDL.SDL_Keycode.SDLK_CAPSLOCK && k.Value);


        public bool NumLockOn => _currentStateKeys.Any(k => k.Key == SDL.SDL_Keycode.SDLK_NUMLOCKCLEAR && k.Value);
        #endregion


        #region Public Methods
        public bool AnyNumpadNumberKeysDown()
        {
            //Get all of the keys that are pressed down
            var downKeys = (from k in _currentStateKeys where k.Value select k).ToArray();


            return downKeys.Any(k => _numPadKeys.Any(n => n == k.Key));
        }


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


        public bool IsKeyPressed(KeyCodes key)
        {
            return _currentStateKeys[KeyboardKeyMapper.ToSDLKeyCode(key)] && !_prevStateKeys[KeyboardKeyMapper.ToSDLKeyCode(key)];
        }


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


        public bool AnyLettersPressed()
        {
            foreach (var letter in _letters)
            {
                if (_currentStateKeys[letter] && !_prevStateKeys[letter])
                    return true;
            }


            return false;
        }
        #endregion
    }
}
