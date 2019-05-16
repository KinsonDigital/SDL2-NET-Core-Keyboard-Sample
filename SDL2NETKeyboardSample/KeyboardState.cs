using SDL2;
using System.Collections.Generic;

namespace SDL2NETKeyboardSample
{
    /// <summary>
    /// Represents the state of the keyboard.
    /// </summary>
    public struct KeyboardState
    {
        private List<SDL.SDL_Keycode> _keys;


        /// <summary>
        /// Creates a new instance of <see cref="KeyboardState"/>.
        /// </summary>
        /// <param name="keys"></param>
        public KeyboardState(List<SDL.SDL_Keycode> keys)
        {
            _keys = new List<SDL.SDL_Keycode>();

            foreach (var key in keys)
                _keys.Add(key);
        }


        public SDL.SDL_Keycode[] PressedKeys => _keys.ToArray();


        /// <summary>
        /// Returns a value indicating if the given <paramref name="key"/> is being pressed down.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns></returns>
        public bool IsKeyDown(SDL.SDL_Keycode key) => _keys.Contains(key);


        /// <summary>
        /// Returns a value indicating if the given <paramref name="key"/> is not being pressed.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns></returns>
        public bool IsKeyUp(SDL.SDL_Keycode key) => !IsKeyDown(key);
    }
}
