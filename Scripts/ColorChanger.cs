using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ColorChanger : MonoBehaviour {
    [SerializeField] private SpriteRenderer[] plateSprites;
    [SerializeField] private SpriteRenderer lightColor;
    [SerializeField] private SpriteRenderer darkColor;

    public void TwoColors() {
        for (int i = 0; i < plateSprites.Length; i++) {
            if (i % 2 == 0) {
                plateSprites[i].color = lightColor.color;
            }
            else {
                plateSprites[i].color = darkColor.color;
            }
        }
    }

    public void OneColorLight() {
        for (int i = 0; i < plateSprites.Length; i++) {
            plateSprites[i].color = lightColor.color;
        }
    }

    public void OneColorDark() {
        for (int i = 0; i < plateSprites.Length; i++) {
            plateSprites[i].color = darkColor.color;
        }
    }
}
