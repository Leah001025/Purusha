using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EveSkill : MonoBehaviour
{
    [SerializeField] private LineRenderer _beam;
    [SerializeField] private Transform _muzzlePoint;
    [SerializeField] private Transform _Drone;
    [SerializeField] private LineRenderer _beam2;
    [SerializeField] private Transform _muzzlePoint2;
    [SerializeField] private Transform _Drone2;
    [SerializeField] private float _maxLength;
    [SerializeField] private GameObject _hitEffect1;
    [SerializeField] private GameObject _hitEffect2;
    [SerializeField] private GameObject _muzzleEffect1;
    [SerializeField] private GameObject _muzzleEffect2;

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
        _hitEffect2.SetActive(false);
        _muzzleEffect1.SetActive(false);
        _muzzleEffect2.SetActive(false);
        targetPos = BattleManager.Instance.targerTrans.position+new Vector3(0,1,0);
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
        _beam2.enabled = true;
        _hitEffect1.SetActive(true);
        _hitEffect2.SetActive(true);
        _muzzleEffect1.SetActive(true);
        _muzzleEffect2.SetActive(true);

    }
    public void DeactivateBeam()
    {
        _beam.enabled = false;
        _beam.SetPosition(0, _muzzlePoint.position);
        _beam.SetPosition(1, _muzzlePoint.position);
        _beam2.enabled = false;
        _beam2.SetPosition(0, _muzzlePoint.position);
        _beam2.SetPosition(1, _muzzlePoint.position);
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
        if (_beam2.enabled)
        {
            Ray ray2 = new Ray(_muzzlePoint2.position, _muzzlePoint2.right);
            bool cast2 = Physics.Raycast(ray2, out RaycastHit hit2, _maxLength);
            Vector3 hitPosition2 = cast2 ? hit2.point : _muzzlePoint2.position + _muzzlePoint2.right * _maxLength;

            _beam2.SetPosition(0, _muzzlePoint2.position);
            _beam2.SetPosition(1, hitPosition2);
            _hitEffect2.transform.position = hitPosition2;
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
            CalAngle(_Drone2);
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
        _Drone.transform.localRotation = Quaternion.Euler(20, -40, 0);
        _Drone2.transform.localRotation = Quaternion.Euler(40, 30, 0);
        Quaternion target1 = Quaternion.Euler(10, 30, 0);
        Quaternion target2 = Quaternion.Euler(10, -40, 0);
        activebeam = true;
        for (int i = 0; i < 100; i++)
        {
            _Drone.transform.localRotation = Quaternion.Lerp(_Drone.transform.localRotation, target1, 3f * Time.deltaTime);
            _Drone2.transform.localRotation = Quaternion.Lerp(_Drone2.transform.localRotation, target2, 3f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.5f);
        DeactivateBeam();
        //Destroy(gameObject);

    }
    IEnumerator Skill4()
    {
        yield return null;
    }
}
