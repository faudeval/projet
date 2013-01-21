using System;

namespace SpaceSurvival_Optimise
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (SpaceSurvival game = new SpaceSurvival())
            {
                game.Run();
            }
        }
    }
#endif
}

