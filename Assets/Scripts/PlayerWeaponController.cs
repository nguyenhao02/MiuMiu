using System.Collections;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] private Transform shootPoint1, shootPoint2; 
    [SerializeField] private GameObject weaponPrefab;
    public float speed = 10f; 
    public float maxDistance = 10f; 

    public void ShootWeapon()
    {
        GameObject weapon = Instantiate(weaponPrefab, shootPoint1.position, Quaternion.identity);
        Vector3 direction = (shootPoint2.position - shootPoint1.position).normalized;
        //FlipWeapon
        if(shootPoint2.position.x < gameObject.transform.position.x)
        {
            Vector3 localScale = weapon.transform.localScale;
            localScale.x *= -1f;
            weapon.transform.localScale = localScale;
        }

        StartCoroutine(MoveWeapon(weapon, direction));
        DataManager.Instance.MinusPlayerWeapon(1);
    }

    private IEnumerator MoveWeapon(GameObject weapon, Vector3 direction)
    {
        Vector3 startPosition = weapon.transform.position;
        float distanceTraveled = 0f;

        while (distanceTraveled < maxDistance)
        {
            float distanceThisFrame = speed * Time.deltaTime;
            weapon.transform.Translate(direction * distanceThisFrame, Space.World);
            distanceTraveled = Vector3.Distance(startPosition, weapon.transform.position);

            yield return null;
        }

        Destroy(weapon);
    }

}
