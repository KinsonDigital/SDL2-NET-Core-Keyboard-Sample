using SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDL2NETKeyboardSample
{
    public static class Keyboard
    {
        private static List<SDL.SDL_Keycode> _keys = new List<SDL.SDL_Keycode>();


        public static KeyboardState GetState() => new KeyboardState(_keys);


        public static void AddKey(SDL.SDL_Keycode key)
        {
            if (!_keys.Contains(key))
                _keys.Add(key);
        }


        public static void RemoveKey(SDL.SDL_Keycode key) => _keys.Remove(key);
    }
}
