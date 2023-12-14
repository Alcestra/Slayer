using UnityEngine;

public class PlayerResetTrigger : MonoBehaviour
{
    public Transform player;
    public Transform resetPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.position = resetPosition.position;
        }
    }
}
