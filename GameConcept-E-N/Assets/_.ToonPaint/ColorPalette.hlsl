//#ifndef COLOR_PALETTE_INCLUDED
//#define COLOR_PALETTE_INCLUDED
//
/////////////////////////////////////////////////////////////////////
//
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
//
//void ColorPalette_float (UnityTexture2D ppalette, float xx, float yy, out float4 ppixel_colour) {
//
//	ppixel_colour = ppalette.SetPixel(xx, yy, 0);
//
//}
//
//#endif

//private Sprite mySprite

#ifndef GRAYSCALE_INCLUDED
#define GRAYSCALE_INCLUDED

void Grayscale_float(UnityTexture2D palette, float2 pos, out float4 output)
{
    output = palette.SetPixel(pos[0], pos[1]);
}

#endif // GRAYSCALE_INCLUDED