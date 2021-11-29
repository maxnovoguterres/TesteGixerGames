using UnityEngine;
using System.Collections;
using System.IO;
using TMPro;

public class ResultController : MonoBehaviour
{
    private TextMeshProUGUI resultText;

    void Awake ()
    {
        resultText = transform.FindRecursively("ResultText").GetComponent<TextMeshProUGUI>();
    }

    void Start ()
    {
        string file = $"{GameManager.Instance.jsonFolder}{GameManager.Instance.fileName}.json";
        if (File.Exists(file))
        {
            string result = File.ReadAllText(file);
            JsonObject jsonObject = JsonUtility.FromJson<JsonObject>(result);
            resultText.text = jsonObject.reversedText;
        }
        else
        {
            Debug.Log($"Didn't found file named:{file}");
        }
    }
}
