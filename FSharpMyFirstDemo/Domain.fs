module Domain

 type Customer(customerId:int, name:string) = 
    inherit FSharp.Desktop.UI.Model()

      let  mutable  customerId =customerId 
      let  mutable name = name

      member  this.CustomerId
       with get() = customerId
       and set value = 
                  customerId <- value
                  this.NotifyPropertyChanged "CustomerId"

      member this.Name
       with get() = name
       and set value = 
                  name <- value
                  this.NotifyPropertyChanged "Name"