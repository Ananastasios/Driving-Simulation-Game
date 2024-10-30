using UnityEngine;
using TMPro;

public class CarHUDController : MonoBehaviour
{
    [SerializeField] private GameObject car;

    [Header("Speedometer")]
    [SerializeField] private RectTransform needle;

    [SerializeField] private TextMeshProUGUI speedText;

    private float needleStartPos = 220f; 
    private float needleEndPos = -42f;
    
    private float needleDesiredPos;

    [Header("Sounds")]
    [SerializeField] private AudioSource engineAudio;

    [Header("References")]
    [SerializeField] private CarController carController;

    private void Update()
    {
        HandleSpeedometer(); 
        HandleAudio();       
    }

    // Method to update the speedometer UI
    private void HandleSpeedometer() 
    {
        int speed = carController.Speed;  

        // Update the speed text UI element with the current speed
        speedText.text = speed.ToString();

        // Calculate the desired needle position based on the current speed
        needleDesiredPos = needleStartPos - needleEndPos; // Range for needle movement
        float temp = (float)speed / 180; // Normalize speed to a 0-1 range (assuming 180 is the max speed)
        
        // Set the needle rotation based on the current speed
        needle.transform.eulerAngles = new Vector3(0, 0, (needleStartPos - temp * needleDesiredPos));
    }

    // Method to update engine audio based on speed
    private void HandleAudio()
    {
        int speed = carController.Speed;  

        if (speed > 0) 
        {
            // Normalize the speed for audio pitch adjustment based on max forward speed
            float normalizedSpeed = Mathf.InverseLerp(0, carController.GetMaxForwardSpeed, speed);  
            
            // Adjust the engine audio pitch based on normalized speed
            engineAudio.pitch = Mathf.Lerp(1f, 2f, normalizedSpeed); // Change pitch from 1 to 2
        }
        else
        {
            engineAudio.pitch = 1f; // Reset pitch to idle
        }
    }

    // Method to reset the car position
    public void OnResetButtonPressed()
    {
        if (car != null)
        {
            car.transform.position = new Vector3(0, 0, 0);
            car.transform.rotation = Quaternion.identity;
        }
    }
    
        public void OnExitButtonPressed()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
