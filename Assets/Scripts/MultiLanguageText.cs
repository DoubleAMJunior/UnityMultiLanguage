//created by AliAbbasiMoghaddam 2020
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MultiLanguageText : MonoBehaviour
{
    // label used to find the corresponding text value from a jason file 
    public string label;
    private Text _text;
    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    void OnEnable()
    {
        LanguageManager.onLanguageChanged += setup;
    }

    private void Start()
    {
        setup();
    }

    void OnDisable()
    {
        LanguageManager.onLanguageChanged -= setup;
    }

    public void setup()
    {
        _text.text=LanguageManager.Instance.getValue(label);
        Font f = LanguageManager.Instance.getFont();
        if (f!=null)
        {
            _text.font = f;
        }
        
    }
}
