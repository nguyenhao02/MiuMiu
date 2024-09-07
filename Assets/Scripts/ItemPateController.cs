using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemPateController : MonoBehaviour
{
     [SerializeField] private float floatSpeed = 0.4f; // Tốc độ di chuyển lên xuống
    [SerializeField] private float floatHeight = 0.4f; // Biên độ di chuyển (cự ly từ vị trí ban đầu)
    [SerializeField] private RectTransform pateImageTras; // Hình ảnh con cá trên Canvas
    [SerializeField] private Vector3 enlargedSize = new Vector3(2f, 2f, 1f); // Kích thước phóng to
    private Vector3 normalSize ;

    private Vector3 patePosition;
    private Vector3 startPosition;
    private bool isMoving = false; // Kiểm soát việc di chuyển đối tượng

    void Start()
    {
        GameObject pateImage = GameObject.Find("PateImage");
        pateImageTras = pateImage.GetComponent<RectTransform>();

        startPosition = transform.position; // Lưu vị trí ban đầu
        normalSize = pateImageTras.localScale; 
    }

    void Update()
    {
        if (!isMoving)
        {
            MotionEffect();
        }
        else 
        {
            transform.position = Vector3.MoveTowards(transform.position, patePosition, 15 * Time.deltaTime);
            // Thay đổi kích thước hình ảnh con cá khi đối tượng di chuyển đến gần
            float distance = Vector3.Distance(transform.position, patePosition);
            float maxDistance = 5f; // Tùy chỉnh khoảng cách tối đa để phóng to

            if (distance < maxDistance)
            {
                float scaleFactor = Mathf.Lerp(1f, enlargedSize.x, 1 - (distance / maxDistance));
                pateImageTras.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
            }

            if (transform.position == patePosition)
            {  
                GameObject summaryLevel  = GameObject.Find("SummaryLevel");
                SummaryLevelController summaryLevelController = summaryLevel.GetComponent<SummaryLevelController>();
                summaryLevelController.AddPateCollect(1);

                DataManager.Instance.AddPlayerPate(1);
                pateImageTras.localScale = normalSize;
                Destroy(gameObject);
            }
        }

        patePosition = GetPatePosition();
        
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

    private Vector3 GetPatePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(pateImageTras.position);
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
