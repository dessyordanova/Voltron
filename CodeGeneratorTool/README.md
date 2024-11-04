# Welcome to `CodeGeneratorTool`
A tool for injecting code snippets into MarkDown files:

1. Define snippets in your source by using a simple notation
2. Put placeholders associated with the snippets in your MarkDown files
3. Run MarkDown code generator to replace the placeholders during your documentation build:

Currently support: C# and VB.NET

# Using `CodeGeneratorTool`

## Defining the place holders in MarkDown files

```
{{source=..\SamplesCS\Buttons\Button.cs region=creatingbutton}} 
{{source=..\SamplesVB\Buttons\Button.vb region=creatingbutton}} 


{{endregion}} 
```

## Defining snippets in `C#` source files 

```
            #region creatingbutton

            RadButton myNewRadButton = new RadButton();
            myNewRadButton.Text = "My New RadButton";
            myNewRadButton.Width = 150;
            myNewRadButton.Height = 50;
            this.Controls.Add(myNewRadButton);
            myNewRadButton.Click+=myNewRadButton_Click;

            #endregion
```

## Defining snippets in `VB.NET`source files
```
        '#region "creatingbutton"

        Dim myNewRadButton As New RadButton()
        myNewRadButton.Text = "My New RadButton"
        myNewRadButton.Width = 150
        myNewRadButton.Height = 50
        Me.Controls.Add(myNewRadButton)
        AddHandler myNewRadButton.Click, AddressOf myNewRadButton_Click

        '#End Region
```

## Result after replacing the code snippets:

{{source=..\SamplesCS\Buttons\Button.cs region=creatingbutton}} 
{{source=..\SamplesVB\Buttons\Button.vb region=creatingbutton}} 
````C#
RadButton myNewRadButton = new RadButton();
myNewRadButton.Text = "My New RadButton";
myNewRadButton.Width = 150;
myNewRadButton.Height = 50;
this.Controls.Add(myNewRadButton);
myNewRadButton.Click+=myNewRadButton_Click;

````
````VB.NET
Dim myNewRadButton As New RadButton()
myNewRadButton.Text = "My New RadButton"
myNewRadButton.Width = 150
myNewRadButton.Height = 50
Me.Controls.Add(myNewRadButton)
AddHandler myNewRadButton.Click, AddressOf myNewRadButton_Click

````
{{endregion}} 





