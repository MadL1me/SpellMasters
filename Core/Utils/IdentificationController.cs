namespace Core.Utils
{
    /// <summary>
    /// Static class for creating unique indexes for any types of entities.
    /// </summary>
    /// <typeparam name="TypeForId">Can be any type, class will generate unique numbers from 0</typeparam>
    public static class IdentificationController<TypeForId>
    {
        public static ulong LastFreeId = 0;

        public static ulong GetNextID()
        {
            return LastFreeId++;
        }
    }
}
