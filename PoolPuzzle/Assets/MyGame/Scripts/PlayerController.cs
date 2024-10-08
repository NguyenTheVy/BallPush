﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BallRed"))
        {
            Debug.Log("Chạm vào BallRed");

            // Lấy hướng tác động từ player tới BallRed
            Vector2 hitDirection = collision.transform.position - transform.position;

            // Chọn trục di chuyển
            if (Mathf.Abs(hitDirection.x) > Mathf.Abs(hitDirection.y))
            {
                hitDirection = new Vector2(hitDirection.x > 0 ? 1 : -1, 0); // Di chuyển theo trục X
            }
            else
            {
                hitDirection = new Vector2(0, hitDirection.y > 0 ? 1 : -1); // Di chuyển theo trục Y
            }

            // Gọi hàm MoveBall từ script ObstacleBallController và truyền hướng va chạm
            ObstacleBall ballController = collision.gameObject.GetComponent<ObstacleBall>();
            if (ballController != null)
            {
                ballController.MoveBall(hitDirection); // Gọi hàm di chuyển BallRed theo hướng thẳng
            }

            // Đặt trạng thái IsMoving của GameManager
            GameManager.Instance.IsMoving = false;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Hole"))
        {
            // Kiểm tra vị trí của BallPlayer
            if (!IsAtLimits(transform.position))
            {
                // Nếu không ở giới hạn, gọi hàm DisablePlayer
                DisablePlayer();
            }
            else
            {
                Debug.Log("BallPlayer không bị tắt vì đã chạm vào giới hạn.");
            }
        }
    }


    private void DisablePlayer()
    {
        // Thực hiện tween scale về 0 cho BallPlayer
        transform.DOScale(0, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            GameManager.Instance.OnPlayerFellIntoHole(); // Gọi hàm khi BallPlayer rơi vào lỗ
            gameObject.SetActive(false); // Vô hiệu hóa BallPlayer sau khi scale về 0
            Debug.Log("BallPlayer đã bị tắt do vào vùng lỗ.");  
        });
    }


    private bool IsAtLimits(Vector2 position)
    {
        // Kiểm tra xem BallPlayer có chạm vào giới hạn không
        return position.x <= GameManager.Instance.BottomLeftLimit.position.x || position.x >= GameManager.Instance.TopRightLimit.position.x ||
               position.y <= GameManager.Instance.BottomLeftLimit.position.y || position.y >= GameManager.Instance.TopRightLimit.position.y;
    }
}
