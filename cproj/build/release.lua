require("common.lua")

-- Compiler defines
cc_defs("RELEASE")

-- Sources
sources(path(bsp_dir, "board_b"), "board.c")