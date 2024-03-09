using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Transform currentCheckpoint;
    private Health playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
    }

    public void Respawn()
    {
        if (currentCheckpoint != null) // Kiểm tra xem có checkpoint nào được gán không
        {
            transform.position = currentCheckpoint.position; // Di chuyển người chơi đến checkpoint
            if (playerHealth != null)
            {
                playerHealth.Respawm(); // Gọi phương thức Respawm của playerHealth để hồi sinh người chơi
            }
            else
            {
                Debug.LogWarning("PlayerHealth không được gán! Không thể hồi sinh."); // Hiển thị cảnh báo nếu playerHealth không được gán
            }
        }
        else
        {
            Debug.LogWarning("Không có checkpoint nào được gán! Không thể hồi sinh."); // Hiển thị cảnh báo nếu không có checkpoint được gán
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            currentCheckpoint = collision.transform;

            // Kiểm tra xem collider của checkpoint có là trigger không
            if (!collision.isTrigger)
            {
                Debug.LogWarning("Collider của Checkpoint không được thiết lập là trigger!");
            }
            else
            {
                // Kích hoạt animation "Apear" của checkpoint nếu có
                Animator checkpointAnimator = collision.GetComponent<Animator>();
                if (checkpointAnimator != null)
                {
                    checkpointAnimator.SetTrigger("Apear");
                }
                else
                {
                    Debug.LogWarning("Animator không được tìm thấy trên Checkpoint!");
                }
            }
        }
    }
}
