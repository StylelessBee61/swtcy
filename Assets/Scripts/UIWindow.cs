
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class UIWindow : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private string _id;

    [Header("UI Settings")]
    [SerializeField] private RectTransform _canvasRectTransform;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private bool _hideOnStart = true;

    [Header("Animation Settings")]
    [SerializeField] private float _showDuration = 0.5f;
    [SerializeField] private float _hideDuration = 0.5f;

    [SerializeField] private Ease _showEase = Ease.OutBack;
    [SerializeField] private Ease _hideEase = Ease.InBack;

    public CanvasGroup CanvasGroup => _canvasGroup;
    public RectTransform CanvasRectTransform => _canvasRectTransform;
    public string Id => _id;
    public string WindowID => _id;

    void Start()
    {
        Initialize();
    }

    public virtual void Initialize()
    {
        if (_hideOnStart)
            Hide(true);
    }

    [Button("Show Window")]
    public virtual void Show(bool instant = false)
    {
        _canvasRectTransform.gameObject.SetActive(true);

        _canvasGroup.DOKill();

        if (instant)
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.transform.localScale = Vector3.one;
            return;
        }

        _canvasGroup.alpha = 1f;
        _canvasGroup.transform.localScale = Vector3.zero;

        _canvasGroup.transform.DOScale(Vector3.one, _showDuration)
            .SetEase(_showEase);
    }

    [Button("Hide Window")]
    public virtual void Hide(bool instant = false)
    {
        _canvasGroup.DOKill();

        if (instant)
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.transform.localScale = Vector3.zero;
            _canvasRectTransform.gameObject.SetActive(false);
            return;
        }

        _canvasGroup.transform.DOScale(Vector3.zero, _hideDuration)
            .SetEase(_hideEase)
            .OnComplete(() =>
            {
                _canvasGroup.alpha = 0f;
                _canvasRectTransform.gameObject.SetActive(false);
            });
    }
}
