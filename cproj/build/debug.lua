require("common.lua")

-- Compiler defines
cc_defs("DEBUG")

-- Add sources to the build
src_rdi(path.concat(bsp_dir, "board_a"))
src_fe("erroneous.c")