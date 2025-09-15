using UnityEngine;
using UnityEngine.UI;

public class Crystal : MonoBehaviour
{
    [SerializeField] Image _image;

    public enum CrystalType { Fire, Water, Poison, Earth, Air } 

    public CrystalType Type { get; private set; }

    [Header("Спрайты кристаллов")]
    [SerializeField] Sprite _fireSprite;
    [SerializeField] Sprite _waterSprite;
    [SerializeField] Sprite _poisonSprite;
    [SerializeField] Sprite _earthSprite;
    [SerializeField] Sprite _airSprite; 

    public void SetRandomColor()
    {
        int random = Random.Range(0, 5);
        Type = (CrystalType)random;

        switch (Type)
        {
            case CrystalType.Fire: _image.sprite = _fireSprite; break;
            case CrystalType.Water: _image.sprite = _waterSprite; break;
            case CrystalType.Poison: _image.sprite = _poisonSprite; break;
            case CrystalType.Earth: _image.sprite = _earthSprite; break;
            case CrystalType.Air: _image.sprite = _airSprite; break;

        }
    }
}
