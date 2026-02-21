using UnityEngine;

public class MoverEnemy : MonoBehaviour
{
    public void Move(Vector3 targetPosition, float speed, float rotationSpeed)
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            Time.deltaTime * rotationSpeed 
        );

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
