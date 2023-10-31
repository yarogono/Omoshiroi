using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSystem : MonoBehaviour
{
    [SerializeField] private BaseWeapon weapon;
    [SerializeField] private BaseMagic magic;
    [SerializeField] private BaseRune rune;

    public BaseWeapon Weapon { get; set; }
    public BaseMagic Magic { get; set; }
    public BaseRune Rune { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
