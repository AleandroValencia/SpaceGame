using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingTextScript : MonoBehaviour {

    public string[] script;
    public Text textBox;
    public GameObject uiImage;
    public GameObject panel;

    private int whichLine = 0;
    private int charIndex = 0;

	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            if (whichLine < script.Length - 1)
            {
                whichLine++;
                charIndex = 0;
                textBox.text = "";
            }
            else
            {
                textBox.gameObject.SetActive(false);
                uiImage.SetActive(false);
                panel.SetActive(false);
            }
        }

        if (whichLine < script.Length)
        {
            ScrollText(script[whichLine]);
        }
	}

    void ScrollText(string _line)
    {
        if (charIndex < _line.Length)
        {
            textBox.text += _line[charIndex];
            charIndex++;
        }
    }
}
