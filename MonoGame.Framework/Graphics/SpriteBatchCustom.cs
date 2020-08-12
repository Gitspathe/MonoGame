using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using System.Numerics;

#if NETSTANDARD2_1
using MathF = System.MathF;
#else
using MathF = System.Math;
#endif

namespace Microsoft.Xna.Framework.Graphics
{
    public partial class SpriteBatch
    {

        /// <summary>
        /// Submit a sprite for drawing in the current batch, using System.Numerics for the Vectors.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="color">A color mask.</param>
        /// <param name="rotation">A rotation of this sprite.</param>
        /// <param name="origin">Center of the rotation. 0,0 by default.</param>
        /// <param name="scale">A scaling of this sprite.</param>
        /// <param name="layerDepth">A depth of the layer of this sprite.</param>
        public void Draw(Texture2D texture,
            System.Numerics.Vector2 position,
            Color color,
            float rotation,
            System.Numerics.Vector2 origin,
            System.Numerics.Vector2 scale,
            float layerDepth)
        {
            if (texture == null)
                throw new ArgumentNullException("texture");
            if (!_beginCalled)
                throw new InvalidOperationException(
                    "Draw was called, but Begin has not yet been called. Begin must be called successfully before you can call Draw.");

            var item = _batcher.CreateBatchItem();
            item.Texture = texture;

            // set SortKey based on SpriteSortMode.
            switch (_sortMode) {
                // Comparison of Texture objects.
                case SpriteSortMode.Texture:
                    item.SortKey = texture.SortingKey;
                    break;
                // Comparison of Depth
                case SpriteSortMode.FrontToBack:
                    item.SortKey = layerDepth;
                    break;
                // Comparison of Depth in reverse
                case SpriteSortMode.BackToFront:
                    item.SortKey = -layerDepth;
                    break;
                case SpriteSortMode.Immediate:
                    throw new Exception("Immediate sorting mode isn't support with this spriteBatch overload.");
            }

            origin *= scale;

            float w = texture.Width * scale.X;
            float h = texture.Height * scale.Y;
            _texCoordTL = Vector2.Zero;
            _texCoordBR = Vector2.One;

            if (rotation == 0f) {
                item.Set(position.X - origin.X,
                    position.Y - origin.Y,
                    w,
                    h,
                    color,
                    _texCoordTL,
                    _texCoordBR,
                    layerDepth);
            } else {
                item.Set(position.X,
                    position.Y,
                    -origin.X,
                    -origin.Y,
                    w,
                    h,
                #if NETSTANDARD2_1
                    MathF.Sin(rotation),
                    MathF.Cos(rotation),
                #else
                    (float) MathF.Sin(rotation),
                    (float) MathF.Cos(rotation),
                #endif
                    color,
                    _texCoordTL,
                    _texCoordBR,
                    layerDepth);
            }
        }

        /// <summary>
        /// Submit a sprite for drawing in the current batch, using System.Numerics for the Vectors.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="color">A color mask.</param>
        /// <param name="rotation">A rotation of this sprite.</param>
        /// <param name="origin">Center of the rotation. 0,0 by default.</param>
        /// <param name="scale">A scaling of this sprite.</param>
        /// <param name="layerDepth">A depth of the layer of this sprite.</param>
        public void DrawDeferred(Texture2D texture,
            System.Numerics.Vector2 position,
            Color color,
            float rotation,
            System.Numerics.Vector2 origin,
            System.Numerics.Vector2 scale,
            float layerDepth)
        {
            if (texture == null)
                throw new ArgumentNullException("texture");
            if (!_beginCalled)
                throw new InvalidOperationException(
                    "Draw was called, but Begin has not yet been called. Begin must be called successfully before you can call Draw.");

            var item = _batcher.CreateBatchItem();
            item.Texture = texture;

            origin *= scale;

            float w = texture.Width * scale.X;
            float h = texture.Height * scale.Y;
            _texCoordTL = Vector2.Zero;
            _texCoordBR = Vector2.One;

            if (rotation == 0f) {
                item.Set(position.X - origin.X,
                    position.Y - origin.Y,
                    w,
                    h,
                    color,
                    _texCoordTL,
                    _texCoordBR,
                    layerDepth);
            } else {
                item.Set(position.X,
                    position.Y,
                    -origin.X,
                    -origin.Y,
                    w,
                    h,
#if NETSTANDARD2_1
                    MathF.Sin(rotation),
                    MathF.Cos(rotation),
#else
                    (float)MathF.Sin(rotation),
                    (float)MathF.Cos(rotation),
#endif
                    color,
                    _texCoordTL,
                    _texCoordBR,
                    layerDepth);
            }
        }

