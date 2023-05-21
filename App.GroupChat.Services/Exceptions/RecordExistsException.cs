using System.Runtime.Serialization;

namespace App.GroupChat.Services.Exceptions {
    [Serializable]
    public class RecordExistsException : Exception {
        public RecordExistsException(string message) : base(message) { }
        public RecordExistsException(string message, Exception exception) : base(message, exception) { }
        public RecordExistsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
