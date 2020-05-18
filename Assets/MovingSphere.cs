using UnityEngine;

public class MovingSphere : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 10f;

    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 10f;

    [SerializeField]
    Rect allowedArea = new Rect(-5f, -5f, 10f, 10f);

    void Update() {
        //add a comment
        Vector3 velocity;
        Vector2 playerInput;

        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);

        velocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        Vector3 acceleration = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;

        Vector3 desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        float maxSpeedChange = maxAcceleration * Time.deltaTime;

        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

        velocity += acceleration * Time.deltaTime;
        Vector3 displacement = velocity * Time.deltaTime;
        
        Vector3 newPosition = transform.localPosition + displacement;

        // if (!allowedArea.Contains(new Vector2(newPosition.x, newPosition.z))) {
        //     newPosition.x = Mathf.Clamp(newPosition.x, allowedArea.xMin, allowedArea.xMax);
        //     newPosition.z = Mathf.Clamp(newPosition.z, allowedArea.yMin, allowedArea.yMax);
        // }

        if (newPosition.x < allowedArea.xMin) {
            newPosition.x = allowedArea.xMin;
            velocity.x = -velocity.x;
        }
        else if (newPosition.x > allowedArea.xMax) {
            newPosition.x = allowedArea.xMax;
            velocity.x = -velocity.x;
        }

        if (newPosition.z < allowedArea.yMin) {
            newPosition.z = allowedArea.yMin;
            velocity.z = -velocity.z;
        }
        else if (newPosition.z > allowedArea.yMax) {
            newPosition.z = allowedArea.yMax;
            velocity.z = -velocity.z;
        }

        transform.localPosition = newPosition;
    }

}
