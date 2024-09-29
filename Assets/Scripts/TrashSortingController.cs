using UnityEngine;

public class TrashSortingController : MonoBehaviour
{
    public static TrashSortingController Instance { get; private set; }
    public int Points;

    public GameObject CurrentTrash;
    private GameObject CurrentTrashBag;

    private Camera _mainCamera;
    private bool _isSortTable;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        Points = 0;
        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (CurrentTrash)
        {
            if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                if (!hit.collider.CompareTag("TrashBag"))
                {
                    if (hit.collider.CompareTag("SortTable"))
                        _isSortTable = true;
                    else
                        _isSortTable = false;
                    CurrentTrash.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                    CurrentTrashBag = null;
                }
                else
                {
                    CurrentTrashBag = hit.collider.gameObject;
                    CurrentTrash.transform.position = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y + 1, hit.collider.transform.position.z);
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!CurrentTrash)
            {
                if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
                {
                    if (!hit.collider.CompareTag("Trash"))
                        return;

                    CurrentTrash = hit.collider.gameObject;
                    CurrentTrash.GetComponent<Rigidbody>().useGravity = false;
                    CurrentTrash.layer = 2;
                }
            }
            else
            {
                if (CurrentTrashBag)
                {
                    if (CurrentTrashBag.GetComponent<TrashBagInfo>().TrashCatygory == CurrentTrash.GetComponent<TrashInfo>().TrashCatygory)
                        Points += 10;
                    else
                        Points -= 10;
                    CurrentTrash.GetComponent<Rigidbody>().useGravity = true;
                    CurrentTrash.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    Destroy(CurrentTrash, 0.75f);
                    CurrentTrash = null;
                    TrashSpawner.Instance.SpawnRandomTrash();
                }
                else if (_isSortTable)
                {
                    CurrentTrash.GetComponent<Rigidbody>().useGravity = true;
                    CurrentTrash.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    CurrentTrash.layer = 6;
                    CurrentTrash = null;
                }
                else
                {
                    Points -= 5;
                    CurrentTrash.GetComponent<Rigidbody>().useGravity = true;
                    CurrentTrash.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    Destroy(CurrentTrash, 0.4f);
                    CurrentTrash = null;
                    TrashSpawner.Instance.SpawnRandomTrash();
                }
            }
        }
    }
}
