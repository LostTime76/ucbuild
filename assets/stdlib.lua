--- @meta

--- Gets the filepath of the currently executing script
--- @type string
scr_fpath = nil

--- Provides utility functions to manipulate and access paths
--- @class path
path = { }

--- Concatenates several path segments into one
--- @param first string The first path segment
--- @param second string The second path segment
--- @param ... string Additional path segments
--- @return string path The concatenated path
function path.concat(first, second, ...) end

--- Gets the extension of a file
--- @param fpath string The filepath of the file
--- @return string fext The extension of the file
function path.fext(fpath) end

--- Gets the name of a file
--- @param fpath string The filepath of the file
--- @return string fname The name of the file without the extension
function path.fname(fpath) end

--- Gets the name of a file
--- @param fpath string The filepath of the file
--- @return string fname The name of the file including the extension
function path.fname_we(fpath) end

--- Gets the directory of a path
--- @param path string The path to get the directory of
--- @return string dir The directory of the path
function path.dir(path) end