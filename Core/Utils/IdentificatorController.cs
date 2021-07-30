using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utils
{
    public static class IdentificatorController<TypeForId>
    {
        public static ulong LastFreeId = 0;

        public static ulong GetNextID()
        {
            return LastFreeId++;
        }
    }
}
