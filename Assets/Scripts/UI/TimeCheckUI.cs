using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeCheckUI : MonoBehaviour
{

    [SerializeField] 
    private TextMeshProUGUI _text;
    // Start is called before the first frame update
    void Start()
    {
        if (_text == null)
        {
            _text = this.gameObject.FindChild("TimeText").GetComponent<TextMeshProUGUI>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = ((int)Managers.GameTime).ToString();
    }
}
