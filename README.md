# Inventory
A basic inventory system for Unity that can be easily plug into any project. A particular attention was given to respect the SOLID principle to make this project adaptable, extendable and debuggable. 

Main features :
1) Player can organize his inventory (swap item, stack similar item, drop item on floor..)
2) Player can interact with chest container (take item from chest, put item in chest, take money from chest...)
3) Player can interact with merchant container (buying, selling...)

Overall architecture: 
1) Inventories are Unity Scriptable Object that contain a list of Item (also Scriptable Object) and the logic to swap/stack items. Using Scriptable objects here make the creation of inventory or item really interactive and fast. 
2) When interacting with an ItemContainer (a chest or a merchant) The "Inventory" Scriptable Object is then send to the UserInterface system that will display the inventory items into "ItemSlotUI" component. Depending on the player input the "ItemSlotUI" will send event to other UI panel (OnMouseEnterItemSlot, OnMouseLeaveItemSlot, OnMouseClickOnItemSlot..) using the observer pattern. 
Using the observer pattern here allow to make the project modular, the ItemSlot do not require the presence of the other UIPanel to operate. 
3) The UIPanel implement the functionnalities of the  inventory. There is 10 panels so far:
- Highlight panel, UI element to highlight the panel when mouse is over
- Info panel, UI element that display information about the item bellow mouse
- Drag panel for dragging item around
- Drop panel when releasing a dragged item
- Fast drop panel that works with double click input, to quickly switch item from one open inventory to the other
- Quantity panel that ask for the quantity to drop
- Message panel that display various messages send by other components. Messages can be "inventory full" or "missing money"
- Trade panel which deals with buying and selling item, plus the associated UI
- Money transfer panel which grab money from chest on double click. Money transfer only goes one way, from chest to player.
- A close panel that allows to close the inventory.
Depending on the owner of the inventory the player interact with some functionnalities must be disable. You don't want a drag and drop possiblity 
when interacting with a merchant. This is done via the PanelManager and the PanelManagerLink components. 

About the file in this repository: 
1) the build of the project
2) the project
3) a unity package that implement the essential of the unity project. A readMe file explain how to setup this essential into any project. 
4) a unity package that implement the entire project.
