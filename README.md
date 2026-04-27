# MnM-UI
# UI Management Tool for Monsters and Memories

This tool helps to manage the UI and character journals for Monsters and Memories by manipulating the json files that control window placement within the game and the text files that contain journal entries.  
It works with the default MnM UI directory, located at: C:\Users\[YourUserName]\AppData\LocalLow\Niche Worlds Cult\Monsters and Memories\.  
Two functions are provided: Copy Windows and Merge Journals.  

## Copy Windows
Copy Windows Takes a template windows.json file (controls all window placement) and chats.json file (controls chat tabs and filters) and copies them to all characters in the MnM game directory. This allows you to set up a template UI and quickly apply it to the game.  
Start by double-clicking or right clicking the text box to select a template directory that contains both windows.json and chats.json. Then click the "Copy" button to apply the template to all characters. Note: this does not affect bag window placement.  
It is recommended to set up a separate template directory with the desired windows.json and chats.json files before using Copy Windows. This protects your UI setup in case the game client resets all UIs. The downside is that it requires maintaining a separate directory and keeping it updated with any UI changes you make in game.  
If you prefer you can use an existing character as your template. To do that, check the box for "Copy Existing Character?" and select a character from the dropdown.  

# Merge Journals
Merge Journals is for moving between MnM installs on different computers. By default, because journal entries are saved locally, conversations you have in game on one machine will not show up in your journal on the other.  
Merge Journals solves that by integrating journal entries from a local MnM install with a backup of the game UI directory (presumably a network location accessible from both computers). This goes both directions; so you can merge journals on one computer, then do it again on another computer, and all journal entries should be updated.  
Start by double-clicking the text box to select a backup UI directory that contains a Player.Log file and folders for each server you play on. Then click the "Merge" button to merge local and backup journals together. Note: this does not affect journal entries that have already been merged, so you can merge multiple times without worrying about duplicates.  
If you want the program to create a backup of your UI directory for you to use for merging, just click the checkbox for "Create Backup?".
                    
