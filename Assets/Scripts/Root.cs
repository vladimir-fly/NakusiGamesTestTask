using System.Collections.Generic;

using UnityEngine;

using NGTT.Systems;
using NGTT.Helpers;
using NGTT.Controllers;
using NGTT.Bases.Abstracts;

namespace NGTT
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private RootController _controller;
        [SerializeField] private Dispatcher _dispatcher;
        [SerializeField] private List<SystemBase> _systems;
        private void Start()
        {
            _controller = GameObjectFactory.Get<RootController>();
            _dispatcher = GameObjectFactory.Get<Dispatcher>();
            _dispatcher.Register(_controller);
            
            _systems = new List<SystemBase>
            {
                GameObjectFactory.Get<ProjectileSystem>(),
                GameObjectFactory.Get<UnitSystem>()
            };

            _systems.ForEach(_dispatcher.Register);
            
            Debug.Log(LogExtension.Bold("[Root][Start] Complete"));
        }
    }
}