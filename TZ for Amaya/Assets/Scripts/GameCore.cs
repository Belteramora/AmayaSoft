using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameCore : MonoBehaviour
{
	[SerializeField] private List<SearchObjectsData> objectsData;

	[SerializeField] private int countOfLevels;

	[SerializeField] private Level level;

	[SerializeField] private CanvasGroup canvasGroup;

	[SerializeField] private Image fade;

	public int currentLevel = 0;

	public static GameCore Instance;

	//private Sequence restartSequence;

    // Start is called before the first frame update
    void Start()
    {
		Instance = this;
		level.AttachListener(NextLevel);
		BeginGame();
    }

	public void ResetGame()
	{
		canvasGroup.interactable = false;
		canvasGroup.blocksRaycasts = false;
		currentLevel = 0;

		Sequence restartSequence = DOTween.Sequence();

		restartSequence.Append(fade.DOFade(1, 0.5f).OnComplete(BeginGame));
		restartSequence.Append(canvasGroup.DOFade(0, 0.2f));
		restartSequence.Append(fade.DOFade(0, 0.5f));

		restartSequence.Play();
	}

	public void BeginGame()
	{
		SearchObjectsData randomData = objectsData[Random.Range(0, objectsData.Count)];
		level.InitializeLevel(currentLevel, randomData);
	}

    public void NextLevel()
	{
		currentLevel++;

		if(currentLevel >= countOfLevels)
		{
			ShowRestartButton();
			return;
		}
		BeginGame();
	}

	private void ShowRestartButton()
	{
		canvasGroup.interactable = true;
		canvasGroup.blocksRaycasts = true;
		canvasGroup.DOFade(1f, 0.5f);
	}

	
}
