using UnityEngine;

public class TrashKillZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Trash") && collision.gameObject != TrashSortingController.Instance.CurrentTrash)
        {
            Destroy(collision.gameObject, 0.4f);
            TrashSortingController.Instance.Points -= 5;
        }
    }
}
