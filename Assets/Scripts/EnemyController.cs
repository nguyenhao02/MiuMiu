using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject damageText;
    [SerializeField] private int maxHealth;
    //DropItem
    [SerializeField] private List<DropItem> dropItems;
    private int currentHealth;
    private HealthBar healthBar;
    // Start is called before the first frame update
    void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
    }
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddDamage(int damage)
    {
        if(damageText != null)
        {
            ShowDamageText(damage);
        }

        currentHealth -= damage;

        if(healthBar != null)
        {
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }

        if(currentHealth <= 0) 
        {
            makeDead();
        }
    }

    public void makeDead()
    {
        DropItems();
        Destroy(gameObject);
    }

    void ShowDamageText(int damage)
    {
        Debug.Log(damage);
        GameObject damageTextTemp = Instantiate(damageText, transform.position, Quaternion.identity);
        DamageText dmgTextComponent = damageTextTemp.GetComponentInChildren<DamageText>();
        if (dmgTextComponent != null)
        {
            dmgTextComponent.SetDamageText(damage);
        }
    }

    private void DropItems()
    {
        foreach (DropItem dropItem in dropItems)
        {
            float chance = Random.Range(0f, 1f);
            if (chance <= dropItem.dropRate)
            {
                int dropAmount = Random.Range(dropItem.minDropAmount, dropItem.maxDropAmount + 1);
                for (int i = 0; i < dropAmount; i++)
                {
                    Vector3 dropPosition = new Vector3(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f), 0);
                    Instantiate(dropItem.itemPrefab, dropPosition, Quaternion.identity);
                }
            }
        }
    }
}
