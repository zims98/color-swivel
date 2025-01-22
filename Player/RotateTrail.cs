using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTrail : MonoBehaviour
{
    public PlayerBox playerScript;

    public TrailRenderer trailTopRight, trailBottomRight, trailBottomLeft, trailTopLeft;

    void Update()
    {
        if (playerScript.direction == 1 || playerScript.direction == 4) // Currently rotating right
        {
            trailTopRight.startColor = new Color(0.2627451f, 0.9843138f, 0.07450981f, 0.5f); // Green
            trailTopRight.endColor = new Color(0.2627451f, 0.9843138f, 0.07450981f, 0f); // Green

            trailBottomRight.startColor = new Color(0.5372549f, 0.1372549f, 0.9450981f, 0.5f); // Purple
            trailBottomRight.endColor = new Color(0.5372549f, 0.1372549f, 0.9450981f, 0f); // Purple

            trailBottomLeft.startColor = new Color(0.2470588f, 0.8862746f, 0.9450981f, 0.5f); // Cyan
            trailBottomLeft.endColor = new Color(0.2470588f, 0.8862746f, 0.9450981f, 0f); // Cyan

            trailTopLeft.startColor = new Color(0.9843138f, 0.3529412f, 0.07450981f, 0.5f); // Orange
            trailTopLeft.endColor = new Color(0.9843138f, 0.3529412f, 0.07450981f, 0f); // Orange
        }
        else if (playerScript.direction == 2 || playerScript.direction == 3)
        {
            trailTopRight.startColor = new Color(0.5372549f, 0.1372549f, 0.9450981f, 0.5f); // Purple
            trailTopRight.endColor = new Color(0.5372549f, 0.1372549f, 0.9450981f, 0f); // Purple

            trailBottomRight.startColor = new Color(0.2470588f, 0.8862746f, 0.9450981f, 0.5f); // Cyan
            trailBottomRight.endColor = new Color(0.2470588f, 0.8862746f, 0.9450981f, 0f); // Cyan

            trailBottomLeft.startColor = new Color(0.9843138f, 0.3529412f, 0.07450981f, 0.5f); // Orange
            trailBottomLeft.endColor = new Color(0.9843138f, 0.3529412f, 0.07450981f, 0f); // Orange

            trailTopLeft.startColor = new Color(0.2627451f, 0.9843138f, 0.07450981f, 0.5f); // Green
            trailTopLeft.endColor = new Color(0.2627451f, 0.9843138f, 0.07450981f, 0f); // Green
        }
    }
}
