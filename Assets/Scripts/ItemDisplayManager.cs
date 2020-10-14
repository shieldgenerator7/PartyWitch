using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using UnityEngine;

/// <summary>
/// Manages the display of the current items
/// </summary>
public class ItemDisplayManager : VariableListener
{
    public GameObject containerPrefab;
    public SpriteBank spriteBank;

    public float startX = -49;
    public float bufferX = -120;

    private List<ItemDisplayContainer> containers = new List<ItemDisplayContainer>();

    protected override bool isTargetVariable(string varName)
        => spriteBank.Contains(varName);
    protected override void checkVariable(string varName, int oldValue, int newValue)
    {
        if (!containers.Any(c => c.targetVariable == varName))
        {
            ItemDisplayContainer container = createDisplayContainer(varName, newValue);
            containers.Add(container);
            container.transform.SetParent(transform, true);
            sortContainers();
        }
        if (newValue <= 0)
        {
            ItemDisplayContainer idc = containers.First(c => c.targetVariable == varName);
            containers.Remove(idc);
            Destroy(idc.gameObject);
            sortContainers();
        }
    }

    private ItemDisplayContainer createDisplayContainer(string varName, int value)
    {
        GameObject container = Instantiate(containerPrefab);
        ItemDisplayContainer idc = container.GetComponent<ItemDisplayContainer>();
        idc.targetVariable = varName;
        idc.Sprite = spriteBank.getSprite(varName);
        idc.updateText(value);
        return idc;
    }

    private void sortContainers()
    {
        containers.OrderBy(c => c.targetVariable);
        float nextPosX = startX;
        containers.ForEach(
            c =>
            {
                RectTransform rectTransform = c.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(
                    nextPosX,
                    rectTransform.anchoredPosition.y
                    );
                nextPosX += bufferX;
            }
            );
    }

}
