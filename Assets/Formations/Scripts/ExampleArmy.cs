using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExampleArmy : MonoBehaviour {
    private FormationBase _formation;

    public FormationBase Formation {
        get {
            if (_formation == null) _formation = GetComponent<FormationBase>();
            return _formation;
        }
        set => _formation = value;
    }

    [SerializeField] private GameObject _unitPrefab;

    [SerializeField] private float _unitSpeed = 2;

    [SerializeField] private  List<GameObject> _spawnedUnits = new List<GameObject>();
    private List<Vector3> _points = new List<Vector3>();
    private Transform _parent;
    /// <summary>
    /// ////////////////
    /// 
    /// </summary>
    /// 
    private List<Transform> HidingSpots = new List<Transform>();
    private bool runOnce;
    private bool SetCoroutingOneTime;

    public static ExampleArmy init;
    private void Awake() {
        init = this;
        _parent = this.gameObject.transform;  //new GameObject("Unit Parent").transform;
    }

    private void Start()
    {
        HidingSpots = HidePos.init.HidePosition;
       
    }
    private void Update() {


        SetFormation();
    }


    private void SetFormation() {
        _points = Formation.EvaluatePoints().ToList();

        if (_points.Count > _spawnedUnits.Count) {
            var remainingPoints = _points.Skip(_spawnedUnits.Count);
            Spawn(remainingPoints);
        }
        else if (_points.Count < _spawnedUnits.Count) {
            Kill(_spawnedUnits.Count - _points.Count);
        }

        for (var i = 0; i < _spawnedUnits.Count; i++) {
            _spawnedUnits[i].transform.position = Vector3.MoveTowards(_spawnedUnits[i].transform.position, transform.position + _points[i], _unitSpeed * Time.deltaTime);
            if (i==_spawnedUnits.Count-1)
            {
                GetComponent<AiDisMaker>().enabled = true;
                GetComponent<AiDisMaker>().Agents = _spawnedUnits;
                this.enabled = false;
            }
    
        }
    }

    private void Spawn(IEnumerable<Vector3> points) {
        foreach (var pos in points) {
            var unit = Instantiate(_unitPrefab, transform.position + pos, Quaternion.identity, _parent);
            if (GameManager.init.PlayerHide==null)
            {
               
                unit.AddComponent<PlayerController>();
                unit.AddComponent<CharacterController>();
                GameManager.init.PlayerHide = unit;
            }
            int randomIndex = Random.Range(0, HidingSpots.Count);
          
            unit.GetComponent<AiHider>().Spot = HidingSpots[randomIndex];
            unit.GetComponent<AiHider>().StartMoving = true;
       
            HidingSpots.RemoveAt(randomIndex);
            _spawnedUnits.Add(unit);
            
        }
    }

    private void Kill(int num) {
        for (var i = 0; i < num; i++) {
            var unit = _spawnedUnits.Last();
            _spawnedUnits.Remove(unit);
            Destroy(unit.gameObject);
        }
    }


}