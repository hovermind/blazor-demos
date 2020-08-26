using Microsoft.AspNetCore.Components;
using System.Text.RegularExpressions;

namespace AzureAppServiceEasyAuth.Components
{
    public class TextAvatarBase: ComponentBase
    {
        [Parameter]
        public string FullName { get; set; }

        public string InitialLetters { get; set; }

        protected override void OnParametersSet()
        {
            InitialLetters = TextUtil.GetInitials(FullName);
        }

    }

    public class TextUtil
    {
        private const string PatternForInitialLetterExtraction = @"(?i)(?:^|\s|-)+([^\s-])[^\s-]*(?:(?:\s+)(?:the\s+)?(?:jr|sr|II|2nd|III|3rd|IV|4th)\.?$)?";
        public static string GetInitials(string fullName) => Regex.Replace(fullName, PatternForInitialLetterExtraction, "$1").ToUpper();
    }
}

