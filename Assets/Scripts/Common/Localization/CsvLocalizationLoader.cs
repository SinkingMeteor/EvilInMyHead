using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Sheldier.Common.Localization
{
    public class CsvLocalizationLoader : ILocalizationLoader
    {
        private readonly LocalizationPathProvider _pathProvider;
        private const string CSV_EXTENSION = ".csv";

        private const string LOCALIZATION_PATH = "Localization/Data";
        public CsvLocalizationLoader(LocalizationPathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }
        
        public Dictionary<string, string> LoadFile(Language language)
        {
            var langIndex = (byte) language + 1;
            var textAssets = Resources.LoadAll<TextAsset>(LOCALIZATION_PATH);
            var Dictionary = new Dictionary<string, string>();
            foreach (var textAsset in textAssets)
            {
                var text = ReplaceMarkers(textAsset.text).Replace("\"\"", "[quotes]");
                var matches = Regex.Matches(text, "\"[\\s\\S]+?\"");

                foreach (Match match in matches)
                {
                    text = text.Replace(match.Value, match.Value.Replace("\"", null).Replace(",", "[comma]").Replace("\n", "[newline]"));
                }

                var lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                for (var i = 1; i < lines.Length; i++)
                {
                    var columns = lines[i].Split(',').Select(j => j.Trim()).Select(j => j.Replace("[comma]", ",").Replace("[newline]", "\n").Replace("[quotes]", "\"")).ToList();
                    var key = columns[0];
                    
                    if (key == "") continue;

                    if(Dictionary.ContainsKey(key))
                        Debug.LogWarning($"[LocalizationManager::Read] key {key} already exists.");
                    
                    Dictionary.Add(key, columns[langIndex]);
                    
                }
            }

            for (int i = 0; i < textAssets.Length; i++)
            {
                Resources.UnloadAsset(textAssets[i]);
            }

            return Dictionary;
            /* string path = _pathProvider.GetPath(language, CSV_EXTENSION);
             if (!File.Exists(path))
                 throw new ArgumentException($"Language file {language.ToString()} by path {path} not found");
             Dictionary<string, string> _localization = new Dictionary<string, string>();
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
 
             return _localization;*/
        }
        
        private string ReplaceMarkers(string text)
        {
            return text.Replace("[Newline]", "\n");
        }


    }
}