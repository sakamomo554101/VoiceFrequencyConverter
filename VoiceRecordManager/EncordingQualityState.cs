using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// add
using Windows.Media.MediaProperties;

namespace VoiceRecordManager
{
    public class EncordingQualityState
    {
        public enum EncordingQualityType
        {
            Auto   = AudioEncodingQuality.Auto,
            High   = AudioEncodingQuality.High,
            Medium = AudioEncodingQuality.Medium,
            Low    = AudioEncodingQuality.Low
        }

        public EncordingQualityType EncordingQuality;
        public EncordingQualityState()
        {
            EncordingQuality = EncordingQualityType.Auto;
        }
    }
}
