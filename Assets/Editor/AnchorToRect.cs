using UnityEngine;
using UnityEditor;

public class AnchorToRect
{
    [MenuItem("GameObject/UI/Fit Anchors to Rect")]
    static void FitAnchorsToRect()
    {
        foreach (var go in Selection.gameObjects)
        {
            var rt = go.GetComponent<RectTransform>();
            if (rt == null) continue;

            var parent = rt.parent as RectTransform;
            if (parent == null) continue;

            Undo.RecordObject(rt, "Fit Anchors to Rect");

            var parentSize = parent.rect.size;

            // 현재 앵커 기준 오프셋 → 실제 픽셀 위치로 변환
            float anchorMinX = rt.anchorMin.x + rt.offsetMin.x / parentSize.x;
            float anchorMinY = rt.anchorMin.y + rt.offsetMin.y / parentSize.y;
            float anchorMaxX = rt.anchorMax.x + rt.offsetMax.x / parentSize.x;
            float anchorMaxY = rt.anchorMax.y + rt.offsetMax.y / parentSize.y;

            rt.anchorMin = new Vector2(anchorMinX, anchorMinY);
            rt.anchorMax = new Vector2(anchorMaxX, anchorMaxY);

            // 오프셋 0으로 리셋
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;
        }
    }

    [MenuItem("GameObject/UI/Fit Anchors to Rect %&a", true)]
    static bool Validate() => Selection.activeGameObject != null;

    [MenuItem("GameObject/UI/Center Anchor to Rect")]
    static void CenterAnchorToRect()
    {
        foreach (var go in Selection.gameObjects)
        {
            var rt = go.GetComponent<RectTransform>();
            if (rt == null) continue;

            var parent = rt.parent as RectTransform;
            if (parent == null) continue;

            Undo.RecordObject(rt, "Center Anchor to Rect");

            var parentSize = parent.rect.size;

            // 현재 앵커 중심점
            Vector2 oldAnchorCenter = (rt.anchorMin + rt.anchorMax) / 2f;

            // 새 앵커 중심점 (이미지 중심)
            float centerX = rt.anchorMin.x + (rt.offsetMin.x + rt.rect.width  * rt.pivot.x) / parentSize.x;
            float centerY = rt.anchorMin.y + (rt.offsetMin.y + rt.rect.height * rt.pivot.y) / parentSize.y;
            Vector2 newAnchorCenter = new Vector2(centerX, centerY);

            // 앵커가 이동한 만큼 오프셋을 반대로 보정
            Vector2 anchorDelta = (newAnchorCenter - oldAnchorCenter) * parentSize;

            rt.anchorMin = newAnchorCenter;
            rt.anchorMax = newAnchorCenter;

            rt.offsetMin -= anchorDelta;
            rt.offsetMax -= anchorDelta;
        }
    }

    [MenuItem("GameObject/UI/Center Anchor to Rect", true)]
    static bool ValidateCenter() => Selection.activeGameObject != null;
}
