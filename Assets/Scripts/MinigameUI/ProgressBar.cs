using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
/// <summary>
/// Attach to UI Image component
/// </summary>
[RequireComponent(typeof(EventManager))]
public class ProgressBar : MonoBehaviour
{


    [SerializeField] private Color _bad, _mediocre, _good, _excelent, _separator; //Gradient step colors
    [SerializeField, Range(0, 100)] private int _badLevel, _mediocreLevel, _goodLevel, _excellentLevel; //Top values of every level
     private Sprite _bar; //holder for _barTexture
    private Texture2D _barTexture, _barFilledTexture, _barEmptyTexture; //textures of current state, full bar and empty bar
    private Image _barTargetUIImage; //UI image to be replaced with progress bar


    private float _progress = 0.0f;
    private float _progressIncrease = 1.0f;
    private float _progressDecrease = 1.0f;
    public const float MaxProgress = 100.0f;
    public bool AutoDecreasing;

    private UnityAction _increaseProgressDelegate; // UnityAction is a delegate defined by Unity to use it with event system etc.
    private UnityAction _decreseProgressDelegate; // instead of C#'s delegates.

    void Awake()
    {
        _barTargetUIImage = this.gameObject.GetComponent<Image>();
        if(_barTargetUIImage==null)Debug.Log("Please attach me to UI Image");
        _increaseProgressDelegate += IncreaseProgress;
        _decreseProgressDelegate += DecreaseProgress;
        EventManager.StartListening("IncreaseProgressBar", IncreaseProgress);
        EventManager.StartListening("DecreaseProgressBar", DecreaseProgress);
        GenerateProgressBar();
    }

    private void GenerateProgressBar()
    {
        GenerateProgressBarsTextures();
        _bar = Sprite.Create(_barTexture, new Rect(0.0f, 0.0f, _barTexture.width, _barTexture.height), new Vector2(0.5f, 0.5f));
        _barTargetUIImage.sprite = _bar;
        _barTargetUIImage.rectTransform.sizeDelta = new Vector2(_barTexture.width, _barTexture.height);
    }

    /// <summary>
    /// Very crude progress bar generator, creates gradient of colours and places separators
    /// </summary>
    private void GenerateProgressBarsTextures()
    {
        _barTexture = new Texture2D(10, 100);
        _barFilledTexture = new Texture2D(10, 100);
        _barEmptyTexture = new Texture2D(10, 100);
        Vector3 colorNormalized = new Vector3();
        float step = 0;
        for (int i = 0; i < _barTexture.height; i++)
        {
            for (int j = 0; j < _barTexture.width; j++)
            {
                _barTexture.SetPixel(j, i, Color.white);
                _barEmptyTexture.SetPixel(j, i, Color.white);
                if (i == 0 || i == _badLevel || i == _mediocreLevel || i == _goodLevel || i == _excellentLevel)
                {
                    _barTexture.SetPixel(j, i, _separator);
                    _barEmptyTexture.SetPixel(j, i, _separator);
                }
                Color deltaColors;
                if (i == 0)
                {
                    _barFilledTexture.SetPixel(j, i, _separator);
                    deltaColors = _mediocre - _bad;
                    colorNormalized = new Vector3(deltaColors.r, deltaColors.g, deltaColors.b);
                    step = colorNormalized.magnitude / (_badLevel);
                    colorNormalized.Normalize();
                }
                else
                {
                    Color currentColor;
                    if (i < _badLevel && i > 0)
                    {
                        currentColor = _bad;
                        currentColor.a = 1;
                        _barFilledTexture.SetPixel(j, i, currentColor);
                    }
                    else if (i == _badLevel)
                    {
                        _barFilledTexture.SetPixel(j, i, _separator);
                        deltaColors = _mediocre - _bad;
                        colorNormalized = new Vector3(deltaColors.r, deltaColors.g, deltaColors.b);
                        step = colorNormalized.magnitude / (_mediocreLevel - _badLevel);
                        colorNormalized.Normalize();
                    }
                    else if (i < _mediocreLevel && i > _badLevel)
                    {
                        currentColor = _bad + new Color(colorNormalized.x, colorNormalized.y, colorNormalized.z, 1) / 255 / step *
                                       (i - _badLevel);
                        currentColor.a = 1;
                        _barFilledTexture.SetPixel(j, i, currentColor);
                    }
                    else if (i == _mediocreLevel)
                    {
                        _barFilledTexture.SetPixel(j, i, _separator);
                        deltaColors = _good - _mediocre;
                        colorNormalized = new Vector3(deltaColors.r, deltaColors.g, deltaColors.b);
                        step = colorNormalized.magnitude / (_goodLevel - _mediocreLevel);
                        colorNormalized.Normalize();
                    }
                    else if (i < _goodLevel && i > _mediocreLevel)
                    {
                        currentColor = _mediocre + new Color(colorNormalized.x, colorNormalized.y, colorNormalized.z, 1) / 255 / step *
                                       (i - _mediocreLevel);
                        currentColor.a = 1;
                        _barFilledTexture.SetPixel(j, i, currentColor);
                    }
                    else if (i == _goodLevel)
                    {
                        _barFilledTexture.SetPixel(j, i, _separator);
                        deltaColors = _excelent - _good;
                        colorNormalized = new Vector3(deltaColors.r, deltaColors.g, deltaColors.b);
                        step = colorNormalized.magnitude / (_excellentLevel - _goodLevel);
                        colorNormalized.Normalize();
                    }
                    else if (i > _goodLevel && i < _excellentLevel)
                    {
                        currentColor = _good + new Color(colorNormalized.x, colorNormalized.y, colorNormalized.z, 1) / 255 / step *
                                       (i - _goodLevel);
                        currentColor.a = 1;
                        _barFilledTexture.SetPixel(j, i, currentColor);
                    }
                    else if (i == _excellentLevel)
                    {
                        _barFilledTexture.SetPixel(j, i, _separator);
                    }
                    else if (i > _excellentLevel)
                    {
                        _barFilledTexture.SetPixel(j, i, _excelent);
                    }
                }
            }
        }
        _barFilledTexture.Apply();
        _barTexture.Apply();
        _barEmptyTexture.Apply();
        _barFilledTexture.filterMode = FilterMode.Point;
        _barTexture.filterMode = FilterMode.Point;
        _barEmptyTexture.filterMode = FilterMode.Point;
    }
    private void DecreaseProgress()
    {
        _progress -= _progressDecrease;
        if (_progress < 0) _progress = 0;   //ensuring progress is always equal or above 0.
        UpdateUIProgressBar();
    }
    private void IncreaseProgress()
    {
        _progress += _progressIncrease;
        if (_progress > MaxProgress)
            _progress = MaxProgress;        // ensuring prograss is always equal or below maxProgress constant.
        UpdateUIProgressBar();
    }

    void UpdateUIProgressBar()
    {
        for (int i = 0; i < _barTexture.height; i++)
        {
            for (int j = 0; j < _barTexture.width; j++)
            {
                _barTexture.SetPixel(j, i,
                    i < _progress ? _barFilledTexture.GetPixel(j, i) : _barEmptyTexture.GetPixel(j, i));
            }
        }
        _barTexture.Apply();
        _bar = Sprite.Create(_barTexture, new Rect(0.0f, 0.0f, _barTexture.width, _barTexture.height), new Vector2(0.5f, 0.5f));
    }
}
