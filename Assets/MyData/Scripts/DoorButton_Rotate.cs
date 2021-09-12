using UnityEngine;

public class DoorButton_Rotate : MonoBehaviour
{
    [SerializeField] private GameObject door;
    Transform text;
    private bool _pressButton;
    private bool _boolSwtich = true;

    private void Start()
    {
        text = transform.GetChild(1);
        text.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _pressButton = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        text.gameObject.SetActive(true);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_pressButton)
            {
                door.GetComponent<DoorRotate>().Initialization(_boolSwtich);
                _pressButton = false;
                _boolSwtich = _boolSwtich ? false : true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        text.gameObject.SetActive(false);
    }
}
