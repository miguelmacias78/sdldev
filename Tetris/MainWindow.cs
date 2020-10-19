using SDL2;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class MainWindow
    {
        bool running;
        SDL.SDL_Event e;
        IntPtr renderer;
        IntPtr tilesTexture;

        int M = 20;
        int N = 10;

        int[,] field;

        int[,] shapes = new int[7, 4]
        {
            { 1,3,5,7 }, //I
            { 2,4,5,7 }, //Z
            { 3,5,4,6 }, //S
            { 3,5,4,7 }, //T
            { 2,3,5,7 }, //L
            { 3,5,7,6 }, //J
            { 2,3,4,5 }  //O
        };

        int shapeToDraw = 6;

        Point[] point = new Point[4]
        {
            new Point(),
            new Point(),
            new Point(),
            new Point()
        };

        int dx = 0;
        bool rotate = false;

        public void Run()
        {
            SDL.SDL_Init(SDL.SDL_INIT_VIDEO);
            SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG | SDL_image.IMG_InitFlags.IMG_INIT_JPG);

            var window = SDL.SDL_CreateWindow("Testing SDL2", SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED, 400, 800, 0);
            renderer = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

            field = new int[M, N];

            tilesTexture = SDL_image.IMG_LoadTexture(renderer, @"img\Tiles.png");
            if (tilesTexture == IntPtr.Zero)
            {
                Console.WriteLine("Error loading texture: " + SDL.SDL_GetError());
            }


            for (int i = 0; i < 4; i++)
            {
                point[i].X = shapes[shapeToDraw, i] % 2;
                point[i].Y = shapes[shapeToDraw, i] / 2;
            }

            running = true;

            while (running)
            {
                HandelInput();
                Update();
                Render();
            }

            SDL.SDL_DestroyWindow(window);
            SDL.SDL_Quit();
        }

        void HandelInput()
        {
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                switch (e.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT:
                        running = false;
                        break;
                }
            }

            int numKeys;
            var keyState = SDL.SDL_GetKeyboardState(out numKeys);
        }

        void Update()
        {

        }

        void Render()
        {
            SDL.SDL_SetRenderDrawColor(renderer, 0, 0, 0, 255);
            SDL.SDL_RenderClear(renderer);

            for (int i = 0; i < 4; i++)
            {
                var srcRect = new SDL.SDL_Rect() { x = 64 * (shapeToDraw + 2), y = 0, w = 64, h = 64 };
                var tgtRectr = new SDL.SDL_Rect() { x = point[i].X * 32, y = point[i].Y * 32, w = 32, h = 32 };
                SDL.SDL_RenderCopy(renderer, tilesTexture, ref srcRect, ref tgtRectr);
            }

            SDL.SDL_RenderPresent(renderer);
        }
    }
}
