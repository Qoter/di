using System.IO;
using TagCloud.Core.Infratructure;

namespace TagCloud.Core.Settings
{
    public class InputSettings
    {
        public string WordsFile { get; }

        private InputSettings(string wordsFile)
        {
            WordsFile = wordsFile;
        }

        public static Result<InputSettings> Create(string wordsFile)
        {
            return !File.Exists(wordsFile) 
                ? Result.Fail<InputSettings>($"Words file not exists {wordsFile}") 
                : Result.Ok(new InputSettings(wordsFile));
        }
    }
}