require("common.lua")

-- Compiler defines
cc_defs("DEBUG")

-- Sources
sources(path(bsp_dir, "board_a"), "board.c")