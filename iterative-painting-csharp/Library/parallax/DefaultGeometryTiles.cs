
using Enums;
using Utility;

namespace Parallax;

public class DefaultGeometryTiles
{
    public Dictionary<TileGeometryAndRotation, TilesetTileCenter> DefaultGeometryTileDictionary;
    
    
    public void InitStage1()
    {
        DefaultGeometryTileDictionary = new Dictionary<TileGeometryAndRotation, TilesetTileCenter>();
    }

    public void InitStage2()
    {
        LoadDefaultGeometryTiles();
    }

    public void AddGeometryTile(Enums.TileGeometryAndRotation geometryAndRotation, TilesetTileCenter tile)
    {
        // Make sure that we dont add the same geomtry twice
        if (DefaultGeometryTileDictionary.ContainsKey(geometryAndRotation))
        {
            KLog.LogDebug($"Default geometry tiles : geometry {geometryAndRotation} already exists ");
            return;
        }

        DefaultGeometryTileDictionary[geometryAndRotation] = tile;
    }

    public TilesetTileCenter GetGeometryTile(TileGeometryAndRotation geometryAndRotation)
    {
        // Make sure that the tile exists
        if (!DefaultGeometryTileDictionary.ContainsKey(geometryAndRotation))
        {
            // Returns null if there is no tile found
            return null;
        }

        return DefaultGeometryTileDictionary[geometryAndRotation];
    }
    
