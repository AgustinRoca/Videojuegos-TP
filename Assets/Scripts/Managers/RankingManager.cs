using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    [SerializeField] private GameObject _elementPrefab;
    [SerializeField] private Transform _elementsGridTransform;
	[SerializeField] private InputField inputName;
	[SerializeField] private Button submitScoreButton;

    private List<RankingModel> _elements = new List<RankingModel>();
    private List<GameObject> _recordsOnScreen = new List<GameObject>();

	private Database _db;

    private void Start()
    {
		_db = new Database();

		GetAllRecordsAndShowThem();
    }

	public void GetAllRecordsAndShowThem() 
	{
		_elements = _db.GetAllRecords();

        _elements.Sort((element1, element2) => element2.Score.CompareTo(element1.Score));

		foreach (var record in _recordsOnScreen)
		{
			Destroy(record);
		}
		_recordsOnScreen = new List<GameObject>();

        foreach (var element in _elements)
        {
            var rankingElement = Instantiate(_elementPrefab, _elementsGridTransform);
			_recordsOnScreen.Add(rankingElement);
			RankingElement re = rankingElement.GetComponent<RankingElement>();
            re.SetModel(element.Name, element.Score);
			re.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 150);
        }
	}

	public void SubmitPlayerScore() 
	{
		if(inputName.text.Length > 0) {
			int score = GlobalData.instance.Score;

			RankingModel model = new RankingModel(inputName.text, score);
        	_db.AddRankingRecord(model);

			inputName.text = "";
			submitScoreButton.interactable = false;

			GetAllRecordsAndShowThem();
		}
	}
}
