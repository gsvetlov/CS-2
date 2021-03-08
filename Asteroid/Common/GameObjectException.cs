using System;

namespace Asteroid
{
    [System.Serializable]
    public class GameObjectException : Exception
    {
        public GameObjectException() { }
        public GameObjectException(string message) : base(message) { }
        public GameObjectException(string message, Exception inner) : base(message, inner) { }
        protected GameObjectException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

}
