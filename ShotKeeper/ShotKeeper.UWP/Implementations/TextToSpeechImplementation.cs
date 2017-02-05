using ShotKeeper.Interfaces;
using ShotKeeper.UWP.Implementations;
using System;
using Windows.UI.Xaml.Controls;

[assembly: Xamarin.Forms.Dependency (typeof (TextToSpeechImplementation))]
namespace ShotKeeper.UWP.Implementations
{
    public class TextToSpeechImplementation : ITextToSpeech
    {
        public TextToSpeechImplementation() { }
        public async void Speak(string text)
        {
            var mediaElement = new MediaElement();
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
            var stream = await synth.SynthesizeTextToStreamAsync(text);

            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
        }
    }
}
