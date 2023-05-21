using System.Runtime.Serialization;

namespace App.GroupChat.Api.Exceptions {
    [Serializable]
    public class ForbiddenException : Exception {
        public ForbiddenException(string message): base(message) { }
        public ForbiddenException(string message, Exception exception): base(message, exception) { }
        public ForbiddenException(SerializationInfo info, StreamingContext context): base(info, context) { }
        
    }
}
