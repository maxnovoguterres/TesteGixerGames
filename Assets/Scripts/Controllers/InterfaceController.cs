using System;
using System.IO;
using System.Linq;
using System.Text;
using TMPro;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{
    private string currentText = string.Empty;
    private TMP_InputField inputField;
    private Button okButton;

    void Awake()
    {
        inputField = transform.FindRecursively("InputField").GetComponent<TMP_InputField>();
        okButton = transform.FindRecursively("OkButton").GetComponent<Button>();
    }

    void Start()
    {
        inputField.onValueChanged.AddListener(OnValueChangedText);
        okButton.onClick.AddListener(OnClickOkButton);
        okButton.interactable = false;
    }

    private void OnValueChangedText(string text)
    {
        foreach (string acceptablePhrase in GameManager.Instance.acceptablePhrases)
        {
            if (text == acceptablePhrase)
            {
                okButton.interactable = true;
                break;
            }
            okButton.interactable = false;
        }

        if (text.Length < currentText.Length)
        {
            string newText = string.Empty;
            for (int i = 0; i < currentText.Length; i++)
            {
                if (i == currentText.Length - 1 || text[i] != currentText[i])
                {
                    currentText = newText;
                    inputField.text = newText;
                    return;
                }
                newText += text[i];
            }
        }

        foreach (string acceptablePhrase in GameManager.Instance.acceptablePhrases)
        {
            if (text.Length > 0 && text[0] == acceptablePhrase[0])
            {
                if (text.Length > acceptablePhrase.Length || text.Last() != acceptablePhrase[text.Length - 1])
                    inputField.text = currentText;
                else
                    currentText = text;
                break;
            }
        }

        if (string.IsNullOrEmpty(currentText))
            inputField.text = string.Empty;
    }

    private void OnClickOkButton()
    {
        GameManager.Instance.fileName = currentText;
        
        GameManager.Instance.CreateJsonFile(GameManager.Instance.GetReversedTextRecursively(currentText));

        SceneManager.LoadScene((int)SceneType.RESULTADO);
    }
}
