using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // Thêm thư viện DoTween

public class ObstacleBall : MonoBehaviour
{
    public float moveDistance = 5f; // Khoảng cách di chuyển
    public float moveDuration = 1f; // Thời gian di chuyển
    [SerializeField] private bool isMoving = false; // Biến để theo dõi trạng thái di chuyển
    void Start()
    {
        // Giả sử bạn có cách nào đó để khởi tạo bóng
        GameManager.instance.CurrentLevel.AddBall(this.gameObject);
    }

    public void MoveBall(Vector2 hitDirection)
    {
        // Nếu đang di chuyển, không làm gì
        if (isMoving) return;
        isMoving = true; // Đánh dấu là đang di chuyển
        Vector3 targetPosition = transform.position + (Vector3)hitDirection * moveDistance;

        // Di chuyển tới vị trí mục tiêu
        transform.DOMove(targetPosition, moveDuration).OnUpdate(() =>
        {
            // Kiểm tra nếu BallRed đã chạm vào giới hạn
            /*if (!IsWithinLimits(transform.position))
            {
                StopBallMovement(); // Dừng lại nếu ra ngoài giới hạn
            }*/
            if (IsInHoleLayer(transform.position))
            {
                GameManager.instance.CurrentLevel.OnBallEnteredHole(this.gameObject);
                DisableBall(); // Dừng nếu vào vùng lỗ
            }
        }).OnComplete(() =>
        {
            isMoving = false; // Đánh dấu không còn di chuyển nữa
        });
    }

    private bool IsWithinLimits(Vector3 position)
    {
        // Kiểm tra xem vị trí có nằm trong giới hạn không
        return position.x >= GameManager.instance.CurrentLevel.BottomLeftLimit.position.x && position.x <= GameManager.instance.CurrentLevel.TopRightLimit.position.x &&
               position.y >= GameManager.instance.CurrentLevel.BottomLeftLimit.position.y && position.y <= GameManager.instance.CurrentLevel.TopRightLimit.position.y;
    }

    private bool IsInHoleLayer(Vector3 position)
    {
        // Kiểm tra xem vị trí có chạm vào layer "Hole" không
        int holeLayer = LayerMask.NameToLayer("Hole");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.1f); // Sử dụng OverlapCircle để kiểm tra
        foreach (var collider in colliders)
        {
            if (collider.gameObject.layer == holeLayer)
            {
                return true; // Nếu chạm vào layer "Hole"
            }
        }
        return false;
    }

    private void StopBallMovement()
    {
        // Dừng tất cả các tween đang chạy
        DOTween.Kill(transform); // Dừng tất cả các tween cho đối tượng này
        isMoving = false; // Đánh dấu không còn di chuyển nữa
        Debug.Log("BallRed đã dừng lại do va chạm với giới hạn.");
    }

    private void DisableBall()
    {
        StopBallMovement(); // Dừng tween nếu vào vùng lỗ

        // Thực hiện tween scale về 0
        transform.DOScale(0, 1f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
          // Gọi hàm khi bóng vào lỗ
            gameObject.SetActive(false); // Vô hiệu hóa BallRed
            Debug.Log("BallRed đã bị tắt do vào vùng lỗ.");
        });
    }


}
