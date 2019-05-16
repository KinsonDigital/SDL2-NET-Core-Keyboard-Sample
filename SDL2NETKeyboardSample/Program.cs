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
        private static KeyboardState _currentKeyState;
        private static KeyboardState _prevKeyState;


        static void Main(string[] args)
        {
            //Initialize SDL.  A true value means initialization was successful.
            var isInitialized = Init();

            Console.WriteLine("Press one of the arrow keys!");
            Console.WriteLine();

            //Local helper function for printing a message to the console.
            void WriteMessage(SDL.SDL_Keycode key)
            {
                Console.Clear();
                Console.WriteLine("Press one of the arrow keys!");
                Console.WriteLine();
                Console.WriteLine($"The '{key.ToString()}' was pressed!!");
            }


            //If successfully initialized.
            if (isInitialized)
            {
                //This can represent your game loop
                while(!_quit)
                {
                    //Process any incoming events from the window such as window specific
                    //events or keyboard events.
                    ProcessWindowEvents();

                    //Get the current state of the keyboard
                    _currentKeyState = Keyboard.GetState();


                    foreach (var key in _currentKeyState.PressedKeys)
                    {
                        var keyText = key.ToString();

                        Console.WriteLine($"Key: {keyText} - {(int)key}");
                    }

                    //If any of the arrow keys are being pressed down, push that message to the console
                    if (_currentKeyState.IsKeyDown(SDL.SDL_Keycode.SDLK_LEFT) && _prevKeyState.IsKeyUp(SDL.SDL_Keycode.SDLK_LEFT))
                    {
                        WriteMessage(SDL.SDL_Keycode.SDLK_LEFT);
                    }
                    else if (_currentKeyState.IsKeyDown(SDL.SDL_Keycode.SDLK_RIGHT) && _prevKeyState.IsKeyUp(SDL.SDL_Keycode.SDLK_RIGHT))
                    {
                        WriteMessage(SDL.SDL_Keycode.SDLK_RIGHT);
                    }
                    else if (_currentKeyState.IsKeyDown(SDL.SDL_Keycode.SDLK_UP) && _prevKeyState.IsKeyUp(SDL.SDL_Keycode.SDLK_UP))
                    {
                        WriteMessage(SDL.SDL_Keycode.SDLK_UP);
                    }
                    else if (_currentKeyState.IsKeyDown(SDL.SDL_Keycode.SDLK_DOWN) && _prevKeyState.IsKeyUp(SDL.SDL_Keycode.SDLK_DOWN))
                    {
                        WriteMessage(SDL.SDL_Keycode.SDLK_DOWN);
                    }

                    //Save the current state of the keyboard as the previous state for the 
                    //next game loop iteration.  The difference of the current keyboard state and
                    //the previous represents the state of the keyboard during this frame
                    //and the previous frame
                    _prevKeyState = _currentKeyState;
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


        /// <summary>
        /// Processes window related events such as movement, resizing, minimize and keyboard.
        /// </summary>
        private static void ProcessWindowEvents()
        {
            while (SDL.SDL_PollEvent(out var e) != 0)
            {
                if (e.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    _quit = true;
                }
                else if (e.type == SDL.SDL_EventType.SDL_KEYDOWN)
                {
                    Keyboard.AddKey(e.key.keysym.sym);
                }
                else if (e.type == SDL.SDL_EventType.SDL_KEYUP)
                {
                    Keyboard.RemoveKey(e.key.keysym.sym);
                }
            }
        }
    }
}
