module DataContext

open FSharp.Data.Sql
open Microsoft.FSharp.Collections

[<Literal>]
let connectionString = "Server=.\SQLExpress2017; Database=ShopDatabase; Trusted_Connection=true;"

type Sql = SqlDataProvider<Common.DatabaseProviderTypes.MSSQLSERVER, connectionString>

let DbContext = Sql.GetDataContext()

let FirstCustomer = query {
                           for customer in DbContext.Dbo.Customers do
                           select customer 
                           } |> Seq.head;

let GetNextCustomer (id) = query {
                                   for customer in DbContext.Dbo.Customers do
                                   where (customer.CustomerId >id)
                                   select customer
                                 } |> Seq.tryHead

let GetBeforeCustomer (id) = query {
                                    for customer in DbContext.Dbo.Customers do
                                    where (customer.CustomerId < id)
                                    select customer
                                 } |> Seq.tryLast