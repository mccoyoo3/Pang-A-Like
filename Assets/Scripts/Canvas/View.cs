using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(GraphicRaycaster))]
public class View : MonoBehaviour
{
    private static View current;

    [HideInInspector]
    public View previous;

    private RectTransform rectTransform;
    private Canvas canvas;
    private GraphicRaycaster raycaster;

    public bool showOnAwake;

    [SerializeField]
    private View[] children;

    [SerializeField]
    private View[] parent;

    public Action onShow;
    public Action onHide;

    private int[] childAffected;
    // a canvas aligner, also used to show and hide canvases
    public static View GetCurrent()
    {
        return current;
    }

    public void Awake()
    {
        rectTransform = (RectTransform)transform;
        canvas = GetComponent<Canvas>();
        raycaster = GetComponent<GraphicRaycaster>();

        canvas.enabled = false;
        if (raycaster != null)
        {
            raycaster.enabled = false;
        }
        rectTransform.offsetMin = new Vector2(0, 0);
        rectTransform.offsetMax = new Vector2(0, 0);
        if (showOnAwake)
            Show();
    }

    public bool isVisible()
    {
        return canvas.enabled;
    }

    public Canvas GetCanvas()
    {
        return canvas;
    }

    public void Next(View view)
    {
        Hide();
        view.Show();
        view.previous = this;
        current = view;
    }

    public void Switch(View view)
    {
        Hide();
        view.Show();
        current = view;
    }

    public View Back()
    {
        if (parent.Length > 0)
        {
            foreach (View view in parent)
            {
                view.Back();
            }
        }
        else if (previous)
        {
            Hide();
            previous.Show();
            current = previous;
        }

        return current;
    }

    public void Hide()
    {
        onHide?.Invoke();
        canvas.enabled = false;
        if (raycaster != null)
        {
            raycaster.enabled = false;
        }
        if (childAffected == null)
        {
            foreach (View view in children)
            {
                if (view)
                    view.Hide();
            }
        }
        else
        {
            foreach (int childIndex in childAffected)
            {
                children[childIndex].Hide();
            }
        }
    }

    public void Show()
    {
            onShow?.Invoke();
            canvas.enabled = true;
            if (raycaster != null)
            {
                raycaster.enabled = true;
            }
            if (childAffected == null)
            {
                foreach (View view in children)
                {
                    if (view)
                        view.Show();
                }
            }
            else
            {
                foreach (int childIndex in childAffected)
                {
                    children[childIndex].Show();
                }
            }
    }

    public void setChildAffected(int[] childAffected)
    {
        this.childAffected = childAffected;
    }
}
