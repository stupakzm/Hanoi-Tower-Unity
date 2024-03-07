using System;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour {
    [SerializeField] PlateController[] plates = new PlateController[9];
    [SerializeField] Position startPos;
    [SerializeField] Position winPos;
    [SerializeField] private TextMeshProUGUI MovesMadeText;
    [SerializeField] private TextMeshProUGUI WinText;
    
    private float currentTime = 0f;
    private Position positionFrom = null;
    private PlateController plate = null;
    private int movesMade;
    public int platesCount = 9;
    private bool won;

    private void Start() {
        Reset();
    }

    private void Update() {
        //RaycastTouch();
        RaycastMousePosition();

        if (!won && movesMade > 0) {
            currentTime += 1 * Time.deltaTime;
            WinText.text = "Time : " + currentTime.ToString("#.0") + " seconds";
        }
        else {
        }
    }

    public void Reset() {
        won = false;
        //WinText.gameObject.SetActive(false);
        currentTime = 0f;
        WinText.text = "Time : ";
        movesMade = -1;
        MoveMade();
        for (int i = platesCount; i > 0; i--) {
            startPos.AddPlate(plates[i - 1]);
        }
    }

    private void RaycastTouch() {
        for (var i = 0; i < Input.touchCount; ++i) {
            RaycastHit hitInfo;
            Ray rayOrigin;
            if (Input.GetTouch(i).phase == UnityEngine.TouchPhase.Began) {

                // Construct a ray from the current touch coordinates
                rayOrigin = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                // Create a particle if hit
                if (Physics.Raycast(rayOrigin, out hitInfo)) {
                    // Debug.Log("Raycast hit object " + hitInfo.transform.name + " at the position of " + hitInfo.transform.position);

                    if (hitInfo.transform.gameObject.TryGetComponent(out Position position)) {
                        //Debug.Log("hiting position" + position);
                    }
                }
            }
        }
    }

    private void RaycastMousePosition() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null) {
                if (hit.collider.gameObject.TryGetComponent(out Position position)) {
                    positionFrom = position;
                    plate = position.TakePlate();
                }
            }
            else {
                plate = null;
            }
        }
        else if (Input.GetMouseButtonUp(0)) {
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            foreach (RaycastHit2D hit in hits) {
                if (hit.collider != null) {
                    if (hit.collider.gameObject.TryGetComponent(out Position position)) {
                        if (position.AddPlate(plate)) {
                            if (!positionFrom.Equals(position)) {
                                //added
                                MoveMade();
                                if (position.GetPositionState() == PositionState.Destination && position.platesCount == platesCount)
                                    ActivateWinText();
                                return;
                            }
                        }
                        else { break; }
                    }
                }
                else { break; }
            }

            // If no collider was hit or none of the hits were valid or plate == null, add plate to positionFrom
            positionFrom?.AddPlate(plate);
            plate = null;
        }
    }

    private void MoveMade() {
        MovesMadeText.text = "Moves made: " + ++movesMade;
    }

    public void ChangeHeight(int height) {
        platesCount = height;
        for (int i = 0; i < 9; i++) {
            if (i > platesCount - 1) {
                plates[i].gameObject.SetActive(false);
            }
            else {
                plates[i].gameObject.SetActive(true);
            }
        }
        Reset();
    }

    private void ActivateWinText() {
        won = true;
        //WinText.gameObject.SetActive(true);
        WinText.text = "You won in " + currentTime.ToString("#.0") + " seconds , Congratulations!";
    }
}
