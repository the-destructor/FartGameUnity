using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LockAndKey : MonoBehaviour
{

    public GameObject LockedDoor;
    public GameObject DoorSymbol;
    public SpriteRenderer LockSymbol;
    public Light2D light;

    void Unlock()
    {
        BoxCollider2D DoorCollider = LockedDoor.GetComponent<BoxCollider2D>();
        DoorCollider.enabled = false;
        SpriteRenderer DoorSprite = LockedDoor.GetComponent<SpriteRenderer>();
        DoorSprite.color = new Color32(62, 51, 77, 255);
        DoorSprite.sortingOrder = -4;
        LockSymbol.sortingOrder = -3;
        DoorSymbol.SetActive(false);
        light.intensity = 0f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Unlock();
        }
    }
}
