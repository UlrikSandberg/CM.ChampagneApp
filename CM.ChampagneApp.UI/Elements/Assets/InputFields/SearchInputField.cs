using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Assets.InputFields
{
    public class SearchInputField : Entry
    {
        private Dictionary<Guid, bool> TypingInProgress { get; set; } = new Dictionary<Guid, bool>();

        public delegate void DelayedTextChanged(object sender, TextChangedEventArgs e);
        public event DelayedTextChanged OnDelayedTextChanged;

        public static BindableProperty InputDelayProperty =
            BindableProperty.Create(nameof(InputDelay), typeof(long), typeof(SearchInputField));

        public SearchInputField()
        {
            TextChanged += SearchInputField_TextChanged;
        }

        public long InputDelay
        {
            get { return (long)GetValue(InputDelayProperty); }
            set { SetValue(InputDelayProperty, value); }
        }

        private void SearchInputField_TextChanged(object sender, TextChangedEventArgs e)
        {
            Handle_TextChangedAsync(e);
        }

        public async Task Handle_TextChangedAsync(TextChangedEventArgs e)
        {
            var typeSessionId = Guid.NewGuid();
            var typeSessionIsOpen = true;

            TypingInProgress.Add(typeSessionId, typeSessionIsOpen);

            //This values determines how lengthy the input pause should be before sending the text for search
            await Task.Delay((int)InputDelay);

            TypingInProgress[typeSessionId] = false;
            SendText(e);
        }

        private void SendText(TextChangedEventArgs e)
        {
            var typeSessionsEnded = true;
            foreach (var typeSessionIsOpen in TypingInProgress.Values)
            {
                if (typeSessionIsOpen)
                {
                    typeSessionsEnded = false;
                }
            }

            if (typeSessionsEnded)
            {
                if(OnDelayedTextChanged != null)
                {
                    TypingInProgress.Clear();
                    OnDelayedTextChanged(this, e);
                }
            }
        }
    }
}