        private object batchLock = new object();

        /// <summary>
        /// Submit a sprite for drawing in the current batch, using System.Numerics for the Vectors.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="color">A color mask.</param>
        /// <param name="rotation">A rotation of this sprite.</param>
        /// <param name="origin">Center of the rotation. 0,0 by default.</param>
        /// <param name="scale">A scaling of this sprite.</param>
        /// <param name="layerDepth">A depth of the layer of this sprite.</param>
        public void DrawDeferred(Texture2D texture,
            System.Numerics.Vector2 position,
            Rectangle sourceRectangle,
            Color color,
            float rotation,
            System.Numerics.Vector2 origin,
            System.Numerics.Vector2 scale,
            float layerDepth)
        {
            if (texture == null)
                throw new ArgumentNullException("texture");
            if (!_beginCalled)
                throw new InvalidOperationException(
                    "Draw was called, but Begin has not yet been called. Begin must be called successfully before you can call Draw.");


            var item = _batcher.CreateBatchItem();
            item.Texture = texture;

            origin *= scale;

            float w = texture.Width * scale.X;
            float h = texture.Height * scale.Y;
            _texCoordTL = Vector2.Zero;
            _texCoordBR = Vector2.One;

            var srcRect = sourceRectangle;
            w = srcRect.Width * scale.X;
            h = srcRect.Height * scale.Y;
            _texCoordTL.X = srcRect.X * texture.TexelWidth;
            _texCoordTL.Y = srcRect.Y * texture.TexelHeight;
            _texCoordBR.X = (srcRect.X + srcRect.Width) * texture.TexelWidth;
            _texCoordBR.Y = (srcRect.Y + srcRect.Height) * texture.TexelHeight;

            if (rotation == 0f) {
                item.Set(position.X - origin.X,
                    position.Y - origin.Y,
                    w,
                    h,
                    color,
                    _texCoordTL,
                    _texCoordBR,
                    layerDepth);
            } else {
                item.Set(position.X,
                    position.Y,
                    -origin.X,
                    -origin.Y,
                    w,
                    h,
#if NETSTANDARD2_1
                    MathF.Sin(rotation),
                    MathF.Cos(rotation),
#else
                    (float)MathF.Sin(rotation),
                    (float)MathF.Cos(rotation),
#endif
                    color,
                    _texCoordTL,
                    _texCoordBR,
                    layerDepth);
            }
        }

