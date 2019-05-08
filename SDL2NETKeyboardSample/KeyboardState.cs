using SDL2;
using System;
using System.Collections.Generic;
using System.Text;

namespace SDL2NETKeyboardSample
{
    public struct KeyboardState
    {
        public List<SDL.SDL_Keycode> Keys { get; set; }


        public KeyboardState(List<SDL.SDL_Keycode> keys)
        {
            Keys = new List<SDL.SDL_Keycode>();

            foreach (var key in keys)
                Keys.Add(key);
        }

        public bool IsKeyDown(SDL.SDL_Keycode key) => Keys.Contains(key);


        public bool IsKeyUp(SDL.SDL_Keycode key) => !IsKeyDown(key);
    }
}
