using System;

using static System.Console;
/// <summary>
/// 
/// https://www.runoob.com/design-pattern/adapter-pattern.html
/// </summary>
namespace DesignPatternStudy.Patterns
{
    public interface IMediaPlayer
    {
        void play(String audioType, String fileName);
    }

    interface IAdvancedMediaPlayer
    {
        public void playVlc(String fileName);
        public void playMp4(String fileName);
    }

    public class VlcPlayer : IAdvancedMediaPlayer
    {
        public void playVlc(String fileName)
        {
            WriteLine("Playing vlc file. Name: " + fileName);
        }


        public void playMp4(String fileName)
        {
            //什么也不做
        }
    }

    public class Mp4Player : IAdvancedMediaPlayer
    {

        public void playVlc(String fileName)
        {
            //什么也不做
        }

        public void playMp4(String fileName)
        {
            WriteLine("Playing mp4 file. Name: " + fileName);
        }
    }

    public class MediaAdapter : IMediaPlayer
    {

        IAdvancedMediaPlayer advancedMusicPlayer;

        // constractor
        public MediaAdapter(String audioType)
        {
            if (audioType.Equals("vlc", StringComparison.OrdinalIgnoreCase))
            {
                advancedMusicPlayer = new VlcPlayer();
            }
            else if (audioType.Equals("mp4", StringComparison.OrdinalIgnoreCase))
            {
                advancedMusicPlayer = new Mp4Player();
            }
        }


        public void play(String audioType, String fileName)
        {
            if (audioType.Equals("vlc", StringComparison.OrdinalIgnoreCase))
            {
                advancedMusicPlayer.playVlc(fileName);
            }
            else if (audioType.Equals("mp4", StringComparison.OrdinalIgnoreCase))
            {
                advancedMusicPlayer.playMp4(fileName);
            }
        }
    }

    public class AudioPlayer : IMediaPlayer
    {
        MediaAdapter mediaAdapter;

        public void play(String audioType, String fileName)
        {

            //播放 mp3 音乐文件的内置支持
            if (audioType.Equals("mp3", StringComparison.OrdinalIgnoreCase))
            {
                WriteLine("Playing mp3 file. Name: " + fileName);
            }
            //mediaAdapter 提供了播放其他文件格式的支持
            else if (audioType.Equals("vlc", StringComparison.OrdinalIgnoreCase)
               || audioType.Equals("mp4", StringComparison.OrdinalIgnoreCase))
            {
                mediaAdapter = new MediaAdapter(audioType);
                mediaAdapter.play(audioType, fileName);
            }
            else
            {
                WriteLine("Invalid media. " +
                   audioType + " format not supported");
            }
        }
    }
}
