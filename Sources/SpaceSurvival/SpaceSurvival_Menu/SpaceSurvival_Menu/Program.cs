using System;

namespace SpaceSurvival_Menu
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (SpaceSurvivalGame game = new SpaceSurvivalGame())
            {
                game.Run();
            }
        }
    }
#endif
}

