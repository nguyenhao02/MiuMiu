using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemPateDrop : MonoBehaviour
{
    private RectTransform pateImageTras; // Hình ảnh trên Canvas UIGame
    private Vector3 zoomSize = new Vector3(2f, 2f, 1f); // Kích thước phóng to
    private Vector3 normalSize ;
    private Rigidbody2D rb;
    private Vector3 imageCanvasPosition;
    private bool isMoving = false; // Kiểm soát việc di chuyển đối tượng
    private bool canCheckCollision = false;

    void Start()
    {
        GameObject pateImage = GameObject.Find("PateImage");
        pateImageTras = pateImage.GetComponent<RectTransform>();

        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(Random.Range(-4f, 4f), Random.Range(7f, 12f)), ForceMode2D.Impulse);       

        normalSize = pateImageTras.localScale ; 

        // Delay ngắn để tránh va chạm ngay khi đối tượng được tạo ra
        StartCoroutine(EnableCollisionCheck());
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, imageCanvasPosition, 15 * Time.deltaTime);
            // Thay đổi kích thước hình ảnh con cá khi đối tượng di chuyển đến gần
            float distance = Vector3.Distance(transform.position, imageCanvasPosition);
            float maxDistance = 5f; // Tùy chỉnh khoảng cách tối đa để phóng to

            if (distance < maxDistance)
            {
                float scaleFactor = Mathf.Lerp(1f, zoomSize.x, 1 - (distance / maxDistance));
                pateImageTras.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
            }

            if (transform.position == imageCanvasPosition)
            {
                DataManager.Instance.AddPlayerPate(1);
                pateImageTras.localScale = normalSize;
                Destroy(gameObject);
            }
        }

        imageCanvasPosition = GetHealthPosition();
        
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (canCheckCollision && collider.gameObject.CompareTag("Player"))
        {
            rb.gravityScale = 0;
            isMoving = true;

            SoundManager.Instance.PlaySFX(SoundManager.Instance.itemDrop);
        }
    }

    private Vector3 GetHealthPosition()
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

    private IEnumerator EnableCollisionCheck()
    {
        // Đợi một khoảng thời gian ngắn trước khi cho phép kiểm tra va chạm
        yield return new WaitForSeconds(0.5f);
        canCheckCollision = true;
    }
}
