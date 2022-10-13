using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Utilities;

namespace HCB.GridSystem
{
    public class PlaceableManager : Singleton<PlaceableManager>
    {
        private List<Behaviour> _placeBlockers = new List<Behaviour>();
        public List<Behaviour> PlaceBlockers { get => _placeBlockers; private set => _placeBlockers = value; }

        public void AddPlaceBlocker(Behaviour blocker)
        {
            if (PlaceBlockers.Contains(blocker))
                return;

            PlaceBlockers.Add(blocker);
        }

        public void RemovePlaceBlocker(Behaviour blocker)
        {
            if (!PlaceBlockers.Contains(blocker))
                return;

            PlaceBlockers.Remove(blocker);
        }
    }
}
