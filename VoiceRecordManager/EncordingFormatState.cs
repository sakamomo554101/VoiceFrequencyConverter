using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// add
using Windows.Media.Capture;

namespace VoiceRecordManager
{
    public class EncordingFormatState
    {
        public EncordingFormatType EncordingFormat;
        public EncordingFormatState()
        {
            EncordingFormat = EncordingFormatType.Mp3;
        }
    }
}