        /// <summary>
        /// Submit a sprite for drawing in the current batch, using System.Numerics for the Vectors.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="sourceRectangle">An optional region on the texture which will be rendered. If null - draws full texture.</param>
        /// <param name="color">A color mask.</param>
        /// <param name="rotation">A rotation of this sprite.</param>
        /// <param name="origin">Center of the rotation. 0,0 by default.</param>
        /// <param name="scale">A scaling of this sprite.</param>
        /// <param name="effects">Modificators for drawing. Can be combined.</param>
        /// <param name="layerDepth">A depth of the layer of this sprite.</param>
		public void Draw(Texture2D texture,
                System.Numerics.Vector2 position,
                Rectangle? sourceRectangle,
                Color color,
                float rotation,
                System.Numerics.Vector2 origin,
                System.Numerics.Vector2 scale,
                SpriteEffects effects,
                float layerDepth)
        {
            if (texture == null)
                throw new ArgumentNullException("texture");
            if (!_beginCalled)
                throw new InvalidOperationException("Draw was called, but Begin has not yet been called. Begin must be called successfully before you can call Draw.");

            var item = _batcher.CreateBatchItem();
            item.Texture = texture;

            // set SortKey based on SpriteSortMode.
            switch (_sortMode)
            {
                // Comparison of Texture objects.
                case SpriteSortMode.Texture:
                    item.SortKey = texture.SortingKey;
                    break;
                // Comparison of Depth
                case SpriteSortMode.FrontToBack:
                    item.SortKey = layerDepth;
                    break;
                // Comparison of Depth in reverse
                case SpriteSortMode.BackToFront:
                    item.SortKey = -layerDepth;
                    break;
            }

            origin = origin * scale;

            float w, h;
            if (sourceRectangle.HasValue)
            {
                var srcRect = sourceRectangle.GetValueOrDefault();
                w = srcRect.Width * scale.X;
                h = srcRect.Height * scale.Y;
                _texCoordTL.X = srcRect.X * texture.TexelWidth;
                _texCoordTL.Y = srcRect.Y * texture.TexelHeight;
                _texCoordBR.X = (srcRect.X + srcRect.Width) * texture.TexelWidth;
                _texCoordBR.Y = (srcRect.Y + srcRect.Height) * texture.TexelHeight;
            }
            else
            {
                w = texture.Width * scale.X;
                h = texture.Height * scale.Y;
                _texCoordTL = Vector2.Zero;
                _texCoordBR = Vector2.One;
            }

            if ((effects & SpriteEffects.FlipVertically) != 0) {
                var temp = _texCoordBR.Y;
                _texCoordBR.Y = _texCoordTL.Y;
                _texCoordTL.Y = temp;
            }
            if ((effects & SpriteEffects.FlipHorizontally) != 0) {
                var temp = _texCoordBR.X;
                _texCoordBR.X = _texCoordTL.X;
                _texCoordTL.X = temp;
            }

            if (rotation == 0f) {
                item.Set(position.X - origin.X,
                    position.Y - origin.Y,
                    w,
                    h,
                    color,
                    _texCoordTL,
                    _texCoordBR,
                    layerDepth);
            } else {
                item.Set(position.X,
                    position.Y,
                    -origin.X,
                    -origin.Y,
                    w,
                    h,
#if NETSTANDARD2_1
                    MathF.Sin(rotation),
                    MathF.Cos(rotation),
#else
                    (float)MathF.Sin(rotation),
                    (float)MathF.Cos(rotation),
#endif
                    color,
                    _texCoordTL,
                    _texCoordBR,
                    layerDepth);
            }

            FlushIfNeeded();
        }

