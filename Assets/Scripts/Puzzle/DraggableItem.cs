using UnityEngine;
using UnityEngine.UI;

namespace Puzzle
{
    public class DraggableItem : MonoBehaviour, IDroppable, IDraggable,IBeginDrag
    {
        [SerializeField] private Image image;
        [SerializeField] private RectTransform rect;
        [SerializeField] private new BoxCollider2D collider;
        private Transform _parent;
        private Vector3 _defPos;
        private bool _attached;
        private Drag _drag;


        private void Start()
        {
            collider.size = rect.sizeDelta/2;
            image.SetNativeSize();
            _parent = transform.parent;
            _drag = GetComponent<Drag>();
        }

        public void Dropped(bool con)
        {
            if (_attached)
                return;
            rect.localPosition = con ? rect.localPosition : _defPos;
            image.maskable = !con;
            image.raycastTarget = !con;
            _attached = con;
            _drag.enabled = !con;
             transform.localScale = Vector3.one;
            image.SetNativeSize();
            gameObject.SetActive(!con);
        }

        public void OnDrag()
        {
            image.maskable = false;
        }

        public void BeginDrag()
        {
            _defPos = rect.localPosition;
            image.maskable = false;
        }
    }
}