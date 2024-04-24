using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChipItemUI : MonoBehaviour
{
    private Image _image;
    private TextMeshProUGUI _text;
    private void Start()
    {
        _image = transform.GetChild(0).gameObject.GetComponent<Image>();
        _text = transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        StartCoroutine(FadeOut());
    }
    IEnumerator FadeOut()
    {
        WaitForSeconds waitTime = new WaitForSeconds(0.1f);
        Color imgColor = _image.color;
        Color textColor = _text.color;
        for (float i = 1; i > 0; i -= 0.03f)
        {

            imgColor.a -= 0.03f;
            textColor.a -= 0.03f;
            _image.color = imgColor;
            _text.color = textColor;

            yield return waitTime;
        }
        Destroy(gameObject);
    }
}
