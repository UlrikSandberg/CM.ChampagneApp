using System;
namespace CM.ChampagneApp.UI.CustomEventArgs
{
    public class TextEnteredEventArgs
    {

        public string TextEntered { get; private set; }

        public TextEnteredEventArgs(string enteredText)
        {
            TextEntered = enteredText;
        }
    }
}
