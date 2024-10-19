using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = transform.GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BallRed"))
        {
            Debug.Log("Chạm vào BallRed");

            _rb.velocity = Vector2.zero;

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

            if(hitDirection.x > 0)
            {
                transform.position = new Vector3(collision.transform.position.x - 0.4f, collision.transform.position.y, 0);
            }
            else if(hitDirection.x < 0)
            {
                transform.position = new Vector3(collision.transform.position.x + 0.4f, collision.transform.position.y, 0);
            }
            else if (hitDirection.y > 0)
            {
                transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y - 0.4f, 0);
            }
            else if (hitDirection.y < 0)
            {
                transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y + 0.4f, 0);
            }

            // Gọi hàm MoveBall từ script ObstacleBallController và truyền hướng va chạm
            ObstacleBall ballController = collision.gameObject.GetComponent<ObstacleBall>();
            if (ballController != null)
            {
                SoundManager.Instance.PlayFxSound(SoundManager.Instance.Ballhit);
                ballController.MoveBall(hitDirection); // Gọi hàm di chuyển BallRed theo hướng thẳng
            }

            // Đặt trạng thái IsMoving của GameManager
            GameManager.instance.CurrentLevel.IsMoving = false;
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
            GameManager.instance.CurrentLevel.OnPlayerFellIntoHole(); // Gọi hàm khi BallPlayer rơi vào lỗ
            gameObject.SetActive(false); // Vô hiệu hóa BallPlayer sau khi scale về 0
            Debug.Log("BallPlayer đã bị tắt do vào vùng lỗ.");
        });
    }


    private bool IsAtLimits(Vector2 position)
    {
        // Kiểm tra xem BallPlayer có chạm vào giới hạn không
        return position.x <= GameManager.instance.CurrentLevel.BottomLeftLimit.position.x || position.x >= GameManager.instance.CurrentLevel.TopRightLimit.position.x ||
               position.y <= GameManager.instance.CurrentLevel.BottomLeftLimit.position.y || position.y >= GameManager.instance.CurrentLevel.TopRightLimit.position.y;
    }
}
