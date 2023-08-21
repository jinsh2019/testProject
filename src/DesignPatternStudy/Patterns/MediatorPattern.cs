using System;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace DesignPatternStudy.Patterns
{
    public class ChatRoom
    {
        public static void ShowMessage(User user, string message)
        {
            Console.WriteLine(new DateTime().ToString() + $"[" + user.Name + "]:" + message);
        }
    }

    public class User
    {
        public string Name { get; set; }

        public User(string name)
        {
            this.Name = name;
        }
        public void sendMessage(string message)
        {
            ChatRoom.ShowMessage(this, message);
        }
    }
}