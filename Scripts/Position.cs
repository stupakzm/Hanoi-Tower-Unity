using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{
    [SerializeField] private PositionState state;

    private float plateOffset = 0.6f;
    private float defaultOffset = 3.2f;
    private Vector3[] positionsForPlates;
    private Stack<PlateController> plateControllers;
    private PlateController plateOnTop;
    private float stablePositionY = 4f;
    public int platesCount { get; private set; }

    private void Awake() {
        SetPositions();
        plateControllers = new Stack<PlateController>();
    }

    private void Start() {
    }

    public bool AddPlate(PlateController plate) {
        if (plate != null) {
            if (plateControllers.Count == 0) {
                //add
                plateControllers.Push(plate);
                plateOnTop = plate;
                plateControllers.Peek().Drop(positionsForPlates[plateControllers.Count - 1]);
                platesCount = plateControllers.Count;
                return true;
            }
            else if (plate.GetPlateState() < plateControllers.Peek().GetPlateState()) {
                //add
                plateControllers.Peek().Idle();
                plateControllers.Push(plate);
                plateOnTop = plate;
                plateControllers.Peek().Drop(positionsForPlates[plateControllers.Count - 1]);
                platesCount = plateControllers.Count;
                return true;
            }
        }
        platesCount = plateControllers.Count;
        return false;
    }

    public PlateController TakePlate() {
        if (plateControllers.Count != 0) {
            plateControllers.Peek().Active();
            PlateController plateToReturn = plateControllers.Peek();
            plateControllers.Pop();
            if (plateControllers.Count != 0) {
                plateControllers.Peek().IdleOnTop();
                plateOnTop = plateControllers.Peek();
            }
            platesCount = plateControllers.Count;
            return plateToReturn;
            
        }
        platesCount = plateControllers.Count;
        return null;
    }

    private void SetPositions() {
        positionsForPlates = new Vector3[9];
        for (int i = 0; i < positionsForPlates.Length; i++) {
            positionsForPlates[i] = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + (plateOffset * (i+1)) - defaultOffset, 0);
        }
    }

    public void Reset() {
        plateControllers.Clear();
    }

    public PositionState GetPositionState() {
        return state;
    }
}
