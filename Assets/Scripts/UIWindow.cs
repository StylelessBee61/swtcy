
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
    [SerializeField] private float _hideDuration = 0.3f;

    [SerializeField] private Ease _showEase = Ease.OutBack;
    [SerializeField] private Ease _hideEase = Ease.InBack;

    private Tween _currentTween;

    public CanvasGroup CanvasGroup => _canvasGroup;
    public RectTransform CanvasRectTransform => _canvasRectTransform;
    public string Id => _id;
    public string WindowID => _id;

    private void Awake()
    {
        if (_canvasRectTransform == null)
            _canvasRectTransform = GetComponent<RectTransform>();

        if (_canvasGroup == null)
            _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        Initialize();
    }

    private void OnDestroy()
    {
        if (_currentTween != null && _currentTween.IsActive())
        {
            _currentTween.Kill();
        }
    }

    public virtual void Initialize()
    {
        if (_hideOnStart)
            Hide(true);
        else
            Show(true);
    }

    [Button("Show Window")]
    public virtual void Show(bool instant = false)
    {
        if (this == null || _canvasRectTransform == null || _canvasGroup == null)
            return;

        _canvasRectTransform.gameObject.SetActive(true);

        if (_currentTween != null)
            _currentTween.Kill();

        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;

        if (instant)
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.transform.localScale = Vector3.one;
            return;
        }

        _canvasGroup.alpha = 1f;
        _canvasGroup.transform.localScale = Vector3.zero;

        _currentTween = _canvasGroup.transform
            .DOScale(Vector3.one, _showDuration)
            .SetEase(_showEase)
            .SetLink(gameObject);
    }

    [Button("Hide Window")]
    public virtual void Hide(bool instant = false)
    {
        if (this == null || _canvasRectTransform == null || _canvasGroup == null)
            return;

        if (_currentTween != null)
            _currentTween.Kill();

        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        if (instant)
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.transform.localScale = Vector3.zero;
            _canvasRectTransform.gameObject.SetActive(false);
            return;
        }

        _currentTween = _canvasGroup.transform
            .DOScale(Vector3.zero, _hideDuration)
            .SetEase(_hideEase)
            .SetLink(gameObject)
            .OnComplete(() =>
            {
                if (this == null || _canvasRectTransform == null || _canvasGroup == null)
                    return;

                _canvasGroup.alpha = 0f;
                _canvasRectTransform.gameObject.SetActive(false);
            });
    }
}
