using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class GameManager : Singleton<GameManager>
{
    public readonly string[] acceptablePhrases = { "alucard", "socorram me subi no onibus em marrocos" };
    public string jsonFolder;
    public string fileName;
    public string reversedText;

    protected override void Awake()
    {
        base.Awake();
        jsonFolder = $"{Application.dataPath}/JsonFiles/";
    }

    public string GetReversedTextRecursively (string text, int index = 0)
    {
        if (!string.IsNullOrEmpty(reversedText) && reversedText.Length == text.Length)
            return reversedText;
        reversedText += text[text.Length - 1 - index];
        index++;
        return GetReversedTextRecursively(text, index);
    }

    public void CreateJsonFile (string reversedText)
    {
        StringBuilder stringBuilder = new StringBuilder();
        string json = string.Empty;

        JsonObject jsonObject = new JsonObject
        {
            reversedText = reversedText
        };
        stringBuilder.Append(JsonUtility.ToJson(jsonObject, true));

        json = stringBuilder.ToString();
        File.WriteAllText($"{jsonFolder}/{fileName}.json", json);

        Debug.Log("File created!");
    }
}
