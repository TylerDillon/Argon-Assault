using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    [Tooltip("in ms^-1")][SerializeField] float xSpeed = 20f;
    [Tooltip("in ms")] [SerializeField]float xRange = 10f;
    [Tooltip("in ms^-1")] [SerializeField] float ySpeed = 20f;
    [Tooltip("in ms")] [SerializeField] float yRange = 6f;

    [SerializeField] float positionPitchFactor = -3f;
    [SerializeField] float positionYawFactor = 3.5f;

    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float controlYawFactor = 15f;
    [SerializeField] float controlRollFactor = -20f;
    float xThrow;
    float yThrow;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update() {
        ProcessTranslation();
        ProcessRotation();

    }

    private void ProcessRotation() {
        //pitch movement
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        //yaw movement
        float yawDueToPosition = transform.localPosition.x * positionYawFactor;
        float yawDueToControlThrow = xThrow * controlYawFactor;
        float yaw = yawDueToPosition + yawDueToControlThrow;

        float roll = xThrow * controlRollFactor;


        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation() {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float rawNewXPos = transform.localPosition.x + xOffset;
        float xPos = Mathf.Clamp(rawNewXPos, -xRange, xRange);

        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * ySpeed * Time.deltaTime;
        float rawNewYPos = transform.localPosition.y + yOffset;
        float yPos = Mathf.Clamp(rawNewYPos, -yRange, yRange + 2);

        transform.localPosition = new Vector3(xPos, yPos, transform.localPosition.z);
    }
}
