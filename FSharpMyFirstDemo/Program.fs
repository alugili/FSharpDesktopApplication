open System
open System.Windows
open FSharp.Desktop.UI

[<STAThread>]
// Learn more about F# at http://fsharp.org
[<EntryPoint>]
do
    let firstCustomer = DataContext.FirstCustomer

    // Create a Customer Model instance.
    let customerModel = Domain.Customer.Create(firstCustomer.CustomerId, firstCustomer.Name)
 
    let view = MainView.MainView()
    let controller:IController<MainView.UpDownEvents, Domain.Customer> = Controller.Create Controller.eventHandler

    let mvc = Mvc(customerModel, view,controller)
    use eventLoop = mvc.Start()
    Application().Run( window = view.Root) |> ignore