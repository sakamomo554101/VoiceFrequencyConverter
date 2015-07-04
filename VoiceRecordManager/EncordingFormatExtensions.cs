using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoiceRecordManager
{
    public enum EncordingFormatType
    {
        Mp3,
        Mp4,
        Avi,
        Wma
    }

    public static class EncordingFormatExtensions
    {
        public static string ToFileExtension(EncordingFormatType encodingFormat)
        {
            switch (encodingFormat)
            {
                case EncordingFormatType.Mp3:
                    return ".mp3";
                case EncordingFormatType.Mp4:
                    return ".mp4";
                case EncordingFormatType.Avi:
                    return ".avi";
                case EncordingFormatType.Wma:
                    return ".wma";
                default:
                    throw new ArgumentOutOfRangeException("encodingFormat");
            }
        }
    }
}
