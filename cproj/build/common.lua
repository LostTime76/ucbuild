-- Main settings
llvm_ver  = "21.1.1"
ld_exe    = "ld.lld"
ld_out    = target .. ".out"
ld_map    = target .. ".map"
vsc_imode = "linux-clang-arm"
c_std     = 23
cc_lst    = true
toolc     = "clang"
toolc_dir = path.concat(os.getenv("LLVM_ROOT") or "", llvm_ver, "bin")
proj_dir  = path.concat(path.dir(scr_fpath), "..")
bld_dir   = path.concat(proj_dir, "build")
src_dir   = path.concat(proj_dir, "src")
app_dir   = path.concat(src_dir, "app")
bsp_dir   = path.concat(app_dir, "bsp")
artf_dir  = path.concat(proj_dir, "artifacts", target)
ld_scr    = path.concat(bld_dir, "linker.ld")
optimz    = release and "O3" or "O0"

-- Common vsc defines
vsc_defs("true=1", "false=0")

-- Common compiler options
cc_opts(optimz)

-- Common compiler includes
cc_incs(src_dir)

-- Common compiler defines
cc_defs("PROJECT")

-- Common linker options
ld_opts()

-- Common linker libraries
ld_libs()

-- Common linker objects
ld_objs()

-- Add common sources to the build
src_di(app_dir)