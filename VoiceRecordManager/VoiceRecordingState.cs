using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoiceRecordManager
{
    class VoiceRecordingState
    {
        public enum RecordingModeType
        {
            Initializing,
            Recording,
            Stopped
        }
        public RecordingModeType RecordingMode;

        public VoiceRecordingState()
        {
            RecordingMode = RecordingModeType.Initializing;
        }
    }
}
