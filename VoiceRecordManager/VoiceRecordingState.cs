using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoiceRecordManager
{
    public enum RecordingModeType
    {
        Uninitialized,
        Initialized,
        Recording,
        Stopped
    }

    public class VoiceRecordingState
    {
        public RecordingModeType RecordingMode;

        public VoiceRecordingState()
        {
            RecordingMode = RecordingModeType.Uninitialized;
        }
    }
}
