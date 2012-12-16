namespace Master_Of_Olympus
{
#if WINDOWS || XBOX
    static class EntryPoint
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Master_Of_Olympus game = new Master_Of_Olympus())
            {
                game.Run();
            }
        }
    }
#endif
}

