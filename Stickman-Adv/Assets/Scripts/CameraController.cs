using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player;
    public float SlideEffectFactor;
    public Vector3 SlideFactor;

    public Vector3 LowerBoundLimit;
    public Vector3 UpperBoundLimit;

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer() {
        Vector3 cameraPosition = transform.position;
        Vector3 playerPosition = Player.position;

        playerPosition.x += SlideFactor.x;
        playerPosition.y += SlideFactor.y;
        playerPosition.z += transform.position.z;
        
        playerPosition.x = Mathf.Clamp(playerPosition.x, LowerBoundLimit.x, UpperBoundLimit.x);
        playerPosition.y = Mathf.Clamp(playerPosition.y, LowerBoundLimit.y, UpperBoundLimit.y);

        transform.position = Vector3.Lerp(cameraPosition, playerPosition, 1f - Mathf.Pow(1f - SlideEffectFactor, Time.deltaTime * 30));
    }
}
