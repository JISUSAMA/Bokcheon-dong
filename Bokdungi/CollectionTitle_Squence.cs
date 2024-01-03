using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class CollectionTitle_Squence : MonoBehaviour
{
    Sequence Image;

    void Start()
    {
        transform.localScale = Vector3.zero;
        Image = DOTween.Sequence()
        .OnStart(() => {
            transform.localScale = Vector3.zero;
            GetComponent<Image>().alphaHitTestMinimumThreshold = 0;
        })
        .Append(transform.DOScale(1, 1).SetEase(Ease.OutBounce))
        .Join(GetComponent<Image>().DOFade(1, 1))
        .SetDelay(0.5f);
    }

    private void OnEnable()
    {
        Image.Restart();
    }

}
