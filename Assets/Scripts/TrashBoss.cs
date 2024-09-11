using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
//using Random = UnityEngine.Random;


public class TrashBoss : MonoBehaviour
{
    [SerializeField] private List<GameObject> trashs;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private float minTimeShoot, maxTimeShoot;
    [SerializeField] private float minHighShoot, maxHighShoot;
    private bool canShoot = true;
    private Vector3 playerPosition;

    private void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.CompareTag("Player") && canShoot)
        {
            canShoot = false;
            playerPosition = collider.transform.position;
            StartCoroutine(ShootPlayer());
        }
    } 

    // private IEnumerator ShootPlayer()
    // {
    //    // Tạo đối tượng trash
    //     GameObject spawnedTrash = Instantiate(trashs[Random.Range(0, trashs.Count)], shootPosition.position, Quaternion.identity);

    //     Rigidbody2D rb = spawnedTrash.GetComponentInChildren<Rigidbody2D>();
    //     rb.gravityScale = 1.5f;

    //     Vector2 direction = playerPosition - shootPosition.position;
    //     float highShoot = Random.Range(minHighShoot, maxHighShoot);
    //     // Tính toán thời gian bay dựa trên độ cao highShoot và trọng lực
    //     float gravity = Physics2D.gravity.y * rb.gravityScale;
    //     float timeToPeak = Mathf.Sqrt(2 * highShoot / -gravity); // Thời gian để đạt đến độ cao highShoot
    //     float totalFlightTime = timeToPeak + Mathf.Sqrt(2 * (highShoot - direction.y) / -gravity); // Tổng thời gian bay

    //     // Tính toán vận tốc cần thiết
    //     Vector2 velocity = new Vector2(direction.x / totalFlightTime, (2 * highShoot) / timeToPeak);
    //     rb.velocity = velocity;
        
    //     yield return new WaitForSeconds(Random.Range(minTimeShoot, maxTimeShoot));

    //     canShoot = true; 
    // }

    private IEnumerator ShootPlayer()
{
    // Tạo đối tượng trash
    GameObject spawnedTrash = Instantiate(trashs[Random.Range(0, trashs.Count)], shootPosition.position, Quaternion.identity);

    Rigidbody2D rb = spawnedTrash.GetComponentInChildren<Rigidbody2D>();
    rb.gravityScale = 1.5f;

    Vector2 direction = playerPosition - shootPosition.position;
    float highShoot = Random.Range(minHighShoot, maxHighShoot);

    float gravity = Physics2D.gravity.y * rb.gravityScale;

    if (gravity == 0)
    {
        gravity = -9.81f; // Thiết lập trọng lực mặc định nếu giá trị bị sai
    }

    float timeToPeak = Mathf.Sqrt(2 * highShoot / -gravity);

    float yDifference = highShoot - direction.y;

    // Đảm bảo yDifference không nhỏ hơn 0 để tránh lỗi tính căn bậc hai của số âm
    if (yDifference < 0)
    {
        yDifference = 0;
    }

    float totalFlightTime = timeToPeak + Mathf.Sqrt(2 * yDifference / -gravity);

    if (totalFlightTime == 0)
    {
        totalFlightTime = 0.1f; // Đảm bảo không chia cho 0
    }

    Vector2 velocity = new Vector2(direction.x / totalFlightTime, (2 * highShoot) / timeToPeak);
    rb.velocity = velocity;

    yield return new WaitForSeconds(Random.Range(minTimeShoot, maxTimeShoot));

    canShoot = true;
}



}
