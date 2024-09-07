using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemWeaponController : MonoBehaviour
{
    [SerializeField] private float floatSpeed = 0.4f; // Tốc độ di chuyển lên xuống
    [SerializeField] private float floatHeight = 0.4f; // Biên độ di chuyển (cự ly từ vị trí ban đầu)
    [SerializeField] private RectTransform weaponImageTras; // Hình ảnh con cá trên Canvas

    [SerializeField] private Vector3 enlargedSize = new Vector3(2f, 2f, 1f); // Kích thước phóng to
    private Vector3 normalSize; 
    private Vector3 weaponPosition;
    private Vector3 startPosition;
    private bool isMoving = false; // Kiểm soát việc di chuyển đối tượng

    void Start()
    {
        GameObject weaponImage = GameObject.Find("WeaponImage");
        weaponImageTras = weaponImage.GetComponent<RectTransform>();

        startPosition = transform.position; // Lưu vị trí ban đầu
        normalSize = weaponImageTras.localScale; 
    }

    void Update()
    {
        if (!isMoving)
        {
            MotionEffect();
        }
        else 
        {
            transform.position = Vector3.MoveTowards(transform.position, weaponPosition, 15 * Time.deltaTime);
            // Thay đổi kích thước hình ảnh con cá khi đối tượng di chuyển đến gần
            float distance = Vector3.Distance(transform.position, weaponPosition);
            float maxDistance = 5f; // Tùy chỉnh khoảng cách tối đa để phóng to

            if (distance < maxDistance)
            {
                float scaleFactor = Mathf.Lerp(1f, enlargedSize.x, 1 - (distance / maxDistance));
                weaponImageTras.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
            }

            if (transform.position == weaponPosition)
            {
                DataManager.Instance.AddPlayerWeapon(1);
                weaponImageTras.localScale = normalSize;
                Destroy(gameObject);
            }
        }

        weaponPosition = GetWeaponPosition();
        
    }

    private void MotionEffect()
    {
        // Tạo hiệu ứng lơ lửng lên xuống
        float newY = startPosition.y + Mathf.PingPong(Time.time * floatSpeed, floatHeight);
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            isMoving = true;

            SoundManager.Instance.PlaySFX(SoundManager.Instance.item);
        }
    }

    private Vector3 GetWeaponPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(weaponImageTras.position);
        Plane plane = new Plane(Vector3.forward, 0);

        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 worldPosition = ray.GetPoint(distance);
            return worldPosition;
        }

        return Vector3.zero;   
    }
}
