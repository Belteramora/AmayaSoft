using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class ObjectiveText : MonoBehaviour
{
	private Text text;
    void Start()
    {
		text = GetComponent<Text>();
		text.DOFade(1, 0.5f);
    }
}
