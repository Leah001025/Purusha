using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KanaSkill : MonoBehaviour
{
    [SerializeField] private LineRenderer _beam;
    [SerializeField] private Transform _muzzlePoint;
    [SerializeField] private Transform _Drone;
    [SerializeField] private float _maxLength;
    [SerializeField] private GameObject _hitEffect1;
    [SerializeField] private GameObject _muzzleEffect1;
    [SerializeField] private GameObject _skill3Effect1;
    [SerializeField] private GameObject _skill4Effect1;
    [SerializeField] private GameObject _skill4Effect2;
    [SerializeField] private GameObject _skill4Effect3;

    public bool activebeam = false;
    public bool isGetAngle = false;
    public Vector3 targetPos;

    private void Awake()
    {
        DeactivateBeam();
    }
    void Start()
    {
        _hitEffect1.SetActive(false);
        _muzzleEffect1.SetActive(false);
        if (BattleManager.Instance != null)
        {
            targetPos = BattleManager.Instance.target.transform.position + new Vector3(0, 0.9f, 0);
        }
        _maxLength = 20f;
        switch (gameObject.tag)
        {
            case "Skill1":
                StartCoroutine(Skill1());
                break;
            case "Skill2":
                StartCoroutine(Skill2());
                break;
            case "Skill3":
                StartCoroutine(Skill3());
                break;
            case "Skill4":
                StartCoroutine(Skill4());
                break;
            default: return;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (activebeam)
        {
            ActivateBeam();
            Beam();
        }
        else
            DeactivateBeam();

    }
    public void ActivateBeam()
    {
        _beam.enabled = true;
        _hitEffect1.SetActive(true);
        _muzzleEffect1.SetActive(true);

    }
    public void DeactivateBeam()
    {
        _beam.enabled = false;
        _beam.SetPosition(0, _muzzlePoint.position);
        _beam.SetPosition(1, _muzzlePoint.position);

    }
    public void Beam()
    {
        if (_beam.enabled)
        {
            Ray ray1 = new Ray(_muzzlePoint.position, -_muzzlePoint.right);
            bool cast1 = Physics.Raycast(ray1, out RaycastHit hit1, _maxLength);
            Vector3 hitPosition1 = cast1 ? hit1.point : _muzzlePoint.position + -_muzzlePoint.right * _maxLength;

            _beam.SetPosition(0, _muzzlePoint.position);
            _beam.SetPosition(1, hitPosition1);
            _hitEffect1.transform.position = hitPosition1;
        }
    }
    public void CalAngle(Transform _Drone)
    {
        Vector3 targetNVector = (targetPos - _Drone.position);

        Vector2 targetTransX = new Vector2(targetNVector.y, targetNVector.z).normalized;
        Vector2 targetTransY = new Vector2(targetNVector.x, targetNVector.z).normalized;
        Vector2 DroneTransX = new Vector2(_Drone.forward.y, _Drone.forward.z).normalized;
        Vector2 DroneTransY = new Vector2(_Drone.forward.x, _Drone.forward.z).normalized;

        float angleX = Mathf.Acos(Vector3.Dot(targetTransX, DroneTransX)) * Mathf.Rad2Deg;
        if (angleX < 1) angleX = 0;
        float angleY = Mathf.Acos(Vector3.Dot(targetTransY, DroneTransY)) * Mathf.Rad2Deg;
        Vector3 rot = Vector3.Cross(targetTransY, DroneTransY).normalized;

        _Drone.transform.localRotation = Quaternion.Euler(_Drone.transform.localRotation.x + angleX, _Drone.transform.localRotation.y + angleY * rot.z, 0);

    }

    IEnumerator Skill1()
    {
        if (!isGetAngle)
        {
            CalAngle(_Drone);
            isGetAngle = true;
        }
        yield return new WaitForSeconds(0.5f);
        activebeam = true;
        yield return new WaitForSeconds(1f);
        DeactivateBeam();
        //Destroy(gameObject);

    }
    IEnumerator Skill2()
    {
        yield return null;
    }
    IEnumerator Skill3()
    {
        if (!isGetAngle)
        {
            CalAngle(_Drone);
            isGetAngle = true;
        }
        yield return new WaitForSeconds(0.5f);
        _skill3Effect1.transform.position = targetPos;
        _skill3Effect1.SetActive(true);
        activebeam = true;
        yield return new WaitForSeconds(1f);
        DeactivateBeam();
        //Destroy(gameObject);

    }
    IEnumerator Skill4()
    {
        if (!isGetAngle)
        {
            CalAngle(_Drone);
            isGetAngle = true;
        }
        yield return new WaitForSeconds(0.4f);
        activebeam = true;
        _skill4Effect1.transform.position = targetPos;
        _skill4Effect1.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        _skill4Effect2.transform.position = targetPos + new Vector3(0.3f,0.3f,0);
        _skill4Effect2.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        _skill4Effect3.transform.position = targetPos + new Vector3(-0.3f, -0.3f, 0);
        _skill4Effect3.SetActive(true);
    }
}
