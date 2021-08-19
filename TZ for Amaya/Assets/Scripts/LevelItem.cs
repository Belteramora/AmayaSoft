using UnityEngine;
using DG.Tweening;

public class LevelItem : MonoBehaviour
{
	private TweenCallback onRightObjectClicked;

	[SerializeField] private SpriteRenderer objectRenderer;
	[SerializeField] private ParticleSystem partSys;

	private bool isRight;
	private bool usable;

	public void Initialize(SearchObjectsData.SearchObject objectToFound, TweenCallback listener)
	{
		onRightObjectClicked = new TweenCallback(listener);
		objectRenderer.sprite = objectToFound.sprite;
		name = objectToFound.name;

		SetUsable(true);

		//Исключение для 7 и 8, так как спрайты повернуты на 90 градусов
		if(name == "7" || name == "8")
		{
			transform.Rotate(new Vector3(0, 0, -90));
		}
	}

	public void SetIsRight(bool isRight)
	{
		this.isRight = isRight;
	}

	public void SetUsable(bool usable)
	{
		this.usable = usable;
	}

	private void OnMouseDown()
	{
		if (usable)
		{
			if (isRight)
			{
				partSys.Play();
				objectRenderer.transform.DOScale(1.2f, 0.4f).SetEase(Ease.InBounce).SetLoops(2, LoopType.Yoyo).OnComplete(onRightObjectClicked);
			}
			else
			{
				objectRenderer.transform.DOMoveX(objectRenderer.transform.position.x + 0.05f, 0.1f).SetEase(Ease.InBounce).SetLoops(4, LoopType.Yoyo);
			}
		}
	}

}
