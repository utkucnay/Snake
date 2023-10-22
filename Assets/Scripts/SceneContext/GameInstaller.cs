using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] Bait baitPrefab;
    public override void InstallBindings()
    {
        Container.Bind<Tails>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Bait>().FromComponentInNewPrefab(baitPrefab).AsSingle().NonLazy();
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<HUDCanvasPresenter>().FromComponentInHierarchy().AsSingle();
    }
}
