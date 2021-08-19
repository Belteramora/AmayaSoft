using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SearchObjectsData", menuName = "Data/SearchObjectsData")]
public class SearchObjectsData : ScriptableObject
{
	public List<SearchObject> searchObjects;

	[System.Serializable]
	public class SearchObject
	{
		public string name;
		public Sprite sprite;
	}
}

