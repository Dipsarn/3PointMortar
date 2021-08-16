using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOver : MonoBehaviour, IPointerEnterHandler
{
    private MenuSoundController mSC;


    private void Start()
    {
        mSC = transform.root.GetComponent<MenuSoundController>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mSC.PlayMouseOver();

    }


}