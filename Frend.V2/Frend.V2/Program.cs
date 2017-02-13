using System;

namespace Frend.V2
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (FrendGame game = new FrendGame())
            {
                game.Run();
            }
        }
    }
#endif
}

