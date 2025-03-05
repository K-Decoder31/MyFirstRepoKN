using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerPosition;
    public Vector3 targetPosition;
    public float followDelay;
    public float minYposition;
    public float initialYposition;

    // Viitan suoraan Namespace TMPro
    // TMPro.TextMeshPro jotainTextUI;
    // Tai laitta yl�s "using TMPro;" jollon voit k�ytt� sen ominaisuuksia
    // TextMeshPro m_jotainTextUI;

    private void Start()
    {
        if (playerPosition == null)
        {
            Debug.LogError("PlayerPosition not assigned");
        }
        initialYposition = transform.position.y; // Kamera ei menne ennem�n kun Groundi
    }

    private void LateUpdate() // Kalkuloidaan Updaten j�lkeen
    {
        if (playerPosition == null)
        {
            return;
        }

        targetPosition = new(playerPosition.position.x,
            playerPosition.position.y, transform.position.z);

        // Blokataan Cameran Y-positio tasolle
        if(targetPosition.y < minYposition) 
        {
            targetPosition.y = minYposition;
        }
        
        transform.position = Vector3.Lerp(transform.position, targetPosition,
            followDelay * Time.fixedDeltaTime);

        //transform.position = playerPosition.position; // returns player postion position

    }

}