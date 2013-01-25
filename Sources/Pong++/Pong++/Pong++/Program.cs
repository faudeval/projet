using System;

namespace Pong__
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Pong__ game = new Pong__())
            {
                game.Run();
            }
        }
    }
#endif
}

