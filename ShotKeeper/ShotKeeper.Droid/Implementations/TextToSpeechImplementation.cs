using System;
using ShotKeeper.Interfaces;
using Android.Speech.Tts;
using Xamarin.Forms;
using ShotKeeper.Droid.Implementations;

[assembly: Xamarin.Forms.Dependency (typeof (TextToSpeechImplementation))]
namespace ShotKeeper.Droid.Implementations
{
    public class TextToSpeechImplementation : Java.Lang.Object, ITextToSpeech, TextToSpeech.IOnInitListener
    {

        private TextToSpeech _speaker;
        private string _toSpeak;

        public TextToSpeechImplementation() { }

        public void Speak(string text)
        {
            var c = Forms.Context;
            _toSpeak = text;
            if (_speaker == null)
            {
                _speaker = new TextToSpeech(c, this);
            }
            else
            {
                _speaker.Speak(_toSpeak, QueueMode.Flush, null, null);
            }
        }

        #region IOnInitListener implementation
        public void OnInit(OperationResult status)
        {
            if (status.Equals(OperationResult.Success))
            {
                _speaker.Speak(_toSpeak, QueueMode.Flush, null, null);
            }
        }

        #endregion
    }
}