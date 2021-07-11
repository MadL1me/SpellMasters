namespace MagicCardGame.Assets.Scripts.Protocol
{
    public static class Network
    {
        public static ServerConnection Connection { get; private set; }

        public static void Connect(string hostName, int port)
        {
            Connection = new ServerConnection(new ClientPacketBus());
            Connection.Connect(hostName, port);
        }

        public static void Poll()
        {
            if (Connection != null && Connection.State != ConnectionState.Invalid)
                Connection.Poll();
        }
    }
}