        /// <summary>
        /// Submit a sprite for drawing in the current batch.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="destinationRectangle">The drawing bounds on screen.</param>
        /// <param name="sourceRectangle">An optional region on the texture which will be rendered. If null - draws full texture.</param>
        /// <param name="color">A color mask.</param>
        /// <param name="rotation">A rotation of this sprite.</param>
        /// <param name="origin">Center of the rotation. 0,0 by default.</param>
        /// <param name="effects">Modificators for drawing. Can be combined.</param>
        /// <param name="layerDepth">A depth of the layer of this sprite.</param>
		public void Draw(Texture2D texture,
            Rectangle destinationRectangle,
            Rectangle? sourceRectangle,
            Color color,
            float rotation,
            System.Numerics.Vector2 origin,
            SpriteEffects effects,
            float layerDepth)
        {
            if (texture == null)
                throw new ArgumentNullException("texture");
            if (!_beginCalled)
                throw new InvalidOperationException("Draw was called, but Begin has not yet been called. Begin must be called successfully before you can call Draw.");

            var item = _batcher.CreateBatchItem();
            item.Texture = texture;

            // set SortKey based on SpriteSortMode.
            switch (_sortMode) {
                // Comparison of Texture objects.
                case SpriteSortMode.Texture:
                    item.SortKey = texture.SortingKey;
                    break;
                // Comparison of Depth
                case SpriteSortMode.FrontToBack:
                    item.SortKey = layerDepth;
                    break;
                // Comparison of Depth in reverse
                case SpriteSortMode.BackToFront:
                    item.SortKey = -layerDepth;
                    break;
            }

            if (sourceRectangle.HasValue) {
                var srcRect = sourceRectangle.GetValueOrDefault();
                _texCoordTL.X = srcRect.X * texture.TexelWidth;
                _texCoordTL.Y = srcRect.Y * texture.TexelHeight;
                _texCoordBR.X = (srcRect.X + srcRect.Width) * texture.TexelWidth;
                _texCoordBR.Y = (srcRect.Y + srcRect.Height) * texture.TexelHeight;

                if (srcRect.Width != 0)
                    origin.X = origin.X * destinationRectangle.Width / srcRect.Width;
                else
                    origin.X = origin.X * destinationRectangle.Width * texture.TexelWidth;
                if (srcRect.Height != 0)
                    origin.Y = origin.Y * destinationRectangle.Height / srcRect.Height;
                else
                    origin.Y = origin.Y * destinationRectangle.Height * texture.TexelHeight;
            } else {
                _texCoordTL = Vector2.Zero;
                _texCoordBR = Vector2.One;

                origin.X = origin.X * destinationRectangle.Width * texture.TexelWidth;
                origin.Y = origin.Y * destinationRectangle.Height * texture.TexelHeight;
            }

            if ((effects & SpriteEffects.FlipVertically) != 0) {
                var temp = _texCoordBR.Y;
                _texCoordBR.Y = _texCoordTL.Y;
                _texCoordTL.Y = temp;
            }
            if ((effects & SpriteEffects.FlipHorizontally) != 0) {
                var temp = _texCoordBR.X;
                _texCoordBR.X = _texCoordTL.X;
                _texCoordTL.X = temp;
            }

            if (rotation == 0f) {
                item.Set(destinationRectangle.X - origin.X,
                        destinationRectangle.Y - origin.Y,
                        destinationRectangle.Width,
                        destinationRectangle.Height,
                        color,
                        _texCoordTL,
                        _texCoordBR,
                        layerDepth);
            } else {
                item.Set(destinationRectangle.X,
                    destinationRectangle.Y,
                    -origin.X,
                    -origin.Y,
                    destinationRectangle.Width,
                    destinationRectangle.Height,
#if NETSTANDARD2_1
                    MathF.Sin(rotation),
                    MathF.Cos(rotation),
#else 
                    (float)MathF.Sin(rotation),
                    (float)MathF.Cos(rotation),
#endif
                    color,
                    _texCoordTL,
                    _texCoordBR,
                    layerDepth);
            }

            FlushIfNeeded();
        }

