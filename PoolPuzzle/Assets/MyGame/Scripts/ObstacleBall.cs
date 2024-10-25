using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // Thêm thư viện DoTween
using Unity.VisualScripting;

public class ObstacleBall : MonoBehaviour
{
    public float moveDistance = 7f; // Khoảng cách di chuyển
    public float moveDuration = 1f; // Thời gian di chuyển
    [SerializeField] private bool isMoving = false; // Biến để theo dõi trạng thái di chuyển

    public CircleCollider2D circleCollider2D;

    Tween T_move;
    void Start()
    {
        // Giả sử bạn có cách nào đó để khởi tạo bóng
        GameManager.instance.CurrentLevel.AddBall(this.gameObject);
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    public void MoveBall(Vector2 hitDirection)
    {

        // Nếu đang di chuyển, không làm gì
        if (isMoving) return;

        //if (IsWithinLimits(transform.position)) return;

        isMoving = true; // Đánh dấu là đang di chuyển
        Vector3 targetPosition = transform.position + (Vector3)hitDirection * moveDistance;

        // Di chuyển tới vị trí mục tiêu
        T_move = transform.DOMove(targetPosition, moveDuration).OnUpdate(() =>
        {

            if (IsInHoleLayer(transform.position))
            {
                DisableBall(); // Dừng nếu vào vùng lỗ

                Debug.Log("vao lo");
                return;
            }

            // Kiểm tra nếu BallRed đã chạm vào giới hạn
/*            if (IsWithinLimits(transform.position))
            {
                Debug.Log("ra ngoai");
                StopBallMovement(); // Dừng lại nếu ra ngoài giới hạn
                return;
            }*/
            Debug.Log("check");

        }).OnComplete(() =>
        {
            isMoving = false; // Đánh dấu không còn di chuyển nữa
        });
    }

    private bool IsWithinLimits(Vector3 position)
    {

        // Lấy tọa độ của giới hạn
        float minX = GameManager.instance.CurrentLevel.BottomLeftLimit.position.x; // Tọa độ x của điểm dưới cùng bên trái cộng thêm 1 đơn vị
        float maxX = GameManager.instance.CurrentLevel.TopRightLimit.position.x - 0.01f;    // Tọa độ x của điểm trên cùng bên phải trừ đi 1 đơn vị
        float minY = GameManager.instance.CurrentLevel.BottomLeftLimit.position.y + 0.05f;  // Tọa độ y của điểm dưới cùng bên trái cộng thêm 1 đơn vị
        float maxY = GameManager.instance.CurrentLevel.TopRightLimit.position.y - 0.01f;    // Tọa độ y của điểm trên cùng bên phải trừ đi 1 đơn vị

        // Kiểm tra xem vị trí có nằm ngoài giới hạn không
        return position.x < minX || position.x > maxX ||
               position.y < minY || position.y > maxY;

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
                transform.position = collider.gameObject.transform.position;
                return true; // Nếu chạm vào layer "Hole"
            }
        }
        return false;
    }

    private void StopBallMovement()
    {
        T_move?.Kill();
        // Dừng tất cả các tween đang chạy
        isMoving = false; // Đánh dấu không còn di chuyển nữa
        Debug.Log("BallRed đã dừng lại do va chạm với giới hạn.");
    }

    private void DisableBall()
    {
        StopBallMovement(); // Dừng tween nếu vào vùng lỗ
        circleCollider2D.enabled = false;
        // Thực hiện tween scale về 0
        transform.DOScale(0, 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            // Gọi hàm khi bóng vào lỗ
            gameObject.SetActive(false); // Vô hiệu hóa BallRed
            GameManager.instance.CurrentLevel.OnBallEnteredHole(this.gameObject);
        });
    }


}
