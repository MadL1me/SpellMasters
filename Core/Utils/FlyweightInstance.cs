namespace Core.Utils
{
    /// <summary>
    /// Represents an object that follows the flyweight data pattern.
    /// https://en.wikipedia.org/wiki/Flyweight_pattern
    /// </summary>
    public abstract class FlyweightInstance<TData>
    {
        /// <summary>
        /// The numeric id used for accessing the storage
        /// </summary>
        public uint NumericId { get; protected set; }

        /// <summary>
        /// Data shared between all instances of this flyweight type
        /// </summary>
        public TData SharedData => FlyweightStorage<TData>.Instance.GetData(NumericId);

        /// <summary>
        /// Creates a flyweight instance and assigns it the specified id
        /// </summary>
        protected FlyweightInstance(uint id)
        {
            NumericId = id;
        }
    }
}