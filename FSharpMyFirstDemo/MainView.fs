module MainView

open FSharp.Desktop.UI
open System.Windows
open System.Windows.Controls
open System.Windows.Input
open System.Windows.Data
open System.Windows.Media;

type UpDownEvents = Up | Down

type MainView() as this = 
    inherit View<UpDownEvents, Domain.Customer, Window>(Window())    
    
    do 
        this.Root.Width <- 800.
        this.Root.Height <- 600.
        this.Root.WindowStartupLocation <- WindowStartupLocation.CenterScreen
        this.Root.Title <- "F# Desktop Demo Application"
        this.Root.Background <- Brushes.LightCyan

    let mainPanel = 
        let grid = Grid(HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch, ShowGridLines = true)
        [ RowDefinition(Height= GridLength(300.)); RowDefinition(Height= GridLength(300.)) ] |> List.iter grid.RowDefinitions.Add  
        [ ColumnDefinition(Width= GridLength(300.)); ColumnDefinition(Width= GridLength(300.));ColumnDefinition(Width= GridLength(200.)) ] |> List.iter grid.ColumnDefinitions.Add
        grid

    let idLabel = Label(Content= "Id: ", FontSize = 20., Width = 150., HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top);
    let userIdTextBox = TextBox(FontSize = 20., Width = 150., Height = 50., HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Background = Brushes.LightBlue)
    let userNameLabel = Label(Content= "Name: ", FontSize = 20., Width = 150., HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top)
    let userNameTextBox = TextBox(FontSize = 20., Width = 150., Height = 50., HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Background = Brushes.LightBlue)

    let upButton = Button(Content = "^", FontSize = 20., Width = 150., Height = 50., Background = Brushes.LightCyan)
    let downButton = Button(Content = "v", FontSize = 20.,Width = 150., Height = 50.,Background = Brushes.LightCyan)
    
    do  
      Grid.SetRow(idLabel,0)
      Grid.SetColumn(idLabel,0)
      
      Grid.SetRow(userIdTextBox,0)
      Grid.SetColumn(userIdTextBox, 1)
      
      Grid.SetRow(userNameLabel, 1)
      Grid.SetColumn(userNameLabel, 0)
       
      Grid.SetRow(userNameTextBox, 1)
      Grid.SetColumn(userNameTextBox, 1)
    
      Grid.SetRow(upButton, 0)
      Grid.SetColumn(upButton, 2)
    
      Grid.SetRow(downButton, 1)
      Grid.SetColumn(downButton, 2)

      mainPanel.Children.Add idLabel |> ignore
      mainPanel.Children.Add userIdTextBox |> ignore
      mainPanel.Children.Add userNameLabel |> ignore
      mainPanel.Children.Add userNameTextBox |> ignore
      mainPanel.Children.Add upButton |> ignore
      mainPanel.Children.Add downButton |> ignore

      this.Root.Content <- mainPanel

    //View implementation 
    override this.EventStreams = [
        upButton.Click |> Observable.map (fun _ -> Up)
        downButton.Click |> Observable.map (fun _ -> Down)

        userIdTextBox.KeyUp |> Observable.choose (fun args -> 
            match args.Key with 
            | Key.Up -> Some Up  
            | Key.Down -> Some Down
            | _ ->  None
        )

        userIdTextBox.MouseWheel |> Observable.map (fun args -> if args.Delta > 0 then Up else Down)
    ]

    override this.SetBindings model =   
        Binding.OfExpression 
            <@
                userIdTextBox.Text <- coerce model.CustomerId 
                userNameTextBox.Text <- coerce model.Name 
            @>