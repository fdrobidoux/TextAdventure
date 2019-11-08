using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using static SadConsole.FontMaster;

namespace SadConsole
{
    public static class FontMasterExtensions
    {
        public static IEnumerable<GlyphDefinition> ListGlyphDefinitions(this FontMaster _thisFM, string basename, SpriteEffects? mirrorMode = null)
        {
            int? frameIndex = null;
            string currentName;
            List<GlyphDefinition> outList = new List<GlyphDefinition>();
            bool useMirror = mirrorMode.HasValue;

            while (_thisFM.HasGlyphDefinition(currentName = basename + frameIndex))
            {
                GlyphDefinition glyph = _thisFM.GetGlyphDefinition(currentName);
                yield return mirrorMode.HasValue
                    ? new GlyphDefinition(glyph.Glyph, mirrorMode.Value) 
                    : glyph;
                frameIndex = (frameIndex == null ? 1 : ++frameIndex);
            }
        }
    }
}
