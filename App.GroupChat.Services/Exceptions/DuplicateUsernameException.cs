using System.Runtime.Serialization;

namespace App.GroupChat.Services.Exceptions {
    [Serializable]
    public class DuplicateUsernameException : Exception {
        public DuplicateUsernameException(string message) : base(message) { }
        public DuplicateUsernameException(string message, Exception exception) : base(message, exception) { }
        public DuplicateUsernameException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
