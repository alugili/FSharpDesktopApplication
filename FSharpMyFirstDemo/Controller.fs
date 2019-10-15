module Controller

let eventHandler event (customer: Domain.Customer) =

    match event with
    | MainView.Up -> 
                   let nextCustomer = DataContext.GetNextCustomer customer.CustomerId
                   match nextCustomer with 
                       | Some c -> customer.CustomerId <- c.CustomerId
                                   customer.Name <- c.Name 
                       | None ->   customer.CustomerId <- 0
                                   customer.Name <- "Not found upper!"  

    | MainView.Down ->
                   let beforeCustomer = DataContext.GetBeforeCustomer customer.CustomerId
                   match beforeCustomer with 
                       | Some c -> customer.CustomerId <- c.CustomerId
                                   customer.Name <- c.Name 
                       | None ->   customer.CustomerId <- 0
                                   customer.Name <- "Not found down!"  