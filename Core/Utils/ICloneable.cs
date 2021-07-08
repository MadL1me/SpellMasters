namespace Core.Utils
{
    public interface ICloneable<TObject> where TObject : class
    {
        TObject Clone();
    }
}