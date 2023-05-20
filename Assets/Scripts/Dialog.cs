using System.Collections;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float writingSpeed = 0.1f;
    
    private bool _isWriting;
    
    /// <summary>
    /// This method starts to write on the text.text property the textToWrite parameter.
    /// </summary>
    /// <param name="textToWrite">The text to write.</param>
    public void Write(string textToWrite)
    {
        StartCoroutine(WriteText(textToWrite));
    }
    
    /// <summary>
    /// This method starts to write on the text.text property the textToWrite parameter after a delay.
    /// </summary>
    /// <param name="textToWrite">The text to write.</param>
    /// <param name="delay">The delay.</param>
    public void DelayedWrite(string textToWrite, float delay)
    {
        StartCoroutine(DelayedWriteText(textToWrite, delay));
    }

    /// <summary>
    /// This method writes the textToWrite parameter on the text.text property.
    /// </summary>
    /// <param name="textToWrite">The text to write.</param>
    private IEnumerator WriteText(string textToWrite)
    {
        yield return new WaitUntil(() => !_isWriting);
        _isWriting = true;
        text.text = "";
        foreach (var letter in textToWrite)
        {
            text.text += letter;
            yield return new WaitForSeconds(writingSpeed);
        }
        _isWriting = false;
    }
    
    private IEnumerator DelayedWriteText(string textToWrite, float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(WriteText(textToWrite));
    }

}
