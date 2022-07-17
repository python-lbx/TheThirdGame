using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//草圖
public class test : MonoBehaviour
{

    //public Canvas can;
    //public GameObject pre;
    [Header("拾取查看道具草圖")]
    public GameObject EquipInfo;
    public ItemOnWorld _Equip;
    public Text _EquipName;
    public Text _EquipHP;
    public Text _EquipATK;
    public Text _EquipDEF;
    public Text _EquipSpeed;

    public bool here;
    public LayerMask item;

    public CharacterStats Player;

    // Start is called before the first frame update
    void Start()
    {
        //EquipInfo = FindObjectOfType<Canvas>().GetComponentInChildren<Image>();
        //EquipInfo.enabled = false;

        EquipInfo = GameObject.Find("EquipInfo");
        EquipInfo.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {   
        Collider2D _here = Physics2D.OverlapBox(transform.position,transform.localScale,0,item);
        
        here = _here;

        if(_here != null)
        {
            print(_here.gameObject.name);
            EquipInfo.SetActive(true);
            _Equip = _here.gameObject.GetComponent<ItemOnWorld>();
            EquipInfo.transform.position = _Equip.point.transform.position;
        }
        else
        {
            EquipInfo.SetActive(false);
        }

        if(_Equip != null)
        {
            _EquipName.text = _Equip.thisItem.ItemName;
            _EquipHP.text = _Equip.thisItem.HP.ToString();
            _EquipATK.text = _Equip.thisItem.ATK.ToString();
            _EquipDEF.text = _Equip.thisItem.DEF.ToString();
            _EquipSpeed.text = _Equip.thisItem.Speed.ToString();
        }

        if(here && Input.GetKeyDown(KeyCode.Z))
        {
            Player.Character.AttackPower += _Equip.thisItem.ATK;
            Player.Character.Defense += _Equip.thisItem.DEF;
            Player.Character.Speed += _Equip.thisItem.Speed;
            Player.Character.MaxHP += _Equip.thisItem.HP;
            print("你獲得了:"    + _Equip.thisItem.ItemName+
                  "最大生命值:"  + Player.Character.MaxHP +
                  "攻擊力:" + Player.Character.AttackPower +
                  "防御力:" + Player.Character.Defense +
                  "移動速度:" + Player.Character.Speed);
            Destroy(_here.gameObject);
        }

    }

    public void reset_show()
    {

    }
}
