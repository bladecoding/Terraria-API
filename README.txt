If you are compiling TerrariaAPI please do the following first.
-Set the environment variable %TERRARIA_BIN% to Terraria's install directory. (IE C:\Program Files (x86)\Steam\steamapps\common\terraria\)
-Run Terraria/compile.bat to compile Terraria.


CHANGES:
V1.3
-Enabled property added. Not currently used but best to make your plugins support it soon.
-APIVersion is now a class attribute. This will prevent any crashes due to having an outdated plugin.
-Order property added. Plugins are sorted by order before being initialized.
-Name/Version/Description/Author made virtual meaning you don't have to overide them.

Plugins keybinds:
F1 = Teleport to last player
F2 = Teleport to last location
F3 = Teleport to cursor position
F4 = Open teleport form
F5 = Show/Hide minimap
F6 = Show minimap settings form
F7 = Show trainer form
F8 = Show texture loader form
F9 = Show item editor form
Ctrl + B = Open bank