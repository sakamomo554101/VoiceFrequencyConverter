using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// add
using VoiceRecordManager;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace VoiceConverterSampleApp
{
    /// <summary>
    /// Frame 内へナビゲートするために利用する空欄ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private VoiceRecorder mVoiceRecorder = null;
        private FileSavePicker mFileSavePicker = null;

        public MainPage()
        {
            this.InitializeComponent();
            mVoiceRecorder = new VoiceRecorder();
        }

        private async void RecordVoice_Button_Click(object sender, RoutedEventArgs e)
        {
            if (mVoiceRecorder.GetRecordingMode() == RecordingModeType.Uninitialized)
            {
                await mVoiceRecorder.Initialize();
                ModifyRecordState_TextBlock(mVoiceRecorder.GetRecordingMode());
            }

            if (mVoiceRecorder.GetRecordingMode() == RecordingModeType.Initialized)
            {
                bool ret = await mVoiceRecorder.StartRecording();
                ModifyRecordState_TextBlock(mVoiceRecorder.GetRecordingMode());
            }
        }

        private async void StopRecordVoice_Button_Click(object sender, RoutedEventArgs e)
        {
            if (mVoiceRecorder.GetRecordingMode() == RecordingModeType.Recording)
            {
                bool ret = await mVoiceRecorder.StopRecording();
                ModifyRecordState_TextBlock(mVoiceRecorder.GetRecordingMode());
            }
        }

        private async void SaveRecordVoice_Button_Click(object sender, RoutedEventArgs e)
        {
            if (mVoiceRecorder.GetRecordingMode() == RecordingModeType.Stopped)
            {
                byte[] voiceData = await mVoiceRecorder.GetRecordingData();

                // 取得した録音データをファイルに保存する
                String fileExtention = EncordingFormatExtensions.ToFileExtension(mVoiceRecorder.GetEncordingFormat());
                InitFileSavePicker(fileExtention);
                var saveFile = await mFileSavePicker.PickSaveFileAsync();
                await FileIO.WriteBytesAsync(saveFile, voiceData);

                ModifyRecordState_TextBlock(mVoiceRecorder.GetRecordingMode());
            }
        }

        private void InitFileSavePicker(String fileExtention)
        {
            mFileSavePicker = new FileSavePicker();
            mFileSavePicker.FileTypeChoices.Add("Encoding", new List<string>() { fileExtention });
            mFileSavePicker.SuggestedStartLocation = PickerLocationId.MusicLibrary;
        }

        private void ModifyRecordState_TextBlock(RecordingModeType mode)
        {
            switch (mode)
            {
                case RecordingModeType.Uninitialized:
                    RecordState_TextBlock.Text = "未初期化";
                    break;
                case RecordingModeType.Initialized:
                    RecordState_TextBlock.Text = "初期化済み";
                    break;
                case RecordingModeType.Recording:
                    RecordState_TextBlock.Text = "録音中";
                    break;
                case RecordingModeType.Stopped:
                    RecordState_TextBlock.Text = "録音終了";
                    break;
            }
        }
    }
}
