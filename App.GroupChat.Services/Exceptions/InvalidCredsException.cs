using System.Runtime.Serialization;

namespace App.GroupChat.Services.Exceptions {
    [Serializable]
    public class InvalidCredsException : Exception {
        public InvalidCredsException(string message) : base(message) { }
        public InvalidCredsException(string message, Exception exception) : base(message, exception) { }
        public InvalidCredsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
