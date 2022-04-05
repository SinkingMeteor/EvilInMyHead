using System;
using Sheldier.Actors;
using Sheldier.Constants;
using Sheldier.Data;
using UnityEngine;

namespace Sheldier.Common
{
    public static class Extensions
    {
        public static ActorDirectionView GetViewDirection(this Vector2 cursorDirection)
        {
            var dotProduct = Vector2.Dot(Vector2.up, cursorDirection);
            if (dotProduct < -0.7f)
                return ActorDirectionView.Front;
            if (dotProduct < 0.2f)
                return ActorDirectionView.FrontSide;
            if (dotProduct < 0.87f)
                return ActorDirectionView.BackSide;
            else
                return ActorDirectionView.Back;
        }

        public static Vector2 UnitVectorFromSegment(this int segmentIndex, int totalSegments)
        {
            float anglePerSegment = MathConstants.TAU / totalSegments;
            var segmentCenterAngle = anglePerSegment / 2;
            var currentSegmentCenter = segmentCenterAngle + anglePerSegment * segmentIndex;
            return new Vector2(-1.0f * Mathf.Cos(currentSegmentCenter), -1.0f * Mathf.Sin(currentSegmentCenter));
        }

        public static void FillDatabase<T>(this Database<T> database, TextAsset textAsset) where T : IDatabaseItem
        {
            T[] itemArrays = JsonHelper.FromJson<T>(textAsset.text);
            if (itemArrays == null)
                throw new NullReferenceException($"Items of type {typeof(T)} can't be loaded and added to database");
            for (int i = 0; i < itemArrays.Length; i++)
                database.Add(itemArrays[i].ID, itemArrays[i]);
        }
    }

}
