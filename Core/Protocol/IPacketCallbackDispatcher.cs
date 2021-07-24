namespace Core.Protocol
{
    public interface IPacketCallbackDispatcher
    {
        void DispatchEvent(object sender, object callbackEvent, object packet);
        void DispatchError(object sender, object callbackError, object packet);
    }
}