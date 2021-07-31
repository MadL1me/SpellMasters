namespace Core.Utils
{
    /// <summary>
    /// Our version of ICloneable class with generic params
    /// </summary>
    /// <typeparam name="TObject">Cloneable object</typeparam>
    public interface ICloneable<TObject> where TObject : class
    {
        TObject Clone();
    }
}