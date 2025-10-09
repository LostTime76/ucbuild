require("common.lua")

-- Compiler defines
cc_defs("RELEASE")

-- Add sources to the build
src_rdi(path.concat(bsp_dir, "board_b"))