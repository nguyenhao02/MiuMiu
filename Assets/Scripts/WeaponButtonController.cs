using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{   
    [SerializeField] private GameObject shootLocation; // Object bạn muốn xoay
    [SerializeField] private GameObject player;
    private bool isDragging = false;
    private Vector2 lastMousePosition;
    private Vector3 mousePosition;
    private PlayerController playerController;
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        mousePosition = Input.mousePosition;

        if (isDragging)
        {
            Vector2 currentMousePosition = mousePosition;
            Vector2 direction = currentMousePosition - lastMousePosition;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            if(!playerController.IsFacingRight())
            {
                angle = 180 + angle;
                if(angle == 180)
                {
                   angle = 0;
                }
            }
            shootLocation.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
           //lastMousePosition = currentMousePosition;
            
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Kiểm tra nếu nhấn chuột trái
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isDragging = true;
            shootLocation.gameObject.SetActive(true);
            lastMousePosition = Input.mousePosition;
        }    
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isDragging = false;
            PlayerWeaponController playerWeaponController = player.GetComponent<PlayerWeaponController>();
            playerWeaponController.ShootWeapon();
            shootLocation.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            shootLocation.gameObject.SetActive(false);
        }
    }
    
    // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    // Plane plane = new Plane(Vector3.forward, 0);

    // float distance;
    // if (plane.Raycast(ray, out distance))
    // {
    //     Vector3 worldPosition = ray.GetPoint(distance);
    //     return worldPosition;
    // }

    // return Vector3.zero;   
    
}
