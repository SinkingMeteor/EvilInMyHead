using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Sheldier.Common.Localization
{
    public class CsvLocalizationLoader : ILocalizationLoader
    {
        private readonly LocalizationPathProvider _pathProvider;
        private const string CSV_EXTENSION = ".csv";
        public CsvLocalizationLoader(LocalizationPathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }
        
        public Dictionary<string, string> LoadFile(Language language)
        {
            string path = _pathProvider.GetPath(language, CSV_EXTENSION);
            if (!File.Exists(path))
                throw new ArgumentException($"Language file {language.ToString()} by path {path} not found");
            Dictionary<string, string> _localization = new Dictionary<string, string>();
            var keys = File.ReadLines(path).Skip(1).First();
            string line;
            using (StreamReader streamReader = new StreamReader(path))
            {
                while (!streamReader.EndOfStream)
                {
                    line = streamReader.ReadLine();
                    string[] lines = line.Split(";");
                    _localization.Add(lines[0], lines[1]);
                }
            }

            return _localization;
        }
        



    }
}