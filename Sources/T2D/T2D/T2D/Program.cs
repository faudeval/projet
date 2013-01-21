using System;

namespace T2D
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (T2D game = new T2D())
            {
                game.Run();
            }
        }
    }
#endif
}

