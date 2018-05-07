using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SheepHerdController : MonoBehaviour
{

    [SerializeField] private float _frequency;
    [SerializeField] private bool _randomize;
    [SerializeField] public float MaxRand,MinRand;


    [Header("sheep object")]
    [SerializeField]
    private GameObject _sheep;

    private float _timer;

    private List<SheepController> _sheeps;
	// Use this for initialization
	void Start () {
		_sheeps = new List<SheepController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _timer += Time.deltaTime;
	    if (_randomize)
	    {
	        if (_timer >= 1f / _frequency + Random.Range(MinRand, MaxRand))
	        {
	            _sheeps.Add(Instantiate(_sheep,transform.position,transform.rotation).gameObject.GetComponent<SheepController>());
	            _timer = 0;
	            if (_sheeps.Count == 1) _sheeps[0].Active = true;
            }
	    }
	    else
	    {
	        if (_timer >= 1f / _frequency)
	        {
	            _sheeps.Add(Instantiate(_sheep, transform.position, transform.rotation).gameObject.GetComponent<SheepController>());
	            _timer = 0;
	            if (_sheeps.Count == 1) _sheeps[0].Active = true;
	        }
        }

	    if (_sheeps.Count>0&&!_sheeps[0].Active)
	    {
	        _sheeps.Remove(_sheeps[0]);
	        if (_sheeps.Count > 0)
	        {
	            _sheeps[0].Active = true;
	        }
	    }
	}

    public bool Randomize
    {
        get { return _randomize; }
        set { _randomize = value; }
    }

    public float Frequency
    {
        get { return _frequency; }
        set { _frequency = value; }
    }

    public GameObject Sheep
    {
        get { return _sheep; }
        set { _sheep = value; }
    }
}

[CustomEditor(typeof(SheepHerdController))]
public class SheepHerdEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var herd = target as SheepHerdController;
        EditorGUILayout.LabelField("Sheep gameobject");
        herd.Sheep = EditorGUILayout.ObjectField(herd.Sheep, typeof(Object), true) as GameObject;
        herd.Frequency = EditorGUILayout.FloatField("Frequency",herd.Frequency);
        herd.Randomize = GUILayout.Toggle(herd.Randomize, "Randomize");
        if (herd.Randomize)
        {
            EditorGUILayout.LabelField("min rand: ",herd.MinRand.ToString());
            EditorGUILayout.LabelField("max rand: ",herd.MaxRand.ToString());
            EditorGUILayout.MinMaxSlider(ref herd.MinRand, ref herd.MaxRand, -1, 1);
        }
    }
}
