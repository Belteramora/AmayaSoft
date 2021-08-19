using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Level : MonoBehaviour
{
	[SerializeField] private GameObject levelItemPrefab;

	[SerializeField] private UnityEvent<string> onSearchObjectDeterminate;

	private string searchObjectName;

	private float itemWidth;
	private float itemHeight;

	private UnityEvent onLevelComplited = new UnityEvent();

	private List<string> usedObjectives = new List<string>();
	private List<SearchObjectsData.SearchObject> unusedObjects = new List<SearchObjectsData.SearchObject>();
	private List<LevelItem> levelItems = new List<LevelItem>();

	private void Awake()
	{
		SpriteRenderer itemRend = levelItemPrefab.GetComponent<SpriteRenderer>();
		itemWidth = itemRend.bounds.size.x;
		itemHeight = itemRend.bounds.size.y;
	}

	public void AttachListener(UnityAction action)
	{
		onLevelComplited.AddListener(action);
	}

	public void InitializeLevel(int currentLevel,SearchObjectsData objectsData)
	{
		Clear();

		unusedObjects.AddRange(objectsData.searchObjects);

		for(int i = 0; i < currentLevel * 3 + 3; i++)
		{
			InstantiateLevelItem(i, objectsData);
		}

		if (currentLevel == 0)
		{
			levelItems.ForEach(x => x.transform.DOScale(1.5f, 0.4f).SetEase(Ease.InBounce).SetLoops(2, LoopType.Yoyo));
		}

		ChooseObjective();
	}

	private void InstantiateLevelItem(int num, SearchObjectsData objectsData)
	{
		GameObject go = Instantiate(levelItemPrefab, transform);
		go.transform.localPosition = new Vector2((num % 3 * itemWidth) - itemWidth, (-(num / 3) * itemHeight) + itemHeight);

		LevelItem itemScript = go.GetComponent<LevelItem>();

		int randomIndex = Random.Range(0, unusedObjects.Count);

		SearchObjectsData.SearchObject randomObject = unusedObjects[randomIndex];
		unusedObjects.Remove(randomObject);

		itemScript.Initialize(randomObject, LevelCompleted);
		levelItems.Add(itemScript);
	}

	private void LevelCompleted()
	{
		levelItems.ForEach(x => x.SetUsable(false));

		onLevelComplited.Invoke();
	}

	private void ChooseObjective()
	{
		LevelItem rightObject;

		while (true)
		{
			rightObject = levelItems[Random.Range(0, levelItems.Count)];
			searchObjectName = rightObject.name;

			if (!usedObjectives.Contains(searchObjectName))
			{
				usedObjectives.Add(searchObjectName);
				rightObject.SetIsRight(true);
				onSearchObjectDeterminate.Invoke("Find " + searchObjectName);
				return;
			}
		}		
	}

	private void Clear()
	{
		levelItems.ForEach(x => Destroy(x.gameObject, 0.01f));
		levelItems.Clear();
		unusedObjects.Clear();
	}
}
