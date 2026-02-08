using UnityEngine;

public class MoverEnemy : MonoBehaviour
{
    public void Move(Vector3 direction, float speed, float rotationSpeed)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );

        transform.position += speed * direction * Time.deltaTime;
    }
}
