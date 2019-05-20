using SDL2;
using System;

namespace SDL2NETKeyboardSample
{
    /*Description
     * This application is a sample to showcase one way of getting keyboard input in SDL2.
     * To demonstrate the sample, run the application.  Make sure the white window is in
     * focus and press any of the arrow keys.  The last key pressed will be displayed in 
     * the console window.
     */

    public class Program
    {
        private const int WINDOW_WIDTH = 640;
        private const int WINDOW_HEIGHT = 480;
        private static bool _quit = false;
        private static IntPtr _windowPtr = IntPtr.Zero;
        private static Keyboard _keyboard;


        static void Main(string[] args)
        {
            //Setup the keyboard
            _keyboard = new Keyboard(new SDLKeyboard());

            //Initialize SDL.  A true value means initialization was successful.
            var isInitialized = Init();

            Console.WriteLine("Press one of the arrow keys!");
            Console.WriteLine();

            //If successfully initialized.
            if (isInitialized)
            {
                //This can represent your game loop
                while(!_quit)
                {
                    //Update the current state of the keyboard
                    _keyboard.UpdateCurrentState();

                    KeyCodes pressedKey;

                    if (_keyboard.AnyNumbersPressed(out pressedKey))
                    {
                        Console.WriteLine($"Number key '{pressedKey.ToString()}' pressed");
                    }
                    else
                    {
                    }

                    //var pressedKeys = _keyboard.GetCurrentPressedKeys();

                    //foreach (var key in pressedKeys)
                    //{
                    //    Console.WriteLine(key.ToString());
                    //}

                    //Update the previous state of the keyboard
                    _keyboard.UpdatePreviousState();
                }
            }
            else
            {
                Console.WriteLine("Failed to initialize!");
                Console.ReadLine();
            }
        }


        /// <summary>
        /// Initialize SDL.
        /// </summary>
        /// <returns>Returns true if succss and false if failure.</returns>
        private static bool Init()
        {
            //Init the video card
            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
            {
                Console.WriteLine("SDL could not initialize! SDL_Error: {0}", SDL.SDL_GetError());
                return false;
            }
            else
            {
                //Create an SDL window to render graphics upon.
                _windowPtr = SDL.SDL_CreateWindow("SDL2 Keyboard Sample", SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED, WINDOW_WIDTH, WINDOW_HEIGHT, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

                if (_windowPtr == IntPtr.Zero)
                {
                    Console.WriteLine("The window could not be created!!");
                    Console.ReadLine();
                    return false;
                }
            }


            return true;
        }
    }
}
