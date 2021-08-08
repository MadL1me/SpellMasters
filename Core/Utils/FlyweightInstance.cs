namespace Core.Utils
{
    /// <summary>
    /// Represents an object that follows the flyweight data pattern.
    /// </summary>
    /// <wiki>https://en.wikipedia.org/wiki/Flyweight_pattern</wiki>
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
        /// Data shared between all instances of this flyweight type but casted
        /// to one of its children. It is very useful for the ActionCard implementation.
        /// Since all CardData children are stored upcasted,we can use it to retrieve a real child instance
        /// </summary>
        /// <typeparam name="Type"></typeparam>
        /// <returns></returns>
        public Type SharedDataAs<Type>() where Type : TData =>
            (Type)FlyweightStorage<TData>.Instance.GetData(NumericId);

        /// <summary>
        /// Creates a flyweight instance and assigns it the specified id
        /// </summary>
        protected FlyweightInstance(uint id)
        {
            NumericId = id;
        }
    }
}