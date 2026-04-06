using UnityEngine;

public class WeaponArea : MonoBehaviour
{
    [SerializeField] private GameObject _weaponPosition;
    [SerializeField] private GameObject _killedEnemyPosition;
    [SerializeField] private CardInstance _currentWeapon;
    [SerializeField] private CardInstance _lastVictim;
    [SerializeField] private int _victimCount = 0;
    [SerializeField] private Vector3 _currentVictimCardPosition;

    private void OnEnable()
    {
        _currentVictimCardPosition = Vector3.zero;
    }


    public GameObject WeaponPosition
    {
        get { return _weaponPosition; }
        set { _weaponPosition = value; }
    }

    public GameObject KilledEnemyPosition
    {
        get { return _killedEnemyPosition; }
        set { _killedEnemyPosition = value; }
    }

    public CardInstance CurrentWeapon
    {
        get { return _currentWeapon;}
        set { _currentWeapon = value; }
    }

    public CardInstance LastVictim
    {
        get {return _lastVictim;}
        set { _lastVictim = value; }
    }

    public int VictimCount
    {
        get { return _victimCount; }
        set { _victimCount = value; }
    }

    public Vector3 CurrentVictimCardPosition
    {
        get { return _currentVictimCardPosition; }
        set { _currentVictimCardPosition = value; }
    }

    public Vector3 GetVictimCardInterval()
    {
        Vector3 victimCardInterval = new Vector3(50f, Random.Range(-80f, 80f), 0f);
        return victimCardInterval;
    }
}
