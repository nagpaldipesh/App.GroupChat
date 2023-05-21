using System.Runtime.Serialization;

namespace App.GroupChat.Services.Exceptions {
    [Serializable]
    public class NotFoundException : Exception {
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, Exception exception) : base(message, exception) { }
        public NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
