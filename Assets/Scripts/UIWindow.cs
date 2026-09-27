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
    [SerializeField] private bool _hideOnStart;

    [Header("Animation Settings")]
    [SerializeField] private float _showDuration = 0.5f;
    [SerializeField] private float _hideDuration = 0.5f;

    [SerializeField] private Ease _showEase = Ease.OutBack;
    [SerializeField] private Ease _hideEase = Ease.InBack;



    public CanvasGroup CanvasGroup => _canvasGroup;
    public RectTransform CanvasRectTransform => _canvasRectTransform;
    public string Id => _id;

    public string WindowId { get; internal set; }
    public string WindowID { get; internal set; }

    void Start()
    {
        Initialize();
    }

    public virtual void Initialize()
    {
        if (_hideOnStart)
        {
            Hide();
        }
    }

    [Button("Show Window")]
    public virtual void Show(bool instant = false)
    {
        if (instant)
        {
            _canvasRectTransform.gameObject.SetActive(true);
        }
        else
        {
            _canvasRectTransform.gameObject.SetActive(true);
            RectTransform rectTransform = _canvasGroup.GetComponent<RectTransform>();
            rectTransform.DOScale(Vector3.one, _showDuration).SetEase(_showEase);
        }
    }

    [Button("Hide Window")]
    public virtual void Hide(bool instant = false)
    {
        if (instant)
        {
            _canvasRectTransform.gameObject.SetActive(false);
        }
        else
        {
            RectTransform rectTransform = _canvasGroup.GetComponent<RectTransform>();
            rectTransform.DOScale(Vector3.zero, _hideDuration).SetEase(_hideEase).OnComplete(() => _canvasRectTransform.gameObject.SetActive(false));
        }
    }

}

