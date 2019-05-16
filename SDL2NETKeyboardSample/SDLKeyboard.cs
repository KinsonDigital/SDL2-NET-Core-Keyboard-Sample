using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDL2NETKeyboardSample
{
    public class SDLKeyboard : IKeyboard
    {
        //Holds the stats of each key.  True is pressed and false is not pressed
        private readonly static Dictionary<SDL.SDL_Keycode, bool> _currentStateKeys = new Dictionary<SDL.SDL_Keycode, bool>();
        private readonly static Dictionary<SDL.SDL_Keycode, bool> _prevStateKeys = new Dictionary<SDL.SDL_Keycode, bool>();

        //TODO: Remove
        //private KeyboardState _currentKeyState;
        //private KeyboardState _prevKeyState;


        public SDLKeyboard()
        {

        }

        public bool CapsLockOn { get; }

        public bool NumLockOn { get; }


        public bool AnyNumpadNumbersKeysDown()
        {
            throw new NotImplementedException();
        }


        public bool AreAnyKeysDown()
        {
            throw new NotImplementedException();
        }


        public bool AreAnyKeysPressed()
        {
            throw new NotImplementedException();
        }


        public int[] GetCurrentPressedKeys()
        {
            return (from k in _currentStateKeys
                    where k.Value
                    select (int)k.Key).ToArray();
        }


        public int[] GetPreviousPressedKeys()
        {
            return (from k in _prevStateKeys
                    where k.Value
                    select (int)k.Key).ToArray();
        }


        public bool IsAnyKeyDown(int[] keys) => _currentStateKeys.Any(k => k.Value);


        public bool IsKeyDown(int key) => _currentStateKeys[(SDL.SDL_Keycode)key];


        public bool IsKeyPressed(int key)
        {
            return _currentStateKeys[(SDL.SDL_Keycode)key] && !_prevStateKeys[(SDL.SDL_Keycode)key];
        }


        public bool IsKeyUp(int key) => !IsKeyDown(key);


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


        public bool WasLetterPressed()
        {
            throw new NotImplementedException();
        }
    }
}
