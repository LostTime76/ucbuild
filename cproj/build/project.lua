-- Variables
proj_dir = path.concat(path.dir(scr_fpath), "..")
bld_dir  = path.concat(proj_dir, "build")
src_dir  = path.concat(proj_dir, "src")

-- Set the artifacts root directory
artf_root = path.concat(proj_dir, "artifacts")

-- Set the visual studio code directory
vsc_dir = path.concat(proj_dir, ".vscode")

-- Add targets
target("debug.lua")