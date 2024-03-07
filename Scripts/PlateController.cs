using UnityEngine;

public class PlateController : MonoBehaviour {

    [SerializeField] private GameObject holeVisual;
    [SerializeField] private GameObject positionVisual;
    [SerializeField] private Plate plateState;

    private Vector3 target;
    private bool canMove = false;
    private float stablePositionY = 4f;
    private float smoothness = 25f;

    private void Update() {
        if (canMove) {
            if (transform.position == target) {
                canMove = false;
            }
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * smoothness);

        }
    }

    public void Idle() {
        positionVisual.SetActive(false);
        holeVisual.SetActive(false);
    }

    public void IdleOnTop() {
        positionVisual.SetActive(true);
        holeVisual.SetActive(true);
    }

    public void Active() {
        //position change -> grab
        MoveToTargetSmooth(new Vector3(transform.position.x, stablePositionY, 0));
        //gameObject.transform.position = new Vector3 (transform.position.x, stablePositionY, 0);
        positionVisual.SetActive(false);
    }

    public void Drop(Vector3 transformFirst) {
        IdleOnTop();
        MoveToTargetSmooth(transformFirst);

        //gameObject.transform.position = transform;
    }

    public void MoveToTargetSmooth(Vector3 target) {
        canMove = true;//bool because smooth movement performed in Update that 
        this.target = target;
    }

    public void MoveToTargetSmooth(Vector3 target, Vector3 targetOptional) {
        canMove = true;//bool because smooth movement performed in Update that 
        this.target = target;
        //this.targetOptional = targetOptional;
    }

    public Plate GetPlateState() {
        return plateState;
    }
}
