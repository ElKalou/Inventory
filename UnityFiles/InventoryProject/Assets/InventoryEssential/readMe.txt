Essential elements to set up the inventory system. 
1) Put InventoryCanvas prefab in the scene
2) add UICanvas prefab
2) Add an ItemContainer component to a game object, fill it with an inventory Scriptable object (that needs item, owner type
and inventoryUI)
3) Send the openInventory GameEvent to open the inventory (via interaction with item container, user input, or interaction with user
interface)
4) and the close Inventory GameEvent to close the inventory (via user input, or interaction with user interface)

What this essential do:
1) Player can organize his inventory (swap item, stack similar item, drop item on floor..)
2) Player can interact with chest container (take item from chest, put item in chest, take money from chest...)
3) Player can interact with merchant container (buying, selling...)
4) Player input are filtered depending if the inventory is open or not, see Event extra package

What this essential doesn't do: 
- Trigger the OnReceiveInteract() method of the ItemContainer component. This is done in the scene preview.
- Destroy the created inventoryUI. This can be problematic when opening/closing a lot of different inventory, all the UI stay
store in memory.

What can be improved: 
1) When items are dropped in between itemSlotUI, ie no itemSlotUI are highlighted, it misses the inventory 
and items are dropped on floor
2) Better instanciating/destroying of inventoryUI. In fact destroy is not done at all. 

Overall behaviour: 
ItemSlotUI send inventoryEvent received mainly by InventoryEventDataCatcher in the DataGates. Those data are either transmitted 
to a dataUser, used to trigger Disable on dataUser or used to trigger suspend/unsuspend boolean of dataCatcher. These observers 
pattern manage the behaviour of the "panels" that represent the functionnalities of the inventory. There is 9 panels in this essential:
- Highlight panel, UI element to highlight the panel when mouse is over
- Info panel, UI element that display information about the item bellow mouse
- Drag panel for dragging item around
- Drop panel when releasing a dragged item
- Fast drop panel that works with double click input, to quickly switch item from one open inventory to the other
- Quantity panel that ask for the quantity to drop
- Message panel that display various messages send by other components. Messages can be "inventory full" or "missing money"
- Trade panel which deals with buying and selling item, plus the associated UI
- Money transfer panel which grab money from chest on double click. Money transfer only goes one way, from chest to player.

Depending on the ItemContainer the player interact with some functionnality must be disable. You don't want a drag and drop possiblity 
when interacting with a merchant. This is done via the PanelManager and the PanelManagerLink components. 

Actual change in the inventory are done via methods in the Inventory scriptable object. After each change the inventory send an event
that update the UI.  
