using System;

using static System.Console;
/// <summary>
/// 
/// https://www.runoob.com/design-pattern/adapter-pattern.html
/// </summary>
namespace DesignPatternStudy.Patterns
{

    #region 注释了
    //// 对外部接口显示出一样的方法名
    //public interface IMediaPlayer
    //{
    //    void play(String audioType, String fileName);
    //}

    //// 封装了不同的接口
    //interface IAdvancedMediaPlayer
    //{
    //    public void playVlc(String fileName);
    //    public void playMp4(String fileName);
    //}

    //public class VlcPlayer : IAdvancedMediaPlayer
    //{
    //    public void playVlc(String fileName)
    //    {
    //        WriteLine("Playing vlc file. Name: " + fileName);
    //    }


    //    public void playMp4(String fileName)
    //    {
    //        //什么也不做
    //    }
    //}

    //public class Mp4Player : IAdvancedMediaPlayer
    //{

    //    public void playVlc(String fileName)
    //    {
    //        //什么也不做
    //    }

    //    public void playMp4(String fileName)
    //    {
    //        WriteLine("Playing mp4 file. Name: " + fileName);
    //    }
    //}

    //public class MediaAdapter : IMediaPlayer
    //{

    //    IAdvancedMediaPlayer advancedMusicPlayer;

    //    // constractor
    //    public MediaAdapter(String audioType)
    //    {
    //        if (audioType.Equals("vlc", StringComparison.OrdinalIgnoreCase))
    //        {
    //            advancedMusicPlayer = new VlcPlayer();
    //        }
    //        else if (audioType.Equals("mp4", StringComparison.OrdinalIgnoreCase))
    //        {
    //            advancedMusicPlayer = new Mp4Player();
    //        }
    //    }

    //    // 构造函数生产具体的播放器，对外显示出Play方法即可（最外部）
    //    public void play(String audioType, String fileName)
    //    {
    //        if (audioType.Equals("vlc", StringComparison.OrdinalIgnoreCase))
    //        {
    //            advancedMusicPlayer.playVlc(fileName);
    //        }
    //        else if (audioType.Equals("mp4", StringComparison.OrdinalIgnoreCase))
    //        {
    //            advancedMusicPlayer.playMp4(fileName);
    //        }
    //    }
    //}

    //public class AudioPlayer : IMediaPlayer
    //{
    //    MediaAdapter mediaAdapter;

    //    public void play(String audioType, String fileName)
    //    {

    //        //播放 mp3 音乐文件的内置支持
    //        if (audioType.Equals("mp3", StringComparison.OrdinalIgnoreCase))
    //        {
    //            WriteLine("Playing mp3 file. Name: " + fileName);
    //        }
    //        //mediaAdapter 提供了播放其他文件格式的支持
    //        else if (audioType.Equals("vlc", StringComparison.OrdinalIgnoreCase)
    //           || audioType.Equals("mp4", StringComparison.OrdinalIgnoreCase))
    //        {
    //            mediaAdapter = new MediaAdapter(audioType);
    //            mediaAdapter.play(audioType, fileName);
    //        }
    //        else
    //        {
    //            WriteLine("Invalid media. " +
    //               audioType + " format not supported");
    //        }
    //    }
    //} 
    #endregion

    //https://www.bilibili.com/video/BV1Hz411e7sA/?spm_id_from=333.788&vd_source=4e306a9c5b741e5f5039fefb051598ff
    // can abstract speaker
    public class Speaker
    {
        public string speak()
        {
            return "China No.1";
        }
    }
    // Adapter interface
    interface Translator
    {
        public string translate();
    }
    // implement Adapter interface
    // A client can call adapter to get the translated result that it wants
    public class Adapter : Translator
    {
        // refered relationship
        private Speaker speaker;
        public Adapter(Speaker speaker)
        {
            this.speaker = speaker;
        }

        public string translate()
        {
            string result = speaker.speak();
            // handle logic: if En; if Fr; if Cn
            return result;
        }
    }

}
