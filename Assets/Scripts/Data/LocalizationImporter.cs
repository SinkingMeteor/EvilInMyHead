using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace Sheldier.Data
{
	
	public class LocalizationImporter : SerializedMonoBehaviour
	{

		[FolderPath] public string SaveFolder;

		private const string UrlPattern = "https://docs.google.com/spreadsheets/d/{0}/export?format=csv&gid={1}";
		
		[OdinSerialize] private Dictionary<string, string> listCollection;
		[SerializeField] private string TableID;

		[Button]
		public void LoadAll()
		{
			StopAllCoroutines();
			StartCoroutine(SyncCoroutine());
		}

		private IEnumerator SyncCoroutine()
		{
			var dict = new Dictionary<string, UnityWebRequest>();

			foreach (var sheet in listCollection)
			{
				var url = string.Format(UrlPattern, TableID, sheet.Value);

				Debug.LogFormat("Downloading: {0}...", url);

				dict.Add(url, UnityWebRequest.Get(url));
			}

			foreach (var entry in dict)
            {
                var url = entry.Key;
                var request = entry.Value;

				if (!request.isDone)
				{
					yield return request.Send();
				}

				if (request.error == null)
				{
					var sheet = listCollection.Single(i => url == string.Format(UrlPattern, TableID, i.Value));
					var path = System.IO.Path.Combine(SaveFolder, sheet.Key + ".csv");

					System.IO.File.WriteAllBytes(path, request.downloadHandler.data);
					Debug.LogFormat("Sheet {0} downloaded to {1}", sheet.Value, path);
				}
            }

			Debug.Log("<color=green>Localization successfully loaded!</color>");
		}
	}
}
