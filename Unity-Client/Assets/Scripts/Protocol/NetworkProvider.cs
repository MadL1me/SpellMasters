namespace MagicCardGame.Assets.Scripts.Protocol
{
    public static class NetworkProvider
    {
        public static ServerConnection Connection { get; private set; }

        public static bool Connect(string hostName, int port)
        {
            Connection = new ServerConnection(new ClientPacketBus());

            if (Connection.Connect(hostName, port))
                return true;

            Connection = null;
            return false;
        }

        public static void Poll()
        {
            if (Connection != null && Connection.State != ConnectionState.Invalid)
                Connection.Poll();
        }
    }
}