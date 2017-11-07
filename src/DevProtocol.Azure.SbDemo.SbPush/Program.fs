// Learn more about F# at http://fsharp.org

open System
open DevProtocol.Azure.SbDemo.SbPush.Sb
open DevProtocol.Azure.SbDemo.SbPush.Data

[<EntryPoint>]
let main argv =
    printfn "Start sending messages"
    sendData (getData() |> List.map(fun x -> sprintf "%s%A" "Message" x )) |> Async.RunSynchronously
    printfn "Ended sending messages"
    0
