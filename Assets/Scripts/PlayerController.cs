using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 50.0f;
    private CharacterController characterController;
    public Rigidbody head;

    // The LayerMask lets you indicate what layers the ray should hit
    public LayerMask layerMask;
    //  currentLookTarget is where you want the marine to stare. 
    private Vector3 currentLookTarget = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 pos = transform.position;
        // pos.x += moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        // pos.z += moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime; 
        // transform.position = pos;

        Vector3 moveDirection = 
            new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.SimpleMove(moveDirection * moveSpeed);
    }

    void FixedUpdate() {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));
        if (moveDirection == Vector3.zero) {
            // TODO
        } else {
            head.AddForce(transform.right * 150, ForceMode.Acceleration);
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green);

        // hysics.Raycast：光线投射
        // First you pass in the ray that you generated along with the hit. Since the hit variable is marked as out, it can be populated by Physics.Raycast().
        // 1000表示射线的长度。在这种情况下，是1000米。
        // layerMask 让 cast 知道你想要击中什么。
        // QueryTriggerInteraction.Ignore ：告诉物理引擎不要激活触发器。
        if (Physics.Raycast(ray, out hit, 1000, layerMask, QueryTriggerInteraction.Ignore)) {
            if (hit.point != currentLookTarget) {
                currentLookTarget = hit.point;
            }
        }
        // 1 - Get target position
        Vector3 targetPosition = new Vector3(hit.point.x,
                transform.position.y, hit.point.z);
        // 2 - calculate the current quaternion, used to determine rotation
        //LookRotation() returns the quaternion for where the marine should turn
        Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position);
        // 3 Lerp()用于随着时间的推移平滑地更改值(比如在这个位置的旋转)
        transform.rotation = Quaternion.Lerp(transform.rotation,rotation, Time.deltaTime * 10.0f);
    }
}
