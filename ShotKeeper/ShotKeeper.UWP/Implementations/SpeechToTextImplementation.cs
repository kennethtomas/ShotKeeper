using ShotKeeper.Interfaces;
using ShotKeeper.UWP.Implementations;
using System;
using System.Diagnostics;
using Windows.Media.SpeechRecognition;

[assembly: Xamarin.Forms.Dependency(typeof(SpeechToTextImplementation))]
namespace ShotKeeper.UWP.Implementations
{
    public class SpeechToTextImplementation : ISpeechToText
    {
        public async void StartListening()
        {

            try
            {
                // Create an instance of SpeechRecognizer.
                var speechRecognizer = new Windows.Media.SpeechRecognition.SpeechRecognizer();

                // Compile the dictation grammar by default.
                await speechRecognizer.CompileConstraintsAsync();

                // Start recognition.
                Windows.Media.SpeechRecognition.SpeechRecognitionResult speechRecognitionResult = await speechRecognizer.RecognizeAsync();

                // Do something with the recognition result.
                var messageDialog = new Windows.UI.Popups.MessageDialog(speechRecognitionResult.Text, "Text spoken");
                await messageDialog.ShowAsync();

            }
            catch (Exception err)
            {
                // Define a variable that holds the error for the speech recognition privacy policy. 
                // This value maps to the SPERR_SPEECH_PRIVACY_POLICY_NOT_ACCEPTED error, 
                // as described in the Windows.Phone.Speech.Recognition error codes section later on.
                const int privacyPolicyHResult = unchecked((int)0x80045509);

                // Check whether the error is for the speech recognition privacy policy.
                if (err.HResult == privacyPolicyHResult)
                {
                    var messageDialog = new Windows.UI.Popups.MessageDialog("The privacy statement was declined. Go to Settings->Privacy->Speech, inking and typing, and ensure you have viewed the privacy policy, and 'Get To Know You' is enabled.", "Error");
                    await messageDialog.ShowAsync();
                }
                else
                {
                    // Handle other types of errors here.
                }
            }


        }
    }
}
