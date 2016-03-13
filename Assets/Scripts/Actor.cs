using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Actor : MonoBehaviour
{
    public enum SizeEnum
    {
        Regular,
        Large,
        Small
    }

    /*
    [System.Serializable]
    public class SizeData
    {
        protected string name = "SizeData";
        public SizeData() { }
    }
    */

    public SizeEnum initSize;
    public float scaleTime = 0.5f;
    public float regularScale = 1f;
    public float largeScale = 2f;
    public float smallScale = 0.5f;
    public float regularMass = 1f;
    public float largeMass = 300f;
    public float smallMass = 0.01f;
    public bool isBusy;

    protected SizeEnum _size;
    protected float _scale;
    protected Vector3 initScale;

    public SizeEnum Size
    {
        get
        {
            return _size;
        }
        set
        {
            _size = value;

			//Debug.LogWarning (value);

            var targetScale = regularScale;

            Scale = GetScale(_size);

            var rigidbody = GetComponent<Rigidbody2D>();

            switch (_size)
            {
                case SizeEnum.Regular:
                    rigidbody.mass = regularMass;
                    break;
                case SizeEnum.Large:
                    rigidbody.mass = largeMass;
                    break;
                case SizeEnum.Small:
                    rigidbody.mass = smallMass;
                    break;
            }
        }
    }

    public float GetScale(SizeEnum targetSize)
    {
        var targetScale = regularScale;

        switch (targetSize)
        {
            case SizeEnum.Regular:
                targetScale = regularScale;
                break;
            case SizeEnum.Large:
                targetScale = largeScale;
                break;
            case SizeEnum.Small:
                targetScale = smallScale;
                break;
        }

        return targetScale;
    }

	virtual public void Update()
	{
		if (transform.position.y < -50)
			transform.position = Vector3.up * 80f + Vector3.right*Random.Range(-1f, 1f)*60f;
	}

    public float Scale
    {
        get
        {
            return _scale;
        }
        set
        {
            _scale = value;

            LeanTween.cancel(gameObject);
			LeanTween.scale(gameObject, initScale * _scale, scaleTime).setEase(LeanTweenType.easeInOutSine);
        }
    }

    protected virtual void Start()
    {
        //_scale = GetScale(initSize);
        //transform.localScale = Vector3.one * _scale;
        initScale = transform.localScale;
        var prevAnimTime = scaleTime;
        scaleTime = 0.001f;
        Size = initSize;
        scaleTime = prevAnimTime;
    }

}
