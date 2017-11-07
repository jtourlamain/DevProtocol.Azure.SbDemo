// Learn more about F# at http://fsharp.org

open System
open DevProtocol.Azure.SbDemo.SbReceive

[<EntryPoint>]
let main argv =
    printfn "Start receiving messages"
    Sb.receiveData()
    Console.ReadLine() |> ignore
    0