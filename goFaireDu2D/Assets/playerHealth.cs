using UnityEngine;
using System.Collections;

public class playerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public healtBar healtbar;

    public bool isInvincible = false;
    public SpriteRenderer graphics;

    public float invincibilityFlashDelay = 0.2f;

    void Start()
    {
        currentHealth = maxHealth;
        healtbar.setMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            takeDamage(20);
        }
    }

    public void takeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;
            healtbar.setHealth(currentHealth);
            isInvincible = true;
            StartCoroutine(invincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }
    }

    public IEnumerator invincibilityFlash()
    {
        while (isInvincible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(3f);
        isInvincible = false;
    }
}
