using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemFishController : MonoBehaviour
{
    [SerializeField] private float floatSpeed = 0.4f; // Tốc độ di chuyển lên xuống
    [SerializeField] private float floatHeight = 0.4f; // Biên độ di chuyển (cự ly từ vị trí ban đầu)
    [SerializeField] private RectTransform fishImageTras; // Hình ảnh con cá trên Canvas

    [SerializeField] private Vector3 enlargedSize = new Vector3(2f, 2f, 1f); // Kích thước phóng to
    private Vector3 normalSize; // Kích thước bình thường

    private Vector3 fishPosition;
    private Vector3 startPosition;
    private bool isMoving = false; // Kiểm soát việc di chuyển đối tượng

    void Start()
    {
        GameObject fishImage = GameObject.Find("FishImage");
        fishImageTras = fishImage.GetComponent<RectTransform>();
        startPosition = transform.position; // Lưu vị trí ban đầu
        normalSize = fishImageTras.localScale ; // Đặt kích thước ban đầu của hình ảnh con cá
    }

    void Update()
    {
        if (!isMoving)
        {
            MotionEffect();
        }
        else 
        {
            transform.position = Vector3.MoveTowards(transform.position, fishPosition, 15 * Time.deltaTime);
            // Thay đổi kích thước hình ảnh con cá khi đối tượng di chuyển đến gần
            float distance = Vector3.Distance(transform.position, fishPosition);
            float maxDistance = 5f; // Tùy chỉnh khoảng cách tối đa để phóng to

            if (distance < maxDistance)
            {
                float scaleFactor = Mathf.Lerp(1f, enlargedSize.x, 1 - (distance / maxDistance));
                fishImageTras.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
            }

            if (transform.position == fishPosition)
            {
                GameObject summaryLevel  = GameObject.Find("SummaryLevel");
                SummaryLevelController summaryLevelController = summaryLevel.GetComponent<SummaryLevelController>();
                summaryLevelController.AddFishCollect(1);

                DataManager.Instance.AddPlayerFish(1);
                fishImageTras.localScale = normalSize;
                Destroy(gameObject);
            }
        }

        fishPosition = GetFishPosition();
        
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

    private Vector3 GetFishPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(fishImageTras.position);
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
