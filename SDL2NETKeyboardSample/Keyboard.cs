using SDL2;
using System.Collections.Generic;

namespace SDL2NETKeyboardSample
{
    /// <summary>
    /// Provides the ability to get the state of the keyboard.
    /// </summary>
    public static class Keyboard
    {
        private readonly static List<SDL.SDL_Keycode> _keys = new List<SDL.SDL_Keycode>();


        /// <summary>
        /// Gets the current state of the keyboard.
        /// </summary>
        /// <returns></returns>
        public static KeyboardState GetState() => new KeyboardState(_keys);


        /// <summary>
        /// Adds the given <paramref name="key"/> to the list of keys being pressed.
        /// </summary>
        /// <param name="key">The key to add.</param>
        public static void AddKey(SDL.SDL_Keycode key)
        {
            if (!_keys.Contains(key))
                _keys.Add(key);
        }


        /// <summary>
        /// Removes the given <paramref name="key"/> from the list of keys being pressed.
        /// </summary>
        /// <param name="key">The key to add.</param>
        public static void RemoveKey(SDL.SDL_Keycode key) => _keys.Remove(key);
    }
}