    public void AddDefaultGeometryTiles(ParallaxManager parallaxManager, UInt64 tilesetUuid)
    {
        TilesetInformation tilesetInformation = parallaxManager.GetTileset(tilesetUuid);

        if (tilesetInformation == null)
        {
            return;
        }
        
        TilesetTileCenter tileSB01 = GetGeometryTile(TileGeometryAndRotation.SB_R0);
        
        // ------------ HB ---------------
        TilesetTileCenter tileHBR0 = GetGeometryTile(TileGeometryAndRotation.HB_R0);
        TilesetTileCenter tileHBR1 = GetGeometryTile(TileGeometryAndRotation.HB_R1);
        TilesetTileCenter tileHBR2 = GetGeometryTile(TileGeometryAndRotation.HB_R2);
        TilesetTileCenter tileHBR3 = GetGeometryTile(TileGeometryAndRotation.HB_R3);
        
        // ------------ TB ---------------
        TilesetTileCenter tileTBR0 = GetGeometryTile(TileGeometryAndRotation.TB_R0);
        TilesetTileCenter tileTBR1 = GetGeometryTile(TileGeometryAndRotation.TB_R1);
        TilesetTileCenter tileTBR2 = GetGeometryTile(TileGeometryAndRotation.TB_R2);
        TilesetTileCenter tileTBR3 = GetGeometryTile(TileGeometryAndRotation.TB_R3);
        
       // ------------ L1 ---------------

       TilesetTileCenter tileL1R0 = GetGeometryTile(TileGeometryAndRotation.L1_R0);
       TilesetTileCenter tileL1R1 = GetGeometryTile(TileGeometryAndRotation.L1_R1);
       TilesetTileCenter tileL1R2 = GetGeometryTile(TileGeometryAndRotation.L1_R2);
       TilesetTileCenter tileL1R3 = GetGeometryTile(TileGeometryAndRotation.L1_R3);
       TilesetTileCenter tileL1R4 = GetGeometryTile(TileGeometryAndRotation.L1_R4);
       TilesetTileCenter tileL1R5 = GetGeometryTile(TileGeometryAndRotation.L1_R5);
       TilesetTileCenter tileL1R6 = GetGeometryTile(TileGeometryAndRotation.L1_R6);
       TilesetTileCenter tileL1R7 = GetGeometryTile(TileGeometryAndRotation.L1_R7);
                
        // ------------ L2 ---------------

        TilesetTileCenter tileL2R0 = GetGeometryTile(TileGeometryAndRotation.L2_R0);
        TilesetTileCenter tileL2R1 = GetGeometryTile(TileGeometryAndRotation.L2_R1);
        TilesetTileCenter tileL2R2 = GetGeometryTile(TileGeometryAndRotation.L2_R2);
        TilesetTileCenter tileL2R3 = GetGeometryTile(TileGeometryAndRotation.L2_R3);
        TilesetTileCenter tileL2R4 = GetGeometryTile(TileGeometryAndRotation.L2_R4);
        TilesetTileCenter tileL2R5 = GetGeometryTile(TileGeometryAndRotation.L2_R5);
        TilesetTileCenter tileL2R6 = GetGeometryTile(TileGeometryAndRotation.L2_R6);
        TilesetTileCenter tileL2R7 = GetGeometryTile(TileGeometryAndRotation.L2_R7);
        
        // --------- QP ------------------
        
        TilesetTileCenter tileQPR0 = GetGeometryTile(TileGeometryAndRotation.QP_R0);
        TilesetTileCenter tileQPR1 = GetGeometryTile(TileGeometryAndRotation.QP_R1);
        TilesetTileCenter tileQPR2 = GetGeometryTile(TileGeometryAndRotation.QP_R2);
        TilesetTileCenter tileQPR3 = GetGeometryTile(TileGeometryAndRotation.QP_R3);
        
        // ----------- HP ------------------

        TilesetTileCenter tileHPR0 = GetGeometryTile(TileGeometryAndRotation.HP_R0);
        TilesetTileCenter tileHPR1 = GetGeometryTile(TileGeometryAndRotation.HP_R1);
        TilesetTileCenter tileHPR2 = GetGeometryTile(TileGeometryAndRotation.HP_R2);
        TilesetTileCenter tileHPR3 = GetGeometryTile(TileGeometryAndRotation.HP_R3);
        
        // ----------- FP ------------------

        TilesetTileCenter tileFPR0 = GetGeometryTile(TileGeometryAndRotation.FP_R0);
        TilesetTileCenter tileFPR1 = GetGeometryTile(TileGeometryAndRotation.FP_R1);
        TilesetTileCenter tileFPR2 = GetGeometryTile(TileGeometryAndRotation.FP_R2);
        TilesetTileCenter tileFPR3 = GetGeometryTile(TileGeometryAndRotation.FP_R3);
        
        // TODO(): Add sanity check for tileset
        // TODO(): Make sure it does not have geometry tiles at this stage
        // TODO(): Make sure it does not use the same geometry tile ids
        
        parallaxManager.AddTileCenter(tilesetUuid, tileSB01);
        
        parallaxManager.AddTileCenter(tilesetUuid, tileHBR0);
        parallaxManager.AddTileCenter(tilesetUuid, tileHBR1);
        parallaxManager.AddTileCenter(tilesetUuid, tileHBR2);
        parallaxManager.AddTileCenter(tilesetUuid, tileHBR3);
        
        parallaxManager.AddTileCenter(tilesetUuid, tileTBR0);
        parallaxManager.AddTileCenter(tilesetUuid, tileTBR1);
        parallaxManager.AddTileCenter(tilesetUuid, tileTBR2);
        parallaxManager.AddTileCenter(tilesetUuid, tileTBR3);
        
        parallaxManager.AddTileCenter(tilesetUuid, tileL1R0);
        parallaxManager.AddTileCenter(tilesetUuid, tileL1R1);
        parallaxManager.AddTileCenter(tilesetUuid, tileL1R2);
        parallaxManager.AddTileCenter(tilesetUuid, tileL1R3);
        parallaxManager.AddTileCenter(tilesetUuid, tileL1R4);
        parallaxManager.AddTileCenter(tilesetUuid, tileL1R5);
        parallaxManager.AddTileCenter(tilesetUuid, tileL1R6);
        parallaxManager.AddTileCenter(tilesetUuid, tileL1R7);
        
        parallaxManager.AddTileCenter(tilesetUuid, tileL2R0);
        parallaxManager.AddTileCenter(tilesetUuid, tileL2R1);
        parallaxManager.AddTileCenter(tilesetUuid, tileL2R2);
        parallaxManager.AddTileCenter(tilesetUuid, tileL2R3);
        parallaxManager.AddTileCenter(tilesetUuid, tileL2R4);
        parallaxManager.AddTileCenter(tilesetUuid, tileL2R5);
        parallaxManager.AddTileCenter(tilesetUuid, tileL2R6);
        parallaxManager.AddTileCenter(tilesetUuid, tileL2R7);
        
        parallaxManager.AddTileCenter(tilesetUuid, tileQPR0);
        parallaxManager.AddTileCenter(tilesetUuid, tileQPR1);
        parallaxManager.AddTileCenter(tilesetUuid, tileQPR2);
        parallaxManager.AddTileCenter(tilesetUuid, tileQPR3);
        
        parallaxManager.AddTileCenter(tilesetUuid, tileHPR0);
        parallaxManager.AddTileCenter(tilesetUuid, tileHPR1);
        parallaxManager.AddTileCenter(tilesetUuid, tileHPR2);
        parallaxManager.AddTileCenter(tilesetUuid, tileHPR3);
        
        parallaxManager.AddTileCenter(tilesetUuid, tileFPR0);
        parallaxManager.AddTileCenter(tilesetUuid, tileFPR1);
        parallaxManager.AddTileCenter(tilesetUuid, tileFPR2);
        parallaxManager.AddTileCenter(tilesetUuid, tileFPR3);
    }

