using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// add
using Windows.Media.Capture;

namespace VoiceRecordManager
{
    class EncordingFormatState
    {
        public enum EncordingFormatType
        {
            Mp3,
            Mp4,
            Wma
        }
        public EncordingFormatType EncordingFormat;
        public EncordingFormatState()
        {
            EncordingFormat = EncordingFormatType.Mp3;
        }
    }
}