        /// <summary>
        /// Submit a text string of sprites for drawing in the current batch.
        /// </summary>
        /// <param name="spriteFont">A font.</param>
        /// <param name="text">The text which will be drawn.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="color">A color mask.</param>
        /// <param name="rotation">A rotation of this string.</param>
        /// <param name="origin">Center of the rotation. 0,0 by default.</param>
        /// <param name="scale">A scaling of this string.</param>
        /// <param name="effects">Modificators for drawing. Can be combined.</param>
        /// <param name="layerDepth">A depth of the layer of this string.</param>
		public unsafe void DrawString(
            SpriteFont spriteFont, string text, System.Numerics.Vector2 position, Color color,
            float rotation, System.Numerics.Vector2 origin, System.Numerics.Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            CheckValid(spriteFont, text);

            float sortKey = 0;
            // set SortKey based on SpriteSortMode.
            switch (_sortMode)
            {
                // Comparison of Texture objects.
                case SpriteSortMode.Texture:
                    sortKey = spriteFont.Texture.SortingKey;
                    break;
                // Comparison of Depth
                case SpriteSortMode.FrontToBack:
                    sortKey = layerDepth;
                    break;
                // Comparison of Depth in reverse
                case SpriteSortMode.BackToFront:
                    sortKey = -layerDepth;
                    break;
            }

            var flipAdjustment = Vector2.Zero;

            var flippedVert = (effects & SpriteEffects.FlipVertically) == SpriteEffects.FlipVertically;
            var flippedHorz = (effects & SpriteEffects.FlipHorizontally) == SpriteEffects.FlipHorizontally;

            if (flippedVert || flippedHorz) {
                Vector2 size;

                var source = new SpriteFont.CharacterSource(text);
                spriteFont.MeasureString(ref source, out size);

                if (flippedHorz) {
                    origin.X *= -1;
                    flipAdjustment.X = -size.X;
                }

                if (flippedVert) {
                    origin.Y *= -1;
                    flipAdjustment.Y = spriteFont.LineSpacing - size.Y;
                }
            }

            Matrix transformation = Matrix.Identity;
            float cos = 0, sin = 0;
            if (rotation == 0) {
                transformation.M11 = (flippedHorz ? -scale.X : scale.X);
                transformation.M22 = (flippedVert ? -scale.Y : scale.Y);
                transformation.M41 = ((flipAdjustment.X - origin.X) * transformation.M11) + position.X;
                transformation.M42 = ((flipAdjustment.Y - origin.Y) * transformation.M22) + position.Y;
            } else {
#if NETSTANDARD2_1
                cos = MathF.Cos(rotation);
                sin = MathF.Sin(rotation);
#else
                cos = (float)MathF.Cos(rotation);
                sin = (float)MathF.Sin(rotation);
#endif
                transformation.M11 = (flippedHorz ? -scale.X : scale.X) * cos;
                transformation.M12 = (flippedHorz ? -scale.X : scale.X) * sin;
                transformation.M21 = (flippedVert ? -scale.Y : scale.Y) * (-sin);
                transformation.M22 = (flippedVert ? -scale.Y : scale.Y) * cos;
                transformation.M41 = (((flipAdjustment.X - origin.X) * transformation.M11) + (flipAdjustment.Y - origin.Y) * transformation.M21) + position.X;
                transformation.M42 = (((flipAdjustment.X - origin.X) * transformation.M12) + (flipAdjustment.Y - origin.Y) * transformation.M22) + position.Y;
            }

            var offset = Vector2.Zero;
            var firstGlyphOfLine = true;

            fixed (SpriteFont.Glyph* pGlyphs = spriteFont.Glyphs)
                for (var i = 0; i < text.Length; ++i) {
                    var c = text[i];

                    if (c == '\r')
                        continue;

                    if (c == '\n') {
                        offset.X = 0;
                        offset.Y += spriteFont.LineSpacing;
                        firstGlyphOfLine = true;
                        continue;
                    }

                    var currentGlyphIndex = spriteFont.GetGlyphIndexOrDefault(c);
                    var pCurrentGlyph = pGlyphs + currentGlyphIndex;

                    // The first character on a line might have a negative left side bearing.
                    // In this scenario, SpriteBatch/SpriteFont normally offset the text to the right,
                    //  so that text does not hang off the left side of its rectangle.
                    if (firstGlyphOfLine) {
                        offset.X = MathF.Max(pCurrentGlyph->LeftSideBearing, 0);
                        firstGlyphOfLine = false;
                    } else {
                        offset.X += spriteFont.Spacing + pCurrentGlyph->LeftSideBearing;
                    }

                    var p = offset;

                    if (flippedHorz)
                        p.X += pCurrentGlyph->BoundsInTexture.Width;
                    p.X += pCurrentGlyph->Cropping.X;

                    if (flippedVert)
                        p.Y += pCurrentGlyph->BoundsInTexture.Height - spriteFont.LineSpacing;
                    p.Y += pCurrentGlyph->Cropping.Y;

                    Vector2.Transform(ref p, ref transformation, out p);

                    var item = _batcher.CreateBatchItem();
                    item.Texture = spriteFont.Texture;
                    item.SortKey = sortKey;

                    _texCoordTL.X = pCurrentGlyph->BoundsInTexture.X * spriteFont.Texture.TexelWidth;
                    _texCoordTL.Y = pCurrentGlyph->BoundsInTexture.Y * spriteFont.Texture.TexelHeight;
                    _texCoordBR.X = (pCurrentGlyph->BoundsInTexture.X + pCurrentGlyph->BoundsInTexture.Width) * spriteFont.Texture.TexelWidth;
                    _texCoordBR.Y = (pCurrentGlyph->BoundsInTexture.Y + pCurrentGlyph->BoundsInTexture.Height) * spriteFont.Texture.TexelHeight;

                    if ((effects & SpriteEffects.FlipVertically) != 0) {
                        var temp = _texCoordBR.Y;
                        _texCoordBR.Y = _texCoordTL.Y;
                        _texCoordTL.Y = temp;
                    }
                    if ((effects & SpriteEffects.FlipHorizontally) != 0) {
                        var temp = _texCoordBR.X;
                        _texCoordBR.X = _texCoordTL.X;
                        _texCoordTL.X = temp;
                    }

                    if (rotation == 0f) {
                        item.Set(p.X,
                            p.Y,
                            pCurrentGlyph->BoundsInTexture.Width * scale.X,
                            pCurrentGlyph->BoundsInTexture.Height * scale.Y,
                            color,
                            _texCoordTL,
                            _texCoordBR,
                            layerDepth);
                    } else {
                        item.Set(p.X,
                            p.Y,
                            0,
                            0,
                            pCurrentGlyph->BoundsInTexture.Width * scale.X,
                            pCurrentGlyph->BoundsInTexture.Height * scale.Y,
                            sin,
                            cos,
                            color,
                            _texCoordTL,
                            _texCoordBR,
                            layerDepth);
                    }

                    offset.X += pCurrentGlyph->Width + pCurrentGlyph->RightSideBearing;
                }

            // We need to flush if we're using Immediate sort mode.
            FlushIfNeeded();
        }

