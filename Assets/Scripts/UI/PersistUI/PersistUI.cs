using UnityEngine;

namespace Sheldier.UI
{
    public class PersistUI
    {
        public Fader Fader => _fader;
        public RectTransform WorldCanvasRectTransform => _worldCanvasRectTransform;

        private RectTransform _worldCanvasRectTransform;
        private Fader _fader;

        public void SetFader(Fader fader) => _fader = fader;

        public void SetWorldCanvas(RectTransform worldCanvas) => _worldCanvasRectTransform = worldCanvas;
    }
}