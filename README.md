# EditLabel
###### An editable Label control, who's text can be modified at run-time by double-clicking on it.
---
 
 
 
##### Basic Info:
 - An editable Label control, who's text can be modified at run-time by double-clicking on it.
 - Under the hood, it contains a **Label** control and **TextBox** control.
 - It works like a Label control until you double-click it with the mouse. When double-clicked it enters 'edit-mode' where a TextBox overlays the Label. 
 - The control will AutoSize to the characters you type in the TextBox, updating the Label's size at the same time.
 - You can leave the control's edit mode by hitting either enter, the escape key, or letting the control lose focus.
 - After editing, the control acts like a label again, with the updated text.
 
##### Properties:
 - The **Text** property can be used to get or set the text.
 - The **IsEditing** property is true while the user is editing the EditLabel.
 - The **IsEditable** property allows you to disable modification by the user. (Text property can still be set programmatically.) Do not set this property when IsEditing is true.
 
##### Events:
 - The **TextChanged** event is fired for every single update to the TextBox, this includes every keystroke.
 - The **TextUpdated** event is fired after 1) the user _successfully_ finishes  editing the text or 2) the Text property gets updated. Unlike TextChanged, it does not fire for every keystroke. Fired after TextChanged event.
 - The **EditingSuccessful** event is fired after the user _successfully_ ends an edit event. This event will not fire if the user cancels during editing with the Escape key or if the Text property is set programmatically. Fired after TextUpdated and TextChanged events.