        /// <summary>
        /// Submit a text string of sprites for drawing in the current batch.
        /// </summary>
        /// <param name="spriteFont">A font.</param>
        /// <param name="text">The text which will be drawn.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="color">A color mask.</param>
		public unsafe void DrawString(SpriteFont spriteFont, StringBuilder text, System.Numerics.Vector2 position, Color color)
        {
            CheckValid(spriteFont, text);

            float sortKey = (_sortMode == SpriteSortMode.Texture) ? spriteFont.Texture.SortingKey : 0;

            var offset = Vector2.Zero;
            var firstGlyphOfLine = true;

            fixed (SpriteFont.Glyph* pGlyphs = spriteFont.Glyphs)
                for (var i = 0; i < text.Length; ++i) {
                    var c = text[i];

                    if (c == '\r')
                        continue;

                    if (c == '\n') {
                        offset.X = 0;
                        offset.Y += spriteFont.LineSpacing;
                        firstGlyphOfLine = true;
                        continue;
                    }

                    var currentGlyphIndex = spriteFont.GetGlyphIndexOrDefault(c);
                    var pCurrentGlyph = pGlyphs + currentGlyphIndex;

                    // The first character on a line might have a negative left side bearing.
                    // In this scenario, SpriteBatch/SpriteFont normally offset the text to the right,
                    //  so that text does not hang off the left side of its rectangle.
                    if (firstGlyphOfLine) {
                        offset.X = MathF.Max(pCurrentGlyph->LeftSideBearing, 0);
                        firstGlyphOfLine = false;
                    } else {
                        offset.X += spriteFont.Spacing + pCurrentGlyph->LeftSideBearing;
                    }

                    var p = offset;
                    p.X += pCurrentGlyph->Cropping.X;
                    p.Y += pCurrentGlyph->Cropping.Y;
                    p += new Vector2(position.X, position.Y);

                    var item = _batcher.CreateBatchItem();
                    item.Texture = spriteFont.Texture;
                    item.SortKey = sortKey;

                    _texCoordTL.X = pCurrentGlyph->BoundsInTexture.X * spriteFont.Texture.TexelWidth;
                    _texCoordTL.Y = pCurrentGlyph->BoundsInTexture.Y * spriteFont.Texture.TexelHeight;
                    _texCoordBR.X = (pCurrentGlyph->BoundsInTexture.X + pCurrentGlyph->BoundsInTexture.Width) * spriteFont.Texture.TexelWidth;
                    _texCoordBR.Y = (pCurrentGlyph->BoundsInTexture.Y + pCurrentGlyph->BoundsInTexture.Height) * spriteFont.Texture.TexelHeight;

                    item.Set(p.X,
                        p.Y,
                        pCurrentGlyph->BoundsInTexture.Width,
                        pCurrentGlyph->BoundsInTexture.Height,
                        color,
                        _texCoordTL,
                        _texCoordBR,
                        0);

                    offset.X += pCurrentGlyph->Width + pCurrentGlyph->RightSideBearing;
                }

            // We need to flush if we're using Immediate sort mode.
            FlushIfNeeded();
        }

    }
}
