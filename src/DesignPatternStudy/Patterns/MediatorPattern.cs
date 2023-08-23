using System;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace DesignPatternStudy.Patterns
{
    // https://www.runoob.com/design-pattern/mediator-pattern.html
    // 中介者模式
    public class ChatRoom
    {
        // 通过中间类，封装多个类中相同之行为
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