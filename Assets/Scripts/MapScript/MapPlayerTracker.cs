﻿using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Map
{
    public class MapPlayerTracker : MonoBehaviour
    {
        public bool lockAfterSelecting = false;
        public float enterNodeDelay = 1f;
        public MapManager mapManager;
        public MapView view;

        public static MapPlayerTracker Instance;

        public bool Locked { get; set; }

        private void Awake()
        {
            Instance = this;
        }

        public void SelectNode(MapNode mapNode)
        {
            if (Locked)
                return;

            // Debug.Log("Selected node: " + mapNode.Node.point);

            if (mapManager.CurrentMap.path.Count == 0)
            {
                // player has not selected the node yet, he can select any of the nodes with y = 0
                if (mapNode.Node.point.y == 0)
                    SendPlayerToNode(mapNode);
                else
                    PlayWarningThatNodeCannotBeAccessed();
            }
            else
            {
                var currentPoint = mapManager.CurrentMap.path[mapManager.CurrentMap.path.Count - 1];
                var currentNode = mapManager.CurrentMap.GetNode(currentPoint);

                if (
                    currentNode != null
                    && currentNode.outgoing.Any(point => point.Equals(mapNode.Node.point))
                )
                    SendPlayerToNode(mapNode);
                else
                    PlayWarningThatNodeCannotBeAccessed();
            }
        }

        private void SendPlayerToNode(MapNode mapNode)
        {
            Locked = lockAfterSelecting;
            mapManager.CurrentMap.path.Add(mapNode.Node.point);
            mapManager.SaveMap();
            view.SetAttainableNodes();
            view.SetLineColors();
            mapNode.ShowSwirlAnimation();

            DOTween.Sequence().AppendInterval(enterNodeDelay).OnComplete(() => EnterNode(mapNode));
        }

        private static void EnterNode(MapNode mapNode)
        {
            // we have access to blueprint name here as well
            Debug.Log(
                "Entering node: "
                    + mapNode.Node.blueprintName
                    + " of type: "
                    + mapNode.Node.nodeType
            );
            // load appropriate scene with context based on nodeType:
            // or show appropriate GUI over the map:
            // if you choose to show GUI in some of these cases, do not forget to set "Locked" in MapPlayerTracker back to false

            switch (mapNode.Node.nodeType)
            {
                case NodeType.MinorEnemy:
                    Debug.Log("Minor Enemy");
                    GameManager.instance.IsInElite = false;
                    GameManager.instance.IsInBoss = false;
                    ScenesManager.Instance.LoadScene(ScenesManager.Scene.CombatScene);
                    break;
                case NodeType.EliteEnemy:
                    Debug.Log("Elite Enemy");
                    GameManager.instance.IsInElite = true;
                    GameManager.instance.IsInBoss = false;
                    ScenesManager.Instance.LoadScene(ScenesManager.Scene.CombatScene);
                    break;
                case NodeType.RestSite:
                    Debug.Log("Rest Site");
                    ScenesManager.Instance.LoadScene(ScenesManager.Scene.RestScene);
                    break;
                case NodeType.Treasure:
                    Debug.Log("Treasure");
                    ScenesManager.Instance.LoadScene(ScenesManager.Scene.ShopScene);
                    break;
                case NodeType.Store:
                    Debug.Log("Store");
                    break;
                case NodeType.Boss:
                    Debug.Log("Boss");
                    GameManager.instance.IsInElite = false;
                    GameManager.instance.IsInBoss = true;
                    ScenesManager.Instance.LoadScene(ScenesManager.Scene.CombatScene);
                    break;
                case NodeType.Mystery:
                    Debug.Log("Mystery");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void PlayWarningThatNodeCannotBeAccessed()
        {
            Debug.Log("Selected node cannot be accessed");
        }
    }
}
