using System.Drawing;

namespace Parallax;

public class TilesetColorPaletteSystem
{
    public Color[] CornerColors;
    public Color[] EdgeVerticalColors;
    public Color[] EdgeHorizontalColors;

    public void InitStage1()
    {
        
    }

    public void InitStage2()
    {
        // Setup the colors
        SetupColors();
    }

    public int GetCornerColorId(int id)
    {
        return id % CornerColors.Length;
    }


    public int GetEdgeVerticalColorId(int id)
    {
        return id % EdgeVerticalColors.Length;
    }
    
    
    public int GetEdgeHorizontalColorId(int id)
    {
        return id % EdgeHorizontalColors.Length;
    }

    public Color GetCornerColor(int colorId)
    {
        if (colorId >= 0 && colorId < CornerColors.Length)
        {
            return CornerColors[colorId];
        }

        return Color.FromArgb(0, 0, 0);
    }
    
    public Color GetEdgeVerticalColor(int colorId)
    {
        if (colorId >= 0 && colorId < EdgeVerticalColors.Length)
        {
            return EdgeVerticalColors[colorId];
        }

        return Color.FromArgb(0, 0, 0);
    }
    
    public Color GetEdgeHorizontalColor(int colorId)
    {
        if (colorId >= 0 && colorId < EdgeHorizontalColors.Length)
        {
            return EdgeHorizontalColors[colorId];
        }

        return Color.FromArgb(0, 0, 0);
    }
    
    public void SetupColors()
    {
        CornerColors = new Color[16];
        EdgeVerticalColors = new Color[16];
        EdgeHorizontalColors = new Color[16];

        
        // https://lospec.com/palette-list/nebulaspace
        CornerColors[0] = ColorTranslator.FromHtml("#0a401a");
        CornerColors[1] = ColorTranslator.FromHtml("#6d852c");
        CornerColors[2] = ColorTranslator.FromHtml("#b3a724");
        CornerColors[3] = ColorTranslator.FromHtml("#e6eb6a");
        CornerColors[4] = ColorTranslator.FromHtml("#ede8e1");
        CornerColors[5] = ColorTranslator.FromHtml("#a7dbbb");
        CornerColors[6] = ColorTranslator.FromHtml("#5d858c");
        CornerColors[7] = ColorTranslator.FromHtml("#3d476e");
        CornerColors[8] = ColorTranslator.FromHtml("#32244d");
        CornerColors[9] = ColorTranslator.FromHtml("#27142b");
        CornerColors[10] = ColorTranslator.FromHtml("#d6c2ba");
        CornerColors[11] = ColorTranslator.FromHtml("#bf9684");
        CornerColors[12] = ColorTranslator.FromHtml("#a66372");
        CornerColors[13] = ColorTranslator.FromHtml("#733754");
        CornerColors[14] = ColorTranslator.FromHtml("#451e3e");
        CornerColors[15] = ColorTranslator.FromHtml("#2e0f29");


        // https://lospec.com/palette-list/autumn-thanksgiving-meal
        EdgeVerticalColors[0] = ColorTranslator.FromHtml("#150102");
        EdgeVerticalColors[1] = ColorTranslator.FromHtml("#eeeae7");
        EdgeVerticalColors[2] = ColorTranslator.FromHtml("#8d3726");
        EdgeVerticalColors[3] = ColorTranslator.FromHtml("#aa4100");
        EdgeVerticalColors[4] = ColorTranslator.FromHtml("#b3947f");
        EdgeVerticalColors[5] = ColorTranslator.FromHtml("#da5a01");
        EdgeVerticalColors[6] = ColorTranslator.FromHtml("#f2970a");
        EdgeVerticalColors[7] = ColorTranslator.FromHtml("#f9bf16");
        EdgeVerticalColors[8] = ColorTranslator.FromHtml("#9fa01e");
        EdgeVerticalColors[9] = ColorTranslator.FromHtml("#4c5400");
        EdgeVerticalColors[10] = ColorTranslator.FromHtml("#202914");
        EdgeVerticalColors[11] = ColorTranslator.FromHtml("#616378");
        EdgeVerticalColors[12] = ColorTranslator.FromHtml("#5b495f");
        EdgeVerticalColors[13] = ColorTranslator.FromHtml("#464445");
        EdgeVerticalColors[14] = ColorTranslator.FromHtml("#a70f24");
        EdgeVerticalColors[15] = ColorTranslator.FromHtml("#bf565b");
        
        
        // https://lospec.com/palette-list/coolours-31
        EdgeHorizontalColors[0] = ColorTranslator.FromHtml("#e6b3ff");
        EdgeHorizontalColors[1] = ColorTranslator.FromHtml("#fa7dfa");
        EdgeHorizontalColors[2] = ColorTranslator.FromHtml("#c73c99");
        EdgeHorizontalColors[3] = ColorTranslator.FromHtml("#804075");
        EdgeHorizontalColors[4] = ColorTranslator.FromHtml("#b37daa");
        EdgeHorizontalColors[5] = ColorTranslator.FromHtml("#e6cfda");
        EdgeHorizontalColors[6] = ColorTranslator.FromHtml("#8c7062");
        EdgeHorizontalColors[7] = ColorTranslator.FromHtml("#b3d9ff");
        EdgeHorizontalColors[8] = ColorTranslator.FromHtml("#7d92fa");
        EdgeHorizontalColors[9] = ColorTranslator.FromHtml("#533cc7");
        EdgeHorizontalColors[10] = ColorTranslator.FromHtml("#6ac73c");
        EdgeHorizontalColors[11] = ColorTranslator.FromHtml("#0f940f");
        EdgeHorizontalColors[12] = ColorTranslator.FromHtml("#ffe6b3");
        EdgeHorizontalColors[13] = ColorTranslator.FromHtml("#f9ffb3");
        EdgeHorizontalColors[14] = ColorTranslator.FromHtml("#fadb7d");
        EdgeHorizontalColors[15] = ColorTranslator.FromHtml("#d0fa7d");
    }
}