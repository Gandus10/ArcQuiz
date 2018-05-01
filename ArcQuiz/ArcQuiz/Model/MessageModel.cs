using Plugin.DeviceInfo;
using System;

namespace ArcQuiz.Model
{
    class MessageModel
    {
        public string Text { get; set; }
        public DateTime MessagDateTime { get; set; }

        public bool IsIncoming => UserId != CrossDeviceInfo.Current.Id;

        public string Name { get; set; }
        public string UserId { get; set; }
    }
}
