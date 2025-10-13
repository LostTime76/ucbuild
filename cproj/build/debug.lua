local common = require("common.lua")
local target = { }

function target.configure(target)
	
end

function target.add_sources(target)

	-- Add common sources
	common.add_sources(target)

end

function target.after_link(target)

	-- Run common post link task
	common.after_link(target)

end

return target