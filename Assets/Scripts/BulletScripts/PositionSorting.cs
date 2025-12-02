using UnityEngine;

public class PositionSorting : MonoBehaviour
{
    [SerializeField] private float verticalChange;
    private void Start()
    {
        transform.position = new Vector3(transform.position.x, verticalChange, transform.position.z);
    }
}
