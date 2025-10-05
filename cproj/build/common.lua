-- Main settings
llvm_ver  = "21.1.1"
ld_exe    = "ld.lld"
ld_out    = target .. ".out"
ld_map    = target .. ".map"
vsc_imode = "linux-clang-arm"
toolc     = "clang"
toolc_dir = path(os.getenv("LLVM_ROOT"), llvm_ver, "bin")
proj_dir  = path(escr_dir, "..")
bld_dir   = path(proj_dir, "build")
src_dir   = path(proj_dir, "src")
app_dir   = path(src_dir, "app")
bsp_dir   = path(app_dir, "bsp")
artf_dir  = path(proj_dir, "artifacts")
ld_scr    = path(bld_dir, "linker.ld")
optimz    = release and "O3" or "O0"

-- Common vsc defines
--vsc_defs("true=1", "false=0")
--
---- Common compiler options
--cc_opts("-std=c23", optimz)
--
---- Common compiler includes
--cc_incs(src_dir)
--
---- Common compiler defines
--cc_defs("PROJECT")
--
---- Common linker options
--ld_opts()
--
---- Common linker libraries
--ld_libs()
--
---- Common linker objects
--ld_objs()
--
---- Common sources
--src_di(src_dir)
--src_de(bsp_dir)