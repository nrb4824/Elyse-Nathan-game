#ifndef COLOR_PALETTE_INCLUDED
#define COLOR_PALETTE_INCLUDED

///////////////////////////////////////////////////////////////////

float ColorPalette (Texture2D palette, float x, float y, out float4 pixel_colour) {

	pixel_colour = palette.GetPixel(1, 5, 0);

}

#endif