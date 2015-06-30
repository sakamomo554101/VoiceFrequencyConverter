using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// add
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage.Streams;
using System.Threading.Tasks;

namespace VoiceRecordManager
{
    public class VoiceRecorder
    {
        private MediaCapture mMediaCapture = null;
        private VoiceRecordingState mRecordingState = null;
        private EncordingFormatState mEncordingFormatState = null;
        private EncordingQualityState mEncordingQualityState = null;
        private IRandomAccessStream mAudioMemory = null; 

        public VoiceRecorder()
        {
            // TODO
        }

        public async void Initialize() 
        {
            // 音声のみを記録するように設定する
            MediaCaptureInitializationSettings settings = new MediaCaptureInitializationSettings();
            settings.StreamingCaptureMode = StreamingCaptureMode.Audio;

            // 各コンポーネントの初期化
            mRecordingState = new VoiceRecordingState();
            mEncordingFormatState = new EncordingFormatState();
            mEncordingQualityState = new EncordingQualityState();
            mMediaCapture = await InitMediaCapture(settings, MediaCaptureOnFailed, MediaCaptureOnRecordLimitationExceeded);
        }

        public void Finalize()
        {
            // TODO
        }

        public async void StartRecording()
        {
            if (mMediaCapture == null)
            {
                return;
            }
            
            MediaEncodingProfile profile = getProfileFromEncordingFormat(mEncordingFormatState, mEncordingQualityState);
            mAudioMemory = new InMemoryRandomAccessStream();
            await mMediaCapture.StartRecordToStreamAsync(profile, mAudioMemory);
            mRecordingState.RecordingMode = VoiceRecordingState.RecordingModeType.Recording;
        }

        public async void StopRecording()
        {
            if (mMediaCapture == null)
            {
                return;
            }

            await mMediaCapture.StopRecordAsync();
            mRecordingState.RecordingMode = VoiceRecordingState.RecordingModeType.Stopped;
        }

        public void setEncordingFormat(EncordingFormatState.EncordingFormatType encordingFormat)
        {
            mEncordingFormatState.EncordingFormat = encordingFormat;
        }

        public EncordingFormatState.EncordingFormatType getEncordingFormat()
        {
            return mEncordingFormatState.EncordingFormat;
        }

        public void setEncordingQuality(EncordingQualityState.EncordingQualityType encordingQuality)
        {
            mEncordingQualityState.EncordingQuality = encordingQuality;
        }

        public EncordingQualityState.EncordingQualityType getEncordingQuality()
        {
            return mEncordingQualityState.EncordingQuality;
        }

        public VoiceRecordingState.RecordingModeType getRecordingMode()
        {
            return mRecordingState.RecordingMode;
        }



        private async Task<MediaCapture> InitMediaCapture(MediaCaptureInitializationSettings Settings,
                                                          MediaCaptureFailedEventHandler FailedDelegate, 
                                                          RecordLimitationExceededEventHandler LimitationDelegate)
        {
            if (Settings == null)
            {
                return null;
            }
            MediaCapture capture = new MediaCapture();
            await capture.InitializeAsync(Settings);

            capture.Failed += FailedDelegate;
            capture.RecordLimitationExceeded += LimitationDelegate;

            return capture;
        }

        private async void MediaCaptureOnFailed(MediaCapture sender, MediaCaptureFailedEventArgs errorEventArgs)
        {

        }

        private async void MediaCaptureOnRecordLimitationExceeded(MediaCapture sender)
        {

        }

        private MediaEncodingProfile getProfileFromEncordingFormat(EncordingFormatState encordingFormatState, 
                                                                   EncordingQualityState encordingQualityState)
        {
            if (encordingFormatState == null || encordingQualityState == null) {
                return null;
            }

            AudioEncodingQuality quality = getAudioEncordingQuality(encordingQualityState);

            MediaEncodingProfile encordingProfile = null;
            switch (mEncordingFormatState.EncordingFormat)
            {
                case EncordingFormatState.EncordingFormatType.Mp3:
                    encordingProfile = MediaEncodingProfile.CreateMp3(quality);
                    break;
                case EncordingFormatState.EncordingFormatType.Mp4:
                    encordingProfile = MediaEncodingProfile.CreateM4a(quality);
                    break;
                case EncordingFormatState.EncordingFormatType.Wma:
                    encordingProfile = MediaEncodingProfile.CreateWma(quality);
                    break;
            }

            return encordingProfile;
        }

        private AudioEncodingQuality getAudioEncordingQuality(EncordingQualityState encordingQualityState)
        {
            AudioEncodingQuality quality = AudioEncodingQuality.Auto;
            switch (encordingQualityState.EncordingQuality)
            {
                case EncordingQualityState.EncordingQualityType.Auto:
                    quality = AudioEncodingQuality.Auto;
                    break;
                case EncordingQualityState.EncordingQualityType.High:
                    quality = AudioEncodingQuality.High;
                    break;
                case EncordingQualityState.EncordingQualityType.Medium:
                    quality = AudioEncodingQuality.Medium;
                    break;
                case EncordingQualityState.EncordingQualityType.Low:
                    quality = AudioEncodingQuality.Low;
                    break;
            }
            return quality;
        }
    }
}
