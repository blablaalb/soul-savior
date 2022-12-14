using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;

namespace Characters.Players
{
    public class Player : MonoBehaviour
    {
        public async void GoToIsland(Island island)
        {
            float moveTime = 4f;
            await transform.DOMove(island.IslandJumpPosition.position, moveTime);
            float jumpDuration = 1f;
            await transform.DOJump(island.IslandLandPosition.position, jumpPower: 2f, numJumps: 1, jumpDuration);
        }
    }
}