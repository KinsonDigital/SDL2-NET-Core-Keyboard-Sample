using SDL2;
using System;

namespace SDL2NETKeyboardSample
{
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
            var isInitialized = Init();

            Console.WriteLine("Press one of the arrow keys!");
            Console.WriteLine();

            void WriteMessage(SDL.SDL_Keycode key)
            {
                Console.Clear();
                Console.WriteLine("Press one of the arrow keys!");
                Console.WriteLine();
                Console.WriteLine($"The '{key.ToString()}' was pressed!!");
            }


            if (isInitialized)
            {
                while(!_quit)
                {
                    ProcessWindowEvents();

                    _currentKeyState = Keyboard.GetState();

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

                    _prevKeyState = _currentKeyState;
                }
            }
            else
            {
                Console.WriteLine("Failed to initialize!");
                Console.ReadLine();
            }
        }


        private static bool Init()
        {
            //Initialize SDL
            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
            {
                Console.WriteLine("SDL could not initialize! SDL_Error: {0}", SDL.SDL_GetError());
                return false;
            }
            else
            {
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
