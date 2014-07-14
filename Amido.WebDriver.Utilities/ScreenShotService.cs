using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;

using OpenQA.Selenium;

namespace Amido.WebDriver.Utilities
{
    public class ScreenShotService
    {
        private static string baseDirectorySetting;

        private static string testRunIdSetting;

        private static Func<IWebDriver> webDriverFunc;

        private static Func<string> featureTitleFunc; 

        private static Func<string> scenarioTitleFunc;

        public static void Init(
            string baseDirectory, 
            Func<IWebDriver> webDriver, 
            Func<string> featureTitle,
            Func<string> scenarioTitle,
            string testRunId = "")
        {
            baseDirectorySetting = baseDirectory;
            testRunIdSetting = testRunId;
            webDriverFunc = webDriver;
            featureTitleFunc = featureTitle;
            scenarioTitleFunc = scenarioTitle;
        }

        public void Capture(string imageName = "", string message = "")
        {
            if (
                string.IsNullOrWhiteSpace(baseDirectorySetting) || 
                webDriverFunc == null || 
                featureTitleFunc == null || 
                scenarioTitleFunc == null)
            {
                throw new Exception("You must call ScreenShotService.Init before using the Capture function");
            }

            var screenShotFilePath = CreateScreenShot(imageName);

            if (!string.IsNullOrWhiteSpace(message))
            {
                AppendErrorMessageToImage(screenShotFilePath, message);
            }
        }

        private static string CreateScreenShot(string imageName)
        {
            if (string.IsNullOrWhiteSpace(imageName))
            {
                imageName = Guid.NewGuid().ToString();
            }

            var featureFolder = featureTitleFunc();
            var scenarioFolder = scenarioTitleFunc();
            var screenshotDriver = webDriverFunc() as ITakesScreenshot;
            if (screenshotDriver == null)
            {
                return string.Empty;
            }

            var screenshot = screenshotDriver.GetScreenshot();

            if (string.IsNullOrWhiteSpace(baseDirectorySetting))
            {
                return string.Empty;
            }

            var directoryPath = baseDirectorySetting + GetTestRunId() + featureFolder + @"\" + scenarioFolder;

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var filePath = string.Format(directoryPath + @"\{0}", TruncateFileName(CleanFileName(imageName)));

            if (!File.Exists(filePath + ".png"))
            {
                screenshot.SaveAsFile(filePath + ".png", ImageFormat.Png);
                return filePath + ".png";
            }

            var counter = 1;
            while (true)
            {
                if (File.Exists(filePath + "_" + counter + ".png"))
                {
                    counter++;
                    continue;
                }

                screenshot.SaveAsFile(filePath + "_" + counter + ".png", ImageFormat.Png);

                return filePath + "_" + counter + ".png";
            }
        }

        private static string CleanFileName(string fileName)
        {
            return Path.GetInvalidFileNameChars().Aggregate(
                fileName,
                (current, c) => current.Replace(c.ToString(CultureInfo.InvariantCulture), string.Empty));
        }

        private static object TruncateFileName(string value)
        {
            return value.Length <= 50 ? value : value.Substring(0, 50);
        }

        private static string GetTestRunId()
        {
            if (string.IsNullOrWhiteSpace(testRunIdSetting))
            {
                return string.Empty;
            }
            return testRunIdSetting + @"\";
        }

        private static void AppendErrorMessageToImage(string imageFilePath, string message)
        {
            Bitmap bitmap;
            using (var stream = File.OpenRead(imageFilePath))
            {
                bitmap = (Bitmap)Image.FromStream(stream);
            }

            using (bitmap)
            using (var graphics = Graphics.FromImage(bitmap))
            using (var font = new Font("Arial", 20, FontStyle.Regular))
            {
                graphics.DrawString(message, font, Brushes.Red, 0, 0);
                bitmap.Save(imageFilePath);
            }
        }
    }
}