    public void LoadDefaultGeometryTiles()
    {

        const int StartingId = 1;

        UInt64 currentUuid = StartingId;
        // ------------ SB ---------------
        TilesetTileCenter tileSBR0 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.SB_R0,
            IsDefaultGeometryTile = true,
            StringId = "sb_r0"
        };
        currentUuid++;
        
        // ------------ HB ---------------
        TilesetTileCenter tileHBR0 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.HB_R0,
            IsDefaultGeometryTile = true,
            StringId = "hb_r0"
        };
        currentUuid++;
        
        TilesetTileCenter tileHBR1 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.HB_R1,
            IsDefaultGeometryTile = true,
            StringId = "hb_r1"
        };
        currentUuid++;
        
        TilesetTileCenter tileHBR2 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.HB_R2,
            IsDefaultGeometryTile = true,
            StringId = "hb_r2"
        };
        currentUuid++;
        
        TilesetTileCenter tileHBR3 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.HB_R3,
            IsDefaultGeometryTile = true,
            StringId = "hb_r3"
        };
        currentUuid++;
        
        // ------------ TB ---------------
        TilesetTileCenter tileTBR0 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.TB_R0,
            IsDefaultGeometryTile = true,
            StringId = "tb_r0"
        };
        currentUuid++;
        
        TilesetTileCenter tileTBR1 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.TB_R1,
            IsDefaultGeometryTile = true,
            StringId = "tb_r1"
        };
        currentUuid++;
        
        TilesetTileCenter tileTBR2 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.TB_R2,
            IsDefaultGeometryTile = true,
            StringId = "tb_r2"
        };
        currentUuid++;
        
        TilesetTileCenter tileTBR3 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.TB_R3,
            IsDefaultGeometryTile = true,
            StringId = "tb_r3"
        };
        currentUuid++;
        
       // ------------ L1 ---------------
        
        TilesetTileCenter tileL1R0 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.L1_R0,
            IsDefaultGeometryTile = true,
            StringId = "l1_r0"
        };
        currentUuid++;
        
        
        TilesetTileCenter tileL1R1 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.L1_R1,
            IsDefaultGeometryTile = true,
            StringId = "l1_r1"
        };
        currentUuid++;
        
        TilesetTileCenter tileL1R2 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.L1_R2,
            IsDefaultGeometryTile = true,
            StringId = "l1_r2"
        };
        currentUuid++;
        
        TilesetTileCenter tileL1R3 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.L1_R3,
            IsDefaultGeometryTile = true,
            StringId = "l1_r3"
        };
        currentUuid++;
        
        TilesetTileCenter tileL1R4 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.L1_R4,
            IsDefaultGeometryTile = true,
            StringId = "l1_r4"
        };
        currentUuid++;
        
        TilesetTileCenter tileL1R5 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.L1_R5,
            IsDefaultGeometryTile = true,
            StringId = "l1_r5"
        };
        currentUuid++;
        
        TilesetTileCenter tileL1R6 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.L1_R6,
            IsDefaultGeometryTile = true,
            StringId = "l1_r6"
        };
        currentUuid++;
        
        TilesetTileCenter tileL1R7 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.L1_R7,
            IsDefaultGeometryTile = true,
            StringId = "l1_r7"
        };
        currentUuid++;
        
                
        // ------------ L2 ---------------
        
        TilesetTileCenter tileL2R0 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.L2_R0,
            IsDefaultGeometryTile = true,
            StringId = "l2_r0"
        };
        currentUuid++;
        
        
        TilesetTileCenter tileL2R1 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.L2_R1,
            IsDefaultGeometryTile = true,
            StringId = "l2_r1"
        };
        currentUuid++;
        
        TilesetTileCenter tileL2R2 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.L2_R2,
            IsDefaultGeometryTile = true,
            StringId = "l2_r2"
        };
        currentUuid++;
        
        TilesetTileCenter tileL2R3 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.L2_R3,
            IsDefaultGeometryTile = true,
            StringId = "l2_r3"
        };
        currentUuid++;
        
        TilesetTileCenter tileL2R4 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.L2_R4,
            IsDefaultGeometryTile = true,
            StringId = "l2_r4"
        };
        currentUuid++;
        
        TilesetTileCenter tileL2R5 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.L2_R5,
            IsDefaultGeometryTile = true,
            StringId = "l2_r5"
        };
        currentUuid++;
        
        TilesetTileCenter tileL2R6 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.L2_R6,
            IsDefaultGeometryTile = true,
            StringId = "l2_r6"
        };
        currentUuid++;
        
        TilesetTileCenter tileL2R7 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.L2_R7,
            IsDefaultGeometryTile = true,
            StringId = "l2_r7"
        };
        currentUuid++;
        
        // --------- QP ------------------
        
        TilesetTileCenter tileQPR0 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.QP_R0,
            IsDefaultGeometryTile = true,
            StringId = "qp_r0"
        };
        currentUuid++;
        
        TilesetTileCenter tileQPR1 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.QP_R1,
            IsDefaultGeometryTile = true,
            StringId = "qp_r1"
        };
        currentUuid++;
        
        TilesetTileCenter tileQPR2 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.QP_R2,
            IsDefaultGeometryTile = true,
            StringId = "qp_r2"
        };
        currentUuid++;
        
        TilesetTileCenter tileQPR3 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.QP_R3,
            IsDefaultGeometryTile = true,
            StringId = "qp_r3"
        };
        currentUuid++;
        
        // ----------- HP ------------------
        
        TilesetTileCenter tileHPR0 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.HP_R0,
            IsDefaultGeometryTile = true,
            StringId = "hp_r0"
        };
        
        TilesetTileCenter tileHPR1 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.HP_R1,
            IsDefaultGeometryTile = true,
            StringId = "hp_r1"
        };
        
        TilesetTileCenter tileHPR2 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.HP_R2,
            IsDefaultGeometryTile = true,
            StringId = "hp_r2"
        };
        
        TilesetTileCenter tileHPR3 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.HP_R3,
            IsDefaultGeometryTile = true,
            StringId = "hp_r3"
        };
        
        currentUuid++;
        
        // ----------- FP ------------------
        
        TilesetTileCenter tileFPR0 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.FP_R0,
            IsDefaultGeometryTile = true,
            StringId = "fp_r0"
        };
        currentUuid++;
        
        TilesetTileCenter tileFPR1 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.FP_R1,
            IsDefaultGeometryTile = true,
            StringId = "fp_r1"
        };
        currentUuid++;
        
        TilesetTileCenter tileFPR2 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.FP_R2,
            IsDefaultGeometryTile = true,
            StringId = "fp_r2"
        };
        currentUuid++;
        
        TilesetTileCenter tileFPR3 = new TilesetTileCenter
        {
            Uuid = currentUuid,
            Geometry = TileGeometryAndRotation.FP_R3,
            IsDefaultGeometryTile = true,
            StringId = "fp_r3"
        };
        
        AddGeometryTile(TileGeometryAndRotation.SB_R0, tileSBR0);
        
        AddGeometryTile(TileGeometryAndRotation.HB_R0, tileHBR0);
        AddGeometryTile(TileGeometryAndRotation.HB_R1, tileHBR1);
        AddGeometryTile(TileGeometryAndRotation.HB_R2, tileHBR2);
        AddGeometryTile(TileGeometryAndRotation.HB_R3, tileHBR3);
        
        AddGeometryTile(TileGeometryAndRotation.TB_R0, tileTBR0);
        AddGeometryTile(TileGeometryAndRotation.TB_R1, tileTBR1);
        AddGeometryTile(TileGeometryAndRotation.TB_R2, tileTBR2);
        AddGeometryTile(TileGeometryAndRotation.TB_R3, tileTBR3);
        
        AddGeometryTile(TileGeometryAndRotation.L1_R0, tileL1R0);
        AddGeometryTile(TileGeometryAndRotation.L1_R1, tileL1R1);
        AddGeometryTile(TileGeometryAndRotation.L1_R2, tileL1R2);
        AddGeometryTile(TileGeometryAndRotation.L1_R3, tileL1R3);
        AddGeometryTile(TileGeometryAndRotation.L1_R4, tileL1R4);
        AddGeometryTile(TileGeometryAndRotation.L1_R5, tileL1R5);
        AddGeometryTile(TileGeometryAndRotation.L1_R6, tileL1R6);
        AddGeometryTile(TileGeometryAndRotation.L1_R7, tileL1R7);
        
        AddGeometryTile(TileGeometryAndRotation.L2_R0, tileL2R0);
        AddGeometryTile(TileGeometryAndRotation.L2_R1, tileL2R1);
        AddGeometryTile(TileGeometryAndRotation.L2_R2, tileL2R2);
        AddGeometryTile(TileGeometryAndRotation.L2_R3, tileL2R3);
        AddGeometryTile(TileGeometryAndRotation.L2_R4, tileL2R4);
        AddGeometryTile(TileGeometryAndRotation.L2_R5, tileL2R5);
        AddGeometryTile(TileGeometryAndRotation.L2_R6, tileL2R6);
        AddGeometryTile(TileGeometryAndRotation.L2_R7, tileL2R7);
        
        AddGeometryTile(TileGeometryAndRotation.QP_R0, tileQPR0);
        AddGeometryTile(TileGeometryAndRotation.QP_R1, tileQPR1);
        AddGeometryTile(TileGeometryAndRotation.QP_R2, tileQPR2);
        AddGeometryTile(TileGeometryAndRotation.QP_R3, tileQPR3);
        
        AddGeometryTile(TileGeometryAndRotation.HP_R0, tileHPR0);
        AddGeometryTile(TileGeometryAndRotation.HP_R1, tileHPR1);
        AddGeometryTile(TileGeometryAndRotation.HP_R2, tileHPR2);
        AddGeometryTile(TileGeometryAndRotation.HP_R3, tileHPR3);
        
        AddGeometryTile(TileGeometryAndRotation.FP_R0, tileFPR0);
        AddGeometryTile(TileGeometryAndRotation.FP_R1, tileFPR1);
        AddGeometryTile(TileGeometryAndRotation.FP_R2, tileFPR2);
        AddGeometryTile(TileGeometryAndRotation.FP_R3, tileFPR3);
    }
}